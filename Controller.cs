using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using NLog.Web;

namespace TicketSystemSearch
{  
    public static class Controller
    {
        // Holds all valid inputs for main menu. If anything other than
        // what is listed here is input from the user, the program will end
        private static ArrayList actionOptions = new ArrayList()
        {
            "1","2","3"
        };

        private static ArrayList typeSensitiveOptions = new ArrayList()
        {
            "1"
        };

        //Model.getLogger()

        // Displays menu
        // Takes User Input
        // Repeats 

        public static void mainLoop()
        {
            string input = "ready";
            int actionChoice = 0;
            int typeChoice = 0;

            bool keepLoop = true;    
            bool needsTypeChoice = false;

            while(keepLoop)
            {
                View.displayMenu();
                input = Console.ReadLine();

                keepLoop = false;
                needsTypeChoice = false;
                foreach(string s in actionOptions)
                {
                    if(s==input)
                    {                        
                        try
                        {
                            actionChoice = int.Parse(input);
                            keepLoop = true;
                            foreach(string sa in typeSensitiveOptions)
                            {
                                if(sa==input)
                                {
                                    needsTypeChoice = true;
                                }
                            }                            
                        }
                        catch
                        {
                            Model.getLogger().Error("No Valid Choice Selected");
                        }
                    }                    
                }

                if(needsTypeChoice)
                {
                    typeChoice = typeSelector();

                    optionTypeSensitiveSelector(actionChoice, typeChoice);
                }
                else if(keepLoop)
                {
                    optionSelector(actionChoice);
                }
                
                
            }            
            
        }
        

        // Delegates to other methods based on user input
        private static void optionSelector(int action)
        {
            if(action == 2)
            {
                View.displayTicketsAll();
            }
            if(action == 3)
            {
                searchTickets();
            }
        }
        private static void optionTypeSensitiveSelector(int action, int type)
        {
            if(action == 1)
            {
                addTicket(type);               
            }
            if(action == 2)
            {
                View.displayTickets(type);                
            }
        }

        private static string searchTermSelector()
        {
            
            bool keepAsking = true;
            string input = "";

            while(keepAsking)
            {
                View.promptSelectorTerms();
                input = System.Console.ReadLine();
                foreach(string s in Model.getSelectorTerms())
                {
                    if(s.ToUpper() == input.ToUpper())
                    {
                        keepAsking = false;
                    }
                }
                if(keepAsking)
                {
                    Model.getLogger().Warn("Not Valid Search Term");
                }
            }

            return input.ToUpper();
        }

        

        private static int typeSelector()
        {
            string input = "ready";
            int typeChoice = 0;  
            bool verifiedType = false; 

            verifiedType = false;
            while(!verifiedType)
            {
                View.displayTypes();
                input = Console.ReadLine();

                verifiedType = false;
                foreach(string s in Model.getTypeOptions())
                {
                    if(s==input)
                    {
                        verifiedType = true;
                        try
                        {
                            typeChoice = int.Parse(input);
                        }
                        catch
                        {
                            Model.getLogger().Error("No Valid Choice Selected");
                        }
                    }                 
                }
                if(!verifiedType)
                {
                    Model.getLogger().Error("No Valid Choice Selected");
                }
            } 

            Model.getLogger().Info($"User Choice: \"{input}\"");
            
            return typeChoice;
        }

        
        private static void searchTickets()
        {
            string searchTerm = searchTermSelector().ToLower();
            View.displaySearchPrompt();
            string input = System.Console.ReadLine();

            IEnumerable<Ticket> temp = new List<Ticket>();
            List<Ticket> conversionList = new List<Ticket>();
            System.Console.WriteLine("SCANNING TICKETS.....");
            for(int i = 1; i<=Model.getNumTicketTypes();i++)
            {
                if(searchTerm == "status")
                {
                    temp = Model.getTicketsBasedOnNumType(i).Where(t => t.status.Contains(input));                    
                }
                if(searchTerm == "priority")
                {
                    temp = Model.getTicketsBasedOnNumType(i).Where(t => t.priority.Contains(input));
                }
                else if(searchTerm == "submitter")
                {
                    temp = Model.getTicketsBasedOnNumType(i).Where(t => t.submitter.Contains(input));
                }  

                foreach(Ticket t in temp)
                {
                    conversionList.Add(t);
                }        
            }
            View.displaySelectedTickets(conversionList);
        }
        

        //creates a csv string through user prompt
        //preppends a generated ticketID to the csv
        //adds csv to file
        //maps csv  to a ticket object
        //adds ticket object to list of tickets found in model
        //logs the addition

