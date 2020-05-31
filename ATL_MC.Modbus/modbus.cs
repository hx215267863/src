using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ATL_MC.Modbus
{
    public class ModbusRTU
    {
        private  Mutex m_ModbusRTU = null;

        private SerialPort m_SerialPort = null; 
        
        private Byte machineAddr = 0;

        public ModbusRTU()
        {
            machineAddr = 0;
            m_ModbusRTU = new Mutex();
            m_SerialPort = new SerialPort();
            m_SerialPort.WriteTimeout = 200;
            m_SerialPort.ReadTimeout = 500;
        }
        private uint CalcCRC16(byte[] modbus, uint Length)
        {
            uint i, j;
            uint crc16 = 0xFFFF;
            for (i = 0; i < Length; i++)
            {
                crc16 ^= modbus[i];
                for (j = 0; j < 8; j++)
                {
                    if ((crc16 & 0x01) == 1)
                    {
                        crc16 = (crc16 >> 1) ^ 0xA001;
                    }
                    else
                    {
                        crc16 = crc16 >> 1;
                    }
                }
            }
            return crc16;
        }

        public bool OponCOM( string strCom, int BaudRate)
        {
            try
            {
                m_SerialPort.PortName = strCom;
                m_SerialPort.BaudRate = BaudRate;
                m_SerialPort.StopBits = StopBits.One;
                m_SerialPort.DataBits = 8;
                m_SerialPort.Parity = Parity.Even;
                m_SerialPort.ReadTimeout = 500;
                m_SerialPort.RtsEnable = true;

                m_SerialPort.Open();
                if (!m_SerialPort.IsOpen)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool CloseCOM()
        {
            m_SerialPort.Close();
            if (m_SerialPort.IsOpen)
            {
                return false;
            }
            return true;
        }

        public void SetMachineAddr(Byte addr)
        {
            machineAddr = addr;
        }

        /*
        功      能：    读位
        参      数：    PLC地址，位地址，存储所用变量
        返  回  值：    true:成功 false：失败
        */
        public bool ReadBool( ushort address, out bool value)
        {
            value = false;
            byte[] cmd = new byte[8] { 1, 1, 0, 0, 0, 1, 0, 0 };
            byte[] buf = new byte[6];
            Array.Clear(buf, 0, buf.Length);

            cmd[0] = machineAddr;
            cmd[2] = (byte)(address >> 8);
            cmd[3] = (byte)((address << 8) >> 8);
            uint crc = CalcCRC16(cmd, 6);
            cmd[6] = (byte)((crc << 8) >> 8);
            cmd[7] = (byte)(crc >> 8);

            int rlen = 0;

            m_ModbusRTU.WaitOne();
            try
            {
                m_SerialPort.DiscardInBuffer();
                m_SerialPort.Write(cmd, 0, 8);
                Thread.Sleep(100);
                rlen = m_SerialPort.Read(buf, 0, 6);     
            }
            catch (Exception)
            {
                m_ModbusRTU.ReleaseMutex();
                return false;
            }

            m_ModbusRTU.ReleaseMutex();
            if ((buf[3] & 0x01) == 0x01)
            {
                value = true;
            }
            else
            {
                value = false;
            }
            if (rlen != 6)
            {
                return false;
            }

            return true;
        }

        /*
        功      能：    写位
        参      数：    PLC地址，位地址，所写入数据
        返  回  值：    true:成功 false：失败
        */
        public bool WriteBool( ushort address, bool value)
        {
            byte[] cmd = new byte[8] { 1, 5, 0, 0, 0, 1, 0, 0 };
            byte[] buf = new byte[8];

            Array.Clear(buf, 0, buf.Length);

            cmd[0] = machineAddr;
            cmd[2] = (byte)(address >> 8);
            cmd[3] = (byte)((address << 8) >> 8);
            if (value)
            {
                cmd[4] = 0xFF;
                cmd[5] = 0x00;
            }
            else
            {
                cmd[4] = 00;
                cmd[5] = 00;
            }
            uint crc = CalcCRC16(cmd, 6);
            cmd[6] = (byte)((crc << 8) >> 8);
            cmd[7] = (byte)(crc >> 8);

            int rlen = 0;

            m_ModbusRTU.WaitOne();
            
            try
            {
                m_SerialPort.DiscardInBuffer();
                m_SerialPort.Write(cmd, 0, 8);
                Thread.Sleep(120);
                rlen = m_SerialPort.Read(buf, 0, 8);
                
            }catch(Exception)
            {
                m_ModbusRTU.ReleaseMutex();
                return false;
            }

            m_ModbusRTU.ReleaseMutex();
            if (rlen != 8)
            {
                return false;
            }
            return true;
        }

        /*
        功      能：    读字
        参      数：    PLC地址，位地址，存储所用变量
        返  回  值：    true:成功 false：失败
        */
        public bool ReadRegister32Bit( ushort address, out uint value)
        {
            value = 0;
            byte[] cmd = new byte[8] { 1, 3, 0, 0, 0, 2, 0, 0 };
            byte[] buf = new byte[9];

            Array.Clear(buf, 0, buf.Length);
            int rlen = 0;

            cmd[0] = machineAddr;
            cmd[2] = (byte)(address >> 8);
            cmd[3] = (byte)((address << 8) >> 8);

            uint crc = CalcCRC16(cmd, 6);
            cmd[6] = (byte)((crc << 8) >> 8);
            cmd[7] = (byte)(crc >> 8);


            m_ModbusRTU.WaitOne();

            try
            {
                m_SerialPort.DiscardInBuffer();
                m_SerialPort.Write(cmd, 0, 8);
                Thread.Sleep(100);
                rlen = m_SerialPort.Read(buf, 0, 9);
               
            }
            catch (Exception)
            {
                m_ModbusRTU.ReleaseMutex();
                return false;
            }

            m_ModbusRTU.ReleaseMutex();
            if (rlen != 9)
            {
                return false;
            }
            if (buf[0] == cmd[0] && buf[1] == 3 && buf[2] == 4)
            {
                uint[] hTemp = new uint[4] { buf[5], buf[6], buf[3], buf[4] };
                value = (hTemp[0] << 24) + (hTemp[1] << 16) + (hTemp[2] << 8) + hTemp[3];
            }

            return true;
        }

        public bool ReadRegister16Bit(ushort address, out uint value)
        {
            value = 0;
            byte[] cmd = new byte[8] { 1, 3, 0, 0, 0, 1, 0, 0 };
            byte[] buf = new byte[7];
            Array.Clear(buf, 0, buf.Length);
            int rlen = 0;

            cmd[0] = machineAddr;
            cmd[2] = (byte)(address >> 8);
            cmd[3] = (byte)((address << 8) >> 8);

            uint crc = CalcCRC16(cmd, 6);
            cmd[6] = (byte)((crc << 8) >> 8);
            cmd[7] = (byte)(crc >> 8);

            m_ModbusRTU.WaitOne();
            try
            {
                m_SerialPort.DiscardInBuffer();
                m_SerialPort.Write(cmd, 0, 8);
                Thread.Sleep(100);
                rlen = m_SerialPort.Read(buf, 0, 7);
            }
            catch (Exception)
            {
                m_ModbusRTU.ReleaseMutex();
                return false;
            }

            m_ModbusRTU.ReleaseMutex();
            if (rlen != 7)
            {
                return false;
            }
            if (buf[0] == cmd[0] && buf[1] == 3 && buf[2] == 2)
            {
                uint[] hTemp = new uint[2] { buf[3], buf[4] };
                value = (hTemp[0] << 8) + hTemp[1];
            }

            return true;
        }

        /*
        功      能：    写字
        参      数：    PLC地址，位地址，所写入数据
        返  回  值：    true:成功 false：失败
        */
        public bool WriteRegister( ushort address, short value)
        {
            byte[] cmd = new byte[8] { 1, 6, 0, 0, 0, 1, 0, 0 };
            byte[] buf = new byte[8];

            Array.Clear(buf, 0, buf.Length);
            int rlen = 0;

            cmd[0] = machineAddr;

            cmd[2] = (byte)(address >> 8);
            cmd[3] = (byte)((address << 8) >> 8);
            cmd[4] = (byte)(value >> 8);
            cmd[5] = (byte)(value & 0xFF);

            uint crc = CalcCRC16(cmd, 6);
            cmd[6] = (byte)((crc << 8) >> 8);
            cmd[7] = (byte)(crc >> 8);

            m_ModbusRTU.WaitOne();
            try
            {
                m_SerialPort.DiscardInBuffer();
                m_SerialPort.Write(cmd, 0, 8);
                Thread.Sleep(100);
                rlen = m_SerialPort.Read(buf, 0, 8); 
            }
            catch (Exception)
            {
                m_ModbusRTU.ReleaseMutex();
                return false;
            }

            m_ModbusRTU.ReleaseMutex();
            if (rlen != 8)
            {
                return false;
            }
            return true;
        }
    }
}
