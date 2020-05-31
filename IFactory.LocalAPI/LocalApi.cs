using System.Text;
using IFactory.DB;
using AutoMapper;
using IFactory.Common.Logs;
using IFactory.Common.Utils;
using IFactory.Data.Crafts;
using IFactory.Domain.Crafts.Base.Entities;
using IFactory.Domain.Crafts.Base.Models;
using IFactory.Domain.Entities;
using IFactory.Domain.Models;
using IFactory.Service;
using IFactory.Service.Crafts;
using IFactory.Platform.Common;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.Alarm;
using IFactory.Platform.Common.Request.Crafts;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Request.Report;
using IFactory.Platform.Common.Request.Setting;
using IFactory.Platform.Common.Request.User;
using IFactory.Platform.Common.Request.SystemParam;
using IFactory.Platform.Common.Response.Alarm;
using IFactory.Platform.Common.Response.SystemParam;
using IFactory.Platform.Common.Response.Crafts;
using IFactory.Platform.Common.Response.Product;
using IFactory.Platform.Common.Response.Report;
using IFactory.Platform.Common.Response.Setting;
using IFactory.Platform.Common.Response.User;
using IFactory.Platform.Common.Util;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace IFactory
{
    public class LocalApi
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static LoginResponse Execute(LoginRequest request)
        {
            LoginResponse loginResponse = new LoginResponse();
            UserInfo userByUserName = ServiceHelper.userDB.GetUserByUserName(request.UserName);
            if (userByUserName != null)
            {
                if (userByUserName.Password == request.Password)
                {
                    userByUserName.LastLoginTime = new DateTime?(DateTime.Now);
                    ServiceHelper.userDB.UpdateUserLastLoginTime(userByUserName);
                    loginResponse.UserId = userByUserName.UserId;
                    loginResponse.Name = userByUserName.Name;
                    loginResponse.PermissionCodes = userByUserName.Role.PermissionCodes;
                    loginResponse.CraftDIDs = userByUserName.CraftDIDs;
                }
                else
                    loginResponse.ErrMsg = "用户名或密码不正确";
            }
            else
                loginResponse.ErrMsg = "用户名或密码不正确";
            return loginResponse;
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static PermissionListResponse Execute(PermissionListRequest request)
        {
            return new PermissionListResponse()
            {
                Permissions = ServiceHelper.userDB.GetAllPermissions().Select(m => m.ToModel()).ToList()
            };
        }
        
        /// <summary>
        /// 获取产线状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ProductionStateGetResponse Execute(ProductionStateGetRequest request)
        {
            return new ProductionStateGetResponse()
            {
                State = ServiceHelper.productionDB.GetProductionState()
            };
        }


        public static CraftStateGetResponse Execute(CraftStateGetRequest request)
        {
            return new CraftStateGetResponse()
            {
                State = ServiceHelper.productionDB.GetCraftState(request.CraftDID)
            };
        }

        public static CraftListResponse GetCraftsList(CraftListRequest request)
        {
            CraftListResponse craftListResponse = new CraftListResponse();
            List<CraftModel> list = ServiceHelper.productionDB.GetCrafts().Select(m => m.ToModel()).ToList();
            Dictionary<int, int> craftStates = ServiceHelper.productionDB.GetCraftStates();
            foreach (CraftModel craftModel in list)
                craftModel.State = craftStates.ContainsKey(craftModel.CraftDID) ? craftStates[craftModel.CraftDID] : 2;
            craftListResponse.Crafts = list;
            return craftListResponse;
        }

        public static ProductionLineProbablyGetResponse Execute(ProductionLineProbablyGetRequest request)
        {
            ProductionLineProbablyGetResponse probablyGetResponse = new ProductionLineProbablyGetResponse();
            ProductionLineProbablyInfo productionLineProbably = ServiceHelper.productionDB.GetProductionLineProbably(request.DID);
            if (productionLineProbably != null)
                probablyGetResponse.ProductionLineProbably = productionLineProbably.ToModel();
            return probablyGetResponse;
        }

        public static ProcessListResponse Execute(ProcessListRequest request)
        {
            ProcessListResponse processListResponse = new ProcessListResponse();
            IList<IFactory.Domain.Entities.ProcessInfo> processes = ServiceHelper.productionDB.GetProcesses(request.CraftDID);
            Dictionary<int, int> processStates = ServiceHelper.productionDB.GetProcessStates(request.CraftDID);
            List<ProcessModel> list = processes.Select(m => m.ToModel()).ToList<ProcessModel>();
            foreach (ProcessModel processModel in list)
            {
                processModel.State = processStates.ContainsKey(processModel.ProcessDID) ? processStates[processModel.ProcessDID] : 2;
            }
            processListResponse.Processes = list;
            return processListResponse;
        }

        public static CraftProbablyGetResponse Execute(CraftProbablyGetRequest request)
        {
            CraftProbablyGetResponse probablyGetResponse = new CraftProbablyGetResponse();
            CraftProbablyInfo craftProbably = ServiceHelper.productionDB.GetCraftProbably(request.CraftDID,request.code);
            if (craftProbably != null)
                probablyGetResponse.CraftProbably = craftProbably.ToModel();
            return probablyGetResponse;
        }

        public static CraftProbablyGetResponse Execute2(CraftProbablyGetRequest request)
        {
            CraftProbablyGetResponse probablyGetResponse = new CraftProbablyGetResponse();
            CraftProbablyInfo craftProbably = ServiceHelper.productionDB.GetCraftProbably(request.CraftDID,request.code);
            if (craftProbably != null)
                probablyGetResponse.CraftProbably = craftProbably.ToModel();
            return probablyGetResponse;
        }

        public static PLCStateListResponse Execute(PLCStateListRequest request)
        {
            PLCStateListResponse stateListResponse = new PLCStateListResponse();
            IList<PLCStateInfo> plcStates = ServiceHelper.productionDB.GetPLCStates(request.CraftDID);
            stateListResponse.PLCStates = plcStates.Select<PLCStateInfo, PLCStateModel>(m => m.ToModel()).ToList();
            return stateListResponse;
        }

        public static FacilityProductionDataListResponse Execute(FacilityProductionDataListRequest request)
        {
            FacilityProductionDataListResponse dataListResponse = new FacilityProductionDataListResponse();
            IPagedList<FacilityProductionDataInfo> pagedList = ServiceHelper.facilityProductionDataDB.GetPagedList(request.CraftNO, request.PageNumber, request.PageSize);
            List<FacilityProductionDataModel> productionDataModelList = new List<FacilityProductionDataModel>();
            int[] array1 = pagedList.Select(m => m.No).Distinct().ToArray();
            int[] array2 = pagedList.Select(m => m.Iden).Distinct().ToArray();
            Dictionary<int, Domain.Entities.ProcessInfo> dictionary1 = ServiceHelper.productionDB.GetProcesses(array1).ToDictionary(m => m.CraftDID);
            Dictionary<int, FacilityInfo> dictionary2 = ServiceHelper.productionDB.GetFacilities(array2).ToDictionary(m => m.FacilityDID);
            foreach (FacilityProductionDataInfo productionDataInfo in pagedList)
            {
                FacilityProductionDataModel productionDataModel = (FacilityProductionDataModel)Mapper.Map(productionDataInfo, FacilityProductionDataInfo.GetFacilityProductionDataType(request.CraftNO), FacilityProductionDataModel.GetFacilityProductionDataType(request.CraftNO));
                productionDataModel.DeviceGroupName = dictionary2.ContainsKey(productionDataInfo.No) ? dictionary2[productionDataInfo.Iden].MMName : null;
                productionDataModel.DeviceGroupName = dictionary1.ContainsKey(productionDataInfo.No) ? dictionary1[productionDataInfo.No].ProcessName : null;
                productionDataModelList.Add(productionDataModel);
            }
            dataListResponse.FacilityProductionDatas = new PagedData<FacilityProductionDataModel>(productionDataModelList, pagedList);
            return dataListResponse;
        }

        public static ProductionTypeListResponse Execute(ProductionTypeListRequest request)
        {
            ProductionTypeListResponse typeListResponse = new ProductionTypeListResponse();
            IList<ProductionTypeInfo> productionTypeInfos = ServiceHelper.productionDB.GetProductionTypeInfos(request.CraftDID);
            typeListResponse.ProductionTypes = productionTypeInfos.Select<ProductionTypeInfo, ProductionTypeModel>(m => m.ToModel()).ToList();
            return typeListResponse;
        }

        public static FacilityListResponse Execute(FacilityListRequest request)
        {
            FacilityListResponse facilityListResponse = new FacilityListResponse();
            IList<FacilityInfo> facilities = ServiceHelper.productionDB.GetFacilities(request.CraftDID);
            facilityListResponse.Facilities = facilities.Select<FacilityInfo, FacilityModel>(m => m.ToModel()).ToList();
            return facilityListResponse;
        }

        public static FacilityRunArgDateListResponse Execute(FacilityRunArgDateListRequest request)
        {
            FacilityRunArgDateListResponse dateListResponse = new FacilityRunArgDateListResponse();
            int[] array = ServiceHelper.productionDB.GetFacilities(request.CraftDID).Select(m => m.FacilityDID).ToArray<int>();
            dateListResponse.CollectDates = ServiceHelper.facilityRunArgDB.GetFacilityRunArgDateTimes(request.CraftNO, array, request.StartTime, request.EndTime);
            return dateListResponse;
        }

        public static UserGetResponse Execute(UserGetRequest request)
        {
            UserGetResponse userGetResponse = new UserGetResponse();
            UserInfo entity = ServiceHelper.userDB.GetUserByUserID(request.UserId);
            if (entity != null)
                userGetResponse.User = entity.ToModel();
            return userGetResponse;
        }

        public static ChangePasswordResponse Execute(ChangePasswordRequest request)
        {
            ChangePasswordResponse passwordResponse = new ChangePasswordResponse();
            UserInfo entity = ServiceHelper.userDB.GetUserByUserID(request.UserId);
            if (entity != null)
            {
                if (entity.Password == request.OldPassword)
                {
                    entity.Password = request.NewPassword;
                    ServiceHelper.userDB.UpdateUsers(entity);
                }
                else
                    passwordResponse.ErrMsg = "旧密码不正确";
            }
            return passwordResponse;
        }

        public static PermissionGetResponse Execute(PermissionGetRequest request)
        {
            PermissionGetResponse permissionGetResponse = new PermissionGetResponse();
            PermissionInfo permission = ServiceHelper.userDB.GetPermission(request.PermissionId);
            permissionGetResponse.Permission = permission.ToModel();
            return permissionGetResponse;
        }

        public static PermissionSaveResponse Execute(PermissionSaveRequest request)
        {
            PermissionSaveResponse permissionSaveResponse = new PermissionSaveResponse();
            if (request.PermissionId > 0)
            {
                PermissionInfo permission = ServiceHelper.userDB.GetPermission(request.PermissionId);
                permission.PermissionName = request.PermissonName;
                permission.Remark = request.Remark;
                ServiceHelper.userDB.UpdatePermissions(permission);
            }
            return permissionSaveResponse;
        }

        public static PermissionOrderResponse Execute(PermissionOrderRequest request)
        {
            PermissionOrderResponse permissionOrderResponse = new PermissionOrderResponse();
            PermissionInfo permission = ServiceHelper.userDB.GetPermission(request.PermissionId);
            List<PermissionInfo> list = ServiceHelper.userDB.GetPermissionsByParentId(permission.ParentId).ToList();
            permission = list.Where(m => m.PermissionId == request.PermissionId).First();
            int num = 0;
            for (int index = 0; index < list.Count; ++index)
            {
                PermissionInfo permissionInfo = list[index];
                permissionInfo.DisplayOrder = index + 1;
                if (ReferenceEquals(permissionInfo, permission))
                    num = index; 
            }
            if (request.Direction == PermissionOrderRequest.DirectionType.Up)
            {
                if (num > 0)
                {
                    ++list[num - 1].DisplayOrder;
                    --permission.DisplayOrder;
                }
            }
            else if (num + 1 < list.Count)
            {
                --list[num + 1].DisplayOrder;
                ++permission.DisplayOrder;
            }
            ServiceHelper.userDB.UpdatePermissions(list);
            return permissionOrderResponse;
        }

        public static PersonalInfoUpdateResponse Execute(PersonalInfoUpdateRequest request)
        {
            PersonalInfoUpdateResponse infoUpdateResponse = new PersonalInfoUpdateResponse();
            UserInfo entity = ServiceHelper.userDB.GetUserByUserID(request.UserId);
            entity.Name = request.Name;
            entity.Gender = request.Gender;
            ServiceHelper.userDB.UpdateUsers(entity);
            return infoUpdateResponse;
        }

        public static RoleSaveResponse Execute(RoleSaveRequest request)
        {
            RoleSaveResponse roleSaveResponse = new RoleSaveResponse();
            if (request.RoleId == 0)
            {
                RoleInfo entity = new RoleInfo();
                entity.RoleName = request.RoleName;
                entity.PermissionCodes = request.PermissionCodes;
                entity.CreateTime = DateTime.Now;
                entity.Remark = request.Remark;
                ServiceHelper.userDB.InsertRoles(entity);
                roleSaveResponse.RoleId = entity.RoleId;
            }
            else
            {
                RoleInfo entity = ServiceHelper.userDB.GetRoleByRoleId(request.RoleId);
                entity.RoleName = request.RoleName;
                entity.PermissionCodes = request.PermissionCodes;
                entity.Remark = request.Remark;
                ServiceHelper.userDB.UpdateRoles(entity);
                roleSaveResponse.RoleId = entity.RoleId;
            }
            return roleSaveResponse;
        }

        public static RoleGetResponse Execute(RoleGetRequest request)
        {
            RoleGetResponse roleGetResponse = new RoleGetResponse();
            RoleInfo entity = ServiceHelper.userDB.GetRoleByRoleId(request.RoleId);
            if (entity != null)
                roleGetResponse.Role = entity.ToModel();
            return roleGetResponse;
        }

        public static RoleDeleteResponse Execute(RoleDeleteRequest request)
        {
            RoleDeleteResponse roleDeleteResponse = new RoleDeleteResponse();
            ServiceHelper.userDB.DeleteRole(request.RoleId);
            return roleDeleteResponse;
        }

        public static RoleListResponse Execute(RoleListRequest request)
        {
            RoleListResponse roleListResponse = new RoleListResponse();
            IPagedList<RoleInfo> pagedRoles = ServiceHelper.roleDB.GetPagedRoles(request.PageNumber, request.PageSize);
            List<RoleModel> list = pagedRoles.Select<RoleInfo, RoleModel>((Func<RoleInfo, RoleModel>)(m => m.ToModel())).ToList<RoleModel>();
            roleListResponse.Roles = new PagedData<RoleModel>((IEnumerable<RoleModel>)list, (IPagedList)pagedRoles);
            return roleListResponse;
        }

        public static UserSaveResponse Execute(UserSaveRequest request)
        {
            UserSaveResponse userSaveResponse = new UserSaveResponse();
            if (request.UserId == 0)
            {
                UserInfo entity = new UserInfo();
                entity.Name = request.Name;
                entity.Password = request.Password;
                entity.RoleId = request.RoleId;
                entity.UserName = request.UserName;
                entity.Gender = request.Gender;
                entity.CreateTime = DateTime.Now;
                entity.CraftDIDs = request.CraftDIDs;
                ServiceHelper.userDB.InsertUsers(entity);
                userSaveResponse.UserId = entity.UserId;
            }
            else
            {
                UserInfo entity = ServiceHelper.userDB.GetUserByUserID(request.UserId);
                entity.Name = request.Name;
                entity.Password = request.Password;
                entity.RoleId = request.RoleId;
                entity.UserName = request.UserName;
                entity.Gender = request.Gender;
                entity.CraftDIDs = request.CraftDIDs;
                ServiceHelper.userDB.UpdateUsers(entity);
                userSaveResponse.UserId = entity.UserId;
            }
            return userSaveResponse;
        }

        public static UserSaveResponse ExecuteAve(UserSaveRequest request)
        {
            UserSaveResponse userSaveResponse = new UserSaveResponse();
                UserInfo entity = new UserInfo();
                entity.Size = request.Size;
            return userSaveResponse;
        }

        public static UserSaveResponse ExecuteInfo(UserSaveRequest request)
        {
            UserSaveResponse userSaveResponse = new UserSaveResponse();
            UserInfo entity = new UserInfo();
            entity.craftwork = request.craftwork;
            entity.process = request.process;
            entity.quarters = request.quarters;
            entity.segments = request.segments;
            entity.staffid = request.staffid;
            ServiceHelper.userDB.InsertUsersInfo(entity);
            userSaveResponse.UserId = entity.UserId;

            return userSaveResponse;
        }

        public static UserSaveResponse factoryInfo(UserSaveRequest request)
        {
            UserSaveResponse userSaveResponse = new UserSaveResponse();
            UserInfo entity = new UserInfo();
            entity.factoryID = request.factoryID;
            entity.fano = request.fano;
            entity.end_product_no = request.end_product_no;
            ServiceHelper.userDB.InsertfactoryInfo(entity);
            userSaveResponse.UserId = entity.UserId;

            return userSaveResponse;
        }

        public static UserDeleteResponse Execute(UserDeleteRequest request)
        {
            UserDeleteResponse userDeleteResponse = new UserDeleteResponse();
            ServiceHelper.userDB.DeleteUser(request.UserId);
            return userDeleteResponse;
        }

        public static UserListResponse Execute(UserListRequest request)
        {
            UserListResponse userListResponse = new UserListResponse();
            IPagedList<UserInfo> pagedUsers = ServiceHelper.userDB.GetPagedUsers(request.PageNumber, request.PageSize);
            IList<UserModel> userModelList = ServiceHelper.userDB.BuildUserModels(pagedUsers);
            userListResponse.Users = new PagedData<UserModel>(userModelList, pagedUsers);
            return userListResponse;
        }


        public static UnitSaveResponse Execute(UnitSaveRequest request)
        {
            UnitSaveResponse unitSaveResponse = new UnitSaveResponse();
            if (request.DID == 0)
            {
                UnitInfo unitInfo = new UnitInfo();
                unitInfo.UnitName = request.Name;
                unitInfo.UnitNO = request.NO;
                ServiceHelper.productionDB.AddUnit(unitInfo);
                unitSaveResponse.UnitDID = unitInfo.UnitDID;
            }
            else
            {
                UnitInfo unit = ServiceHelper.productionDB.GetUnit(request.DID);
                unit.UnitName = request.Name;
                unit.UnitNO = request.NO;
                ServiceHelper.productionDB.UpdateUnit(unit);
                unitSaveResponse.UnitDID = unit.UnitDID;
            }
            return unitSaveResponse;
        }

        public static UnitGetResponse Execute(UnitGetRequest request)
        {
            UnitGetResponse unitGetResponse = new UnitGetResponse();
            UnitInfo unit = ServiceHelper.productionDB.GetUnit(request.UnitDID);
            unitGetResponse.Unit = unit.ToModel();
            return unitGetResponse;
        }

        public static UnitDeleteResponse Execute(UnitDeleteRequest request)
        {
            UnitDeleteResponse unitDeleteResponse = new UnitDeleteResponse();
            ServiceHelper.productionDB.DeleteUnit(ServiceHelper.productionDB.GetUnit(request.UnitDID));
            return unitDeleteResponse;
        }

        public static UnitListResponse Execute(UnitListRequest request)
        {
            return new UnitListResponse()
            {
                Units = ServiceHelper.productionDB.GetAllUnits().Select<UnitInfo, UnitModel>(m => m.ToModel()).ToList<UnitModel>()
            };
        }

        public static CraftDetailSaveResponse Execute(CraftDetailSaveRequest request)
        {
            CraftDetailSaveResponse detailSaveResponse = new CraftDetailSaveResponse();
            ServiceHelper.productionDB.SaveCraftDetail(request.CraftDetail);
            return detailSaveResponse;
        }

        public static CraftDetailGetResponse Execute(CraftDetailGetRequest request)
        {
            return new CraftDetailGetResponse()
            {
                CraftDetail = ServiceHelper.productionDB.GetCraftDetail(request.CraftDID)
            };
        }

        public static CraftDetailListResponse Execute(CraftDetailListRequest request)
        {
            return new CraftDetailListResponse()
            {
                CraftDetails = ServiceHelper.productionDB.GetCraftDetails()
            };
        }

        public static KanbanSettingGetResponse Execute(KanbanSettingGetRequest request)
        {
            KanbanSettingGetResponse settingGetResponse = new KanbanSettingGetResponse();
            KanbanSettingInfo kanbanSetting = ServiceHelper.settingDB.GetKanbanSetting(1);
            settingGetResponse.KanbanSetting = kanbanSetting.ToModel();
            return settingGetResponse;
        }

        public static KanbanSettingSaveResponse Execute(KanbanSettingSaveRequest request)
        {
            KanbanSettingSaveResponse settingSaveResponse = new KanbanSettingSaveResponse();
            KanbanSettingInfo kanbanSetting = ServiceHelper.settingDB.GetKanbanSetting(request.KanbanSettingId);
            kanbanSetting.ExcellentRateReportTimeSection = request.ExcellentRateReportTimeSection;
            kanbanSetting.AlarmReportTimeSection = request.AlarmReportTimeSection;
            kanbanSetting.ProductionReportTimeSection = request.ProductionReportTimeSection;
            kanbanSetting.RefreshInterval = request.RefreshInterval;
            ServiceHelper.settingDB.SaveKanbanSetting(kanbanSetting);
            return settingSaveResponse;
        }

        public static ProductionLineProbablySaveResponse Execute(ProductionLineProbablySaveRequest request)
        {
            ProductionLineProbablySaveResponse probablySaveResponse = new ProductionLineProbablySaveResponse();
            ProductionLineProbablyInfo productionLineProbably = ServiceHelper.productionDB.GetProductionLineProbably(request.DID);
            productionLineProbably.Name = request.Name;
            productionLineProbably.TargetYield = request.TargetYield;
            ServiceHelper.productionDB.SaveProductionLineProbably(productionLineProbably);
            return probablySaveResponse;
        }

        public static AlarmTemporaryListResponse Execute(AlarmTemporaryListRequest request)
        {
            AlarmTemporaryListResponse temporaryListResponse = new AlarmTemporaryListResponse();
            IPagedList<AlarmTemporaryItem> alarmTemporaries = ServiceHelper.alarmDB.GetPagedAlarmTemporaries(request.ProcessDID, request.PageNumber, request.PageSize, request.CraftsDid);
            temporaryListResponse.AlarmTemporaries = alarmTemporaries.ToPagedData<AlarmTemporaryItem>();
            return temporaryListResponse;
        }

        public static ZhuiSuResponse Execute(ZhuiSuRequest request)
        {
            ZhuiSuResponse zhuiSuResponse = new ZhuiSuResponse();
            IPagedList<ZhuiSuItem> zhuisu = ServiceHelper.zhuisuDB.GetPagedZhuiSuData(request.ProcessDID, request.PageNumber, request.PageSize);
            zhuiSuResponse.ZhuiSus = zhuisu.ToPagedData<ZhuiSuItem>();
            return zhuiSuResponse;
        }

        public static ZhuiSuResponse Execute2(ZhuiSuRequest request)
        {
            ZhuiSuResponse zhuiSuResponse = new ZhuiSuResponse();
            IPagedList<ZhuiSuItem> zhuisu = ServiceHelper.zhuisuDB.GetPagedZhuiSuData2(request.ProcessDID, request.PageNumber, request.PageSize);
            zhuiSuResponse.ZhuiSus = zhuisu.ToPagedData<ZhuiSuItem>();
            return zhuiSuResponse;
        }

        public static Detail1Response ExecuteDetail1(Detail1Request request)
        {
            Detail1Response detail1Response = new Detail1Response();
            IPagedList<Detail1Item> detail = ServiceHelper.detail1DB.GetDetail1(request.ProcessDID, request.PageNumber, request.PageSize);
           detail1Response.Detail1s = detail.ToPagedData<Detail1Item>();
            return detail1Response;
        }

        public static Detail1Response ExecuteDetail12(Detail1Request request)
        {
            Detail1Response detail1Response = new Detail1Response();
            IPagedList<Detail1Item> detail = ServiceHelper.detail1DB.GetDetail12(request.ProcessDID, request.PageNumber, request.PageSize);
            detail1Response.Detail1s = detail.ToPagedData<Detail1Item>();
            return detail1Response;
        }

        public static ZhuiSuResponse ExecuteHistory(ZhuiSuRequest request)
        {
            ZhuiSuResponse zhuiSuResponse = new ZhuiSuResponse();
            IPagedList<ZhuiSuItem> zhuisu = ServiceHelper.zhuisuDB.GetPagedZhuiSuHistory(request.Keyword, request.TimeStart, request.TimeEnd, request.PageNumber, request.PageSize);
            zhuiSuResponse.ZhuiSus = zhuisu.ToPagedData<ZhuiSuItem>();
            return zhuiSuResponse;
        }

        public static ZhuiSuResponse ExecuteHistory2(ZhuiSuRequest request)
        {
            ZhuiSuResponse zhuiSuResponse = new ZhuiSuResponse();
            IPagedList<ZhuiSuItem> zhuisu = ServiceHelper.zhuisuDB.GetPagedZhuiSuHistory2(request.Keyword, request.TimeStart, request.TimeEnd, request.PageNumber, request.PageSize);
            zhuiSuResponse.ZhuiSus = zhuisu.ToPagedData<ZhuiSuItem>(); 
            return zhuiSuResponse;
        }        

        public static DataProductionResponse ExecuteDataHistory(DataProductionRequest request)
        {
            DataProductionResponse dataProductionResponse = new DataProductionResponse();
            IPagedList<DataProductionItem> dataproduction = ServiceHelper.dataproductionDB.GetPagedDataProductionHistory(request.Keyword, request.TimeStart, request.PageNumber, request.PageSize,request.code);
            dataProductionResponse.DataProductions = dataproduction.ToPagedData<DataProductionItem>();
            return dataProductionResponse;
        }

        public static DataProductionResponse ExecuteDataHistory2(DataProductionRequest request)
        {
            DataProductionResponse dataProductionResponse = new DataProductionResponse();
            IPagedList<DataProductionItem> dataproduction = ServiceHelper.dataproductionDB.GetPagedDataProductionHistory2(request.Keyword, request.TimeStart, request.TimeEnd, request.PageNumber, request.PageSize, request.code);
            dataProductionResponse.DataProductions = dataproduction.ToPagedData<DataProductionItem>();
            return dataProductionResponse;
        }

        public static DataProductionResponse ExecuteDataReal(DataProductionRequest request)
        {
            DataProductionResponse dataProductionResponse = new DataProductionResponse();
            IPagedList<DataProductionItem> dataproduction = ServiceHelper.dataproductionDB.GetPagedDataProductionReal(request.Keyword, request.TimeStart, request.TimeEnd, request.PageNumber, request.PageSize, request.code);
            dataProductionResponse.DataProductions = dataproduction.ToPagedData<DataProductionItem>();
            return dataProductionResponse;
        }

        public static DataProductionResponse ExecuteDataReal2(DataProductionRequest request)
        {
            DataProductionResponse dataProductionResponse = new DataProductionResponse();
            IPagedList<DataProductionItem> dataproduction = ServiceHelper.dataproductionDB.GetPagedDataProductionReal2(request.Keyword, request.TimeStart, request.TimeEnd, request.PageNumber, request.PageSize, request.code);
            dataProductionResponse.DataProductions = dataproduction.ToPagedData<DataProductionItem>();
            return dataProductionResponse;
        }

        public static ProductNGResponse Execute(ProductNGRequest request)
        {
            ProductNGResponse productNGResponse = new ProductNGResponse();
            IPagedList<ProductNGItem> productng = ServiceHelper.productngDB.GetPagedProductNGData(request.ProcessDID, request.PageNumber, request.PageSize);
            productNGResponse.productNGs = productng.ToPagedData<ProductNGItem>();
            return productNGResponse;
        }

        public static ProductNGResponse Execute2(ProductNGRequest request)
        {
            ProductNGResponse productNGResponse = new ProductNGResponse();
            IPagedList<ProductNGItem> productng = ServiceHelper.productngDB.GetPagedProductNGData2(request.ProcessDID, request.PageNumber, request.PageSize);
            productNGResponse.productNGs = productng.ToPagedData<ProductNGItem>();
            return productNGResponse;
        }

        public static ProductNGResponse ExecuteOK(ProductNGRequest request)
        {
            ProductNGResponse productNGResponse = new ProductNGResponse();
            IPagedList<ProductNGItem> productng = ServiceHelper.productngDB.GetPagedProductOKData(request.ProcessDID, request.PageNumber, request.PageSize);
            productNGResponse.productNGs = productng.ToPagedData<ProductNGItem>();
            return productNGResponse;
        }

        public static ProductNGResponse ExecuteOK2(ProductNGRequest request)
        {
            ProductNGResponse productNGResponse = new ProductNGResponse();
            IPagedList<ProductNGItem> productng = ServiceHelper.productngDB.GetPagedProductOKData2(request.ProcessDID, request.PageNumber, request.PageSize);
            productNGResponse.productNGs = productng.ToPagedData<ProductNGItem>();
            return productNGResponse;
        }

        public static AlarmCheckResponse AlarmExecute(AlarmCheckRequest request)
        {
            return new AlarmCheckResponse()
            {
                AlarmCheck = ServiceHelper.productionDB.GetAlarmCheck()
            };
        }

        public static DataPicResponse ExcutePicOK(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetDataOK(request.TimeStart, request.TimeStart, request.dates, request.datee)
            };
        }
        public static DataPicResponse ExcutePicOK2(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetDataOK2(request.TimeStart, request.TimeStart, request.dates, request.datee)
            };
        }

        public static DataPicResponse ExcutePicNG(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetDataNG(request.TimeStart, request.TimeStart, request.dates, request.datee)
            };
        }

        public static DataPicResponse ExcutePicNG2(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetDataNG2(request.TimeStart, request.TimeStart, request.dates, request.datee)
            };
        }
        public static DataPicResponse ExcutePicCapacity(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetDataCapacity(request.TimeStart, request.TimeStart, request.dates, request.datee)
            };
        }

        public static DataPicResponse ExcutePicPPM(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetPPM(request.TimeStart, request.TimeStart, request.dates, request.datee)
            };
        }

        public static DataPicResponse ExcutePicPPM2(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetPPM2(request.TimeStart, request.TimeStart, request.dates, request.datee)
            };
        }

        public static OneKeyResponse OneKey(OneKeyRequest request)
        {
            return new OneKeyResponse()
            {
                oneKeys = ServiceHelper.onekeyDB.GetPagedOneKeyData()
            };
        }

        public static OneKeyResponse OneKey2(OneKeyRequest request)
        {
            return new OneKeyResponse()
            {
                oneKeys = ServiceHelper.onekeyDB.GetPagedOneKeyData2()
            };
        }

        public static OneKeyResponse IsReady(OneKeyRequest request)
        {
            OneKeyResponse oneKeyResponse = new OneKeyResponse();
            OneKeyItem okItem = new OneKeyItem();           
            okItem.OneKey_flag = request.OneKey_flag;
            ServiceHelper.onekeyDB.IsReady_change(okItem);
            return oneKeyResponse;
        }

        public static OneKeyResponse IsReady2(OneKeyRequest request)
        {
            OneKeyResponse oneKeyResponse = new OneKeyResponse();
            OneKeyItem okItem = new OneKeyItem();
            okItem.OneKey_flag = request.OneKey_flag;
            ServiceHelper.onekeyDB.IsReady_change2(okItem);
            return oneKeyResponse;
        }


        public static DataPicResponse WorkCalendar(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetWorkDate()
            };
        }

        public static DataPicResponse WorkCalendar2(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetWorkDate2()
            };
        }

        public static DataPicResponse WorkDays(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetWorkDays()
            };
        }

        public static DataPicResponse WorkDays2(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetWorkDays2()
            };
        }

        public static DataPicResponse Workhours(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetWorkhours()
            };
        }

        public static DataPicResponse Workhours2(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetWorkhours2()
            };
        }

        public static AVEResponse ExcuteAVE(AVERequest request)
        {
            return new AVEResponse()
            {
                Aves = ServiceHelper.datapicDB.GetAVE(request.TimeStart, request.TimeStart,request.side,request.datee,request.type)
            };
        }

        public static AVEResponse ExcuteAVE2(AVERequest request)
        {
            return new AVEResponse()
            {
                Aves = ServiceHelper.datapicDB.GetAVE2(request.TimeStart, request.TimeStart, request.side, request.datee, request.type)
            };
        }

        public static AVEResponse ExcuteType(AVERequest request)
        {
            return new AVEResponse()
            {
                Aves = ServiceHelper.datapicDB.GetType(request.datee)
            };
        }

        public static AVEResponse ExcuteAlarm(AVERequest request)
        {
            return new AVEResponse() 
            {
                Aves = ServiceHelper.datapicDB.GetAlarm(request.TimeStart, request.TimeStart, request.side)
            };
        }

        public static DataPicResponse ExcutePicCapacity2(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetDataCapacity2(request.TimeStart, request.TimeStart, request.dates, request.datee)
            };
        }

        public static DataPicResponse ExcutePicQuality(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetDataQuality(request.TimeStart, request.TimeStart, request.dates, request.datee)
            };
        }

        public static DataPicResponse ExcutePicQuality2(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetDataQuality2(request.TimeStart, request.TimeStart, request.dates, request.datee)
            };
        }

        public static AlarmFieldSaveResponse Execute(AlarmFieldSaveRequest request)
        {
            AlarmFieldSaveResponse fieldSaveResponse = new AlarmFieldSaveResponse();
            ServiceHelper.alarmDB.AddAlarmField(request.FieldName, request.FieldDescription);
            return fieldSaveResponse;
        }

        public static AlarmFieldListResponse Execute(AlarmFieldListRequest request)
        {
            return new AlarmFieldListResponse()
            {
                AlarmFields = ServiceHelper.alarmDB.GetAllAlarmFields().Select(m => m.ToModel()).ToList()
            };
        }

        public static AlarmTypeListResponse Execute(AlarmTypeListRequest request)
        {
            return new AlarmTypeListResponse()
            {
                AlarmTypes = ServiceHelper.alarmDB.GetAlarmTypes().Select(m => m.ToModel()).ToList()
            };
        }

        /// <summary>
        /// 新增一行，插入AlarmRule
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static AlarmRuleSaveResponse Execute(AlarmRuleSaveRequest request)
        {
            AlarmRuleSaveResponse ruleSaveResponse = new AlarmRuleSaveResponse();
            if (string.IsNullOrEmpty(request.AlarmRuleDID))
            {
                AlarmRuleInfo alarmRuleInfo = new AlarmRuleInfo();
                //FacilityInfo facility = ServiceHelper.productionDB.GetFacilityByID(request.CraftDID);
                string CraftNO = ServiceHelper.productionDB.GetCraftsByID(request.CraftDID).CraftNO;
                alarmRuleInfo.RuleDID = ServiceHelper.alarmDB.GetNextAlarmRuleDID(CraftNO);
                //alarmRuleInfo.FacilityDID = facility.FacilityDID;
                alarmRuleInfo.CraftDID = request.CraftDID;
                alarmRuleInfo.UnitDID = request.UnitDID;
                alarmRuleInfo.AlarmContent = request.AlarmContent;
                alarmRuleInfo.AlarmReason = request.AlarmReason;
                alarmRuleInfo.Solution = new SolutionInfo()
                {
                    Content = request.SolutionText
                };
                alarmRuleInfo.AlarmTypeDID = request.AlarmTypeDID;
                string str1 = "Upload/SolutionImages/";
                string str2 = alarmRuleInfo.RuleDID + ".jpg";
                if (!Directory.Exists(Application.StartupPath + ("/" + str1)))
                    Directory.CreateDirectory(Application.StartupPath + ("/" + str1));
                ImageHelper.SaveImage(ImageHelper.BytesToImage(request.GetFileParameters()["SolutionImage"].GetContent()), Application.StartupPath + ("/" + str1 + str2));
                string str3 = "Upload/AlarmLocationImages/";
                string str4 = alarmRuleInfo.RuleDID + ".jpg";
                if (!Directory.Exists(Application.StartupPath + ("/" + str3)))
                    Directory.CreateDirectory(Application.StartupPath + ("/" + str3));
                ImageHelper.SaveImage(ImageHelper.BytesToImage(request.GetFileParameters()["AlarmLocationImage"].GetContent()), Application.StartupPath + ("/" + str3 + str4));
                alarmRuleInfo.AlarmLocationImage = new AlarmLocationImageInfo()
                {
                    Path = str3 + str4
                };
                alarmRuleInfo.SolutionImage = new SolutionImageInfo()
                {
                    Path = str1 + str2
                };
                ServiceHelper.alarmDB.AddAlarmRule(alarmRuleInfo);
                ServiceHelper.alarmDB.AddImagePath(alarmRuleInfo);
                ServiceHelper.alarmDB.SaveAlarmRuleFields(alarmRuleInfo.RuleDID, request.Fields);
                ruleSaveResponse.AlarmRuleDID = alarmRuleInfo.RuleDID;
            }
            return ruleSaveResponse;
        }

        public static AlarmRecordGetResponse GetAlarmRecord(AlarmRecordGetRequest request)
        {
            return new AlarmRecordGetResponse()
            {
                AlarmRecord = ServiceHelper.alarmDB.GetAlarmRecordModel(request.AlarmRecordDID)
            };
        }

        public static AlarmFacilityTopListResponse GetAlarmFacilityTopList(AlarmFacilityTopListRequest request)
        {
            return new AlarmFacilityTopListResponse()
            {
                AlarmFacilityTops = ServiceHelper.alarmDB.GetAlarmFacilityTops(request.AlarmDateStart, request.AlarmDateStart)
            };
        }

        public static AlarmFacilityTopListResponse GetAlarmFacilityTopList2(AlarmFacilityTopListRequest request)
        {
            return new AlarmFacilityTopListResponse()
            {
                AlarmFacilityTops = ServiceHelper.alarmDB.GetAlarmFacilityTops2(request.AlarmDateStart, request.AlarmDateStart)
            };
        }

        public static AlarmContentTopListResponse GetAlarmContentTopList(AlarmContentTopListRequest request)
        {
            return new AlarmContentTopListResponse()
            {
                AlarmContentTops = ServiceHelper.alarmDB.GetAlarmContentTops(request.AlarmDateStart, request.AlarmDateStart)
            };
        }

        public static AlarmContentTopListResponse GetAlarmContentTopList2(AlarmContentTopListRequest request)
        {
            return new AlarmContentTopListResponse()
            {
                AlarmContentTops = ServiceHelper.alarmDB.GetAlarmContentTops2(request.AlarmDateStart, request.AlarmDateStart)
            };
        }

        public static AlarmCraftTopListResponse GetAlarmCraftTopList(AlarmCraftTopListRequest request)
        {
            return new AlarmCraftTopListResponse()
            {
                AlarmCraftTops = ServiceHelper.alarmDB.GetAlarmCraftTops(request.AlarmDateStart, request.AlarmDateStart)
            };
        }

        public static AlarmCraftTopListResponse GetAlarmCraftTopList2(AlarmCraftTopListRequest request)
        {
            return new AlarmCraftTopListResponse()
            {
                AlarmCraftTops = ServiceHelper.alarmDB.GetAlarmCraftTops2(request.AlarmDateStart, request.AlarmDateStart)
            };
        }

        public static AlarmRecordListResponse GetAlarmRecordList(AlarmRecordListRequest request)
        {
            AlarmRecordListResponse recordListResponse = new AlarmRecordListResponse();
            IPagedList<AlarmRecordItem> pagedAlarmRecords = ServiceHelper.alarmDB.GetPagedAlarmRecords(request.Keyword, request.AlarmDateStart, request.AlarmDateEnd, request.PageNumber, request.PageSize, request.CraftsDid);
            recordListResponse.AlarmRecords = pagedAlarmRecords.ToPagedData<AlarmRecordItem>();
            return recordListResponse;
        }


        public static AlarmInfoListResponse GetAlarmInfoList(AlarmInfoListRequest request)
        {
            AlarmInfoListResponse infoListResponse = new AlarmInfoListResponse();
            AlarmInfoDB alarmInfoDB = new AlarmInfoDB();
            IPagedList<AlarmInfoModel> pagedAlarmInfo = alarmInfoDB.GetR_MCH_ALARM_DTL_T(request.Keyword, request.AlarmDateStart, request.AlarmDateEnd, request.PageNumber, request.PageSize, request.CraftsDid);
            infoListResponse.AlarmInfoModel = pagedAlarmInfo.ToPagedData<AlarmInfoModel>();
            return infoListResponse;
        }


        public static AlarmTemporaryGetResponse Execute(AlarmTemporaryGetRequest request)
        {
            return new AlarmTemporaryGetResponse()
            {
                AlarmTemporary = ServiceHelper.alarmDB.GetAlarmTemporaryModel(request.AlarmTemporaryDID)
            };
        }

        public static AlarmTemporaryUpdateHandledResponse Execute(AlarmTemporaryUpdateHandledRequest request)
        {
            AlarmTemporaryUpdateHandledResponse updateHandledResponse = new AlarmTemporaryUpdateHandledResponse();
            ServiceHelper.alarmDB.UpdateAlarmTemporaryHandled(request.AlarmTemporaryDID, request.HandlerId);
            return updateHandledResponse;
        }

        public static ArgumentResponse ExecuteArgument(ArgumentRequest request)
        {
            ArgumentResponse argumentResponse = new ArgumentResponse();
            IPagedList<ArgumentItem> argument = ServiceHelper.argumentDB.GetPagedArgumentData(request.ProcessDID, request.PageNumber, request.PageSize);
            argumentResponse.Augus = argument.ToPagedData<ArgumentItem>();
            return argumentResponse;
        }

        public static ArgumentResponse ExecuteArgument1(ArgumentRequest request)
        {
            ArgumentResponse argumentResponse = new ArgumentResponse();
            IPagedList<ArgumentItem> argument = ServiceHelper.argumentDB.GetPagedArgumentData1(request.ProcessDID, request.PageNumber, request.PageSize);
            argumentResponse.Augus = argument.ToPagedData<ArgumentItem>();
            return argumentResponse;
        }

        public static DataPicResponse ExcutePicAlarm(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetDataAlarm(request.TimeStart, request.TimeStart, request.dates, request.datee)
            };
        }

        public static DataPicResponse ExcutePicAlarm2(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetDataAlarm2(request.TimeStart, request.TimeStart, request.dates, request.datee)
            };
        }

        public static DataPicResponse ExcutePicAlarmA(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetDataAlarmA(request.TimeStart, request.TimeStart, request.dates, request.datee)
            };
        }

        public static DataPicResponse ExcutePicAlarmA2(DataPicRequest request)
        {
            return new DataPicResponse()
            {
                DataPics = ServiceHelper.datapicDB.GetDataAlarmA2(request.TimeStart, request.TimeStart, request.dates, request.datee)
            };
        }

        public static DataProductionResponse ExecuteVulnerable(DataProductionRequest request)
        {
            DataProductionResponse dataProductionResponse = new DataProductionResponse();
            IPagedList<DataVulnerableItem> dataproduction = ServiceHelper.dataproductionDB.GetPagedDataVulnerable(request.Keyword, request.TimeStart, request.TimeEnd, request.PageNumber, request.PageSize, request.code);
            dataProductionResponse.DataVulnerables = dataproduction.ToPagedData<DataVulnerableItem>();
            return dataProductionResponse;
        }

        public static DataProductionResponse VulnerableExecute(DataProductionRequest request)
        {
            return new DataProductionResponse()
            {
                CheckVulnerables = ServiceHelper.dataproductionDB.GetDataVulnerable()
            };
        }

        //   产品参数保存
        public static ProductParamSaveResponse Execute(ProductParamSaveRequest request)
        {
            ProductParamSaveResponse productParamSaveResponse = new ProductParamSaveResponse();
            if ( true == request.Insert )
            {
                ProductParamInfo entity = new ProductParamInfo();
                entity.ITEM_CD = request.ITEM_CD;
                entity.ITEM_NM = request.ITEM_NM;
                entity.ITEM_DESC = request.ITEM_DESC;
                entity.MODEL_CD = request.MODEL_CD;
                entity.MODEL_NM = request.MODEL_NM;
                entity.ITEM_COLOR = request.ITEM_COLOR;
                entity.ITEM_LONG = request.ITEM_LONG;
                entity.ITEM_WIDE = request.ITEM_WIDE;
                entity.LIGHT_BRIGHT = request.LIGHT_BRIGHT;
                entity.QTY_FOR_CRIB = request.QTY_FOR_CRIB;
                entity.QTY_FOR_TARY = request.QTY_FOR_TARY;
                entity.MO = request.MO;
                entity.CRT_ID = request.CRT_ID;
                entity.CRT_DT = DateTime.Now;

                ServiceHelper.SystemParamDB.InsertProductParamsInfo(entity);
                productParamSaveResponse.ITEM_CD = entity.ITEM_CD;
            }
            else
            {
                ProductParamInfo entity = new ProductParamInfo();
                entity.ITEM_CD = request.ITEM_CD;
                entity.ITEM_NM = request.ITEM_NM;
                entity.ITEM_DESC = request.ITEM_DESC;
                entity.MODEL_CD = request.MODEL_CD;
                entity.MODEL_NM = request.MODEL_NM;
                entity.ITEM_COLOR = request.ITEM_COLOR;
                entity.ITEM_LONG = request.ITEM_LONG;
                entity.ITEM_WIDE = request.ITEM_WIDE;
                entity.LIGHT_BRIGHT = request.LIGHT_BRIGHT;
                entity.QTY_FOR_CRIB = request.QTY_FOR_CRIB;
                entity.QTY_FOR_TARY = request.QTY_FOR_TARY;
                entity.MO = request.MO;
                ServiceHelper.SystemParamDB.UpdateProductParams(entity);
                productParamSaveResponse.ITEM_CD = entity.ITEM_CD;
            }
            return productParamSaveResponse;
        }

        public static ProductParamSaveResponse ExecuteAve(ProductParamSaveRequest request)
        {
            ProductParamSaveResponse productParamSaveResponse = new ProductParamSaveResponse();
            UserInfo entity = new UserInfo();
            entity.Size = request.Size;
            return productParamSaveResponse;
        }

        public static ProductParamDeleteResponse Execute(ProductParamDeleteRequest request)
        {
            ProductParamDeleteResponse productParamDeleteResponse = new ProductParamDeleteResponse();
            ServiceHelper.SystemParamDB.DeleteProductParam(request.ITEM_CD);
            return productParamDeleteResponse;
        }

        public static ProductParamListResponse Execute(ProductParamListRequest request)
        {
            ProductParamListResponse productParamListResponse = new ProductParamListResponse();
            IPagedList<ProductParamInfo> pagedUsers = ServiceHelper.SystemParamDB.GetPagedProductParams(request.PageNumber, request.PageSize, request.ITEM_CD);
            IList<ProductParamModel> userModelList = ServiceHelper.SystemParamDB.BuildProductParamModels(pagedUsers);
            productParamListResponse.ProductParams = new PagedData<ProductParamModel>(userModelList, pagedUsers);
            return productParamListResponse;
        }

        // -------------------------------------------------------------------------------------系统参数设定

        //   产品参数保存
        public static SystemParamSaveResponse Execute(SystemParamSaveRequest request)
        {
            SystemParamSaveResponse systemParamSaveResponse = new SystemParamSaveResponse();
            if (true == request.Insert)
            {
                SystemParamInfo entity = new SystemParamInfo();
                entity.IDX = request.IDX;
                entity.ITEM_CD = request.ITEM_CD;
                entity.SLOT_TY = request.SLOT_TY;
                entity.SLOT_SITE = request.SLOT_SITE;
                entity.SLOT_X_DOT = request.SLOT_X_DOT;
                entity.SLOT_Y_DOT = request.SLOT_Y_DOT;
                entity.SLOT_Z_DOT = request.SLOT_Z_DOT;
                entity.SLOT_U_DOT = request.SLOT_U_DOT;
                entity.LIGHT_1 = request.LIGHT_1;
                entity.LIGHT_2 = request.LIGHT_2;
                entity.LIGHT_3 = request.LIGHT_3;
                entity.LIGHT_4 = request.LIGHT_4;
            
                entity.MO = request.MO;
                entity.CRT_ID = request.CRT_ID;
                entity.CRT_DT = DateTime.Now;

                ServiceHelper.SystemParamDB.InsertSystemParamsInfo(entity);
                systemParamSaveResponse.ITEM_CD = entity.ITEM_CD;
            }
            else
            {
                SystemParamInfo entity = new SystemParamInfo();
                entity.IDX = request.IDX;
                entity.ITEM_CD = request.ITEM_CD;
                entity.SLOT_TY = request.SLOT_TY;
                entity.SLOT_SITE = request.SLOT_SITE;
                entity.SLOT_X_DOT = request.SLOT_X_DOT;
                entity.SLOT_Y_DOT = request.SLOT_Y_DOT;
                entity.SLOT_Z_DOT = request.SLOT_Z_DOT;
                entity.SLOT_U_DOT = request.SLOT_U_DOT;
                entity.LIGHT_1 = request.LIGHT_1;
                entity.LIGHT_2 = request.LIGHT_2;
                entity.LIGHT_3 = request.LIGHT_3;
                entity.LIGHT_4 = request.LIGHT_4;

                entity.MO = request.MO;
                entity.CRT_ID = request.CRT_ID;
                entity.CRT_DT = DateTime.Now;

                ServiceHelper.SystemParamDB.UpdateSystemParams(entity);
                systemParamSaveResponse.ITEM_CD = entity.ITEM_CD;
            }
            return systemParamSaveResponse;
        }

        public static SystemParamSaveResponse ExecuteAve(SystemParamSaveRequest request)
        {
            SystemParamSaveResponse systemParamSaveResponse = new SystemParamSaveResponse();
            SystemParamInfo entity = new SystemParamInfo();
            entity.Size = request.Size;
            return systemParamSaveResponse;
        }

        public static SystemParamSaveResponse ExecuteInfo(SystemParamSaveRequest request)
        {
            SystemParamSaveResponse systemParamSaveResponse = new SystemParamSaveResponse();
            SystemParamInfo entity = new SystemParamInfo();
            entity.IDX = request.IDX;
            entity.ITEM_CD = request.ITEM_CD;
            entity.SLOT_TY = request.SLOT_TY;
            entity.SLOT_SITE = request.SLOT_SITE;
            entity.SLOT_X_DOT = request.SLOT_X_DOT;
            entity.SLOT_Y_DOT = request.SLOT_Y_DOT;
            entity.SLOT_Z_DOT = request.SLOT_Z_DOT;
            entity.SLOT_U_DOT = request.SLOT_U_DOT;

            entity.LIGHT_1 = request.LIGHT_1;
            entity.LIGHT_2 = request.LIGHT_2;
            entity.LIGHT_3 = request.LIGHT_3;
            entity.LIGHT_4 = request.LIGHT_4;
            

            entity.MO = request.MO;
            entity.CRT_ID = request.CRT_ID;
            entity.CRT_DT = DateTime.Now;
            ServiceHelper.SystemParamDB.InsertSystemParamsInfo(entity);
            systemParamSaveResponse.ITEM_CD = entity.ITEM_CD;

            return systemParamSaveResponse;
        }


        public static SystemParamDeleteResponse Execute(SystemParamDeleteRequest request)
        {
            SystemParamDeleteResponse productParamDeleteResponse = new SystemParamDeleteResponse();
            ServiceHelper.SystemParamDB.DeleteProductParam(request.ITEM_CD,request.SLOT_SITE);
            return productParamDeleteResponse;
        }

        public static List<AllUsefullProductModel> GetAllPara(string product)
        {
            return ServiceHelper.SystemParamDB.GetProductByName(product);
        }

        public static SystemParamListResponse Execute(SystemParamListRequest request)
        {
            SystemParamListResponse systemParamListResponse = new SystemParamListResponse();
            IPagedList<SystemParamInfo> pagedUsers = ServiceHelper.SystemParamDB.GetPagedSystemParams(request.PageNumber, request.PageSize, request.ITEM_CD);
            IList<SystemParamModel> userModelList = ServiceHelper.SystemParamDB.BuildSystemParamModels(pagedUsers);
            systemParamListResponse.SystemParams = new PagedData<SystemParamModel>(userModelList, pagedUsers);
            return systemParamListResponse;
        }

        public static void InsertAlarmExecute(string AlarmContent)
        {
            ServiceHelper.myDB.InsertAlarm(AlarmContent);
        }
        public static void InsertCapacityExecute(int capacity, int ok, double rate)
        {
            ServiceHelper.myDB.InsertCapacityOfProduction(capacity, ok, rate);
        }
        public static void InsertProductionDataExecute(string barcode)
        {
            ServiceHelper.myDB.InsertProductionData(barcode);
        }

        public static void CreateAlarmExecute()
        {
            ServiceHelper.myCDB.CreateAlarmTable();
        }

        public static void CreateCapacityOfProductionExecute()
        {
            ServiceHelper.myCDB.CreateCapacityOfProductinoTable();
        }

        public static void CreateProductionDataExecute()
        {
            ServiceHelper.myCDB.CreateLoadUnloadProductionTable();
        }
    }
}
