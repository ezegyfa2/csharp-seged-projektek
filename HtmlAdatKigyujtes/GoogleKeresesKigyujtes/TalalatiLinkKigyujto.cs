using HtmlAdatKigyujtes.Feltetelek;
using HtmlAgilityPack;
using HtmlSegedFunkciok;
using SegedFunkciok;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HtmlAdatKigyujtes.GoogleKeresesKigyujtes
{
    public class TalalatiLinkKigyujto
    {
        public static string FeltetelElfogadoGombSzoveg = "Accept all";

        public List<string> TalalatiLinkekKigyujtese(string keresesiSzoveg, int kigyujtendoOldalakSzama = 3, IWeboldalForraskodKigyujto forraskodKigyujto = null)
        {
            if (forraskodKigyujto == null)
                forraskodKigyujto = new FormosWeboldalForraskodKigyujtes.Kigyujto();
            HtmlNode aktualisanLekertOldal = forraskodKigyujto.Kigyujtes("https://www.google.com/search?q=" + keresesiSzoveg);
            while (coockieElfogadoOldal(aktualisanLekertOldal))
            {
                Thread.Sleep(5000);
                aktualisanLekertOldal = forraskodKigyujto.Kigyujtes("https://www.google.com/search?q=" + keresesiSzoveg);
            }
            List<string> kigyujtottHostok = HostokKigyujtese(aktualisanLekertOldal);
            for (int i = 1; i < kigyujtendoOldalakSzama; ++i)
            {
                var v = kovetkezoOldalLink(aktualisanLekertOldal);
                aktualisanLekertOldal = forraskodKigyujto.Kigyujtes("https://www.google.com/search?q=" + keresesiSzoveg + "&start=" + i * 10);
                kigyujtottHostok.AddRange(HostokKigyujtese(aktualisanLekertOldal));
            }
            return kigyujtottHostok.Distinct().ToList();
        }

        private bool coockieElfogadoOldal(HtmlNode aktualisanLekertOldal)
        {
            var cookieElfogadoGombKigyujto = new HtmlNodeKigyujto(
                new NevFeltetel("button"),
                new InnerTextFeltetel(FeltetelElfogadoGombSzoveg)
            );
            List<HtmlNode> kigyujtottGombok = cookieElfogadoGombKigyujto.AdatokKigyujtese(aktualisanLekertOldal).ToList();
            return kigyujtottGombok.Count > 0;
        }

        public List<string> HostokKigyujtese(HtmlNode lekertNode)
        {
            List<HtmlAdatKigyujtes.AdatKigyujto<HtmlNode>> szurok = new List<HtmlAdatKigyujtes.AdatKigyujto<HtmlNode>>();
            szurok.Add(new HtmlNodeKigyujto(new TeljesAttributumFeltetel(new Attributum("id", "rcnt"))));
            LinkKigyujto linkKigyujto = new LinkKigyujto(
                false,
                new TagadasiFeltetel(
                    new ReszAttributumFeltetel(
                        new Attributum("href", "webcache.googleusercontent.com")
                    )
                )
            );
            List<string> kigyujtottLinkek = linkKigyujto.AdatokKigyujtese(lekertNode, szurok);
            return kigyujtottLinkek
                .Where(link => link.First() != '/')
                .Select(link => linkHostNev(link))
                .Distinct()
                .ToList();
        }

        protected string kovetkezoOldalLink(HtmlNode node)
        {
            LinkKigyujto kigyujto = new LinkKigyujto(false, new TeljesAttributumFeltetel(new Attributum("id", "pnnext")));
            return "https://www.google.com" + kigyujto.EgyediAdatKigyujtese(node);
        }

        public string linkHostNev(string link)
        {
            UriBuilder url = new UriBuilder(link);
            return url.Host.Replace("www.", "");
        }
    }
}
