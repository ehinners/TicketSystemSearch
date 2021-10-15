using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace MediaLibrary
{
    public class Enhancement : Ticket
        {
            public string software;
            public double cost;
            public string reason;
            public double estimate;

            public override int getNumAttributes()
            {
                return 11;
            }
        }
}