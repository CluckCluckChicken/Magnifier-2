using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Universal
{
    public enum ProjectPlayer
    {
        Scratch,
        TurboWarp,
        None
    }

    public class Settings
    {
        public ProjectPlayer ProjectPlayer { get; set; }
    }
}
