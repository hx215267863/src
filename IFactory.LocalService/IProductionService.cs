using IFactory.Domain.Entities;
using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.LocalService
{
    public interface IProductionService
    {
        IList<ProductionTypeInfo> GetProductionTypeInfos(int craftDID);

        IList<ProcessInfo> GetProcesses(int craftDID);

        IList<ProcessInfo> GetProcesses(int[] processDIDs);

        IList<FacilityInfo> GetFacilities(int craftDID);

        IList<FacilityInfo> GetFacilities(int[] facilityDIDs);

        IList<UnitInfo> GetAllUnits();

        IList<CraftInfo> GetCrafts();

        CraftProbablyInfo GetCraftProbably(int craftDID);

        ProductionTypeInfo GetProductionType(int did);

        ProductionLineProbablyInfo GetProductionLineProbably(int did);

        PLCStateInfo GetPLCState(int did);

        FacilityInfo GetFacility(int did);

        CraftInfo GetCraft(int did);

        ProcessInfo GetProcess(int did);

        UnitInfo GetUnit(int did);

        IList<PLCStateInfo> GetPLCStates(int craftDID);

        Dictionary<int, int> GetCraftStates();

        Dictionary<int, int> GetProcessStates(int craftDID);

        int GetProductionState();

        int GetCraftState(int craftDID);

        void AddUnit(UnitInfo unitInfo);

        void UpdateUnit(UnitInfo unitInfo);

        void DeleteUnit(UnitInfo unitInfo);

        void SaveProductionLineProbably(ProductionLineProbablyInfo productionLineProbablyInfo);

        IList<CraftDetailModel> GetCraftDetails();

        CraftDetailModel GetCraftDetail(int craftDID);

        void SaveCraftDetail(CraftDetailModel model);
    }
}
