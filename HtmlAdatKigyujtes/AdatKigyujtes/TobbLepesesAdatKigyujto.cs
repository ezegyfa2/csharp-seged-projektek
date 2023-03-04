using FormosWeboldalForraskodKigyujtes;
using HtmlAdatKigyujtes.Feltetelek;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAdatKigyujtes.AdatKigyujtes
{
    public class TobbLepesesAdatKigyujto<KigyujtendoAdatTipus>
    {
        public AdatKigyujto<KigyujtendoAdatTipus> FoAdatKigyujto;
        public List<HtmlNodeKigyujto> MellekAdatKigyujtok = new List<HtmlNodeKigyujto>();
        public List<int> MellekAdatKigyujtoTalatIndexek = new List<int>();

        public TobbLepesesAdatKigyujto(AdatKigyujto<KigyujtendoAdatTipus> foAdatKigyujto, List<List<IHtmlNodeFeltetel>> mellekFeltetelListak):
            this(foAdatKigyujto, mellekFeltetelListak.Select(mellekFeltetelLista => new HtmlNodeKigyujto(mellekFeltetelLista.ToArray())).ToList())
        {
        }

        public TobbLepesesAdatKigyujto(AdatKigyujto<KigyujtendoAdatTipus> foAdatKigyujto, List<HtmlNodeKigyujto> mellekAdatKigyujtok)
        {
            FoAdatKigyujto = foAdatKigyujto;
            MellekAdatKigyujtok = mellekAdatKigyujtok;
        }

        public List<KigyujtendoAdatTipus> AdatKigyujtese(string url)
        {
            HtmlNode kigyujtottNode;
            using (Kigyujto forraskodKigyujto = new Kigyujto())
            {
                kigyujtottNode = forraskodKigyujto.Kigyujtes(url);
            }
            for (int i = 0; i < MellekAdatKigyujtok.Count; ++i)
            {
                kigyujtottNode = MellekAdatKigyujtok[i].EgyediAdatKigyujtese(kigyujtottNode, talalatIndex(i));
            }
            return FoAdatKigyujto.AdatokKigyujtese(kigyujtottNode);
        }

        private int talalatIndex(int i)
        {
            if (MellekAdatKigyujtoTalatIndexek.Count > i)
            {
                return MellekAdatKigyujtoTalatIndexek[i];
            }
            else
            {
                return 0;
            }
        }
    }
}
