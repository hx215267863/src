using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.KEYENCE_PLC
{
    public class KEYENCE_PLC
    {
        private bool bSimulate = true;
        private ATL_MC.Modbus.ModbusRTU m_modbus = new ATL_MC.Modbus.ModbusRTU();

        public bool Init(string com,bool simulate)
        {
            bool iRes = false;
            bSimulate = simulate;
            if(bSimulate)
            {
                return true;
            }
            m_modbus.SetMachineAddr(1);

            try
            {
                iRes = m_modbus.OponCOM(com, 115200);
                return iRes;
            }
            catch
            {
                return false;
            }
            
        }

        public void Close()
        {
            if (bSimulate)
            {
                return;
            }
            m_modbus.CloseCOM();
        }

        public bool ReadBool(ushort addr,out bool value)
        {
            value = false;
            if (bSimulate)
            {
                return true;
            }
            return m_modbus.ReadBool(addr, out value);
        }
        public bool WriteBool(ushort addr, bool value)
        {
            if (bSimulate)
            {
                return true;
            }
            return m_modbus.WriteBool(addr, value);
        }
        public bool ReadRegister(ushort addr,out uint value)
        {
            value = 0;
            if (bSimulate)
            {
                return true;
            }
            return m_modbus.ReadRegister16Bit(addr,out value);
        }

        public bool WriteRegister(ushort addr, short value)
        {
            if (bSimulate)
            {
                return true;
            }
            return m_modbus.WriteRegister(addr, value);
        }


        //启动机械手程序
        public bool ResetScaraRobot()
        {
            if (bSimulate)
            {
                return true;
            }

            bool result = false;
            result = m_modbus.WriteRegister(42, 0);

            result = m_modbus.WriteBool(16714, true);  
            
            result = m_modbus.WriteBool(16715, true);
            return result;
        }

        public bool NGBoxIsFull()
        {
            if (bSimulate)
            {
                return true;
            }
            return m_modbus.WriteBool(16717, true);
        }

        public bool NGBoxIsMissing()
        {
            if (bSimulate)
            {
                return true;
            }
            return m_modbus.WriteBool(16718, true);
        }

        public bool GetScaraRunningStatus(out bool status)
        {
            if (bSimulate)
            {
                status = true;
                return true;
            }
            status = false;
            bool result = false;
            result = m_modbus.ReadBool(16545, out status);

            return result;
        }

        public bool PCError()
        {
            if (bSimulate)
            {
                return true;
            }

            bool result = false;
            result = m_modbus.WriteRegister(44, 1);
            return result;
        }

        public bool PCCriticalError()
        {
            if (bSimulate)
            {
                return true;
            }

            bool result = false;
            result = m_modbus.WriteRegister(46, 1);
            return result;
        }

        //通知PLC机械手复位完成
        public bool ScaraHomeFinished()
        {
            if (bSimulate)
            {
                return true;
            }

            return m_modbus.WriteBool(16704, true);
        }

        //通知PLC开始回零
        public bool StartHome()
        {
            if (bSimulate)
            {
                return true;
            }
            return m_modbus.WriteBool(16707, true);
        }

        //通知PLC报警清除
        public bool Reset()
        {
            if (bSimulate)
            {
                return true;
            }
            return m_modbus.WriteBool(16705, true);
        }

        //清盘
        public bool StartClear()
        {
            if (bSimulate)
            {
                return true;
            }
            return m_modbus.WriteBool(16710, true);
        }

        public bool EndClear()
        {
            if (bSimulate)
            {
                return true;
            }
            return m_modbus.WriteBool(16706, true);
        }

        //获取PLC报警代码
        public bool GetErrorCode(out ulong ErrorCode )
        {
            ErrorCode = 0;
            bool result = false;
            if (bSimulate)
            {
                return true;
            }
            uint value1 = 0;
            uint value2 = 0;
            uint value3 = 0;
            result = m_modbus.ReadRegister16Bit(8, out value1);
            result = m_modbus.ReadRegister16Bit(10, out value2);
            result = m_modbus.ReadRegister16Bit(24, out value3);

            if(result)
            {
                ErrorCode = (( value3 & 0xFF00 ) << 32 ) +
                            ( value2 << 16 ) +
                            value1;
            }

            return result;
        }

        public bool GetCount(out uint Count)
        {
            Count = 0;
            bool result = false;
            if (bSimulate)
            {
                return true;
            }

            uint value1 = 0;

            result = m_modbus.ReadRegister16Bit(26, out value1);

            if (result)
            {
                Count = value1;
            }

            return result;
        }

        public bool Start()
        {
            if (bSimulate)
            {
                return true;
            }

            return m_modbus.WriteBool(16708, true);
        }

        public bool Pause()
        {
            if (bSimulate)
            {
                return true;
            }

            return m_modbus.WriteBool(16711, true);
        }

        public bool GetBatteryPosFailed()
        {
            if (bSimulate)
            {
                return true;
            }

            return m_modbus.WriteBool(16712, true);
        }

        public bool GetTrayVosionError()
        {
            if (bSimulate)
            {
                return true;
            }

            return m_modbus.WriteBool(16713, true);
        }

        public bool Stop()
        {
            if (bSimulate)
            {
                return true;
            }

            return m_modbus.WriteBool(16709, true);
        }


        public bool GetHomeStatus(out bool status)
        {
            if (bSimulate)
            {
                status = true;
                return true;
            }
            status = false;
            bool result = false;
            result = m_modbus.ReadBool(16544,out status);

            return result;
        }

        public bool GetClearEnd(out bool status)
        {
            if (bSimulate)
            {
                status = true;
                return true;
            }
            status = false;
            bool result = false;
            result = m_modbus.ReadBool(16546, out status);

            return result;
        }

        public bool SetCount(int count)
        {
            if (bSimulate)
            {
                return true;
            }
            bool result = false;

            uint aaa = 0;

            if (!m_modbus.ReadRegister16Bit(14, out aaa))
            {
                return false;
            }

            if (aaa != count)
            {
                result = m_modbus.WriteRegister(14, (short)count);
            }

            return result;
        }

        //ljx 2019-11-18
        public uint GetGreenLight()
        {
            uint status = 0;
            ReadRegister(75, out status);
            return status;
        }

        public uint GetYellowLight()
        {
            uint status = 0;
            ReadRegister(74, out status);
            return status;
        }

        public uint GetRedLight()
        {
            uint status = 0;
            ReadRegister(76, out status);
            return status;
        }

        public uint GetDTTYPE()
        {
            uint res;
            ReadRegister(79, out res);
            return res;
        }
    }
}
