using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyDumpInfoDemo
{
    class AssemblyDumpInfoDemoExercise
    {
        static void Main(string[] args)
        {
            string path = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.ServiceProcess.dll";

            // Using BindingFlags to only get declared and instance members
            BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance;


            // Load the Assembly from the path
            Assembly assembly = LoadAssembly(path);

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Assembly FullName:   ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(assembly.FullName);
            Console.WriteLine();

            // Get all the types from the assembly object
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Type:            ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(type.Name);
                Console.ResetColor();
                MemberInfo[] members = type.GetMembers(flags);

                Console.WriteLine();
                foreach (MemberInfo member in members)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("MemberType:      ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(member.MemberType);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("MemberName:      ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(member.Name);
                    Console.ResetColor();
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            Console.ReadLine();

        }

        /// <summary>
        /// Load a specific assembly file
        /// </summary>
        /// <param name="path">The path of the assembly file</param>
        /// <returns>Assembly object</returns>
        static Assembly LoadAssembly(string path)
        {
            Assembly assem = null;
            try
            {
                assem = Assembly.LoadFrom(path);
                if (assem == null)
                    Console.WriteLine("Unable to load assembly...");
            }
            catch (FileLoadException fe)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(fe);
                Console.ResetColor();
                Console.ReadLine();
            }
            return assem;
        }
    }
}
