using System;

namespace IFactory.Domain.Models
{
    public class AVEItem
    {
        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public Double Size { get; set; }

        public Double Ave { get; set; }

        public string Type { get; set; }

        public IFactory.Domain.Common.SizeMeas? SizeMeas { get; set; }
    }
}
