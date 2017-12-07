using System;
using System.Reflection;

namespace AssemblyDemo
{
    class AssemblyDemoExercise
    {
        static void Main(string[] args)
        {
            // Load a specific Assembly
            string longName = "system, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
            Assembly a = LoadSystemAssembly(longName);
            ShowAssemblyInfo(a);

            // Get our Assembly
            Assembly ourAssembly = Assembly.GetExecutingAssembly();
            ShowAssemblyInfo(ourAssembly);

            Console.ReadLine();
        }


        /// <summary>
        /// Print out a specific Assembly details
        /// </summary>
        /// <param name="a">The Assembly object</param>
        static void ShowAssemblyInfo(Assembly a)
        {
            // Print out the Assembly name
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Assembly FullName:   ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(a.FullName);
            Console.WriteLine();

            // Print out the Assembly cache
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("From GAC?            ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(a.GlobalAssemblyCache);
            Console.WriteLine();

            // Print out the Assembly location
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Path:                ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(a.Location);
            Console.WriteLine();

            // Print out the Assembly version
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Version:             ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(a.ImageRuntimeVersion);
            Console.WriteLine();
            Console.ResetColor();

            // Print out the modules
            foreach (Module m in a.GetModules())
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Mod:                 ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(m.Name);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
        }


        /// <summary>
        /// Load a specific assembly file
        /// </summary>
        /// <param name="path">The path of the assembly file</param>
        /// <returns>Assembly object</returns>
        static Assembly LoadSystemAssembly(string path)
        {
            Assembly assem = Assembly.Load(path);
            if (assem == null)
                Console.WriteLine("Unable to load assembly...");

            return assem;
        }
    }
}