        private static void addTicket(int type)
        {
            string csv = promptNewTicket(type);
            
            Ticket ticket;
            ticket = TicketService.mapTicketFromCSVGenerateID(type, csv);
            csv = ticket.ticketId.ToString() + "," +csv;
            if(type == 1)
            {
                Model.addTicketBD(ticket);
                FileHandler.addLineToFile(Model.BugDefectFile, csv);
            }
            if(type == 2)
            {
                Model.addTicketEH(ticket);
                FileHandler.addLineToFile(Model.EnhancementsFile, csv);
            }
            if(type == 3)
            {
                Model.addTicketTK(ticket);
                FileHandler.addLineToFile(Model.TaskFile, csv);
            }

            Model.getLogger().Info($"Ticket id {ticket.ticketId} added");
        }


        // gets the prompt from the view
        // builds a csv along with user input
        // loops the input for genres until user types "done"
        // loops input for runtime until it parses to a TimeSpan object without exception

        private static string promptNewTicket(int type)
        {
            string selection;
            string newCsv = "";
            bool additionalWatchers = false;

            for(int i = 1; i < Model.getBaseNumAttributes(); i++)
            {   
                selection = "ready";
                
                if(i != 6)
                {
                    View.creationPrompt(i);
                    selection = System.Console.ReadLine();
                    if(i!=1)
                    {
                        newCsv += ",";
                    }
                    newCsv += selection;
                }
                else
                {                   
                    while(selection.ToUpper() != Model.getWatcherEscape())
                    {
                        View.creationPrompt(i);
                        selection = System.Console.ReadLine();
                        if((selection.ToUpper() != Model.getWatcherEscape()) || !additionalWatchers)
                        {
                            if(additionalWatchers)
                            {
                                newCsv += "|";
                            }
                            else
                            {
                                newCsv += ",";
                            }
                            
                            additionalWatchers = true;
                            newCsv += selection;
                        }
                    }
                    
                }                
            }

            if(type ==1)
            {
                newCsv = appendCSVWithPromptBD(newCsv);
            }
            if(type ==2)
            {
                newCsv = appendCSVWithPromptEH(newCsv);
            }
            if(type ==3)
            {
                newCsv = appendCSVWithPromptTK(newCsv);
            }

            return newCsv;
        }

        private static string appendCSVWithPromptBD(string csv)
        {
            string selection;
            string newCsv = csv;
            BugDefect temp = new BugDefect();
            for(int i = 1; i <= temp.getNumAttributes() - Model.getBaseNumAttributes(); i++)
            {   
                selection = "ready";
                
                if(i == 1)
                {
                    View.creationPromptBD(i);
                    selection = System.Console.ReadLine();
                    newCsv += ",";
                    newCsv += selection;
                }              
            }
            csv = newCsv;
            return csv;
        }

        private static string appendCSVWithPromptEH(string csv)
        {
            string selection;
            string newCsv = csv;
            Enhancement temp = new Enhancement();
            bool validDouble = false;
            string[] tempDoubleVal;
            for(int i = 1; i <= temp.getNumAttributes() - Model.getBaseNumAttributes(); i++)
            {   
                selection = "ready";
                
                if(i==2 || i==4)
                {                  
                    validDouble = false; 
                    while(!validDouble)
                    {
                        View.creationPromptEH(i);
                        selection = System.Console.ReadLine();
                        try
                        {
                            double.Parse(selection);
                            tempDoubleVal = selection.Split(".");

                            if(tempDoubleVal[1].Length ==2)
                            {
                                validDouble = true;
                            }
                            else
                            {
                                Model.getLogger().Warn("Monetary Value Must Contain 2 digits past decimal");
                            }
                            
                        }
                        catch
                        {
                            Model.getLogger().Error("Not valid double value");
                        }    
                    }
                    newCsv += ",";
                    newCsv += selection;
                    
                } 
                else
                {
                    View.creationPromptEH(i);
                    selection = System.Console.ReadLine();
                    newCsv += ",";
                    newCsv += selection;
                }              
            }
            csv = newCsv;
            return csv;
        }

        private static string appendCSVWithPromptTK(string csv)
        {
            string selection;
            string newCsv = csv;
            Task temp = new Task();
            bool validDate = false;
            for(int i = 1; i <= temp.getNumAttributes() - Model.getBaseNumAttributes(); i++)
            {   
                selection = "ready";
                
                if(i==2)
                {                  
                    validDate = false; 
                    while(!validDate)
                    {
                        View.creationPromptTK(i);
                        selection = System.Console.ReadLine();
                        try
                        {
                            DateTime.Parse(selection);
                            validDate = true;
                        }
                        catch
                        {
                            Model.getLogger().Error("Not valid DateTime");
                        }    
                    }
                    newCsv += ",";
                    newCsv += selection;
                    
                } 
                else
                {
                    View.creationPromptTK(i);
                    selection = System.Console.ReadLine();
                    newCsv += ",";
                    newCsv += selection;
                }              
            }
            csv = newCsv;
            return csv;
        }

        
    }


}