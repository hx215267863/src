
using ATL_MC.DAL.Model;
using MES.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PVCommon.List;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.DAL.Service
{

  
    public class ProductService
    {
        private Database _db { get; set; }
        public ProductService()
        {
            _db = new DataBasePersistBroker().EQUIPDataBase;
        }

        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="ITEM_CD">产品编码</param>
        /// <returns></returns>
        public PagedList.PagedList<ProductDto> GetProductInfo(int pageNumber, int pageSize, string ITEM_CD)
        {
            string sql = "SELECT * from product";
            if (!string.IsNullOrWhiteSpace(ITEM_CD))
            {
                sql += " WHERE item_cd =" + ITEM_CD;
            }
            var dt = _db.ExecuteDataSet(CommandType.Text, sql).Tables[0];
            var list = ListConvert.DtToList<ProductDto>(dt);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].RowID = i + 1;
            }
            var pagelist = new PagedList.PagedList<ProductDto>(list, pageNumber, pageSize);
            return pagelist;
        }

        /// <summary>
        /// 获取所有产品编号
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="ITEM_CD">产品编码</param>
        /// <returns></returns>
        public List<ProductDto> GetAllProductNum()
        {
            string sql = "SELECT  t1.id,t1.ITEM_CD from product t1 ";
            var dt = _db.ExecuteDataSet(CommandType.Text, sql).Tables[0];
            var list = ListConvert.DtToList<ProductDto>(dt);
            var distinctList = list.Distinct(new ProductComparer<ProductDto>()).ToList();
            return distinctList;
        }
        /// <summary>
        /// TODO:级联删除,需要测试
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int RemoveProductInfo(string id)
        {
            string sql = $@"DELETE t1, t2 
                            FROM product AS t1 
                            LEFT JOIN productdetail AS t2 ON t1.id = t2.productid 
                            WHERE t1.id = '{id}'";
            var r = _db.ExecuteNonQuery(CommandType.Text, sql);
            return r;
        }
        public int InsertProductInfo(ProductDto product)
        {
            string sql = $@"INSERT INTO product  (id,ITEM_CD,ITEM_NM,MODEL_CD,ITEM_COLOR,ITEM_HEIGHT,ITEM_WIDTH,QTY_FOR_CRIB,QTY_FOR_TRAY,MoveInLight_1,MoveInLight_2,Remark,CRT_DT)  
                            VALUES  (
                            '{Guid.NewGuid().ToString()}',
                            '{product.ITEM_CD}',
                            '{product.ITEM_NM}',
                            '{product.MODEL_CD}',
                            '{product.ITEM_COLOR}',
                            {product.ITEM_HEIGHT},
                            {product.ITEM_WIDTH},
                            {product.QTY_FOR_CRIB},
                            {product.QTY_FOR_TRAY},
                            {product.MoveInLight_1},
                            {product.MoveInLight_2}, 
                             '{product.Remark}', 
                            '{DateTime.Now.ToString()}'
                            )";
            var r = _db.ExecuteNonQuery(CommandType.Text, sql);
            return r;
        }
        public int UpdateProductInfo(ProductDto product)
        {
            string sql = $@"
                        UPDATE product t1 SET                         
                            ITEM_CD = '{product.ITEM_CD}',
                            ITEM_NM = '{product.ITEM_NM}',
                            MODEL_CD = '{product.MODEL_CD}',
                            ITEM_COLOR = '{product.ITEM_COLOR}',
                            ITEM_HEIGHT = {product.ITEM_HEIGHT},
                            ITEM_WIDTH = {product.ITEM_WIDTH},
                            QTY_FOR_CRIB ={ product.QTY_FOR_CRIB},
                            QTY_FOR_TRAY ={ product.QTY_FOR_TRAY},
                            MoveInLight_1 ={ product.MoveInLight_1}, 
                            MoveInLight_2 ={ product.MoveInLight_2},
                            Remark ='{ product.Remark}',
                            UPT_DT = '{DateTime.Now.ToString()}'                        
                        WHERE t1.id='{product.ID}'";
            var r = _db.ExecuteNonQuery(CommandType.Text, sql);
            return r;
        }

        //以下为产品槽位明细信息-------------------------------

        /// <summary>
        /// 获取产品槽位明细
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="ITEM_CD">产品编码</param>
        /// <returns></returns>
        public PagedList.PagedList<ProductDetailDto> GetProductdetailInfo(int pageNumber, int pageSize, string ITEM_CD)
        {
            string sql = @"SELECT prodetail.* , pro.ITEM_CD , pro.ITEM_NM ,pro.MODEL_CD , pro.ITEM_COLOR  
                           FROM productdetail prodetail
                           LEFT JOIN product pro ON  pro.id = prodetail.productid";
            if (!string.IsNullOrWhiteSpace(ITEM_CD))
            {
                sql += " WHERE pro.ITEM_CD = " + ITEM_CD;
            }
            var dt = _db.ExecuteDataSet(CommandType.Text, sql).Tables[0];
            var list = ListConvert.DtToList<ProductDetailDto>(dt);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].RowID = i + 1;
            }
            var pagelist = new PagedList.PagedList<ProductDetailDto>(list, pageNumber, pageSize);
            return pagelist;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Removeproductdetail(string id)
        {
            string sql = $@"DELETE FROM productdetail t1 WHERE t1.id = '{id}'";
            var r = _db.ExecuteNonQuery(CommandType.Text, sql);
            return r;
        }
        public int InsertProductdetailInfo(ProductDetailDto detail)
        {
            string sql = $@"INSERT INTO productdetail  (id,slot_ty,slot_site,slot_xaxis,slot_yaxis,slot_zaxis,slot_rxaxis,slot_ryaxis,slot_rzaxis,slot_fig,slot_remark,brightness_1,brightness_2,brightness_3,brightness_4 ,crt_dt)  
                            VALUES  (
                               '{ detail.ID}',                          
                               { detail.SLOT_TY},
                               { detail.SLOT_SITE},
                               {detail.SLOT_xAxis},
                               {detail.SLOT_yAxis},
                               { detail.SLOT_zAxis},
                               { detail.SLOT_rxAxis},
                               { detail.SLOT_ryAxis},
                               { detail.SLOT_rzAxis},
                               '{ detail.SLOT_Fig}',
                               '{ detail.SLOT_Remark}',
                               {detail.Brightness_1},
                               {detail.Brightness_2},
                               {detail.Brightness_3},
                               {detail.Brightness_4},
                               '{ detail.CRT_DT}'
                         )";
            var r = _db.ExecuteNonQuery(CommandType.Text, sql);
            return r;
        }
        public int UpdateProductDetail(ProductDetailDto detail)
        {
            string sql = $@"
                        UPDATE productdetail t1 SET  
                            SLOT_TY={ detail.SLOT_TY},
                            SLOT_SITE={ detail.SLOT_SITE},
                            SLOT_xAxis={detail.SLOT_xAxis},
                            SLOT_yAxis={detail.SLOT_yAxis},
                            SLOT_zAxis={ detail.SLOT_zAxis},
                            SLOT_rxAxis={ detail.SLOT_rxAxis},
                            SLOT_ryAxis={ detail.SLOT_ryAxis},
                            SLOT_rzAxis={ detail.SLOT_rzAxis},
                            SLOT_Fig='{ detail.SLOT_Fig}',
                            SLOT_Remark='{ detail.SLOT_Remark}',
                            Brightness_1={detail.Brightness_1},
                            Brightness_2={detail.Brightness_2},
                            Brightness_3={detail.Brightness_3},
                            Brightness_4={detail.Brightness_4},
                            UPT_DT='{ detail.UPT_DT}'   
                        WHERE t1.id='{detail.ID}'";
            var r = _db.ExecuteNonQuery(CommandType.Text, sql);
            return r;
        }



        //        SLOT_TY
        //        SLOT_SITE
        //SLOT_xAxis
        //SLOT_yAxis
        //SLOT_zAxis
        //SLOT_rxAxis
        //SLOT_ryAxis
        //SLOT_rzAxis
        //SLOT_Fig
        //SLOT_Remark
        //Brightness_1
        //Brightness_2
        //Brightness_3
        //Brightness_4
        //UPT_DT












    }
    public class ProductComparer<T> : IEqualityComparer<T> where T : ProductDto
    {
        public bool Equals(T x, T y)
        {
            return x.ITEM_CD == y.ITEM_CD;
        }

        public int GetHashCode(T obj)
        {
            return obj.ITEM_CD.GetHashCode();
        }
    }
}
