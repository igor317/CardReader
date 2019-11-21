using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace CardReader
{
    class IpPortsEventArgs
    {
        public string Data { get; }
        public IpPortsEventArgs(string Data)
        {
            this.Data = Data;
        }
    }


    class IpPorts
    {
        #region PRIVATE METHODS
        private SerialPort m_Port;
        private string m_Buffer;
        private bool m_ReadData;

        public delegate void IpDataRecieved(object sender, IpPortsEventArgs e);
        public event IpDataRecieved IpPortDataRecieved;

       // private string 

        private void M_Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!m_ReadData)
                return;

            SerialPort port = (SerialPort)sender;

            m_Buffer += port.ReadExisting();

            if (IpPortDataRecieved != null)
                IpPortDataRecieved(this, new IpPortsEventArgs(m_Buffer));
        }

        #endregion

        public IpPorts(int BaudRate)
        {
            m_ReadData = true;
            m_Port = new SerialPort();
            m_Port.BaudRate = BaudRate;
            m_Port.DataReceived += M_Port_DataReceived;
        }

        public void SetBaudRate(int BaudRate)
        {
            m_Port.BaudRate = BaudRate;
        }

        public String[] GetPortList()
        {
            return SerialPort.GetPortNames();
        }

        public bool OpenPort(string portName)
        {
            try
            {
                m_Port.PortName = portName;
                m_Port.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ClosePort()
        {
            if (!m_Port.IsOpen)
                return false;
            m_Port.Close();
            return true;
        }

        public void SetReadState(bool state)
        {
            m_ReadData = state;
        }

        public String GetBuffer()
        {
            return m_Buffer;
        }

        public string GetPortName()
        {
            return m_Port.PortName;
        }
       // public void ClearBuffer()
    }
}
