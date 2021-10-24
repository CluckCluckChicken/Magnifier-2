using Shared.Models.Universal;
using System;

namespace Magnifier_2.Attributes
{
    public class RequireAuthAttribute : Attribute
    {
        public bool AdminOnly { get; set; }
    }
}
