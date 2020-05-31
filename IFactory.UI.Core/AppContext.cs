using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.User;
using IFactory.Platform.Common.Response.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IFactory.UI.Core
{
    public class AppContext
    {
        private static AppContext current = new AppContext();
        private List<int> craftDIDs;
        private List<string> permissionCodes;

        public static AppContext Current
        {
            get
            {
                return AppContext.current;
            }
        }

        public LocalConfig LocalConfig { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public List<int> CraftDIDs
        {
            get
            {
                return this.craftDIDs ?? (this.craftDIDs = new List<int>());
            }
            set
            {
                this.craftDIDs = value;
            }
        }

        public List<string> PermissionCodes
        {
            get
            {
                return this.permissionCodes ?? (this.permissionCodes = new List<string>());
            }
            set
            {
                this.permissionCodes = value;
            }
        }

        static AppContext()
        {
            AppContext.current.LocalConfig = new LocalConfig();
        }

        public bool LoadLocalConfig()
        {
            try
            {
                string str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Local/LocalConfig.xml");
                if (File.Exists(str))
                {
                    XDocument xdocument = XDocument.Load(str);
                    if (xdocument.Root != null)
                    {
                        foreach (XElement element in xdocument.Root.Elements())
                        {
                            LocalConfig.ClientTypeEnum result;
                            if (element.Name.LocalName == "ClientType" && Enum.TryParse<LocalConfig.ClientTypeEnum>(element.Value, out result))
                                this.LocalConfig.ClientType = result;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Reset()
        {
            this.UserId = 0;
            this.Name = (string)null;
            this.permissionCodes = null;
            this.craftDIDs = (List<int>)null;
        }

        public List<PermissionNode> GetPermissionNodesAsync()
        {
            return this.BuildUserPermissionNodes(PermissionNode.BuildPermissionNodes((LocalApi.Execute(new PermissionListRequest())).Permissions));
        }

        private List<PermissionNode> BuildUserPermissionNodes(IList<PermissionNode> permissionNodes)
        {
            List<PermissionNode> permissionNodeList1 = new List<PermissionNode>();
            foreach (PermissionNode permissionNode1 in (IEnumerable<PermissionNode>)permissionNodes)
            {
                List<PermissionNode> permissionNodeList2 = this.BuildUserPermissionNodes(permissionNode1.Children);
                if (permissionNodeList2.Count > 0 || string.IsNullOrEmpty(permissionNode1.Code) || this.PermissionCodes.Contains(permissionNode1.Code))
                {
                    PermissionNode permissionNode2 = new PermissionNode() { Text = permissionNode1.Text, Code = permissionNode1.Code, Children = (IList<PermissionNode>)permissionNodeList2 };
                    permissionNodeList1.Add(permissionNode2);
                }
            }
            return permissionNodeList1;
        }
    }
}
