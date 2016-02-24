using System;
using System.Text;

namespace OsiguSDK.Models
{
    [Serializable]
    public class PolicyException
    {
       
        /// <summary>
        /// return the textual representation of the error
        /// </summary>
        public string Text;

        /// <summary>
        /// return any instance specific error variables
        /// </summary>
        public string[] Variables;


        /// <summary>
        /// default constructor
        /// </summary>
        public PolicyException() { }

        /// <summary>
        /// utility constructor to create a ServiceException object with all fields set </summary>
        /// <param name="text"> </param>
        /// <param name="variables"> </param>
        public PolicyException(string text, string[] variables)
        {
            Text = text;
            Variables = variables;
        }

        /// <summary>
        /// generate a textual representation of the ServiceException instance  
        /// </summary>
        public override string ToString()
        {
            var buffer = new StringBuilder();
            buffer.Append(", text = ");
            buffer.Append(Text);
            buffer.Append(", variables = {");
            if (Variables != null)
            {
                for (var i = 0; i < Variables.Length; i++)
                {
                    buffer.Append("[");
                    buffer.Append(i);
                    buffer.Append("] = ");
                    buffer.Append(Variables[i]);
                }
            }
            buffer.Append("}");
            return buffer.ToString();
        }
    }
}
