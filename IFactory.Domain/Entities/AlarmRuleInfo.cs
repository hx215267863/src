namespace IFactory.Domain.Entities
{
  public class AlarmRuleInfo
  {
    public string RuleDID { get; set; }

    public string AlarmContent { get; set; }

    public int AlarmTypeDID { get; set; }

    public string AlarmReason { get; set; }

    public int SolutionDID { get; set; }

    public int SolutionImageDID { get; set; }

    public int AlarmLocationImageDID { get; set; }

    public int CraftDID { get; set; }

        public int FacilityDID { get; set; }
        
        public int UnitDID { get; set; }

    public virtual AlarmTypeInfo AlarmType { get; set; }

    public virtual SolutionInfo Solution { get; set; }

    public virtual CraftInfo Craft { get; set; }

    public virtual UnitInfo Unit { get; set; }

    public virtual AlarmLocationImageInfo AlarmLocationImage { get; set; }

    public virtual SolutionImageInfo SolutionImage { get; set; }
  }
}
