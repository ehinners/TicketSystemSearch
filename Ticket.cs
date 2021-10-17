using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace TripleTicketType
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

        public virtual int getNumAttributes()
        {
            return 7;
        }

        public virtual string display()
        {
            string toDisplay = "";
            toDisplay += "Ticket ID: " + ticketId;
            toDisplay += "\n";
            toDisplay += "Summary:   " + summary;
            toDisplay += "\n";
            toDisplay += "Priority:  " + priority;
            toDisplay += "\n";
            toDisplay += "Submitter: " + submitter;
            toDisplay += "\n";
            toDisplay += "Assigned:  " + assigned;
            toDisplay += "\n";
            toDisplay += "Watchers:  ";
            
            foreach(string s in watching)
            {
                toDisplay += "\n";
                toDisplay += " - " + s;
            }

            return toDisplay;
        }

        
    }
}
