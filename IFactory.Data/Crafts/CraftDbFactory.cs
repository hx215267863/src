using IFactory.Common;
using System;

namespace IFactory.Data.Crafts
{
    public class CraftDbFactory : Disposable, ICraftDbFactory, IDisposable
    {
        // Fields
        private CraftDbContext dataContext;

        // Methods
        protected override void DisposeCore()
        {
            if (this.dataContext != null)
            {
                this.dataContext.Dispose();
            }
        }

        public CraftDbContext Get()
        {
            if (this.dataContext == null)
            {
                switch (CommonHelper.GetCraftShortNO(this.CraftNO))
                {
                    case "IN1":
                        this.dataContext = new Inspection1DbContext("DataContext");
                        break;

                    case "IN2":
                        this.dataContext = new Inspection2DbContext("DataContext");
                        break;

                    case "MIB":
                        this.dataContext = new MIBDbContext("DataContext");
                        break;

                    case "PIE":
                        this.dataContext = new PIEFDbContext("DataContext");
                        break;

                    case "PAK":
                        this.dataContext = new PackingDbContext("DataContext");
                        break;

                    case "MLA":
                        this.dataContext = new MylarDbContext("DataContext");
                        break;

                    case "INJ":
                        this.dataContext = new InjectionDbContext("DataContext");
                        break;

                    case "DGA":
                        this.dataContext = new DegassingDbContext("DataContext");
                        break;

                    case "FEF":
                        this.dataContext = new FEFDbContext("DataContext");
                        break;

                    case "OC1":
                        this.dataContext = new OCV1DbContext("DataContext");
                        break;

                    case "OCB":
                        this.dataContext = new OCVBDbContext("DataContext");
                        break;

                    case "BAK":
                        this.dataContext = new BakingDbContext("DataContext");
                        break;
                }
            }
            return this.dataContext;
        }

        // Properties
        public string CraftNO { get; set; }
    }


}
