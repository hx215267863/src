using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATL_MC.MainCtrl.System
{
    public class StatusManager
    {
        //耦合了,后续再更新
        //private Simulate simulate = new Simulate();

        private static StatusManager _statusManager = null;
        private static object _lock = new object();
        private StatusManager()
        {

        }
        public static StatusManager CreateInstance()
        {
            if (_statusManager == null)
            {
                lock (_lock)
                {
                    if (_statusManager == null)
                    {
                        _statusManager = new StatusManager();
                    }
                }
            }
            return _statusManager;
        }

        #region 赋值与取值
        public T2 Get<T1, T2>(Func<T1, T2> func) where T1 : BaseStatus
        {
            var name = typeof(T1).Name;
            T1 status = SwitchHandle<T1>(name);
            return func(status);
        }

        public void Set<T>(Action<T> action) where T : BaseStatus
        {
            var name = typeof(T).Name;
            T status = SwitchHandle<T>(name);
            Mutex mutex = new Mutex(false, name);
            mutex.WaitOne();
            action(status);
            mutex.ReleaseMutex();
        }
        #endregion

        #region 扩展数据类型


        public RobotStatus _robotStatus { get; private set; } = new RobotStatus();
        public PLCStatus _plcStatus { get; private set; } = new PLCStatus();
        public SystemStatus _systemStatus { get; private set; } = new SystemStatus();
        public SystemConfig _systemConfig { get; private set; } = new SystemConfig();
        public SimulateConfig _simulateConfig { get; private set; } = new SimulateConfig();
        public ProgramConfig _programConfig { get; private set; } = new ProgramConfig();
        public ProductParam _productParam { get; private set; } = new ProductParam();

        private T SwitchHandle<T>(string name) where T : BaseStatus
        {
            T status = null;
            switch (name)
            {
                case "PLCStatus":
                    status = _plcStatus as T;
                    break;
                case "RobotStatus":
                    status = _robotStatus as T;
                    break;
                case "SystemStatus":
                    status = _systemStatus as T;
                    break;
                case "SystemConfig":
                    status = _systemConfig as T;
                    break;
                case "SimulateConfig":
                    status = _simulateConfig as T;
                    break;
                case "ProgramConfig":
                    status = _programConfig as T;
                    break;
                case "ProductParam":
                    status = _productParam as T;
                    break;

            }
            return status;
        }
        #endregion


    }


    public class BaseStatus { }
}
