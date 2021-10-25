using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator.Legacy
{
    public record User
    {
        // User info

        public string username { get; set; } // This user's Scratch username

        public ScratchCommentAuthor scratchUser { get; set; } // This user's Scratch profile

        public bool isAdmin { get; set; } // Is this user an admin?

        public bool isBanned { get; set; } // Is this user the sus imposter?

        public DateTime created { get; private set; } // When this user's account was created

    }
}
