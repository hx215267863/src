using IFactory.Platform.Common;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IFactory.Platform.Util
{
    public static class Utils
    {
        private static Dictionary<string, ApiMethodInfo> apiMethods = new Dictionary<string, ApiMethodInfo>();

        static Utils()
        {
            foreach (MethodInfo method in typeof(WebApiController).GetMethods())
            {
                ApiMethodAttribute customAttribute = method.GetCustomAttribute<ApiMethodAttribute>();
                if (customAttribute != null)
                {
                    Type parameterType = ((IEnumerable<ParameterInfo>)method.GetParameters()).First<ParameterInfo>().ParameterType;
                    IRequest<IResponse> request = (IRequest<IResponse>)Activator.CreateInstance(parameterType);
                    ApiMethodInfo apiMethodInfo = new ApiMethodInfo()
                    {
                        ApiName = request.ApiName,
                        IsCheckSession = customAttribute.IsCheckSession,
                        Method = method,
                        RequestType = parameterType
                    };
                    Utils.apiMethods.Add(apiMethodInfo.ApiName, apiMethodInfo);
                }
            }
        }

        public static Dictionary<string, ApiMethodInfo> GetApiMethods()
        {
            return Utils.apiMethods;
        }
    }
}
