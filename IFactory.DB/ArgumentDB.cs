using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using IFactory.Domain.Models;
using PagedList;
using System.IO;
using System.Xml;

namespace IFactory.DB
{
    public class ArgumentDB : BaseFacade
    {

        public static string[] lines;

        public static string Name { get; set; }
        public IPagedList<ArgumentItem> GetPagedArgumentData(int? processDID, int pageNo, int pageSize)
        {

            List<ArgumentItem> lst = new List<ArgumentItem>();

            string path = "C:\\Users\\junxi\\Desktop\\bat";

            DirectoryInfo theFolder = new DirectoryInfo(path);

            DirectoryInfo[] dirInfo = theFolder.GetDirectories();

            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                ArgumentItem info = new ArgumentItem();
                info.Name = NextFolder.ToString();

                lst.Add(info);
            }
            IQueryable<ArgumentItem> superset = lst.AsQueryable();
            return new PagedList<ArgumentItem>(superset, pageNo, pageSize);
        }

        public IPagedList<ArgumentItem> GetPagedArgumentData1(int? processDID, int pageNo, int pageSize)
        {

            List<ArgumentItem> lst = new List<ArgumentItem>();

            string path = "C:\\Users\\junxi\\Desktop\\bat";

            DirectoryInfo theFolder = new DirectoryInfo(path);

            DirectoryInfo[] dirInfo = theFolder.GetDirectories();

            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                //if(NextFolder.ToString() == Name)
                //{
                    XmlDocument doc = new XmlDocument();
                    //string strName = path + "\\" + NextFolder.ToString() + "\\batteryParameters.xml";
                    doc.Load("C:\\Users\\junxi\\Desktop\\bat\\333993\\batteryParameters.xml");
                    //doc.Load(strName);
                    XmlElement rootElem = doc.DocumentElement;              //根节点
                    XmlNodeList oList = rootElem.ChildNodes;                //二级节点
                    XmlNode oCurrentNode;
                    for (int i = 0; i < oList.Count; i++)
                    {
                        oCurrentNode = oList[i];
                        if (oCurrentNode.HasChildNodes)
                        {
                            for (int j = 0; j < oCurrentNode.ChildNodes.Count; j++)            //三级节点
                            {
                                if (oCurrentNode.ChildNodes[j].Name != "#comment" && oCurrentNode.ChildNodes[j].Name != "#text")
                                {
                                    ArgumentItem info = new ArgumentItem();
                                    info.Name = oCurrentNode.ChildNodes[j].Name;
                                    info.Argument = oCurrentNode.ChildNodes[j].InnerText;
                                    lst.Add(info);
                                }
                            }
                        }
                    }
                //}
            }
            IQueryable<ArgumentItem> superset = lst.AsQueryable();
            return new PagedList<ArgumentItem>(superset, pageNo, pageSize);
        }

    }
}