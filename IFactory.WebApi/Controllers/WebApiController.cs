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
using IFactory.Platform.Core;
using IFactory.Platform.Common;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.Alarm;
using IFactory.Platform.Common.Request.Crafts;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Request.Report;
using IFactory.Platform.Common.Request.Setting;
using IFactory.Platform.Common.Request.User;
using IFactory.Platform.Common.Response.Alarm;
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
using System.Web;
using System.Web.Mvc;

namespace IFactory.Platform.Controllers
{
    public class WebApiController : Controller
    {
        protected IWebApiContext Context { get; set; }

        public WebApiController(IWebApiContext context)
        {
            this.Context = context;
        }

        [ApiMethod]
        public AlarmContentTopListResponse GetAlarmContentTopList(AlarmContentTopListRequest request)
        {
            return new AlarmContentTopListResponse()
            {
                AlarmContentTops = ServiceHelper.LoadService<IAlarmService>().GetAlarmContentTops(request.AlarmDateStart, request.AlarmDateStart)
            };
        }

        [ApiMethod]
        public AlarmCraftTopListResponse GetAlarmCraftTopList(AlarmCraftTopListRequest request)
        {
            return new AlarmCraftTopListResponse()
            {
                AlarmCraftTops = ServiceHelper.LoadService<IAlarmService>().GetAlarmCraftTops(request.AlarmDateStart, request.AlarmDateStart)
            };
        }

        [ApiMethod]
        public AlarmFacilityTopListResponse GetAlarmCraftTopList(AlarmFacilityTopListRequest request)
        {
            return new AlarmFacilityTopListResponse()
            {
                AlarmFacilityTops = ServiceHelper.LoadService<IAlarmService>().GetAlarmFacilityTops(request.AlarmDateStart, request.AlarmDateStart)
            };
        }

        [ApiMethod]
        public AlarmRecordGetResponse GetAlarmRecord(AlarmRecordGetRequest request)
        {
            return new AlarmRecordGetResponse()
            {
                AlarmRecord = ServiceHelper.LoadService<IAlarmService>().GetAlarmRecordModel(request.AlarmRecordDID)
            };
        }

        [ApiMethod]
        public AlarmRecordListResponse GetAlarmRecordList(AlarmRecordListRequest request)
        {
            AlarmRecordListResponse recordListResponse = new AlarmRecordListResponse();
            IPagedList<AlarmRecordItem> pagedAlarmRecords = ServiceHelper.LoadService<IAlarmService>().GetPagedAlarmRecords(request.Keyword, request.AlarmDateStart, request.AlarmDateEnd, request.PageNumber, request.PageSize);
            recordListResponse.AlarmRecords = pagedAlarmRecords.ToPagedData<AlarmRecordItem>();
            return recordListResponse;
        }

        [ApiMethod]
        public AlarmRuleSaveResponse Execute(AlarmRuleSaveRequest request)
        {
            AlarmRuleSaveResponse ruleSaveResponse = new AlarmRuleSaveResponse();
            if (string.IsNullOrEmpty(request.AlarmRuleDID))
            {
                AlarmRuleInfo alarmRuleInfo = new AlarmRuleInfo();
                FacilityInfo facility = ServiceHelper.LoadService<IProductionService>().GetFacility(request.CraftDID);
                alarmRuleInfo.RuleDID = ServiceHelper.LoadService<IAlarmService>().GetNextAlarmRuleDID(facility.Process.Craft.CraftNO);
                alarmRuleInfo.FacilityDID = facility.FacilityDID;
                alarmRuleInfo.UnitDID = facility.ProcessDID;
                alarmRuleInfo.CraftDID = facility.Process.CraftDID;
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
                if (!Directory.Exists(this.Server.MapPath("~/" + str1)))
                    Directory.CreateDirectory(this.Server.MapPath("~/" + str1));
                ImageHelper.SaveImage(ImageHelper.BytesToImage(request.GetFileParameters()["SolutionImage"].GetContent()), this.Server.MapPath("~/" + str1 + str2));
                string str3 = "Upload/AlarmLocationImages/";
                string str4 = alarmRuleInfo.RuleDID + ".jpg";
                if (!Directory.Exists(this.Server.MapPath("~/" + str3)))
                    Directory.CreateDirectory(this.Server.MapPath("~/" + str3));
                ImageHelper.SaveImage(ImageHelper.BytesToImage(request.GetFileParameters()["AlarmLocationImage"].GetContent()), this.Server.MapPath("~/" + str3 + str4));
                alarmRuleInfo.AlarmLocationImage = new AlarmLocationImageInfo()
                {
                    Path = str3 + str4
                };
                alarmRuleInfo.SolutionImage = new SolutionImageInfo()
                {
                    Path = str1 + str2
                };
                ServiceHelper.LoadService<IAlarmService>().AddAlarmRule(alarmRuleInfo);
                ServiceHelper.LoadService<IAlarmService>().SaveAlarmRuleFields(alarmRuleInfo.RuleDID, request.Fields);
                ruleSaveResponse.AlarmRuleDID = alarmRuleInfo.RuleDID;
            }
            return ruleSaveResponse;
        }

