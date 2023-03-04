using SegedFunkciok;
using HtmlSegedFunkciok;

using CefSharp;
using CefSharp.WinForms;
using HtmlAgilityPack;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace FormosWeboldalForraskodKigyujtes
{
    public partial class KigyujtoForm : Form
    {
        protected ChromiumWebBrowser bongeszo;
        public string KigyujtottHtml = null;
        protected bool kigyujtesFolyamatban = false;
        protected bool atiranyitasFolyamatban = true;
        public bool Betoltodott = true;
        public int MaxKigyujtesiIdo = 30;
        public int MaxAtiranyitasiIdo = 30;
        public bool Letrehozodott = false;
        public String Url
        {
            get
            {
                return bongeszo.Address;
            }
        }
        public bool Utf8Convert = false;

        public KigyujtoForm(int maxKigyujtesiIdo = 30, int maxAtiranyitasiIdo = 30, bool utf8Convert = false)
        {
            InitializeComponent();
            bongeszoLetrehozas();
            MaxKigyujtesiIdo = maxKigyujtesiIdo;
            MaxAtiranyitasiIdo = maxAtiranyitasiIdo;
            Utf8Convert = utf8Convert;
        }

        private void bongeszoLetrehozas()
        {
            if (!Cef.IsInitialized)
            {
                CefSettings cefSettings = new CefSettings();
                //cefSettings.BrowserSubprocessPath = @"x86\CefSharp.BrowserSubprocess.exe";
                Cef.Initialize(cefSettings);
            }
            bongeszo = new ChromiumWebBrowser();
            this.Controls.Add(bongeszo);
            bongeszo.Dock = DockStyle.Fill;
            bongeszo.FrameLoadEnd += atiranyitasBefejezodott;
            bongeszo.LoadError += betoltesiHiba;
        }

        private void betoltesiHiba(object sender, LoadErrorEventArgs e)
        {
            atiranyitasFolyamatban = false;
        }

        private void atiranyitasBefejezodott(object sender, FrameLoadEndEventArgs e)
        {
            atiranyitasFolyamatban = false;
            Betoltodott = true;
        }

        public HtmlNode Kigyujtes(string url)
        {
            Atiranyitas(url);
            return Kigyujtes();
        }

        public HtmlNode Kigyujtes()
        {
            if (Betoltodott)
            {
                int aktualisKigyujtesnelElteltIdo = 0;
                string elozolegKigyujtottHtml;
                do
                {
                    elozolegKigyujtottHtml = KigyujtottHtml;
                    Thread.Sleep(2000);
                    aktualisKigyujtesnelElteltIdo += 2;
                    kigyujtoJavascriptLefuttatasa();
                } while (elozolegKigyujtottHtml != KigyujtottHtml && aktualisKigyujtesnelElteltIdo < MaxAtiranyitasiIdo);
                return HtmlNodeFactory.UjNode(KigyujtottHtml);
            }
            else
                return HtmlNodeFactory.UjNode("");
        }

        protected void kigyujtoJavascriptLefuttatasa()
        {
            kigyujtesFolyamatban = true;
            bongeszo.GetMainFrame().EvaluateScriptAsync(@"document.getElementsByTagName('html')[0].innerHTML").ContinueWith(taskHtml =>
            {
                kigyujtesFolyamatban = false;
                if (taskHtml.Result.Result != null)
                {
                    KigyujtottHtml = taskHtml.Result.Result.ToString();
                    if (Utf8Convert)
                    {
                        KigyujtottHtml = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(KigyujtottHtml)));
                    }
                }
            });
            while (kigyujtesFolyamatban)
                Thread.Sleep(100);
        }
        
        public bool AzOldalMegValtozik(string elozolegKigyujtottHtml)
        {
            Kigyujtes();
            return elozolegKigyujtottHtml != KigyujtottHtml;
        }

        public void Atiranyitas(string url)
        {
            Betoltodott = false;
            string slugositottUrl = LinkFunkciok.SlugositottUrl(url);
            if (url.ToLower() == slugositottUrl.ToLower())
            {
                int aktualisAtiranyitasnalElteltIdo = 0;
                KigyujtottHtml = null;
                atiranyitasFolyamatban = true;
                bongeszo.Load(url);
                while (atiranyitasFolyamatban && aktualisAtiranyitasnalElteltIdo < MaxAtiranyitasiIdo)
                {
                    Thread.Sleep(1000);
                    aktualisAtiranyitasnalElteltIdo += 1;
                }
                if (aktualisAtiranyitasnalElteltIdo == MaxAtiranyitasiIdo)
                    Logger.log("Sikertelen atiranyitas: " + url);
            }
            else
            {
                Logger.log("Nem betoltheto link: " + url);
                Logger.log("Atiranyitas: " + slugositottUrl);
                Atiranyitas(slugositottUrl);
            }
        }

        private delegate void BlankDelegate();
        public void Bezar()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new BlankDelegate(this.Bezar));
            }
            else
            {
                this.Close();
            }
        }

        private void KigyujtoForm_Load(object sender, EventArgs e)
        {
            Letrehozodott = true;
        }
    }
}
