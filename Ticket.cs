using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace ClassedTicketSystem
{
    public abstract class Ticket
    {
        public int ticketId { get; set; }
        public string summary { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public string submitter { get; set; }
        public string assigned { get; set; }
        public string[] watching { get; set; }

        public class BugDefect : Ticket
        {
            public string severity;
        }

        public class Enhancement : Ticket
        {
            public string software;
            public double cost;
            public string reason;
            public double estimate;
        }

        public class Task : Ticket
        {
            public string projectName;
            public DateTime dueDate;
        }
    }
}
