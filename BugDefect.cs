using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace MediaLibrary
{
    public class BugDefect : Ticket
        {
            public string severity;

            public override int getNumAttributes()
            {
                return 8;
            }
        }
}