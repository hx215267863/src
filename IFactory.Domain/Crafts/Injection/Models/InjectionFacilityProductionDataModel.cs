using IFactory.Domain.Crafts.Base.Models;
using System;

namespace IFactory.Domain.Crafts.Injection.Models
{
    public class InjectionFacilityProductionDataModel : FacilityProductionDataModel
    {
        public DateTime? StartDate { get; set; }

        public Decimal? BeforeWeight { get; set; }

        public string BeforePass { get; set; }

        public Decimal? AfterWeight { get; set; }

        public Decimal? InjectionWeight { get; set; }

        public string AfterPass { get; set; }

        public int InjectionNeedle { get; set; }

        public Decimal? PumpTime { get; set; }

        public Decimal? PackageTime { get; set; }

        public Decimal? PocketTime { get; set; }

        public Decimal? SaveTime { get; set; }

        public Decimal? PackageTemp { get; set; }

        public string ProductType { get; set; }

        public string ProductNo { get; set; }
    }
}
