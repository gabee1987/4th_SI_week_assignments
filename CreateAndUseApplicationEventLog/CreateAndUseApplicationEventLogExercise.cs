using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace CreateAndUseApplicationEventLog
{
    class CreateAndUseApplicationEventLogExercise
    {
        static void Main(string[] args)
        {
            string message = "This is a demo EventLog";
            string myLogName = "This is my new EventLog";
            string sourceName = Environment.MachineName;


            EventLog MyLog = new EventLog(myLogName, sourceName, message);
            Trace.AutoFlush = true;
            EventLogTraceListener MyListener = new EventLogTraceListener(MyLog);
            Trace.WriteLine("This is a test");

            //DisplayEventLogProperties();
            Console.ReadLine(); 
        }


        static void DisplayEventLogProperties()
        {
            // Iterate through the current set of event log files,
            // displaying the property settings for each file.

            EventLog[] eventLogs = EventLog.GetEventLogs();
            foreach (EventLog e in eventLogs)
            {
                Int64 sizeKB = 0;

                Console.WriteLine();
                Console.WriteLine("{0}:", e.LogDisplayName);
                Console.WriteLine("  Log name = \t\t {0}", e.Log);

                Console.WriteLine("  Number of event log entries = {0}", e.Entries.Count.ToString());

                // Determine if there is an event log file for this event log.
                RegistryKey regEventLog = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\EventLog\\" + e.Log);
                if (regEventLog != null)
                {
                    Object temp = regEventLog.GetValue("File");
                    if (temp != null)
                    {
                        Console.WriteLine("  Log file path = \t {0}", temp.ToString());
                        FileInfo file = new FileInfo(temp.ToString());

                        // Get the current size of the event log file.
                        if (file.Exists)
                        {
                            sizeKB = file.Length / 1024;
                            if ((file.Length % 1024) != 0)
                            {
                                sizeKB++;
                            }
                            Console.WriteLine("  Current size = \t {0} kilobytes", sizeKB.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("  Log file path = \t <not set>");
                    }
                }

                // Display the maximum size and overflow settings.

                sizeKB = e.MaximumKilobytes;
                Console.WriteLine("  Maximum size = \t {0} kilobytes", sizeKB.ToString());
                Console.WriteLine("  Overflow setting = \t {0}", e.OverflowAction.ToString());

                switch (e.OverflowAction)
                {
                    case OverflowAction.OverwriteOlder:
                        Console.WriteLine("\t Entries are retained a minimum of {0} days.",
                            e.MinimumRetentionDays);
                        break;
                    case OverflowAction.DoNotOverwrite:
                        Console.WriteLine("\t Older entries are not overwritten.");
                        break;
                    case OverflowAction.OverwriteAsNeeded:
                        Console.WriteLine("\t If number of entries equals max size limit, a new event log entry overwrites the oldest entry.");
                        break;
                    default:
                        break;
                }
            }
        }

        static void CreateAnEventSource(string messageFile)
        {
            string myLogName;
            string sourceName = "SampleApplicationSource";

            // Create the event source if it does not exist.
            if (!EventLog.SourceExists(sourceName))
            {
                // Create a new event source for the custom event log
                // named "myNewLog."  

                myLogName = "myNewLog";
                EventSourceCreationData mySourceData = new EventSourceCreationData(sourceName, myLogName);

                // Set the message resource file that the event source references.
                // All event resource identifiers correspond to text in this file.
                if (!System.IO.File.Exists(messageFile))
                {
                    Console.WriteLine("Input message resource file does not exist - {0}",
                        messageFile);
                    messageFile = "";
                }
                else
                {
                    // Set the specified file as the resource
                    // file for message text, category text, and 
                    // message parameter strings.  

                    mySourceData.MessageResourceFile = messageFile;
                    mySourceData.CategoryResourceFile = messageFile;
                    mySourceData.CategoryCount = mySourceData.CategoryCount;
                    mySourceData.ParameterResourceFile = messageFile;

                    Console.WriteLine("Event source message resource file set to {0}",
                        messageFile);
                }

                Console.WriteLine("Registering new source for event log.");
                EventLog.CreateEventSource(mySourceData);
            }
            else
            {
                // Get the event log corresponding to the existing source.
                myLogName = EventLog.LogNameFromSourceName(sourceName, ".");
            }

            // Register the localized name of the event log.
            // For example, the actual name of the event log is "myNewLog," but
            // the event log name displayed in the Event Viewer might be
            // "Sample Application Log" or some other application-specific
            // text.
            EventLog myEventLog = new EventLog(myLogName, ".", sourceName);

            if (messageFile.Length > 0)
            {
                myEventLog.RegisterDisplayName(messageFile, 234);
            }
        }
    }
}
