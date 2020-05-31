using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFactory.Domain.Entities;
using IFactory.Domain.Common;
using System.Data.Common;
using PagedList;
using IFactory.Domain.Models;
using IFactory.Common;

namespace IFactory.DB
{
    public class SystemParamDB : BaseFacade
    {
        public static bool logCell = true;


        //--------------------------------------------产品参数设定  lipl

        public IList<ProductParamModel> BuildProductParamModels(IEnumerable<ProductParamInfo> productParamInfos)
        {
           
            List<ProductParamModel> list = new List<ProductParamModel>();
            foreach (ProductParamInfo info in productParamInfos)
            {
                ProductParamModel item = new ProductParamModel
                {
                    ITEM_CD = info.ITEM_CD,
                    ITEM_NM = info.ITEM_NM,
                    ITEM_DESC = info.ITEM_DESC,
                    MODEL_CD = info.MODEL_CD,
                    MODEL_NM = info.MODEL_NM,
                    ITEM_COLOR = info.ITEM_COLOR,
                    ITEM_LONG = info.ITEM_LONG,
                    ITEM_WIDE = info.ITEM_WIDE,
                    LIGHT_BRIGHT = info.LIGHT_BRIGHT,
                    QTY_FOR_CRIB = info.QTY_FOR_CRIB,
                    QTY_FOR_TARY = info.QTY_FOR_TARY,
                    MO = info.MO,
                    CRT_ID = info.CRT_ID,
                    CRT_DT = info.CRT_DT,
                    UPT_ID = info.UPT_ID,
                    UPT_DT = info.UPT_DT
                };
                list.Add(item);
            }
            return list;
        }

