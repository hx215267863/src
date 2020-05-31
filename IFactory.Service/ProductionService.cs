using IFactory.Data;
using IFactory.Domain.Entities;
using IFactory.Domain.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IFactory.Service
{
    public class ProductionService : BaseService, IProductionService
    {
        public ProductionService(IDatabaseFactory databaseFactory)
          : base(databaseFactory)
        {
        }

        public IList<ProductionTypeInfo> GetProductionTypeInfos(int craftDID)
        {
            IQueryable<ProductionTypeInfo> source = from pti in base.DataContext.ProductionTypeInfos
                                                    join fi in base.DataContext.FacilityInfos on pti.FacilityDID equals fi.FacilityDID
                                                    join p in base.DataContext.ProcessInfos on fi.ProcessDID equals p.ProcessDID
                                                    where p.CraftDID == craftDID
                                                    select pti;
            return source.ToList();

        }

        public IList<ProcessInfo> GetProcesses(int craftDID)
        {
            return this.DataContext.ProcessInfos.Where(m => m.CraftDID == craftDID).ToList();
        }

        public IList<FacilityInfo> GetFacilities(int craftDID)
        {
            return this.DataContext.FacilityInfos.Join(this.DataContext.ProcessInfos, fi => fi.ProcessDID, p => p.ProcessDID, (fi, p) => new { fi = fi, p = p }).Where(data => data.p.CraftDID == craftDID).Select(data => data.fi).ToList();
        }

        public IList<UnitInfo> GetAllUnits()
        {
            return this.DataContext.UnitInfos.ToList();
        }

        public IList<CraftInfo> GetCrafts()
        {
            return this.DataContext.CraftInfos.ToList();
        }

        public CraftProbablyInfo GetCraftProbably(int craftDID)
        {
            return this.DataContext.CraftProbablyInfos.Find(craftDID);
        }

        public ProductionTypeInfo GetProductionType(int did)
        {
            return this.DataContext.ProductionTypeInfos.Find(did);
        }

        public ProductionLineProbablyInfo GetProductionLineProbably(int did)
        {
            return this.DataContext.ProductionLineProbablyInfos.Find(did);
        }

        public PLCStateInfo GetPLCState(int did)
        {
            return this.DataContext.PLCStateInfos.Find(did);
        }

        public CraftInfo GetCraft(int did)
        {
            return this.DataContext.CraftInfos.Find(did);
        }

        public FacilityInfo GetFacility(int did)
        {
            return this.DataContext.FacilityInfos.Find(did);
        }

        public ProcessInfo GetProcess(int did)
        {
            return this.DataContext.ProcessInfos.Find(did);
        }

        public UnitInfo GetUnit(int did)
        {
            return this.DataContext.UnitInfos.Find(did);
        }

        public IList<PLCStateInfo> GetPLCStates(int craftDID)
        {
            return this.DataContext.PLCStateInfos.Where(m => m.CraftDID == craftDID).ToList();
        }

        public Dictionary<int, int> GetCraftStates()
        {
            return this.DataContext.Database.SqlQuery<KeyValueModel<int, int>>("\r\n    select craft_did as 'Key',max(state) as 'Value'\r\n    from (\r\n        select craft_did,3 as state from alarm_temporary group by craft_did\r\n        union all select p.craft_did,max(fi.state) as state from facility_info fi join process p on fi.process_did=p.did group by p.craft_did\r\n    ) as t group by craft_did;").ToDictionary(m => m.Key, m => m.Value);
        }

        public Dictionary<int, int> GetProcessStates(int craftDID)
        {
            MySqlParameter[] mySqlParameterArray = new MySqlParameter[1]
            {
        new MySqlParameter("?craft_did", MySqlDbType.Int24)
            };
            mySqlParameterArray[0].Value = craftDID;
            return this.DataContext.Database.SqlQuery<KeyValueModel<int, int>>("\r\n    select process_did as 'Key',max(state) as 'Value'\r\n    from (\r\n        select process_did,3 as state from alarm_temporary where craft_did=?craft_did group by process_did\r\n        union all select fi.process_did,max(fi.state) as state from facility_info fi join process p on fi.process_did=p.did where p.craft_did=?craft_did group by fi.process_did\r\n    ) as t group by process_did;", mySqlParameterArray).ToDictionary(m => m.Key, m => m.Value);
        }

        public int GetProductionState()
        {
            string commandText = "\r\n    select max(state) as state\r\n    from (\r\n        select 3 as state from alarm_temporary\r\n        union all select max(fi.state) as state from facility_info fi\r\n    ) as t;";
            object obj = MySqlHelper.ExecuteScalar(DataContext.ConnectionString, commandText);
            if (obj != null && obj != DBNull.Value)
                return Convert.ToInt32(obj);
            return 2;
        }

        public int GetCraftState(int craftDID)
        {
            MySqlParameter[] mySqlParameterArray = new MySqlParameter[1]
            {
        new MySqlParameter("?craft_did", MySqlDbType.Int24)
            };
            mySqlParameterArray[0].Value = craftDID;
            string commandText = "\r\n    select max(state) as state\r\n    from (\r\n        select 3 as state from alarm_temporary where craft_did=?craft_did\r\n        union all select max(fi.state) as state from facility_info fi join process p on fi.process_did=p.did where p.craft_did=?craft_did \r\n    ) as t;";
            object obj = MySqlHelper.ExecuteScalar(DataContext.ConnectionString, commandText, mySqlParameterArray);
            if (obj != null && obj != DBNull.Value)
                return Convert.ToInt32(obj);
            return 2;
        }

        public void AddUnit(UnitInfo unitInfo)
        {
            this.DataContext.UnitInfos.Add(unitInfo);
            this.DataContext.SaveChanges();
        }

        public void UpdateUnit(UnitInfo unitInfo)
        {
            this.DataContext.SaveChanges();
        }

        public void DeleteUnit(UnitInfo unitInfo)
        {
            this.DataContext.UnitInfos.Remove(unitInfo);
            this.DataContext.SaveChanges();
        }

        public void SaveProductionLineProbably(ProductionLineProbablyInfo productionLineProbablyInfo)
        {
            this.DataContext.SaveChanges();
        }

        public IList<CraftDetailModel> GetCraftDetails()
        {
            return this.DataContext.CraftInfos.Join(this.DataContext.CraftProbablyInfos, c => c.CraftDID, cp => cp.CraftDID, (c, cp) => new CraftDetailModel()
            {
                CraftDID = c.CraftDID,
                CraftName = c.CraftName,
                CraftNO = c.CraftNO,
                TargetYield = cp.TargetYield
            }).ToList();
        }

        public CraftDetailModel GetCraftDetail(int craftDID)
        {
            return this.DataContext.CraftInfos.Join(this.DataContext.CraftProbablyInfos, c => c.CraftDID, cp => cp.CraftDID, (c, cp) => new { c = c, cp = cp }).Where(data => data.c.CraftDID == craftDID).Select(data => new CraftDetailModel()
            {
                CraftDID = data.c.CraftDID,
                CraftName = data.c.CraftName,
                CraftNO = data.c.CraftNO,
                TargetYield = data.cp.TargetYield
            }).FirstOrDefault();
        }

        public void SaveCraftDetail(CraftDetailModel model)
        {
            CraftInfo craft = this.GetCraft(model.CraftDID);
            CraftProbablyInfo craftProbably = this.GetCraftProbably(model.CraftDID);
            craft.CraftName = model.CraftName;
            craftProbably.TargetYield = model.TargetYield;
            this.DataContext.SaveChanges();
        }

        public IList<ProcessInfo> GetProcesses(int[] processDIDs)
        {
            return this.DataContext.ProcessInfos.Where(m => processDIDs.Contains<int>(m.CraftDID)).ToList();
        }

        public IList<FacilityInfo> GetFacilities(int[] facilityDIDs)
        {
            return this.DataContext.FacilityInfos.Where(m => facilityDIDs.Contains<int>(m.FacilityDID)).ToList();
        }
    }
}
