using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.DB
{
    public class ServiceHelper
    {
        public static UserDB userDB = new UserDB();
        public static ProductionDB productionDB = new ProductionDB();
        public static FacilityProductionDataDB facilityProductionDataDB = new FacilityProductionDataDB();
        public static FacilityRunArgDB facilityRunArgDB = new FacilityRunArgDB();
        public static RoleDB roleDB = new RoleDB();
        public static SettingDB settingDB = new SettingDB();
        public static AlarmDB alarmDB = new AlarmDB();
        public static ZhuiSuDB zhuisuDB = new ZhuiSuDB();
        public static ProductNGDB productngDB = new ProductNGDB();
        public static DataProductionDB dataproductionDB = new DataProductionDB();
        public static DataPicDB datapicDB = new DataPicDB();
        public static Detail1DB detail1DB = new Detail1DB();
        public static ArgumentDB argumentDB = new ArgumentDB();
        public static OneKeyDB onekeyDB = new OneKeyDB();
        public static AlarmInfoDB alarmInfoDB = new AlarmInfoDB();
        public static SystemParamDB SystemParamDB = new SystemParamDB();
        public static MyDB myDB = new MyDB();
        public static MyCreateDB myCDB = new MyCreateDB();
    }
}
