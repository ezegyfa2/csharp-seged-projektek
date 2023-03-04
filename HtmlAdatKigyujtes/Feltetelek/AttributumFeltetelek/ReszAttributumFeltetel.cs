using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HtmlAdatKigyujtes.Feltetelek
{
    public class ReszAttributumFeltetel : AttributumFeltetel
    {
        public ReszAttributumFeltetel(params Attributum[] keresendoAttributumok) : base(keresendoAttributumok) { }

        protected override bool meglevoAttributume(HtmlNode node, Attributum attributum)
        {
            return attributum.ReszbenMeglevoe(node);
        }
    }
}
