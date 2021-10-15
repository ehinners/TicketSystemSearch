using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace MediaLibrary
{
    public class Task : Ticket
        {
            public string projectName;
            public DateTime dueDate;

            public override int getNumAttributes()
            {
                return 9;
            }
        }
}