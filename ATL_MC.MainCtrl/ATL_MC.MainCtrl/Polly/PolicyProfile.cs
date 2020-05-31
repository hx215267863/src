using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.MainCtrl.Polly
{
    public class PolicyProfile
    {

        private static ATL_MC.IBG_LOG.IBG_LOG mIBG_LOG = new ATL_MC.IBG_LOG.IBG_LOG();
        public static ISyncPolicy Build(int retryCount, int retryTime)
        {
            return Policy.Handle<Exception>().WaitAndRetry(
                retryCount,
                (r, contex) =>
                    {
                        return new TimeSpan(0, 0, 0, 0, retryTime);
                    },
                (ex, timespan, reTryNum, context) =>
                    {
                        mIBG_LOG.LOG(0, 0, 0, ex.Message + ",开始重次:第" + reTryNum + "次,上次重试所花费时间" + timespan.TotalMilliseconds + "毫秒");
                        Console.WriteLine(ex.Message + ",开始重次:第" + reTryNum + "次,上次重试所花费时间" + timespan.TotalMilliseconds + "毫秒");
                    }
                );
        }
    }
}
