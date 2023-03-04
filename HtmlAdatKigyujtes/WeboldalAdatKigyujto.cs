using HtmlSegedFunkciok;
using SegedFunkciok;

using HtmlAgilityPack;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAdatKigyujtes
{
    public class WeboldalAdatKigyujto<KigyujtendoAdatTipus>
    {
        public AdatKigyujto<KigyujtendoAdatTipus> AdatKigyujto;
        public LinkSzuro LinkSzuro = new LinkSzuro();
        public int AtnezendoWeblapokMaxSzama = 30;
        protected List<string> kigyujtottLinkek = new List<string>();

        public WeboldalAdatKigyujto(AdatKigyujto<KigyujtendoAdatTipus> adatKigyujto, int atnezendoWeblapokMaxSzama = 30, LinkSzuro linkSzuro = null)
        {
            AdatKigyujto = adatKigyujto;
            AtnezendoWeblapokMaxSzama = atnezendoWeblapokMaxSzama;
            if (linkSzuro != null)
                LinkSzuro = linkSzuro;
        }

        public List<KigyujtendoAdatTipus> Kigyujtes(string url)
        {
            kigyujtottLinkek = new List<string>();
            List<KigyujtendoAdatTipus> kigyujtottAdatok = kigyujtes(url);
            if (kigyujtottLinkek.Count >= 100)
                hiba(url);
            return kigyujtottAdatok;
        }

        protected List<KigyujtendoAdatTipus> kigyujtes(string url)
        {
            if (kigyujtottLinkek.Count < AtnezendoWeblapokMaxSzama && LinkSzuro.engedelyezettLinke(url))
            {
                List<KigyujtendoAdatTipus> kigyujtottAdatok = new List<KigyujtendoAdatTipus>();
                HtmlNode weblapHtml = AdatKigyujto.ForraskodKigyujto.Kigyujtes(url);
                kigyujtottAdatok.AddRange(AdatKigyujto.AdatokKigyujtese(weblapHtml));
                kigyujtottLinkek.Add(LinkFunkciok.LinkParameterekNelkul(url));
                foreach (string link in WeblaponLevoBelsoLinkekKigyujtese(weblapHtml, url))
                    if (!kigyujtottLinkek.Contains(LinkFunkciok.LinkParameterekNelkul(link)))
                        kigyujtottAdatok.AddRange(kigyujtes(link));
                return kigyujtottAdatok.Distinct().ToList();
            }
            else
                return new List<KigyujtendoAdatTipus>();
        }

        public List<string> WeblaponLevoBelsoLinkekKigyujtese(HtmlNode weblapHtml, string url)
        {
            LinkKigyujto linkKigyujto = new LinkKigyujto(false);
            List<string> weblaponLevoLinkek = linkKigyujto.AdatokKigyujtese(weblapHtml);
            UriBuilder uri = new UriBuilder(url);
            return weblaponLevoLinkek
                .Select(link => link.First() == '/' ? uri.Host + link : link)
                .Where(link => megegyezoHoste(link, url)).ToList();
        }

        public static bool megegyezoHoste(string url1, string url2)
        {
            try
            {
                UriBuilder uri1 = new UriBuilder(url1);
                UriBuilder uri2 = new UriBuilder(url2);
                return uri1.Host == uri2.Host;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void hiba(string url)
        {
            Logger.log("Sikertelen Kigyujtes. Tul sok weblap: " + url);
        }
    }
}
