using System;
using System.IO;
using System.Reflection;

namespace DynamicCodeDemo
{
    class DynamicCodeDemoExercise
    {
        static void Main(string[] args)
        {
            string path = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.Web.dll";

            // Get the Assembly from the file
            Assembly webAssembly = LoadAssembly(path);

            // Get the type to the HttpUtility class
            Type utilType = webAssembly.GetType("System.Web.HttpUtility");

            // Get the static HtmlEncode and HtmlDecode methods
            MethodInfo encode = utilType.GetMethod("HtmlEncode", new Type[] { typeof(string) });
            MethodInfo decode = utilType.GetMethod("HtmlDecode", new Type[] { typeof(string) });

            // Create a string to be encoded
            string originalString = "This is Sally & Jack's Anniversary <sic>";

            // Print out the original string
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Original String:     ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(originalString);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();


            // Encode it and print out the encoded value
            string encodedString = (string)encode.Invoke(null, new object[] { originalString });
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Encoded String:     ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(encodedString);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();


            // Decode it and print out to make sure it comes back right
            string decodedString = (string)decode.Invoke(null, new object[] { encodedString });
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Decoded String:     ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(decodedString);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();

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
                assem = Assembly.LoadFile(path);
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
