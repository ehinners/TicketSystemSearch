using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using NLog.Web;


namespace MediaLibrary
{  
    public static class TicketService
    {
        public static Ticket mapTicketFromCSVGenerateID(int type, string csv)
        {
            Ticket ticket= new BugDefect();

            if(type == 1)
            {
                ticket = mapTicketBDFromCSV(csv);
            }
            if(type == 2)
            {
                ticket = mapTicketEHFromCSV(csv);
            }
            if(type == 3)
            {
                ticket = mapTicketTKFromCSV(csv);
            }
            else
            {
                Model.getLogger().Error("Ticket Type Invalid");
            }          

            return ticket;
        }

        private static Ticket baseMappingGenID(string csv, Ticket ticket)
        {
            string[] csvsplit = csv.Split(",");
            string[] watchsplit;

            int ticketAttribute = 1;
            int tempInt = -1;

            foreach(string property in csvsplit)
            {
                if(ticketAttribute == 1)
                {
                    try
                    {
                        tempInt = int.Parse(property);
                    }
                    catch
                    {
                        tempInt = -1;
                    }     
                    ticket.ticketId = tempInt;               
                }
                else if(ticketAttribute==2){ticket.summary   = property;}                
                else if(ticketAttribute==3){ticket.status    = property;}                
                else if(ticketAttribute==4){ticket.priority  = property;}                
                else if(ticketAttribute==5){ticket.submitter = property;}                
                else if(ticketAttribute==6){ticket.assigned  = property;}             
                else if(ticketAttribute==7)
                {
                    watchsplit = property.Split("|");
                    ticket.watching = watchsplit;
                }                

                ticketAttribute++;
            }
            return ticket;
        }

        private static BugDefect mapTicketBDFromCSV(string csv)
        {
            BugDefect ticket = new BugDefect();
            ticket = (BugDefect)baseMappingGenID(csv, ticket);

            string[] csvsplit = csv.Split(",");

            int ticketAttribute = 1;

            foreach(string property in csvsplit)
            {
                if(ticketAttribute == 8)    
                {
                    ticket.severity = property;
                }      
                ticketAttribute++;
            }

            return ticket;
        }

        private static Enhancement mapTicketEHFromCSV(string csv)
        {
            Enhancement ticket = new Enhancement();
            ticket = (Enhancement)baseMappingGenID(csv, ticket);

            string[] csvsplit = csv.Split(",");

            int ticketAttribute = 1;

            foreach(string property in csvsplit)
            {
                if(ticketAttribute == 8)    
                {
                    ticket.software = property;
                }    
                if(ticketAttribute == 9)    
                {
                    ticket.cost = 0.00;
                }   
                if(ticketAttribute == 10)    
                {
                    ticket.reason = property;
                } 
                if(ticketAttribute == 8)    
                {
                    ticket.estimate = 1.11;
                } 
                ticketAttribute++;
            }

            return ticket;
        }

        private static Task mapTicketTKFromCSV(string csv)
        {
            Task ticket = new Task();
            ticket = (Task)baseMappingGenID(csv, ticket);
            DateTime tempDue = new DateTime();

            string[] csvsplit = csv.Split(",");

            int ticketAttribute = 1;

            foreach(string property in csvsplit)
            {
                if(ticketAttribute == 8)    
                {
                    ticket.projectName = property;
                }  
                if(ticketAttribute == 9)    
                {
                    ticket.dueDate = tempDue;
                }      
                ticketAttribute++;
            }

            return ticket;
        }
    }
}