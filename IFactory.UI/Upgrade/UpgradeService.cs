using IFactory.Common.Logs;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Xml.Linq;

namespace IFactory.UI.Upgrade
{
    public class UpgradeService : IDisposable
    {
        private bool isStop = true;
        private static UpgradeService instance = new UpgradeService();
        private bool alive;
        private int sleepIndex;

        public static UpgradeService Instance
        {
            get
            {
                return instance;
            }
        }

        public bool IsStop
        {
            get
            {
                return this.isStop;
            }
        }

        public bool Alive
        {
            get
            {
                return this.alive;
            }
        }

        public void Start()
        {
            if (this.alive)
                return;
            this.alive = true;
            new Thread(new ThreadStart(this.Run))
            {
                IsBackground = true
            }.Start();
        }

        private void RunBegin()
        {
        }

        private void RunMain()
        {
            this.isStop = false;
            while (!this.isStop)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    string str = ConfigurationManager.AppSettings["ServerRootUrl"];
                    XDocument xdocument = XDocument.Parse(httpClient.GetStringAsync(str + "/Upgrade/SmartClient/version.xml").Result);

                    string content = xdocument.Root.Element("versionCode").Value;
                    string str3 = xdocument.Root.Element("fileName").Value;
                    string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updater\\version.xml");
                    if (content != Assembly.GetExecutingAssembly().GetName().Version.ToString())
                    {
                        string strB = null;
                        if (File.Exists(text))
                            strB = XDocument.Load(text).Root.Element("versionCode").Value;
                        if (content.CompareTo(strB) > 0)
                        {
                            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updater\\Versions")))
                                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updater\\Versions"));

                            byte[] result = httpClient.GetByteArrayAsync(str + "/Upgrade/SmartClient/" + str3).Result;
                            File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updater\\Versions\\" + str3), result);

                            XElement xElement = new XElement("root", new object[]
                            {
                                new XElement("versionCode", content),
                                new XElement("fileName", str3)
                            });
                            new XDocument(new object[]
                            {
                                xElement
                            }).Save(text);
                            Application.Current.Dispatcher.Invoke((Action)(() =>
                            {
                                if (MessageBox.Show("有新版本，是否立刻升级？", "提示", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                                    return;
                                UpgradeHelper.CheckNewVersion();
                            }));
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.LogError("获取升级版本出错", ex);
                }
                int num = 60;
                this.sleepIndex = 0;
                while (this.sleepIndex < num && !this.isStop)
                {
                    Thread.Sleep(1000);
                    this.sleepIndex++;
                }
            }
        }

        private void RunEnd()
        {
        }

        protected void Run()
        {
            try
            {
                this.RunBegin();
                this.RunMain();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                this.RunEnd();
            }
            this.alive = false;
        }

        public void Stop()
        {
            this.isStop = true;
        }

        public void Dispose()
        {
            this.Stop();
            GC.Collect();
        }
    }
}
