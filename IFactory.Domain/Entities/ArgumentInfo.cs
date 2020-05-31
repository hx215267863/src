using System;

namespace IFactory.Domain.Entities
{
    public class ArgumentInfo
    {
        public int Name { get; set; }

        public virtual FacilityInfo Facility { get; set; }

    }
}
