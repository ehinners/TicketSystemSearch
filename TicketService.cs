using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using NLog.Web;


namespace TicketSystemSearch
{  
    public static class TicketService
    {
        public static Ticket mapTicketFromCSVGenerateID(int type, string csv)
        {
            int id = 0;
            string newCsv;
            Ticket ticket= new BugDefect();

            if(type == 1)
            {
                id = Model.getLargestIDBD();
                id++;
            }
            else if(type == 2)
            {
                id = Model.getLargestIDEH();
                id++;
            }
            else if(type == 3)
            {
                id = Model.getLargestIDTK();
                id++;
            }
            else
            {
                Model.getLogger().Error("Ticket Type Invalid");
            }
            
            newCsv = id.ToString() + "," + csv;   
            
            ticket = mapTicketFromCSV(type, newCsv);     

            return ticket;
        }
        public static Ticket mapTicketFromCSV(int type, string csv)
        {
            Ticket ticket= new BugDefect();

            if(type == 1)
            {
                ticket = mapTicketBDFromCSV(csv);
            }
            else if(type == 2)
            {
                ticket = mapTicketEHFromCSV(csv);
            }
            else if(type == 3)
            {
                ticket = mapTicketTKFromCSV(csv);
            }
            else
            {
                Model.getLogger().Error("Ticket Type Invalid");
            }          

            return ticket;
        }

        private static Ticket baseMapping(string csv, Ticket ticket)
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
            ticket = (BugDefect)baseMapping(csv, ticket);

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
            ticket = (Enhancement)baseMapping(csv, ticket);

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
                    try
                    {
                        ticket.cost = double.Parse(property);
                    }
                    catch
                    {
                        Model.getLogger().Error("Not Valid Double Value");
                        ticket.cost = 0.00;
                    }
                }   
                if(ticketAttribute == 10)    
                {
                    ticket.reason = property;
                } 
                if(ticketAttribute == 11)    
                {
                    try
                    {
                        ticket.estimate = double.Parse(property);
                    }
                    catch
                    {
                        Model.getLogger().Error("Not Valid Double Value");
                        ticket.estimate = 1.11;
                    }
                } 
                ticketAttribute++;
            }

            return ticket;
        }

        private static Task mapTicketTKFromCSV(string csv)
        {
            Task ticket = new Task();
            ticket = (Task)baseMapping(csv, ticket);
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
                    try
                    {
                        ticket.dueDate = DateTime.Parse(property);
                    }
                    catch
                    {
                        Model.getLogger().Error("Not Valid Double Value");
                        ticket.dueDate = tempDue;
                    }
                    
                }      
                ticketAttribute++;
            }

            return ticket;
        }

        public static List<BugDefect> mapTicketsFromStringListBD(List<string> fileContents)
        {
            List<BugDefect> tickets = new List<BugDefect>();
            foreach(string s in fileContents)
            {
                tickets.Add((BugDefect)mapTicketFromCSV(1, s));                
            }
            return tickets;
        }

        public static List<Enhancement> mapTicketsFromStringListEH(List<string> fileContents)
        {
            List<Enhancement> tickets = new List<Enhancement>();
            foreach(string s in fileContents)
            {
                tickets.Add((Enhancement)mapTicketFromCSV(2, s));
            }
            return tickets;
        }

        public static List<Task> mapTicketsFromStringListTK(List<string> fileContents)
        {
            List<Task> tickets = new List<Task>();
            foreach(string s in fileContents)
            {
                tickets.Add((Task)mapTicketFromCSV(3, s));
            }
            return tickets;
        }
    }
}