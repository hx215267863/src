using ETHAN.PLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.KEYENCE.PLC
{


    public class KeyenceNetPLC
    {
        private string _plcIp;
        private KvComm _mainPLC;
        private bool _simulate;
        public KeyenceNetPLC(string ipString, bool simulate)
        {
            _simulate = simulate;
            _plcIp = ipString;
           
        }
        public bool Connect()
        {
          
            if (_simulate) return true;
            try
            {
                _mainPLC = new KvComm(_plcIp);
                if (!_mainPLC.Connected)
                {
                    _mainPLC.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                _mainPLC.Close();
                throw new Exception("PLC连接异常" + ex);
            }
        }
        public bool WriteUshort(string partName, ushort value)
        {
            if (_simulate) return true;
            try
            {              
                return _mainPLC.WriteUshort(partName, value);
            }
            catch (Exception ex)
            {
                throw new Exception("WriteUshort异常" + ex);
            }
        }
        public bool WriteAscString(string partName, string msg, int writeCount = 0)
        {
            if (_simulate) return true;
            try
            {
                return _mainPLC.WriteAscString(partName, msg, writeCount);
            }
            catch (Exception ex)
            {
                throw new Exception("WriteAscString异常" + ex);
            }
        }
        public bool WriteDouble(string partName, double value)
        {
            if (_simulate) return true;
            try
            {
                return _mainPLC.WriteDouble(partName, value);
            }
            catch (Exception ex)
            {
                throw new Exception("WriteDouble异常" + ex);
            }
        }
        public bool ReadBool(string relayName)
        {
            if (_simulate) return true;
            try
            {
                return _mainPLC.ReadBool(relayName);
            }
            catch (Exception ex)
            {
                throw new Exception("ReadBool异常" + ex);
            }
        }
        public string ReadAscString(string partName, int readLength)
        {
            if (_simulate) return "simulate";
            try
            {
                return _mainPLC.ReadAscString(partName, readLength);
            }
            catch (Exception ex)
            {
                throw new Exception("ReadAscString异常" + ex);
            }
        }
        public Dictionary<string, bool> DicBoolRead(string relayName, int readCount)
        {
            if (_simulate) return new Dictionary<string, bool>();
            try
            {
                return _mainPLC.DicBoolRead(relayName, readCount);
            }
            catch (Exception ex)
            {
                throw new Exception("DicBoolRead异常" + ex);
            }
        }



        public void Close()
        {
            if (_simulate) return;
            try
            {
                _mainPLC.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("PLC Close异常" + ex);
            }
        }

        /// <summary>
        /// 仅测试用,需要具体实现
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            if (_simulate) return 0;
            try
            {
                return 100;
                //  _mainPLC.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("PLC 异常" + ex);
            }
        }
        /// <summary>
        /// 仅测试用,需要具体实现
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {
            if (_simulate)
            {
                return true;
            }

            return true;
        }


    }
}
