using IFactory.Domain.Crafts.Base.Models;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Parser;
using IFactory.Platform.Common.Response.Crafts;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Request.Crafts
{
    public class FacilityProductionDataListRequest : BaseRequest<FacilityProductionDataListResponse>, ICraftsReqeust, ICustomRequest<FacilityProductionDataListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "facility.production.data.list";
            }
        }

        public string CraftNO { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public FacilityProductionDataListResponse PareseResponse(string json)
        {
            JObject jobject1 = JObject.Parse(json);
            PagedData<FacilityProductionDataModel> pagedData1 = new PagedData<FacilityProductionDataModel>();
            if (jobject1["FacilityProductionDatas"] != null)
            {
                JObject jobject2 = (JObject)jobject1["FacilityProductionDatas"];
                pagedData1.PageCount = jobject2.Value<int>("PageCount");
                pagedData1.PageNumber = jobject2.Value<int>("PageNumber");
                pagedData1.PageSize = jobject2.Value<int>("PageSize");
                pagedData1.TotalItemCount = jobject2.Value<int>("TotalItemCount");
                List<FacilityProductionDataModel> productionDataModelList = new List<FacilityProductionDataModel>();
                foreach (JToken jtoken in (JArray)jobject2["Items"])
                {
                    FacilityProductionDataModel productionDataModel = (FacilityProductionDataModel)jtoken.ToObject(FacilityProductionDataModel.GetFacilityProductionDataType(CraftNO), JsonParser<BaseResponse>.GetJsonSerializer());
                    productionDataModelList.Add(productionDataModel);
                }
                pagedData1.AddRange(productionDataModelList);
            }
            jobject1.Remove("FacilityProductionDatas");
            FacilityProductionDataListResponse @object = jobject1.ToObject<FacilityProductionDataListResponse>();
            PagedData<FacilityProductionDataModel> pagedData2 = pagedData1;
            @object.FacilityProductionDatas = pagedData2;
            return @object;
        }
    }
}
