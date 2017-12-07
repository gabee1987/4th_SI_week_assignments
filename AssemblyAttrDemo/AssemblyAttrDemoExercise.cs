using System;
using System.Reflection;

namespace AssemblyAttrDemo
{
    class AssemblyAttrDemoExercise
    {
        static void Main(string[] args)
        {
            Assembly a = Assembly.GetExecutingAssembly();
            Type attrType = typeof(AssemblyDescriptionAttribute);
            object[] attrs = a.GetCustomAttributes(attrType, false);

            if (attrs.Length > 0)
            {
                AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)attrs[0];
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Description is:  ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(desc.Description);
                Console.ResetColor();
            }

            Console.ReadLine();
        }
    }
}
