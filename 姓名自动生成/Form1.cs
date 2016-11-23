using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace 姓名自动生成
{
    public partial class Form1 : Form
    {
        HotKeys h = new HotKeys();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hwnd, int wMsg, int wParam, StringBuilder lParam);

        private List<string> 姓名s = new List<string>();
        private List<string> 身份证号s = new List<string>();
        private List<string> 手机号s = new List<string>();

        public Form1()
        {
            InitializeComponent();

            checkBox1.Checked = true;
            
        }

        private void Regist()
        {
            var v = h.Regist(this.Handle, 0, Keys.Q, 姓名);
            if (!v)
            {
                MessageBox.Show("注册失败！请重新启动");
                return;
            }
            v = h.Regist(this.Handle, 0, Keys.A, 身份证号);
            if (!v)
            {
                MessageBox.Show("注册失败！请重新启动");
                return;
            }
            v = h.Regist(this.Handle, 0, Keys.Z, 手机号);
            if (!v)
            {
                MessageBox.Show("注册失败！请重新启动");
                return;
            }
        }

        private void UnRegist()
        {
            h.UnRegist(this.Handle, 姓名);
            h.UnRegist(this.Handle, 身份证号);
            h.UnRegist(this.Handle, 手机号);
        }

        protected override void WndProc(ref Message m)
        {
            //窗口消息处理函数
            h.ProcessHotKey(m);
            base.WndProc(ref m);
        }

        public void 姓名()
        {
            bool t = true;
            string str = "";

            while(t)
            {
                str = 自动生成.姓名();
                if (!姓名s.Contains(str))
                {
                    t = false;
                    姓名s.Add(str);
                }
            }

            Copy(str);
            Paste();
        }

        public void 身份证号()
        {            
            bool t = true;
            string str = "";

            while (t)
            {
                str = 自动生成.身份证号();
                if (!身份证号s.Contains(str))
                {
                    t = false;
                    身份证号s.Add(str);
                }
            }

            Copy(str);
            Paste();
        }

        public void 手机号()
        {
            bool t = true;
            string str = "";

            while (t)
            {
                str = 自动生成.手机号();
                if (!手机号s.Contains(str))
                {
                    t = false;
                    手机号s.Add(str);
                }
            }
            
            Copy(str);
            Paste();
        }

        private void Paste()
        {
            IDataObject iData = Clipboard.GetDataObject();
            string str = (String)iData.GetData(DataFormats.Text);
            SendKeys.Send(str);//(String)iData.GetData(DataFormats.Text)
        }

        //复制
        private void Copy(string str)
        {
            try
            {
                Clipboard.SetDataObject(str);
            }
            catch { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                Regist();
            else
                UnRegist();
        }
    }
}
