using Magnifier_2.Attributes;
using Magnifier_2.Models;
using Magnifier_2.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Shared.Models.MongoDB;
using Shared.Models.Universal;
using System.Net;
using System.Text;

namespace Magnifier_2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "cors",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.Configure<Magnifier2Settings>(
                Configuration.GetSection(nameof(Magnifier2Settings)));

            services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<Magnifier2Settings>>().Value);

            services.AddSingleton<ReactionService>();
            services.AddSingleton<AuthCodeService>();
            services.AddSingleton<UserService>();
            services.AddSingleton<SessionService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Magnifier_2", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Magnifier_2 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("cors");

            app.Use(async (context, next) =>
            {
                Endpoint endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
                RequireAuthAttribute attribute = endpoint?.Metadata.GetMetadata<RequireAuthAttribute>();

                if (attribute == null)
                {
                    await next();
                    return;
                }

                if (context.Request.Headers.ContainsKey("sessionId"))
                {
                    string sessionId = context.Request.Headers["sessionid"];

                    UserService userService = app.ApplicationServices.GetService<UserService>();
                    SessionService sessionService = app.ApplicationServices.GetService<SessionService>();

                    Session session = sessionService.Get(sessionId);

                    Shared.Models.Universal.User user = userService.Get(session?.Username);

                    if (user == null)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return;
                    }

                    if (attribute.AdminOnly && !user.IsAdmin)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        return;
                    }

                    context.User = new UserClaimsPrincipal(user);

                    await next();
                    return;
                }

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
