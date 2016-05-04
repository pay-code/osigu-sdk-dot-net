using System;

namespace OsiguSDK.SpecificationTests.Tools
{
    public class Utils
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}