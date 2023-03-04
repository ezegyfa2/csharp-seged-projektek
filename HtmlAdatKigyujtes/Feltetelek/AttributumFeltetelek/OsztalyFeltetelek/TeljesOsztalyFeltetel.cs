using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAdatKigyujtes.Feltetelek.AttributumFeltetelek.OsztalyFeltetelek
{
    public class TeljesOsztalyFeltetel : OsztalyFeltetel
    {
        public TeljesOsztalyFeltetel(params string[] osztalyNevek) : base(osztalyNevek)
        {
        }

        protected override bool meglevoAttributume(HtmlNode node, Attributum attributum)
        {
            return attributum.Meglevoe(node);
        }
    }
}
