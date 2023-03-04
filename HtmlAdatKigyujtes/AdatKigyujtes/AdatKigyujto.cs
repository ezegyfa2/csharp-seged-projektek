using HtmlAgilityPack;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using HtmlSegedFunkciok;
using System;
using HtmlAdatKigyujtes.Feltetelek;

namespace HtmlAdatKigyujtes
{
    public abstract class AdatKigyujto<KigyujtendoAdatTipus>
    {
        public List<IHtmlNodeFeltetel> KigyujtesiFeltetelek;
        public IWeboldalForraskodKigyujto ForraskodKigyujto = new FormosWeboldalForraskodKigyujtes.Kigyujto();

        public AdatKigyujto(params IHtmlNodeFeltetel[] kigyujtesiFeltetelek) : this(null, null, kigyujtesiFeltetelek)
        {
            KigyujtesiFeltetelek = kigyujtesiFeltetelek.ToList();
        }

        public AdatKigyujto(IWeboldalForraskodKigyujto forraskodKigyujto, LinkSzuro linkSzuro, params IHtmlNodeFeltetel[] kigyujtesiFeltetelek)
        {
            KigyujtesiFeltetelek = kigyujtesiFeltetelek.ToList();
            if (forraskodKigyujto != null)
                ForraskodKigyujto = forraskodKigyujto;
        }

        public virtual List<KigyujtendoAdatTipus> AdatokKigyujtese(string url, List<AdatKigyujto<HtmlNode>> szurok)
        {
            return AdatokKigyujtese(ForraskodKigyujto.Kigyujtes(url), szurok);
        }
        
        public virtual List<KigyujtendoAdatTipus> AdatokKigyujtese(string url)
        {
            return AdatokKigyujtese(ForraskodKigyujto.Kigyujtes(url));
        }

        public List<KigyujtendoAdatTipus> AdatokKigyujtese(HtmlNode node, List<AdatKigyujto<HtmlNode>> szurok)
        {
            List<HtmlNode> talaltNodek = Szures(szurok, node);
            List<KigyujtendoAdatTipus> kigyujtottAdatok = new List<KigyujtendoAdatTipus>();
            foreach (HtmlNode talaltNode in talaltNodek)
                kigyujtottAdatok.AddRange(AdatokKigyujtese(talaltNode));
            return kigyujtottAdatok;
        }
        
        public KigyujtendoAdatTipus EgyediAdatKigyujtese(HtmlNode node, int talalatIndex = 0)
        {
            List<KigyujtendoAdatTipus> talalatok = AdatokKigyujtese(node);
            return talalatok[talalatIndex];
        }
        
        public List<KigyujtendoAdatTipus> AdatokKigyujtese(HtmlNode node)
        {
            List<KigyujtendoAdatTipus> kigyujtottAdatok = new List<KigyujtendoAdatTipus>();
            if (kigyujtendoe(node))
                kigyujtottAdatok.Add(adatKigyujtese(node));
            foreach (HtmlNode child in node.ChildNodes)
                kigyujtottAdatok.AddRange(AdatokKigyujtese(child));
            return kigyujtottAdatok;
        }

        public static List<HtmlNode> Szures(List<AdatKigyujto<HtmlNode>> szurok, HtmlNode node)
        {
            if (szurok.Count == 0)
                return new List<HtmlNode>();
            else {
                List<HtmlNode> kigyujtottAdatok = szurok[0].AdatokKigyujtese(node);
                if (szurok.Count == 1)
                    return kigyujtottAdatok;
                else
                {
                    List<HtmlNode> vegsoAdatok = new List<HtmlNode>();
                    foreach (HtmlNode kigyujtottAdat in kigyujtottAdatok)
                        vegsoAdatok.AddRange(Szures(szurok.GetRange(1, szurok.Count - 1), kigyujtottAdat));
                    return vegsoAdatok;
                }
            }
        }

        protected bool kigyujtendoe(HtmlNode node)
        {
            return !KigyujtesiFeltetelek.Exists(feltetel => !feltetel.Megfelele(node));
        }

        protected abstract KigyujtendoAdatTipus adatKigyujtese(HtmlNode node);
    }
}
