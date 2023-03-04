using HtmlAgilityPack;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System;
using HtmlSegedFunkciok;

namespace FormosWeboldalForraskodKigyujtes
{
    public class Kigyujto: IWeboldalForraskodKigyujto, IDisposable
    {
        protected KigyujtoForm kigyujtoForm = null;
        protected Thread kigyujtoFormSzal;
        public bool Utf8Convert
        {
            get 
            {
                if (kigyujtoForm == null)
                    kigyujtoFormLetrehozas();
                return kigyujtoForm.Utf8Convert; 
            }
            set 
            {
                if (kigyujtoForm == null)
                    kigyujtoFormLetrehozas();
                kigyujtoForm.Utf8Convert = value; 
            }
        }
        public string Url
        {
            get
            {
                return kigyujtoForm.Url;
            }
        }

        public Kigyujto(bool utf8Convert = true)
        {
            Utf8Convert = utf8Convert;
        }

        public HtmlNode Kigyujtes(string url)
        {
            if (kigyujtoForm == null)
                kigyujtoFormLetrehozas();
            kigyujtoForm.Atiranyitas(url);
            return kigyujtoForm.Kigyujtes();
        }
        
        private void kigyujtoFormLetrehozas()
        {
            kigyujtoFormSzal = new Thread(() => {
                kigyujtoForm = new KigyujtoForm();
                kigyujtoForm.ShowDialog();
            });
            kigyujtoFormSzal.Start();
            while (kigyujtoForm == null || !kigyujtoForm.Letrehozodott)
            {
                Thread.Sleep(100);
            }
        }

        public HtmlNode BetoltesNelkuliKigyujtes(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                return HtmlNode.CreateNode(client.DownloadString(url.Replace("&amp;", "&")));
            }
        }

        public static HtmlNode StatikusKigyujtes(string url, bool utf8Convert = false)
        {
            using (Kigyujto kigyujto = new Kigyujto(utf8Convert))
            {
                return kigyujto.Kigyujtes(url);
            }
        }

        public void Dispose()
        {
            kigyujtoForm.Bezar();
            kigyujtoFormSzal.Join();
        }
    }
}
