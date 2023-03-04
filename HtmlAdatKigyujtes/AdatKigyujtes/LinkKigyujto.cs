using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlSegedFunkciok;
using HtmlAdatKigyujtes.Feltetelek;
using SegedFunkciok;

namespace HtmlAdatKigyujtes
{
    public class LinkKigyujto : AdatKigyujto<string>
    {
        TagadasiFeltetel fileLinkSzuroFeltetel = new TagadasiFeltetel(
            new TeljesAttributumFeltetel(
                new Attributum("href", RegularisKifejezesek.FileKiterjesztes)
            )
        );
        public bool FileLinkekEngedelyezve {
            set
            {
                if (!value && !KigyujtesiFeltetelek.Contains(fileLinkSzuroFeltetel))
                    KigyujtesiFeltetelek.Add(fileLinkSzuroFeltetel);
                if (value && KigyujtesiFeltetelek.Contains(fileLinkSzuroFeltetel))
                    KigyujtesiFeltetelek.Remove(fileLinkSzuroFeltetel);
            }
            get {
                return KigyujtesiFeltetelek.Contains(fileLinkSzuroFeltetel);
            }
        }

        public LinkKigyujto(bool fileLinkekEngedelyezve, params IHtmlNodeFeltetel[] kigyujtesiFeltetelek) : base(
            NevFeltetel.a(), 
            new VagyFeltetel(
                new TeljesAttributumFeltetel(new Attributum("href", "http.*")), 
                new TeljesAttributumFeltetel(new Attributum("href", "/.*"))
            )
        )
        {
            FileLinkekEngedelyezve = fileLinkekEngedelyezve;
            KigyujtesiFeltetelek.AddRange(kigyujtesiFeltetelek);
        }

        public override List<string> AdatokKigyujtese(string url)
        {
            List<string> kigyujtottLinkek = base.AdatokKigyujtese(url);
            return HostHozzaadasaHelyiLinkekhez(kigyujtottLinkek, url);
        }

        public override List<string> AdatokKigyujtese(string url, List<AdatKigyujto<HtmlNode>> szurok)
        {
            List<string> kigyujtottLinkek = base.AdatokKigyujtese(url, szurok);
            return HostHozzaadasaHelyiLinkekhez(kigyujtottLinkek, url);
        }

        public static List<string> HostHozzaadasaHelyiLinkekhez(List<string> linkek, string lekertUrl)
        {
            Uri url = new Uri(lekertUrl);
            return linkek.Select(link => link.First() == '/' ? url.Host + link : link).ToList();
        }

        protected override string adatKigyujtese(HtmlNode node)
        {
            return node.Attributes["href"].Value;
        }
    }
}