        public DataTable ProductParamTableQuery(string item_cd)
        //public DataTable ProductParamTableQuery()
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql;
            if (item_cd.Length > 0)
            {
                sql = string.Format(@"select * from C_BASE_PRODUCT_T where ITEM_CD = '{0}' order by crt_dt desc", item_cd);
            }
            else
            {
                sql = "select * from C_BASE_PRODUCT_T order by crt_dt desc";
            }
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public  List<AllUsefullProductModel>  GetProductByName(string product)
        {
            string SQL = string.Format(@"SELECT* FROM
            (select ITEM_CD, MODEL_CD, ITEM_COLOR, ITEM_LONG, ITEM_WIDE, QTY_FOR_CRIB, QTY_FOR_TARY from C_BASE_PRODUCT_T WHERE ITEM_CD = '{0}' ) AS TA
            JOIN
            (select ITEM_CD, SLOT_SITE, SLOT_X_DOT, SLOT_Y_DOT, SLOT_Z_DOT, SLOT_U_DOT, LIGHT_1, LIGHT_2, LIGHT_3, LIGHT_4 from C_BASE_PRODUCT_SLOT_T ) AS TB
            on TA.ITEM_CD = TB.ITEM_CD", product);

            Database equipDB = dataProvider.EQUIPDataBase;

            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, SQL);

            DataTable tb = ds.Tables[0];


            List<AllUsefullProductModel> list = new List<AllUsefullProductModel>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    AllUsefullProductModel info = new AllUsefullProductModel();
                    info.ITEM_CD = row[0].ToString();
                    info.MODEL_CD = row[1].ToString();
                    info.ITEM_COLOR = row[2].ToString();
                    info.ITEM_LONG = row[3].ToString();
                    info.ITEM_WIDE = row[4].ToString();
                    info.QTY_FOR_CRIB = row[5].ToString();
                    info.QTY_FOR_TARY = row[6].ToString();
                    info.SLOT_SITE = row[8].ToString();
                    info.SLOT_X_DOT = row[9].ToString();
                    info.SLOT_Y_DOT = row[10].ToString();
                    info.SLOT_Z_DOT = row[11].ToString();
                    info.SLOT_U_DOT = row[12].ToString();
                    info.LIGHT_1 = row[13].ToString();
                    info.LIGHT_2 = row[14].ToString();
                    info.LIGHT_3 = row[15].ToString();
                    info.LIGHT_4 = row[16].ToString();

                    list.Add(info);
                }
            }
            return list;
        }
        
        //
        public void InsertProductParamsInfo(ProductParamInfo info)
        {
            Database equipDB = dataProvider.EQUIPDataBase;

            string sql = string.Format(@"insert C_BASE_PRODUCT_T (ITEM_CD, ITEM_NM, ITEM_DESC, MODEL_CD, MODEL_NM, ITEM_COLOR, ITEM_LONG,ITEM_WIDE,LIGHT_BRIGHT,QTY_FOR_CRIB,QTY_FOR_TARY,MO,CRT_ID,CRT_DT,UPT_ID,UPT_DT) values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}'); "
                                        ,info.ITEM_CD, info.ITEM_NM, info.ITEM_DESC, info.MODEL_CD, info.MODEL_NM, info.ITEM_COLOR, info.ITEM_LONG, info.ITEM_WIDE, /*info.LIGHT_BRIGHT*/0, info.QTY_FOR_CRIB, info.QTY_FOR_TARY,
                                        info.MO, info.CRT_ID, info.CRT_DT.ToString(), info.UPT_ID, info.UPT_DT.ToString());
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        //删除用户信息
        public void DeleteProductParam(string ITEM_CD)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format( @"delete from C_BASE_PRODUCT_T where ITEM_CD = '{0}';delete from C_BASE_PRODUCT_SLOT_T where ITEM_CD = '{1}'" , (ITEM_CD), ITEM_CD);
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        public void DeleteProductParam(string ITEM_CD,string SLOT_SITE)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(@"delete from C_BASE_PRODUCT_SLOT_T where ITEM_CD = '{0}' and SLOT_SITE = '{1}'", ITEM_CD, SLOT_SITE);
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        public List<ProductParamInfo> GetProductParams(string item_cd)
        {
            DataTable tb = ProductParamTableQuery(item_cd);
           
            List<ProductParamInfo> list = new List<ProductParamInfo>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    ProductParamInfo info = new ProductParamInfo();
                    info.ITEM_CD = row[0].ToString();
                    info.ITEM_NM = row[1].ToString();
                    info.ITEM_DESC = row[2].ToString();
                    info.MODEL_CD = row[3].ToString();
                    info.MODEL_NM = row[4].ToString();
                    info.ITEM_COLOR = row[5].ToString();
                    info.ITEM_LONG = row[6].ToString();
                    info.ITEM_WIDE = row[7].ToString();
                    info.LIGHT_BRIGHT = row[8].ToString();
                    info.QTY_FOR_CRIB = row[9].ToString();
                    info.QTY_FOR_TARY = row[10].ToString();
                    info.MO = row[11].ToString();
                    info.CRT_ID = row[12].ToString();
                    info.CRT_DT = DateTime.Parse(row[13].ToString());
                    info.UPT_ID = row[14].ToString();
                    info.UPT_DT = DateTime.Parse(row[15].ToString());

                    list.Add(info);
                }
            }
            return list;
        }

        public IPagedList<ProductParamInfo> GetPagedProductParams(int pageNo, int pageSize,string item_cd)
        {
            return new PagedList.PagedList<ProductParamInfo>(from m in GetProductParams(item_cd)
                                                     orderby m.CRT_DT
                                                     select m, pageNo, pageSize);
        }

        private DataTable getProductByMODEL_CD(string MODEL_CD)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"select * from C_BASE_PRODUCT_T t where
                                                ('{0}' = '' or t.MODEL_CD = '{1}')"
                                        , MODEL_CD, MODEL_CD);
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

       

       
        public void UpdateProductParams(ProductParamInfo info)
        {
            if (!logCell) return;
            Database equipDB = dataProvider.EQUIPDataBase;
         
                string sql = string.Format(
                @"update C_BASE_PRODUCT_T t 
                    set
			        t.ITEM_NM = '{1}',
			        t.ITEM_DESC = '{2}',
			        t.MODEL_CD = '{3}',
                    t.MODEL_NM = '{4}',
			        t.ITEM_COLOR = '{5}',
			        t.ITEM_LONG = '{6}',
			        t.ITEM_WIDE = '{7}',
                    t.LIGHT_BRIGHT = '{8}',
			        t.QTY_FOR_CRIB = '{9}',
                    t.QTY_FOR_TARY = '{10}',
			        t.MO = '{11}',
			        t.UPT_ID = '{12}',
			        t.UPT_DT = '{13}'
                    where t.ITEM_CD = '{0}'"
              , info.ITEM_CD, info.ITEM_NM, info.ITEM_DESC, info.MODEL_CD, info.MODEL_NM, info.ITEM_COLOR
              , info.ITEM_LONG, info.ITEM_WIDE, info.LIGHT_BRIGHT, info.QTY_FOR_CRIB, info.QTY_FOR_TARY, info.MO
              , info.UPT_ID, info.UPT_DT.ToString()
              );
                equipDB.ExecuteScalar(CommandType.Text, sql); 
        }

      
        private DataTable getProductParamByITEM_CD(string ITEM_CD)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"select * from C_BASE_PRODUCT_T t where t.ITEM_CD = '{0}' "
                                        , ITEM_CD);
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public ProductParamInfo GetProductParamByITEM_CD(string ITEM_CD)
        {
            DataTable tb = getProductParamByITEM_CD(ITEM_CD);
            ProductParamInfo info = new ProductParamInfo();
            if (tb != null && tb.Rows.Count > 0)
            {
                info.ITEM_CD = tb.Rows[0][1].ToString();
                info.ITEM_NM = tb.Rows[0][2].ToString();
                info.ITEM_DESC = tb.Rows[0][3].ToString();
                info.MODEL_CD = tb.Rows[0][4].ToString();
                info.MODEL_NM = tb.Rows[0][5].ToString();
                info.ITEM_COLOR = tb.Rows[0][6].ToString();
                info.ITEM_LONG = tb.Rows[0][7].ToString();
                info.ITEM_WIDE = tb.Rows[0][8].ToString();
                info.LIGHT_BRIGHT = tb.Rows[0][9].ToString();
                info.QTY_FOR_CRIB = tb.Rows[0][10].ToString();
                info.QTY_FOR_TARY = tb.Rows[0][11].ToString();
                info.MO = tb.Rows[0][12].ToString();
                info.CRT_ID = tb.Rows[0][13].ToString();
                info.CRT_DT = DateTime.Parse(tb.Rows[0][14].ToString());
                info.UPT_ID = tb.Rows[0][15].ToString();
                info.UPT_DT = DateTime.Parse(tb.Rows[0][16].ToString());

                return info;
            }
            else
            {
                throw new Exception("执行 getProductParamByITEM_CD 查询到空值");
            }
        }



        //-------------------------------------------  系统参数设定  lipl
        public IList<SystemParamModel> BuildSystemParamModels(IEnumerable<SystemParamInfo> systemParamInfos)
        {

            List<SystemParamModel> list = new List<SystemParamModel>();
            foreach (SystemParamInfo info in systemParamInfos)
            {
                SystemParamModel item = new SystemParamModel
                {
                    IDX = info.IDX,
                    ITEM_CD = info.ITEM_CD,
                    SLOT_TY = info.SLOT_TY,
                    SLOT_SITE = info.SLOT_SITE,
                    SLOT_X_DOT = info.SLOT_X_DOT,
                    SLOT_Y_DOT = info.SLOT_Y_DOT,
                    SLOT_Z_DOT = info.SLOT_Z_DOT,
                    SLOT_U_DOT = info.SLOT_U_DOT,

                    LIGHT_1 = info.LIGHT_1,
                    LIGHT_2 = info.LIGHT_2,
                    LIGHT_3 = info.LIGHT_3,
                    LIGHT_4 = info.LIGHT_4,

                    MO = info.MO,
                    CRT_ID = info.CRT_ID,
                    CRT_DT = info.CRT_DT,
                    UPT_ID = info.UPT_ID,
                    UPT_DT = info.UPT_DT
                };
                list.Add(item);
            }
            return list;
        }

        public DataTable SystemParamTableQuery(string item_cd)
        //public DataTable SystemParamTableQuery()
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql;

            if (item_cd.Length > 0)
            {
                sql = string.Format(@"select* from C_BASE_PRODUCT_SLOT_T where ITEM_CD = '{0}' order by idx desc", item_cd);
            }
            else
            {
                sql = "select * from C_BASE_PRODUCT_SLOT_T order by idx desc";
            }

            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        //
        public void InsertSystemParamsInfo(SystemParamInfo info)
        {
            Database equipDB = dataProvider.EQUIPDataBase;

            string SQL = "select max(IDX) from C_BASE_PRODUCT_SLOT_T";
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, SQL);
            DataTable tb = ds.Tables[0];
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    string str = row[0].ToString();
                    if(str.Length != 0)
                    {
                        info.IDX = int.Parse(row[0].ToString()) + 1;
                    }
                    else
                    {
                        info.IDX = 0;
                    }
                    
                }
            }

            string sql = string.Format(@"insert C_BASE_PRODUCT_SLOT_T (IDX,ITEM_CD, SLOT_TY, SLOT_SITE, SLOT_X_DOT, SLOT_Y_DOT, SLOT_Z_DOT, SLOT_U_DOT,LIGHT_1,LIGHT_2,LIGHT_3,LIGHT_4,MO,CRT_ID,CRT_DT,UPT_ID,UPT_DT)
                                            values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}'); "
                                        , info.IDX, info.ITEM_CD, info.SLOT_TY, info.SLOT_SITE, info.SLOT_X_DOT, info.SLOT_Y_DOT, info.SLOT_Z_DOT, info.SLOT_U_DOT,
                                        info.LIGHT_1, info.LIGHT_2, info.LIGHT_3, info.LIGHT_4,
                                        info.MO, info.CRT_ID, info.CRT_DT.ToString(), info.UPT_ID, info.UPT_DT.ToString());
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        //删除用户信息
        public void DeleteSystemParam(int IDX)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"delete from C_BASE_PRODUCT_SLOT_T  where IDX = {0}"
              , (IDX));
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        public List<SystemParamInfo> GetSystemParams(string item_cd)
        //public List<SystemParamInfo> GetSystemParams()
        {
            DataTable tb = SystemParamTableQuery(item_cd);

            List<SystemParamInfo> list = new List<SystemParamInfo>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    SystemParamInfo info = new SystemParamInfo();
                    info.IDX = int.Parse(row[0].ToString());
                    info.ITEM_CD = row[1].ToString();
                    info.SLOT_TY = row[2].ToString();
                    info.SLOT_SITE = row[3].ToString();
                    info.SLOT_X_DOT = row[4].ToString();
                    info.SLOT_Y_DOT = row[5].ToString();
                    info.SLOT_Z_DOT = row[6].ToString();
                    info.SLOT_U_DOT = row[7].ToString();
                    info.LIGHT_1 = row[8].ToString();
                    info.LIGHT_2 = row[9].ToString();
                    info.LIGHT_3 = row[10].ToString();
                    info.LIGHT_4 = row[11].ToString();

                    info.MO = row[12].ToString();
                    info.CRT_ID = row[13].ToString();
                    info.CRT_DT = DateTime.Parse(row[14].ToString());
                    info.UPT_ID = row[15].ToString();
                    info.UPT_DT = DateTime.Parse(row[16].ToString());
                    list.Add(info);
                }
            }
            return list;
        }

        public IPagedList<SystemParamInfo> GetPagedSystemParams(int pageNo, int pageSize,string item_cd)
        {
            return new PagedList.PagedList<SystemParamInfo>(from m in GetSystemParams(item_cd)
                                                             orderby m.CRT_DT
                                                             select m, pageNo, pageSize);
        }

        private DataTable getSystemByITEM_CD(string ITEM_CD)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"select * from C_BASE_PRODUCT_SLOT_T t where
                                                ('{0}' = '' or t.ITEM_CD = '{1}')"
                                        , ITEM_CD, ITEM_CD);
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public void UpdateSystemParams(SystemParamInfo info)
        {
            if (!logCell) return;
            Database equipDB = dataProvider.EQUIPDataBase;

            string sql = string.Format(
            @"update C_BASE_PRODUCT_SLOT_T t 
                    set
			        t.SLOT_TY = '{1}',
			        t.SLOT_SITE = '{2}',
			        t.SLOT_X_DOT = '{3}',
                    t.SLOT_Y_DOT = '{4}',
			        t.SLOT_Z_DOT = '{5}',
			        t.SLOT_U_DOT = '{6}',
                    t.LIGHT_1 = '{7}',
                    t.LIGHT_2 = '{8}',
                    t.LIGHT_3 = '{9}',
                    t.LIGHT_4 = '{10}',
			        t.MO = '{11}',
			        t.UPT_ID = '{12}',
			        t.UPT_DT = '{13}'
                    where t.IDX = '{0}'"
          , info.IDX, info.SLOT_TY, info.SLOT_SITE, info.SLOT_X_DOT, info.SLOT_Y_DOT, info.SLOT_Z_DOT
          , info.SLOT_U_DOT,info.LIGHT_1, info.LIGHT_2, info.LIGHT_3, info.LIGHT_4, info.MO
          , info.UPT_ID, info.UPT_DT.ToString()
          );
            equipDB.ExecuteScalar(CommandType.Text, sql);
        }


        private DataTable getSystemParamByIDX(int IDX)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"select * from C_BASE_PRODUCT_SLOT_T t where t.IDX = {0}"
                                        , IDX);
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public SystemParamInfo GetSystemParamByIDX(int IDX)
        {
            DataTable tb = getSystemParamByIDX(IDX);
            SystemParamInfo info = new SystemParamInfo();
            if (tb != null && tb.Rows.Count > 0)
            {
                info.IDX = int.Parse(tb.Rows[0][0].ToString());
                info.ITEM_CD = tb.Rows[0][2].ToString();
                info.SLOT_TY = tb.Rows[0][3].ToString();
                info.SLOT_SITE = tb.Rows[0][4].ToString();
                info.SLOT_X_DOT = tb.Rows[0][5].ToString();
                info.SLOT_Y_DOT = tb.Rows[0][6].ToString();
                info.SLOT_Z_DOT = tb.Rows[0][7].ToString();
                info.SLOT_U_DOT = tb.Rows[0][8].ToString();

                info.LIGHT_1 = tb.Rows[0][9].ToString();
                info.LIGHT_2 = tb.Rows[0][10].ToString();
                info.LIGHT_3 = tb.Rows[0][11].ToString();
                info.LIGHT_4 = tb.Rows[0][12].ToString();

                info.MO = tb.Rows[0][12].ToString();
                info.CRT_ID = tb.Rows[0][13].ToString();
                info.CRT_DT = DateTime.Parse(tb.Rows[0][14].ToString());
                info.UPT_ID = tb.Rows[0][15].ToString();
                info.UPT_DT = DateTime.Parse(tb.Rows[0][16].ToString());

                return info;
            }
            else
            {
                throw new Exception("执行 getSystemParamByIDX 查询到空值");
            }
        }




    }
}
