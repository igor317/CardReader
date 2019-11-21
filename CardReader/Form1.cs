using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardReader
{
    public partial class Form1 : Form
    {
        private IpPorts Port;

        private void UpdatePortList()
        {
            listBox1.Items.Clear();

            String[] v = Port.GetPortList();

            for (int i = 0; i < v.Length; ++i)
                listBox1.Items.Add(v[i]);
        }

        public Form1()
        {
            InitializeComponent();
            Port = new IpPorts(9600);
            Port.IpPortDataRecieved += Port_IpPortDataRecieved;

        }

        private void Port_IpPortDataRecieved(object sender, IpPortsEventArgs e)
        {
            richTextBox1.Invoke(new Action(() => richTextBox1.Text = e.Data));
           // richTextBox1.Text = e.Data;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdatePortList();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string PortName = listBox1.Items[listBox1.SelectedIndex].ToString();
            if (Port.OpenPort(PortName))
                listBox2.Items.Add(PortName + " successfully opened");
            else
                listBox2.Items.Add("Cannot open port");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Port.ClosePort())
                listBox2.Items.Add(Port.GetPortName() + " successfully closed");
            else
                listBox2.Items.Add("Cannot close port");
        }
    }
}
