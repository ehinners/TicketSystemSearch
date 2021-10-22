using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using NLog.Web;

namespace TicketSystemSearch
{  
    public static class Model
    {
        private static ArrayList typeOptions = new ArrayList()
        {
            "1","2","3"
        };

        public static int getNumTicketTypes()
        {
            return typeOptions.Count;
        }

        public static ArrayList getTypeOptions()
        {
            return typeOptions;
        }

        private static List<string> selectorTerms = new List<string>()
        {
            "status",
            "priority",
            "submitter"
        };

        public static List<string> getSelectorTerms()
        {
            return selectorTerms;
        }
        private static string watcherEscape = "DONE";

        // This class allows controller to communicate with services and views

        // static instance of logger held so multiple instances don't have to be created
        private static NLog.Logger logger;

        // static instance of file contents so multiple instances don't have to be created
        //private static List<string> fileContents;

        private static List<string> bugDefectFileContents;

        private static List<string> enhancementsFileContents;

        private static List<string> taskFileContents;

        // static instance of ticket list so multiple instances don't have to be created
        private static List<BugDefect> ticketsBD;
        private static List<Enhancement> ticketsEH;
        private static List<Task> ticketsTK;


        //public static string fileName;
        public static string BugDefectFile;
        public static string EnhancementsFile;
        public static string TaskFile;

        public static string getWatcherEscape()
        {
            return watcherEscape;
        }

        public static void setLogger(NLog.Logger l)
        {
            logger = l;
        }

        public static NLog.Logger getLogger()
        {
            return logger;
        }

        public static void setBugDefectFile(string fn)
        {
            BugDefectFile = fn;
        }

        public static List<string> getBugDefectFileContents()
        {
            if(bugDefectFileContents == null)
            {
                bugDefectFileContents = FileHandler.getFileContents(BugDefectFile);
            }
            return bugDefectFileContents;            
        }

        ////

        public static void setEnhancementsFile(string fn)
        {
            EnhancementsFile = fn;
        }

        public static List<string> getEnhancementsFileContents()
        {
            if(enhancementsFileContents == null)
            {
                enhancementsFileContents = FileHandler.getFileContents(EnhancementsFile);
            }
            return enhancementsFileContents;            
        }

        ////

        public static void setTaskFileName(string fn)
        {
            TaskFile = fn;
        }

        public static List<string> getTaskFileContents()
        {
            if(taskFileContents == null)
            {
                taskFileContents = FileHandler.getFileContents(TaskFile);
            }
            return taskFileContents;            
        }

        public static List<Ticket> getTicketsBasedOnNumType(int type)
        {
            List<Ticket> tickets = new List<Ticket>();
            if(type == 1)
            {
                //tickets = getTicketsBD();
                foreach(BugDefect t in getTicketsBD())
                {
                    tickets.Add((Ticket)t);
                }
            }
            if(type == 2)
            {
                //tickets = getTicketsEH();
                foreach(Enhancement t in getTicketsEH())
                {
                    tickets.Add((Ticket)t);
                }
            }
            if(type == 3)
            {
                //tickets = getTicketsTK();
                foreach(Task t in getTicketsTK())
                {
                    tickets.Add((Ticket)t);
                }
            }

            return tickets;
        }

        ////
        public static List<BugDefect> getTicketsBD()
        {
            if(ticketsBD == null)
            {
                ticketsBD = TicketService.mapTicketsFromStringListBD(getBugDefectFileContents());
            }            
            return ticketsBD;            
        }

        public static List<Enhancement> getTicketsEH()
        {
            if(ticketsEH == null)
            {
                ticketsEH = TicketService.mapTicketsFromStringListEH(getEnhancementsFileContents());
            }
            return ticketsEH;            
        }

        public static List<Task> getTicketsTK()
        {
            if(ticketsTK == null)
            {
                ticketsTK = TicketService.mapTicketsFromStringListTK(getTaskFileContents());
            }
            return ticketsTK;            
        }
        

        // elminates redundancies in code requiring this value
        
        public static int getLargestIDBD()
        {
            
            int largestID = 0;
            foreach(Ticket t in getTicketsBD())
            {   
                if(t.ticketId > largestID)
                {
                    largestID = t.ticketId;
                }
            }
            return largestID;
        }

        public static int getLargestIDEH()
        {
            
            int largestID = 0;
            foreach(Ticket t in getTicketsEH())
            {
                if(t.ticketId > largestID)
                {
                    largestID = t.ticketId;
                }
            }
            return largestID;
        }

        public static int getLargestIDTK()
        {
            
            int largestID = 0;
            foreach(Ticket t in getTicketsTK())
            {
                if(t.ticketId > largestID)
                {
                    largestID = t.ticketId;
                }
            }
            return largestID;
        }
              
        public static int getBaseNumAttributes()
        {
            return 7;
        }
        
        
        public static void addTicketBD(Ticket ticket)
        {
            ticketsBD.Add((BugDefect)ticket);
        }

        public static void addTicketBDSourceFromCSV(string csv)
        {
            FileHandler.addLineToFile(Model.BugDefectFile, csv);
        }
        
        public static void addTicketEH(Ticket ticket)
        {
            ticketsEH.Add((Enhancement)ticket);
        }

        public static void addTicketEHSourceFromCSV(string csv)
        {
            FileHandler.addLineToFile(Model.EnhancementsFile, csv);
        }

        public static void addTicketTK(Ticket ticket)
        {
            ticketsTK.Add((Task)ticket);
        }

        public static void addTicketTKSourceFromCSV(string csv)
        {
            FileHandler.addLineToFile(Model.TaskFile, csv);
        }

        

        

    }


}