        [ApiMethod]
        public AlarmFieldSaveResponse Execute(AlarmFieldSaveRequest request)
        {
            AlarmFieldSaveResponse fieldSaveResponse = new AlarmFieldSaveResponse();
            if (request.AlarmFieldId == 0)
                ServiceHelper.LoadService<IAlarmService>().AddAlarmField(request.FieldName, request.FieldDescription);
            return fieldSaveResponse;
        }

        [ApiMethod]
        public AlarmTemporaryGetResponse Execute(AlarmTemporaryGetRequest request)
        {
            return new AlarmTemporaryGetResponse()
            {
                AlarmTemporary = ServiceHelper.LoadService<IAlarmService>().GetAlarmTemporaryModel(request.AlarmTemporaryDID)
            };
        }

        [ApiMethod]
        public AlarmTemporaryListResponse Execute(AlarmTemporaryListRequest request)
        {
            AlarmTemporaryListResponse temporaryListResponse = new AlarmTemporaryListResponse();
            IPagedList<AlarmTemporaryItem> alarmTemporaries = ServiceHelper.LoadService<IAlarmService>().GetPagedAlarmTemporaries(request.ProcessDID, request.PageNumber, request.PageSize);
            temporaryListResponse.AlarmTemporaries = alarmTemporaries.ToPagedData<AlarmTemporaryItem>();
            return temporaryListResponse;
        }

        [ApiMethod]
        public AlarmFieldListResponse Execute(AlarmFieldListRequest request)
        {
            return new AlarmFieldListResponse()
            {
                AlarmFields = ServiceHelper.LoadService<IAlarmService>().GetAllAlarmFields().Select(m => m.ToModel()).ToList()
            };
        }

        [ApiMethod]
        public AlarmTypeListResponse Execute(AlarmTypeListRequest request)
        {
            return new AlarmTypeListResponse()
            {
                AlarmTypes = ServiceHelper.LoadService<IAlarmService>().GetAlarmTypes().Select(m => m.ToModel()).ToList()
            };
        }

        [ApiMethod]
        public AlarmTemporaryUpdateHandledResponse Execute(AlarmTemporaryUpdateHandledRequest request)
        {
            AlarmTemporaryUpdateHandledResponse updateHandledResponse = new AlarmTemporaryUpdateHandledResponse();
            ServiceHelper.LoadService<IAlarmService>().UpdateAlarmTemporaryHandled(request.AlarmTemporaryDID, request.HandlerId);
            return updateHandledResponse;
        }

