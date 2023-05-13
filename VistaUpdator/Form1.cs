using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VistaUpdater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartUpdate su = new StartUpdate();
            su.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Check64() & System.Environment.OSVersion.Version.Minor != 0)
            {
                Compatibility(false, false);
            }
            if (Check64() & System.Environment.OSVersion.Version.Minor != 0)
            {
                Compatibility(true, false);
            }
            if (!Check64() & System.Environment.OSVersion.Version.Minor == 0)
            {
                Compatibility(true, false);
            }
        }

        private void Compatibility(bool bit, bool os)
        {
            if (!bit & !os)
            {
                MessageBox.Show("結果: 不合格\nOS は 64bit で動作していません。\nOS は Windows Vista で動作していません", "互換性チェック - VistaUpdater", MessageBoxButtons.OK, MessageBoxIcon.Question);
                Application.Exit();
            }
            if (bit)
            {
                MessageBox.Show("結果: 不合格\nOS は 64bit で動作しています。\nOS は Windows Vista で動作していません", "互換性チェック - VistaUpdater", MessageBoxButtons.OK, MessageBoxIcon.Question);
                Application.Exit();
            }
            if (os)
            {
                MessageBox.Show("結果: 不合格\nOS は 64bit で動作していません。\nOS は Windows Vista で動作しています", "互換性チェック - VistaUpdater", MessageBoxButtons.OK, MessageBoxIcon.Question);
                Application.Exit();
            }
        }

        private bool Check64()
        {
            System.Management.ManagementObject mo =
                 new System.Management.ManagementObject("Win32_Processor.DeviceID='CPU0'");
            ushort addWidth = (ushort)mo["AddressWidth"];
            if (addWidth == 64)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
