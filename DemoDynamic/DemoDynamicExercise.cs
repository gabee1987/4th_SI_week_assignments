using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DemoDynamic
{
    class DemoDynamicExercise
    {
        static void Main(string[] args)
        {

            // Create an Assembly Name
            AssemblyName theName = new AssemblyName();
            theName.Name = "DemoAssembly";
            theName.Version = new Version("1.0.0.0");

            // Get the AppDomain to put our assembly in
            AppDomain domain = AppDomain.CurrentDomain;

            // Create the Assembly
            AssemblyBuilder assemBldr = domain.DefineDynamicAssembly(theName, AssemblyBuilderAccess.ReflectionOnly);

            // Define a module to hold our type
            ModuleBuilder modBldr = assemBldr.DefineDynamicModule("CodeModule", "DemoAssembly.dll");

            // Create a new type
            TypeBuilder animalBldr = modBldr.DefineType("Animal", TypeAttributes.Public);

            // Display the new Type
            Type animal = animalBldr.CreateType();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("New Type name:      ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(animal.FullName);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();

            foreach (MemberInfo member in animal.GetMembers())
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

            Console.ReadLine();
        }
    }
}
