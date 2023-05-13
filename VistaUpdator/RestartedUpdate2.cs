using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VistaUpdater
{
    public partial class RestartedUpdate2 : Form
    {
        public RestartedUpdate2()
        {
            InitializeComponent();
        }

        Timer timer = new Timer();

        bool u1ended = false;
        bool u2ended = false;
        bool u3ended = false;

        private void RestartedUpdate2_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            Uri u = new Uri("http://catalog.s.download.windowsupdate.com/c/csa/csa/secu/2017/05/ie9-windows6.0-kb4018271-x64-custom_03cc4a6e57cecbe73ea5c9f45dc90a0cfe15eecb.msu");
            Uri u2 = new Uri("http://catalog.s.download.windowsupdate.com/d/msdownload/update/software/secu/2019/04/windows6.0-kb4493730-x64_5cb91f4e9000383f061b80f88feffdf228c2443c.msu");
            Uri u3 = new Uri("http://catalog.s.download.windowsupdate.com/d/msdownload/update/software/secu/2019/09/windows6.0-kb4474419-v4-x64_09cb148f6ef10779d7352b7269d66a7f23019207.msu");
            WebClient wc = new WebClient();
            WebClient wc2 = new WebClient();
            WebClient wc3 = new WebClient();
            wc.DownloadFileAsync(u, "C:\\Program Files\\VistaUpdater\\Update\\kb4018271.msu");
            listBox1.Items.Add("Windows Vista X64 Edition 用 Internet Explorer 9 の累積的なセキュリティ更新プログラム (KB4018271) をダウンロードしています...");
            wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
            wc2.DownloadFileAsync(u2, "C:\\Program Files\\VistaUpdater\\Update\\kb4493730.msu");
            listBox1.Items.Add("Windows Server 2008 for x64-Based Systems 用セキュリティ更新プログラム (KB4493730) をダウンロードしています...");
            wc2.DownloadFileCompleted += Wc2_DownloadFileCompleted;
            wc3.DownloadFileAsync(u3, "C:\\Program Files\\VistaUpdater\\Update\\kb4474419.msu");
            listBox1.Items.Add("2019-09 x64 ベース システム用 Windows Server 2008 のセキュリティ更新プログラム (KB4474419) をダウンロードしています...");
            wc3.DownloadFileCompleted += Wc3_DownloadFileCompleted;
            timer.Tick += Timer_Tick;
            timer.Interval = 100;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (u1ended & u2ended & u3ended)
            {
                timer.Stop();
                listBox1.Items.Add("ダウンロードに成功しました！");
                listBox1.Items.Add("インストールを開始...");
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.Arguments = "\"C:\\Program Files\\VistaUpdater\\Update\\kb4018271.msu\" /quiet /norestart";
                p.EnableRaisingEvents = true;
                p.SynchronizingObject = this;
                p.StartInfo.FileName = "wusa.exe";
                p.Exited += p_Exited;
                listBox1.Items.Add("Windows Vista X64 Edition 用 Internet Explorer 9 の累積的なセキュリティ更新プログラム (KB4018271) をインストールしています...");
                listBox1.Items.Add("コマンドの実行: " + p.StartInfo.Arguments);
                p.Start();
            }
        }

        private void p_Exited(object sender, EventArgs e)
        {
            listBox1.Items.Add("Windows Vista X64 Edition 用 Internet Explorer 9 の累積的なセキュリティ更新プログラム (KB4018271) をインストールしました");
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.Arguments = "\"C:\\Program Files\\VistaUpdater\\Update\\kb4493730.msu\" /quiet /norestart";
            p.EnableRaisingEvents = true;
            p.SynchronizingObject = this;
            p.StartInfo.FileName = "wusa.exe";
            p.Exited += p2_Exited;
            listBox1.Items.Add("Windows Server 2008 for x64-Based Systems 用セキュリティ更新プログラム (KB4493730) をインストールしています...");
            listBox1.Items.Add("コマンドの実行: " + p.StartInfo.Arguments);
            p.Start();
        }

        private void p2_Exited(object sender, EventArgs e)
        {
            listBox1.Items.Add("Windows Server 2008 for x64 - Based Systems 用セキュリティ更新プログラム (KB4493730) をインストールしました");
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.Arguments = "\"C:\\Program Files\\VistaUpdater\\Update\\kb4474419.msu\" /quiet /norestart";
            p.EnableRaisingEvents = true;
            p.SynchronizingObject = this;
            p.StartInfo.FileName = "wusa.exe";
            p.Exited += p3_Exited;
            listBox1.Items.Add("2019-09 x64 ベース システム用 Windows Server 2008 のセキュリティ更新プログラム (KB4474419) をインストールしています...");
            listBox1.Items.Add("コマンドの実行: " + p.StartInfo.Arguments);
            p.Start();
        }

        private void p3_Exited(object sender, EventArgs e)
        {
            listBox1.Items.Add("2019-09 x64 ベース システム用 Windows Server 2008 のセキュリティ更新プログラム (KB4474419) をインストールしました");
            label2.Text = "最終処理の実行中...";
            listBox1.Items.Add("Shellの設定中...");
            Microsoft.Win32.RegistryKey regkey1 =
                Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\VistaUpdater");
            regkey1.SetValue("Restarted", 3, Microsoft.Win32.RegistryValueKind.QWord);
            regkey1.Close();
            listBox1.Items.Add("Shellの設定が完了しました");
            listBox1.Items.Add("再起動中...");
            label2.Text = "再起動しています...";
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "shutdown.exe";
            p.StartInfo.Arguments = "/r /t 10 /c \"Vista Updater の処理を続行するため、10秒後に再起動します。\"";
            p.Start();
        }

        private void Wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                listBox1.Items.Add("Windows Vista for x64-based Systems 用プラットフォーム更新プログラム補足 (KB2117917) のダウンロードに失敗...");
            }
            else
            {
                listBox1.Items.Add("Windows Vista for x64-based Systems 用プラットフォーム更新プログラム補足 (KB2117917) のダウンロードに成功！");
                u1ended = true;
            }
        }

        private void Wc2_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                listBox1.Items.Add("Windows Server 2008 for x64-Based Systems 用セキュリティ更新プログラム (KB4493730) のダウンロードに失敗...");
            }
            else
            {
                listBox1.Items.Add("Windows Server 2008 for x64-Based Systems 用セキュリティ更新プログラム (KB4493730) のダウンロードに成功！");
                u2ended = true;
            }
        }

        private void Wc3_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                listBox1.Items.Add("2019-09 x64 ベース システム用 Windows Server 2008 のセキュリティ更新プログラム (KB4474419) のダウンロードに失敗...");
            }
            else
            {
                listBox1.Items.Add("2019-09 x64 ベース システム用 Windows Server 2008 のセキュリティ更新プログラム (KB4474419) のダウンロードに成功！");
                u3ended = true;
            }
        }

        private void RestartedUpdate2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
