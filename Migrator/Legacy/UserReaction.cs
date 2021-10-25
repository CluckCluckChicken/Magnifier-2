using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Migrator.Legacy
{
    public record UserReaction
    {
        [JsonConstructor]
        public UserReaction() { }


        public string user { get; set; }

        public string reaction { get; set; }
    }
}