        [ApiMethod]
        public FacilityProductionDataListResponse Execute(FacilityProductionDataListRequest request)
        {
            FacilityProductionDataListResponse dataListResponse = new FacilityProductionDataListResponse();
            IPagedList<FacilityProductionDataInfo> pagedList = ServiceHelper.LoadService<IFacilityProductionDataService>().GetPagedList(request.PageNumber, request.PageSize);
            List<FacilityProductionDataModel> productionDataModelList = new List<FacilityProductionDataModel>();
            int[] array1 = pagedList.Select(m => m.No).Distinct().ToArray();
            int[] array2 = pagedList.Select(m => m.Iden).Distinct().ToArray();
            Dictionary<int, Domain.Entities.ProcessInfo> dictionary1 = ServiceHelper.LoadService<IProductionService>().GetProcesses(array1).ToDictionary(m => m.CraftDID);
            Dictionary<int, FacilityInfo> dictionary2 = ServiceHelper.LoadService<IProductionService>().GetFacilities(array2).ToDictionary(m => m.FacilityDID);
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

        [ApiMethod]
        public FacilityRunArgDateListResponse Execute(FacilityRunArgDateListRequest request)
        {
            FacilityRunArgDateListResponse dateListResponse = new FacilityRunArgDateListResponse();
            int[] array = ServiceHelper.LoadService<IProductionService>().GetFacilities(request.CraftDID).Select(m => m.FacilityDID).ToArray<int>();
            dateListResponse.CollectDates = (IList<DateTime>)ServiceHelper.LoadService<IFacilityRunArgService>().GetFacilityRunArgDateTimes(array, request.StartTime, request.EndTime);
            return dateListResponse;
        }

        [ApiMethod]
        public FacilityRunArgSumResponse Execute(FacilityRunArgSumRequest request)
        {
            FacilityRunArgSumResponse runArgSumResponse = new FacilityRunArgSumResponse();
            int[] array = ServiceHelper.LoadService<IProductionService>().GetFacilities(request.CraftDID).Select(m => m.FacilityDID).ToArray();
            runArgSumResponse.FacilityRunArgSum = ServiceHelper.LoadService<IFacilityRunArgService>().Sum(array, request.CollectDate);
            return runArgSumResponse;
        }

        [HttpGet]
        public ActionResult Index(string _r)
        {
            return (ActionResult)this.Content("ok");
        }

        [HttpPost]
        public ActionResult Index()
        {
            IResponse response;
            try
            {
                string appKey = null;
                string key = null;
                string str = null;
                string strB = null;
                string accessToken = null;
                DateTime dateTime = DateTime.MinValue;
                NameValueCollection form = this.Request.Form;
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (string allKey in form.AllKeys)
                {
                    string s = form[allKey];
                    switch (allKey)
                    {
                        case "app_key":
                            appKey = s;
                            break;
                        case "param_json":
                            str = s;
                            break;
                        case "method":
                            key = s;
                            break;
                        case "timestamp":
                            long result;
                            if (long.TryParse(s, out result))
                            {
                                dateTime = new DateTime(1970, 1, 1).ToLocalTime().AddMilliseconds((double)result);
                                break;
                            }
                            break;
                        case "sign":
                            strB = s;
                            break;
                        case "access_token":
                            accessToken = s;
                            break;
                    }
                    if (allKey != "sign")
                        dictionary.Add(allKey, s);
                }
                if (dateTime < DateTime.Now.AddMinutes(-15.0))
                {
                    response = new BaseResponse()
                    {
                        ErrCode = "005"
                    };
                }
                else
                {
                    AppInfo app = ServiceHelper.LoadService<IAppService>().GetApp(appKey);
                    if (app != null)
                    {
                        if (string.Compare(WebApiUtils.SignYrqRequest(dictionary, app.AppSecret), strB, true) == 0)
                        {
                            Dictionary<string, ApiMethodInfo> apiMethods = Util.Utils.GetApiMethods();
                            if (apiMethods.ContainsKey(key))
                            {
                                ApiMethodInfo apiMethodInfo = apiMethods[key];
                                IRequest<IResponse> request = (IRequest<IResponse>)JsonConvert.DeserializeObject(str ?? "{}", apiMethodInfo.RequestType, WebApiUtils.GetJsonConverters());
                                request.Validate();
                                this.Context.AppId = app.AppId;
                                if (request is ICraftsReqeust)
                                    ServiceHelper.LoadService<ICraftDbFactory>().CraftNO = ((ICraftsReqeust)request).CraftNO;
                                if (request is IUploadRequest)
                                {
                                    IUploadRequest uploadRequest = (IUploadRequest)request;
                                    IDictionary<string, FileItem> fileParameters = new Dictionary<string, FileItem>();
                                    foreach (string allKey in this.Request.Files.AllKeys)
                                    {
                                        HttpPostedFileBase httpPostedFileBase = this.Request.Files[allKey];
                                        byte[] numArray = new byte[httpPostedFileBase.InputStream.Length];
                                        httpPostedFileBase.InputStream.Read(numArray, 0, numArray.Length);
                                        fileParameters.Add(allKey, new FileItem(httpPostedFileBase.FileName, numArray));
                                        httpPostedFileBase.InputStream.Dispose();
                                    }
                                    uploadRequest.SetFileParamaters(fileParameters);
                                }
                                if (apiMethodInfo.IsCheckSession)
                                {
                                    AuthInfo auth = ServiceHelper.LoadService<IAuthService>().GetAuth(accessToken);
                                    if (auth != null && auth.AppId == this.Context.AppId)
                                    {
                                        this.Context.AuthId = auth.AppId;
                                        this.Context.UserId = auth.UserId;
                                        response = (IResponse)apiMethodInfo.Method.Invoke(this, new object[1]
                                        {
                       request
                                        });
                                    }
                                    else
                                        response = new BaseResponse()
                                        {
                                            ErrCode = "004"
                                        };
                                }
                                else
                                    response = (IResponse)apiMethodInfo.Method.Invoke(this, new object[1]
                                    {
                     request
                                    });
                            }
                            else
                                response = new BaseResponse()
                                {
                                    ErrCode = "003"
                                };
                        }
                        else
                            response = new BaseResponse()
                            {
                                ErrCode = "002"
                            };
                    }
                    else
                        response = new BaseResponse()
                        {
                            ErrCode = "008"
                        };
                }
            }
            catch (TargetInvocationException ex)
            {
                LogUtil.LogError("处理请求异常", ex.GetBaseException());
                response = new BaseResponse()
                {
                    ErrCode = "001",
                    ErrMsg = ex.GetBaseException().Message
                };
            }
            catch (Exception ex)
            {
                LogUtil.LogError("处理请求异常", ex);
                response = new BaseResponse()
                {
                    ErrCode = "001",
                    ErrMsg = ex.Message
                };
            }
            return Content(JsonConvert.SerializeObject(response, WebApiUtils.GetJsonConverters()));
        }

        [ApiMethod]
        public CraftListResponse GetCraftList(CraftListRequest request)
        {
            CraftListResponse craftListResponse = new CraftListResponse();
            List<CraftModel> list = ServiceHelper.LoadService<IProductionService>().GetCrafts().Select<CraftInfo, CraftModel>(m => m.ToModel()).ToList<CraftModel>();
            Dictionary<int, int> craftStates = ServiceHelper.LoadService<IProductionService>().GetCraftStates();
            foreach (CraftModel craftModel in list)
                craftModel.State = craftStates.ContainsKey(craftModel.CraftDID) ? craftStates[craftModel.CraftDID] : 2;
            craftListResponse.Crafts = (IList<CraftModel>)list;
            return craftListResponse;
        }

        [ApiMethod]
        public CraftProbablyGetResponse Execute(CraftProbablyGetRequest request)
        {
            CraftProbablyGetResponse probablyGetResponse = new CraftProbablyGetResponse();
            CraftProbablyInfo craftProbably = ServiceHelper.LoadService<IProductionService>().GetCraftProbably(request.CraftDID);
            if (craftProbably != null)
                probablyGetResponse.CraftProbably = craftProbably.ToModel();
            return probablyGetResponse;
        }

        [ApiMethod]
        public ProcessListResponse Execute(ProcessListRequest request)
        {
            ProcessListResponse processListResponse = new ProcessListResponse();
            IList<IFactory.Domain.Entities.ProcessInfo> processes = ServiceHelper.LoadService<IProductionService>().GetProcesses(request.CraftDID);
            Dictionary<int, int> processStates = ServiceHelper.LoadService<IProductionService>().GetProcessStates(request.CraftDID);
            List<ProcessModel> list = processes.Select(m => m.ToModel()).ToList<ProcessModel>();
            foreach (ProcessModel processModel in list)
                processModel.State = processStates.ContainsKey(processModel.ProcessDID) ? processStates[processModel.ProcessDID] : 2;
            processListResponse.Processes = list;
            return processListResponse;
        }

        [ApiMethod]
        public PLCStateListResponse Execute(PLCStateListRequest request)
        {
            PLCStateListResponse stateListResponse = new PLCStateListResponse();
            IList<PLCStateInfo> plcStates = ServiceHelper.LoadService<IProductionService>().GetPLCStates(request.CraftDID);
            stateListResponse.PLCStates = (IList<PLCStateModel>)plcStates.Select<PLCStateInfo, PLCStateModel>((Func<PLCStateInfo, PLCStateModel>)(m => m.ToModel())).ToList<PLCStateModel>();
            return stateListResponse;
        }

        [ApiMethod]
        public ProductionLineProbablyGetResponse Execute(ProductionLineProbablyGetRequest request)
        {
            ProductionLineProbablyGetResponse probablyGetResponse = new ProductionLineProbablyGetResponse();
            ProductionLineProbablyInfo productionLineProbably = ServiceHelper.LoadService<IProductionService>().GetProductionLineProbably(request.DID);
            if (productionLineProbably != null)
                probablyGetResponse.ProductionLineProbably = productionLineProbably.ToModel();
            return probablyGetResponse;
        }

        [ApiMethod]
        public ProductionTypeGetResponse Execute(ProductionTypeGetRequest request)
        {
            ProductionTypeGetResponse productionTypeGetResponse = new ProductionTypeGetResponse();
            ProductionTypeInfo productionType = ServiceHelper.LoadService<IProductionService>().GetProductionType(request.DID);
            if (productionType != null)
                productionTypeGetResponse.ProductionType = productionType.ToModel();
            return productionTypeGetResponse;
        }

        [ApiMethod]
        public UnitListResponse Execute(UnitListRequest request)
        {
            return new UnitListResponse()
            {
                Units = (IList<UnitModel>)ServiceHelper.LoadService<IProductionService>().GetAllUnits().Select<UnitInfo, UnitModel>((Func<UnitInfo, UnitModel>)(m => m.ToModel())).ToList<UnitModel>()
            };
        }

        [ApiMethod]
        public FacilityListResponse Execute(FacilityListRequest request)
        {
            FacilityListResponse facilityListResponse = new FacilityListResponse();
            IList<FacilityInfo> facilities = ServiceHelper.LoadService<IProductionService>().GetFacilities(request.CraftDID);
            facilityListResponse.Facilities = (IList<FacilityModel>)facilities.Select<FacilityInfo, FacilityModel>((Func<FacilityInfo, FacilityModel>)(m => m.ToModel())).ToList<FacilityModel>();
            return facilityListResponse;
        }

        [ApiMethod]
        public ProductionTypeListResponse Execute(ProductionTypeListRequest request)
        {
            ProductionTypeListResponse typeListResponse = new ProductionTypeListResponse();
            IList<ProductionTypeInfo> productionTypeInfos = ServiceHelper.LoadService<IProductionService>().GetProductionTypeInfos(request.CraftDID);
            typeListResponse.ProductionTypes = (IList<ProductionTypeModel>)productionTypeInfos.Select<ProductionTypeInfo, ProductionTypeModel>((Func<ProductionTypeInfo, ProductionTypeModel>)(m => m.ToModel())).ToList<ProductionTypeModel>();
            return typeListResponse;
        }

        [ApiMethod]
        public ProductionStateGetResponse Execute(ProductionStateGetRequest request)
        {
            return new ProductionStateGetResponse()
            {
                State = ServiceHelper.LoadService<IProductionService>().GetProductionState()
            };
        }

        [ApiMethod]
        public CraftStateGetResponse Execute(CraftStateGetRequest request)
        {
            return new CraftStateGetResponse()
            {
                State = ServiceHelper.LoadService<IProductionService>().GetCraftState(request.CraftDID)
            };
        }

        [ApiMethod]
        public UnitSaveResponse Execute(UnitSaveRequest request)
        {
            UnitSaveResponse unitSaveResponse = new UnitSaveResponse();
            if (request.DID == 0)
            {
                UnitInfo unitInfo = new UnitInfo();
                unitInfo.UnitName = request.Name;
                unitInfo.UnitNO = request.NO;
                ServiceHelper.LoadService<IProductionService>().AddUnit(unitInfo);
                unitSaveResponse.UnitDID = unitInfo.UnitDID;
            }
            else
            {
                UnitInfo unit = ServiceHelper.LoadService<IProductionService>().GetUnit(request.DID);
                unit.UnitName = request.Name;
                unit.UnitNO = request.NO;
                ServiceHelper.LoadService<IProductionService>().UpdateUnit(unit);
                unitSaveResponse.UnitDID = unit.UnitDID;
            }
            return unitSaveResponse;
        }

        [ApiMethod]
        public UnitDeleteResponse Execute(UnitDeleteRequest request)
        {
            UnitDeleteResponse unitDeleteResponse = new UnitDeleteResponse();
            ServiceHelper.LoadService<IProductionService>().DeleteUnit(ServiceHelper.LoadService<IProductionService>().GetUnit(request.UnitDID));
            return unitDeleteResponse;
        }

        [ApiMethod]
        public UnitGetResponse Execute(UnitGetRequest request)
        {
            UnitGetResponse unitGetResponse = new UnitGetResponse();
            UnitInfo unit = ServiceHelper.LoadService<IProductionService>().GetUnit(request.UnitDID);
            unitGetResponse.Unit = unit.ToModel();
            return unitGetResponse;
        }

        [ApiMethod]
        public ProductionLineProbablySaveResponse Execute(ProductionLineProbablySaveRequest request)
        {
            ProductionLineProbablySaveResponse probablySaveResponse = new ProductionLineProbablySaveResponse();
            ProductionLineProbablyInfo productionLineProbably = ServiceHelper.LoadService<IProductionService>().GetProductionLineProbably(request.DID);
            productionLineProbably.Name = request.Name;
            productionLineProbably.TargetYield = request.TargetYield;
            ServiceHelper.LoadService<IProductionService>().SaveProductionLineProbably(productionLineProbably);
            return probablySaveResponse;
        }

        [ApiMethod]
        public CraftDetailListResponse Execute(CraftDetailListRequest request)
        {
            return new CraftDetailListResponse()
            {
                CraftDetails = ServiceHelper.LoadService<IProductionService>().GetCraftDetails()
            };
        }

        [ApiMethod]
        public CraftDetailGetResponse Execute(CraftDetailGetRequest request)
        {
            return new CraftDetailGetResponse()
            {
                CraftDetail = ServiceHelper.LoadService<IProductionService>().GetCraftDetail(request.CraftDID)
            };
        }

        [ApiMethod]
        public CraftDetailSaveResponse Execute(CraftDetailSaveRequest request)
        {
            CraftDetailSaveResponse detailSaveResponse = new CraftDetailSaveResponse();
            ServiceHelper.LoadService<IProductionService>().SaveCraftDetail(request.CraftDetail);
            return detailSaveResponse;
        }

        [ApiMethod]
        public KanbanReportGetResponse Execute(KanbanReportGetRequest request)
        {
            KanbanReportGetResponse reportGetResponse = new KanbanReportGetResponse();
            KanbanSettingInfo kanbanSetting = ServiceHelper.LoadService<ISettingService>().GetKanbanSetting(1);
            reportGetResponse.KanbanSetting = kanbanSetting.ToModel();
            reportGetResponse.AlarmReportData = ServiceHelper.LoadService<IReportService>().GetAlarmReport(kanbanSetting.AlarmReportTimeSection);
            reportGetResponse.ProductionReportData = ServiceHelper.LoadService<IReportService>().GetProductionReport(kanbanSetting.ProductionReportTimeSection);
            reportGetResponse.ExcellentRateReportData = ServiceHelper.LoadService<IReportService>().GetExcellentRateReport(kanbanSetting.ExcellentRateReportTimeSection);
            return reportGetResponse;
        }

        [ApiMethod]
        public KanbanSettingGetResponse Execute(KanbanSettingGetRequest request)
        {
            KanbanSettingGetResponse settingGetResponse = new KanbanSettingGetResponse();
            KanbanSettingInfo kanbanSetting = ServiceHelper.LoadService<ISettingService>().GetKanbanSetting(1);
            settingGetResponse.KanbanSetting = kanbanSetting.ToModel();
            return settingGetResponse;
        }

        [ApiMethod]
        public KanbanSettingSaveResponse Execute(KanbanSettingSaveRequest request)
        {
            KanbanSettingSaveResponse settingSaveResponse = new KanbanSettingSaveResponse();
            KanbanSettingInfo kanbanSetting = ServiceHelper.LoadService<ISettingService>().GetKanbanSetting(request.KanbanSettingId);
            kanbanSetting.ExcellentRateReportTimeSection = request.ExcellentRateReportTimeSection;
            kanbanSetting.AlarmReportTimeSection = request.AlarmReportTimeSection;
            kanbanSetting.ProductionReportTimeSection = request.ProductionReportTimeSection;
            kanbanSetting.RefreshInterval = request.RefreshInterval;
            ServiceHelper.LoadService<ISettingService>().SaveKanbanSetting(kanbanSetting);
            return settingSaveResponse;
        }

        [ApiMethod]
        public RoleGetResponse Execute(RoleGetRequest request)
        {
            RoleGetResponse roleGetResponse = new RoleGetResponse();
            RoleInfo entity = ServiceHelper.LoadService<IRoleService>().Get(request.RoleId);
            if (entity != null)
                roleGetResponse.Role = entity.ToModel();
            return roleGetResponse;
        }

        [ApiMethod]
        public RoleListResponse Execute(RoleListRequest request)
        {
            RoleListResponse roleListResponse = new RoleListResponse();
            IPagedList<RoleInfo> pagedRoles = ServiceHelper.LoadService<IRoleService>().GetPagedRoles(request.PageNumber, request.PageSize);
            List<RoleModel> list = pagedRoles.Select<RoleInfo, RoleModel>((Func<RoleInfo, RoleModel>)(m => m.ToModel())).ToList<RoleModel>();
            roleListResponse.Roles = new PagedData<RoleModel>((IEnumerable<RoleModel>)list, (IPagedList)pagedRoles);
            return roleListResponse;
        }

        [ApiMethod]
        public UserGetResponse Execute(UserGetRequest request)
        {
            UserGetResponse userGetResponse = new UserGetResponse();
            UserInfo entity = ServiceHelper.LoadService<IUserService>().Get(request.UserId);
            if (entity != null)
                userGetResponse.User = entity.ToModel();
            return userGetResponse;
        }

        [ApiMethod]
        public ChangePasswordResponse Execute(ChangePasswordRequest request)
        {
            ChangePasswordResponse passwordResponse = new ChangePasswordResponse();
            UserInfo entity = ServiceHelper.LoadService<IUserService>().Get(request.UserId);
            if (entity != null)
            {
                if (entity.Password == request.OldPassword)
                {
                    entity.Password = request.NewPassword;
                    ServiceHelper.LoadService<IUserService>().Update(entity);
                }
                else
                    passwordResponse.ErrMsg = "旧密码不正确";
            }
            return passwordResponse;
        }

        [ApiMethod]
        public PersonalInfoUpdateResponse Execute(PersonalInfoUpdateRequest request)
        {
            PersonalInfoUpdateResponse infoUpdateResponse = new PersonalInfoUpdateResponse();
            UserInfo entity = ServiceHelper.LoadService<IUserService>().Get(request.UserId);
            entity.Name = request.Name;
            entity.Gender = request.Gender;
            ServiceHelper.LoadService<IUserService>().Update(entity);
            return infoUpdateResponse;
        }

        [ApiMethod]
        public UserListResponse Execute(UserListRequest request)
        {
            UserListResponse userListResponse = new UserListResponse();
            IPagedList<UserInfo> pagedUsers = ServiceHelper.LoadService<IUserService>().GetPagedUsers(request.PageNumber, request.PageSize);
            IList<UserModel> userModelList = ServiceHelper.LoadService<IUserService>().BuildUserModels((IEnumerable<UserInfo>)pagedUsers);
            userListResponse.Users = new PagedData<UserModel>((IEnumerable<UserModel>)userModelList, (IPagedList)pagedUsers);
            return userListResponse;
        }

        [ApiMethod]
        public UserDeleteResponse Execute(UserDeleteRequest request)
        {
            UserDeleteResponse userDeleteResponse = new UserDeleteResponse();
            ServiceHelper.LoadService<IUserService>().Delete(ServiceHelper.LoadService<IUserService>().Get(request.UserId));
            return userDeleteResponse;
        }

        [ApiMethod]
        public UserSaveResponse Execute(UserSaveRequest request)
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
                ServiceHelper.LoadService<IUserService>().Insert(entity);
                userSaveResponse.UserId = entity.UserId;
            }
            else
            {
                UserInfo entity = ServiceHelper.LoadService<IUserService>().Get(request.UserId);
                entity.Name = request.Name;
                entity.Password = request.Password;
                entity.RoleId = request.RoleId;
                entity.UserName = request.UserName;
                entity.Gender = request.Gender;
                entity.CraftDIDs = request.CraftDIDs;
                ServiceHelper.LoadService<IUserService>().Update(entity);
                userSaveResponse.UserId = entity.UserId;
            }
            return userSaveResponse;
        }

