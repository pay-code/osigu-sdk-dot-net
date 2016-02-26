//using System;

//namespace OsiguSDK.Models
//{
//    public class StringValue : System.Attribute
//    {
//        public string Value { get; private set; }

//        public StringValue(string value)
//        {
//            Value = value;
//        }
//    }

//    public static class StringEnum
//    {
//        public static string GetStringValue(Enum value)
//        {
//            string output = null;
//            if (null == value)
//                return null;
//            var type = value.GetType();

//            var fi = type.GetField(value.ToString());
//            var attrs =
//                fi.GetCustomAttributes(typeof (StringValue), false) as StringValue[];

//            if (attrs.Length > 0)
//                output = attrs[0].Value;

//            return output;
//        }

//        public static object GetEnumValue(string stringValue, Type enumType)
//        {
//            var fields = enumType.GetFields();
//            foreach (var field in fields)
//            {
//                var attrs = field.GetCustomAttributes(typeof (StringValue), false) as StringValue[];
//                if (attrs.Length > 0)
//                {
//                    if (attrs[0].Value == stringValue)
//                    {
//                        return field.GetRawConstantValue();
//                    }
//                }
//            }
//            return null;
//        }
//    }
//}
