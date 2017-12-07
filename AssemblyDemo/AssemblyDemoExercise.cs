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
            Assembly assembly = LoadAssembly(longName);
            ShowAssemblyInfo(assembly);

            // Get our Assembly
            Assembly ourAssembly = Assembly.GetExecutingAssembly();
            ShowAssemblyInfo(ourAssembly);

            Console.ReadLine();
        }


        /// <summary>
        /// Print out a specific Assembly details
        /// </summary>
        /// <param name="assembly">The Assembly object</param>
        static void ShowAssemblyInfo(Assembly assembly)
        {
            // Print out the Assembly name
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Assembly FullName:   ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(assembly.FullName);
            Console.WriteLine();

            // Print out the Assembly cache
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("From GAC?            ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(assembly.GlobalAssemblyCache);
            Console.WriteLine();

            // Print out the Assembly location
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Path:                ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(assembly.Location);
            Console.WriteLine();

            // Print out the Assembly version
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Version:             ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(assembly.ImageRuntimeVersion);
            Console.WriteLine();
            Console.ResetColor();

            // Print out the modules
            foreach (Module m in assembly.GetModules())
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
        static Assembly LoadAssembly(string path)
        {
            Assembly assem = Assembly.Load(path);
            if (assem == null)
                Console.WriteLine("Unable to load assembly...");

            return assem;
        }
    }
}
