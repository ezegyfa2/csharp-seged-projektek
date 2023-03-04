using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HtmlAdatKigyujtes.Feltetelek
{
    public class VagyFeltetel : IHtmlNodeFeltetel
    {
        public List<IHtmlNodeFeltetel> Feltetelek;

        public VagyFeltetel(params IHtmlNodeFeltetel[] feltetelek)
        {
            Feltetelek = feltetelek.ToList();
        }

        public bool Megfelele(HtmlNode node)
        {
            return Feltetelek.Exists(feltetel => feltetel.Megfelele(node));
        }
    }
}
