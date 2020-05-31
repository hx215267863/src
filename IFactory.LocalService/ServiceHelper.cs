using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFactory.LocalService;

namespace IFactory.LocalService
{
    public class ServiceHelper
    {
        public static UserService userService = new UserService();
    }
}
