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

                return toDisplay;
            }
        }
}