        [ApiMethod]
        public RoleDeleteResponse Execute(RoleDeleteRequest request)
        {
            RoleDeleteResponse roleDeleteResponse = new RoleDeleteResponse();
            ServiceHelper.LoadService<IRoleService>().Delete(ServiceHelper.LoadService<IRoleService>().Get(request.RoleId));
            return roleDeleteResponse;
        }

        [ApiMethod]
        public RoleSaveResponse Execute(RoleSaveRequest request)
        {
            RoleSaveResponse roleSaveResponse = new RoleSaveResponse();
            if (request.RoleId == 0)
            {
                RoleInfo entity = new RoleInfo();
                entity.RoleName = request.RoleName;
                entity.PermissionCodes = request.PermissionCodes;
                entity.CreateTime = DateTime.Now;
                entity.Remark = request.Remark;
                ServiceHelper.LoadService<IRoleService>().Insert(entity);
                roleSaveResponse.RoleId = entity.RoleId;
            }
            else
            {
                RoleInfo entity = ServiceHelper.LoadService<IRoleService>().Get(request.RoleId);
                entity.RoleName = request.RoleName;
                entity.PermissionCodes = request.PermissionCodes;
                entity.Remark = request.Remark;
                ServiceHelper.LoadService<IRoleService>().Update(entity);
                roleSaveResponse.RoleId = entity.RoleId;
            }
            return roleSaveResponse;
        }

