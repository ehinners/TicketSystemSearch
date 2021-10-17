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