using MES.Data;

namespace IFactory.DB
{
    public class BaseFacade
    {
        
        public DataBasePersistBroker dataProvider = null;
        
        public BaseFacade()
        {
            dataProvider = new DataBasePersistBroker();
        }
    }

}