        [ApiMethod]
        public LoginResponse Execute(LoginRequest request)
        {
            LoginResponse loginResponse = new LoginResponse();
            UserInfo userByUserName = ServiceHelper.LoadService<IUserService>().GetUserByUserName(request.UserName);
            if (userByUserName != null)
            {
                if (userByUserName.Password == request.Password)
                {
                    userByUserName.LastLoginTime = new DateTime?(DateTime.Now);
                    ServiceHelper.LoadService<IUserService>().Update(userByUserName);
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

        [ApiMethod]
        public PermissionGetResponse Execute(PermissionGetRequest request)
        {
            PermissionGetResponse permissionGetResponse = new PermissionGetResponse();
            PermissionInfo permission = ServiceHelper.LoadService<IUserService>().GetPermission(request.PermissionId);
            permissionGetResponse.Permission = permission.ToModel();
            return permissionGetResponse;
        }

        [ApiMethod]
        public PermissionSaveResponse Execute(PermissionSaveRequest request)
        {
            PermissionSaveResponse permissionSaveResponse = new PermissionSaveResponse();
            if (request.PermissionId > 0)
            {
                PermissionInfo permission = ServiceHelper.LoadService<IUserService>().GetPermission(request.PermissionId);
                permission.PermissionName = request.PermissonName;
                permission.Remark = request.Remark;
                ServiceHelper.LoadService<IUserService>().Update(permission);
            }
            return permissionSaveResponse;
        }

        [ApiMethod]
        public PermissionOrderResponse Execute(PermissionOrderRequest request)
        {
            PermissionOrderResponse permissionOrderResponse = new PermissionOrderResponse();
            PermissionInfo permission = ServiceHelper.LoadService<IUserService>().GetPermission(request.PermissionId);
            List<PermissionInfo> list = ServiceHelper.LoadService<IUserService>().GetPermissionsByParentId(permission.ParentId).ToList<PermissionInfo>();
            int num = 0;
            for (int index = 0; index < list.Count; ++index)
            {
                PermissionInfo permissionInfo = list[index];
                permissionInfo.DisplayOrder = index + 1;
                if (permissionInfo == permission)
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
            ServiceHelper.LoadService<IUserService>().UpdatePermissions((IList<PermissionInfo>)list);
            return permissionOrderResponse;
        }

        [ApiMethod]
        public PermissionListResponse Execute(PermissionListRequest request)
        {
            return new PermissionListResponse()
            {
                Permissions = (IList<PermissionModel>)ServiceHelper.LoadService<IUserService>().GetAllPermissions().Select<PermissionInfo, PermissionModel>(m => m.ToModel()).ToList<PermissionModel>()
            };
        }
    }
}
