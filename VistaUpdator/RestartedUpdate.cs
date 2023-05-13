using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace VistaUpdater
{
    public partial class RestartedUpdate : Form
    {
        public RestartedUpdate()
        {
            InitializeComponent();
        }

        private void RestartedUpdate_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo("C:\\Program Files\\Internet Explorer\\iexplore.exe");
            if (myFileVersionInfo.FileVersion.Contains("9.0"))
            {
                GoNext();
            } else
            {
                label2.Text = "Internet Explorer 9 をインストールしています...";
                Uri u = new Uri("http://catalog.s.download.windowsupdate.com/msdownload/update/software/uprl/2011/03/wu-ie9-windowsvista-x64_f599c02e7e1ea8a4e1029f0e49418a8be8416367.exe");
                WebClient wc = new WebClient();
                wc.DownloadFileAsync(u, "C:\\Program Files\\VistaUpdater\\Update\\ie9.exe");
                listBox1.Items.Add("x64 ベース システム Windows Vista 用 Windows Internet Explorer 9 をダウンロードしています...");
                wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
                GoNext();
            }
        }

        private void Wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            listBox1.Items.Add("x64 ベース システム Windows Vista 用 Windows Internet Explorer 9 をダウンロードしました");
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "C:\\Program Files\\VistaUpdater\\Update\\ie9.exe";
            process.StartInfo.Arguments = "/quiet /norestart";
            process.EnableRaisingEvents = true;
            process.SynchronizingObject = this;
            process.Exited += Process_Exited;
            listBox1.Items.Add("x64 ベース システム Windows Vista 用 Windows Internet Explorer 9 をインストールしています...");
            listBox1.Items.Add("コマンドの実行: " + process.StartInfo.Arguments);
            process.Start();
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            listBox1.Items.Add("x64 ベース システム Windows Vista 用 Windows Internet Explorer 9 をインストールしました");
            GoNext();
        }

        private void GoNext()
        {
            label2.Text = "Windows Update Agent のインストール";

            Uri u = new Uri("https://archive.org/download/windows-update-agent-7.6/WindowsUpdateAgent-7.6-x64.exe");
            WebClient wc = new WebClient();
            wc.DownloadFileAsync(u, "C:\\Program Files\\VistaUpdater\\Update\\wuagent.exe");
            listBox1.Items.Add("Windows Update Agent 7.6 をダウンロードしています...");
            wc.DownloadFileCompleted += Wc2_DownloadFileCompleted;
        }

        private void Wc2_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            listBox1.Items.Add("Windows Update Agent 7.6 をダウンロードしました");
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "C:\\Program Files\\VistaUpdater\\Update\\wuagent.exe";
            process.StartInfo.Arguments = "/quiet /norestart";
            process.EnableRaisingEvents = true;
            process.SynchronizingObject = this;
            process.Exited += Process2_Exited;
            listBox1.Items.Add("Windows Update Agent 7.6 をインストールしています...");
            listBox1.Items.Add("コマンドの実行: " + process.StartInfo.Arguments);
            process.Start();
        }

        private void Process2_Exited(object sender, EventArgs e)
        {
            label2.Text = "最終処理の実行中...";
            listBox1.Items.Add("Shellの設定中...");
            Microsoft.Win32.RegistryKey regkey1 =
                Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\VistaUpdater");
            regkey1.SetValue("Restarted", 2, Microsoft.Win32.RegistryValueKind.QWord);
            regkey1.Close();
            listBox1.Items.Add("Shellの設定が完了しました");
            listBox1.Items.Add("再起動中...");
            label2.Text = "再起動しています...";
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "shutdown.exe";
            p.StartInfo.Arguments = "/r /t 10 /c \"Vista Updater の処理を続行するため、10秒後に再起動します。\"";
            p.Start();
        }

        private void RestartedUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
