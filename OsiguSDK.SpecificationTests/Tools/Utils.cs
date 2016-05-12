using System;
using OsiguSDK.SpecificationTests.Settlements.Models;
using ServiceStack.Text;

namespace OsiguSDK.SpecificationTests.Tools
{
    public class Utils
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static void Dump(string message, object objectToPrint)
        {
            Console.WriteLine(message);
            Console.WriteLine(objectToPrint.Dump());
        }
    }
}