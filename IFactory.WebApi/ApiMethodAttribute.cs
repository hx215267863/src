using System;

namespace IFactory.Platform
{
    public class ApiMethodAttribute : Attribute
    {
        public bool IsCheckSession { get; set; }

        public ApiMethodAttribute()
        {
        }

        public ApiMethodAttribute(bool isCheckSession)
        {
            this.IsCheckSession = isCheckSession;
        }
    }
}
