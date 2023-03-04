using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAdatKigyujtes.Feltetelek.AttributumFeltetelek.OsztalyFeltetelek
{
    public class ReszOsztalyFeltetel: OsztalyFeltetel
    {
        public ReszOsztalyFeltetel(params string[] osztalyNevek) : base(osztalyNevek)
        {
        }

        protected override bool meglevoAttributume(HtmlNode node, Attributum attributum)
        {
            return attributum.ReszbenMeglevoe(node);
        }
    }
}
