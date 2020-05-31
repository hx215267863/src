using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.Domain.Entities
{
    public class AlarmCraftTopInfo
    {
        public int DID { get; set; }

        public int CraftDID { get; set; }

        public int Count { get; set; }

        public DateTime AlarmTime { get; set; }
    }
}
