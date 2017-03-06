﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceStack.Redis;
using System.IO;

namespace INDNC
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        

        ServerLink serverlink = new ServerLink();
        RedisClient Client;
        bool ClientDispose = false;
        UserControlMachineState machinestate;

        public struct machine
        {
            UInt16 MachineIndex;
            String MachineState;
            String MachineAlarm;
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            for(int i = 1; i <= 4; ++i)
            {
                //检验输入是否为数字，数字是否介于0~255之间
                int tmp = -1;
                TextBox objText = (TextBox)this.Controls["textBox" + i.ToString()];
                if (int.TryParse(objText.Text, out tmp) != true)
                {
                    MessageBox.Show("错误:服务器IP地址输入错误，请重新输入！", "ERROR");
                    return;
                }
                else if(!(tmp>=0 && tmp <= 255))
                {
                    MessageBox.Show("错误:服务器IP地址输入错误，请重新输入！", "ERROR");
                    return;
                }
                    
            }
            serverlink.ServerIPAddressName= textBox1.Text + '.' + textBox2.Text + '.' + textBox3.Text + '.' + textBox4.Text;
            //serverlink.ServerIPAddress = textBox1.Text + '.' + textBox2.Text + '.' + textBox3.Text + '.' + textBox4.Text;
            int port = 0;
            if (int.TryParse(textBox5.Text, out port) != true)
            {
                MessageBox.Show("错误:服务器端口号输入错误，请重新输入！", "ERROR");
                return;
            }
            serverlink.ServerPortName = port;
            serverlink.ServerPasswordName = textBox6.Text;
            try
            {
                serverlink.Link(ref (Client));
                ClientDispose = false;
            }
            catch(Exception)
            {
                return;
            }
            MessageBox.Show("1");

            machinestate = new UserControlMachineState();
            machinestate.Visible = true;
            machinestate.Dock = DockStyle.Fill;
            machinestate.ListViewDraw(ref (Client), ref (serverlink));
            this.panel1.Controls.Add(machinestate);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ClientDispose)
                return;
            //MessageBox.Show(Client.Lists);
            try
            {
                Client.Dispose();
                ClientDispose = true;
                long tmp = Client.DbSize;
                MessageBox.Show(tmp.ToString());
                machinestate.Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message, "Error");
            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsNumber(e.KeyChar)) || e.KeyChar == (char)8)
                e.Handled = false;
            else if(e.KeyChar == (char)13)
            {
                button1.Focus();
                button1_Click_1(sender, e);
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("请输入数字", "ERROR");
            }       
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsNumber(e.KeyChar)) || e.KeyChar == (char)8)
                e.Handled = false;
            else if (e.KeyChar == (char)13)
            {
                button1.Focus();
                button1_Click_1(sender, e);
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("请输入数字", "ERROR");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsNumber(e.KeyChar)) || e.KeyChar == (char)8)
                e.Handled = false;
            else if (e.KeyChar == (char)13)
            {
                button1.Focus();
                button1_Click_1(sender, e);
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("请输入数字", "ERROR");
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsNumber(e.KeyChar)) || e.KeyChar == (char)8)
                e.Handled = false;
            else if (e.KeyChar == (char)13)
            {
                button1.Focus();
                button1_Click_1(sender, e);
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("请输入数字", "ERROR");
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsNumber(e.KeyChar)) || e.KeyChar == (char)8 )
                e.Handled = false;
            else if (e.KeyChar == (char)13)
            {
                button1.Focus();
                button1_Click_1(sender, e);
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("请输入数字", "ERROR");
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1.Focus();
                button1_Click_1(sender, e);
            }
        }
    }
    public partial class ServerLink
    {
        private String ServerIPAddress = null;
        private int ServerPort = 0;
        private String ServerPassword = null;
        private long DBNo = 0;

        public String ServerIPAddressName
        {
            get{ return ServerIPAddress; }
            set { ServerIPAddress = value; }
        }

        public int ServerPortName
        {
            get { return ServerPort; }
            set { ServerPort = value; }
        }

        public String ServerPasswordName
        {
            get { return ServerPassword; }
            set { ServerPassword = value; }
        }

        public long DBNoName
        {
            get { return DBNo; }
            set { DBNo = value; }
        }
        public UInt16 Link(ref RedisClient Client)
        {
            try
            {
                long tmp = -1;
                Client = new RedisClient(ServerIPAddress, ServerPort, ServerPassword, DBNo);
                tmp = Client.DbSize;
                if(tmp!=-1)
                    Properties.Settings.Default.Save();
                //MessageBox.Show(tmp.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message, "ERROR");
                ServerIPAddress = null;
                ServerPort = 0;
                ServerPassword = null;
                DBNo = 0;
                return 1;
            } 

            return 0;
        }

    }

    public enum ERRORMESSAGE
    {
        NOERR=0
    }

}
