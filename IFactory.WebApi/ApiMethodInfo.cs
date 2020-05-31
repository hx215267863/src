using System;
using System.Reflection;

namespace IFactory.Platform
{
    public class ApiMethodInfo
    {
        public string ApiName { get; set; }

        public bool IsCheckSession { get; set; }

        public MethodInfo Method { get; set; }

        public Type RequestType { get; set; }
    }
}
