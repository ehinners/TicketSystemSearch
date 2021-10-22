using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace TicketSystemSearch
{
    public class BugDefect : Ticket
        {
            public string severity;

            public override int getNumAttributes()
            {
                return 8;
            }

            public override string display()
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
                toDisplay += "\n";
                toDisplay += "Severity:  " + severity;
                toDisplay += "\n";

                return toDisplay;
            }
        }
}