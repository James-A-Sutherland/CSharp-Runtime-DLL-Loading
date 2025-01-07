using System;
using System.Reflection;

namespace Runtime_DLL_Loading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Replace this with the path of MyLibrary.dll once built if this isn't correct
            var assembly = LoadAssembly(@"..\..\..\MyLibrary\bin\Debug\MyLibrary.dll");

            if (assembly != null)
            {
                // Run ExampleFunction() which takes no args
                InvokeMethod(assembly, "MyLibrary.ExampleClass", "ExampleFunction", null);

                // Run ExampleWithParameters() which takes a string argument for the users' name
                InvokeMethod(assembly, "MyLibrary.ExampleClass", "ExampleWithParameters", new object[] { "James" });
            }

            Console.Write("Press any key to exit... ");
            Console.ReadLine();
        }

        public static Assembly LoadAssembly(string path)
        {
            try
            {
                return Assembly.LoadFrom(path);
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("DLL not found");
                return null;
            }
        }

        public static void InvokeMethod(Assembly assembly, string className, string methodName, object[] paramaters)
        {
            Type typeOfClass = assembly.GetType(className);

            if (typeOfClass != null)
            {
                object instance = Activator.CreateInstance(typeOfClass);
                MethodInfo method = typeOfClass.GetMethod(methodName);

                if (method != null)
                {
                    method.Invoke(instance, paramaters);
                }
                else
                {
                    Console.WriteLine("Error loading method");
                }
            }
            else
            {
                Console.WriteLine("Error loading class");
            }
        }
    }
}
