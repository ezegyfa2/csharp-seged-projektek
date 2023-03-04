using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HtmlAdatKigyujtes.Feltetelek
{
    public class TeljesAttributumFeltetel : AttributumFeltetel
    {
        public TeljesAttributumFeltetel(params Attributum[] keresendoAttributumok) : base(keresendoAttributumok) { }

        protected override bool meglevoAttributume(HtmlNode node, Attributum attributum)
        {
            return attributum.Meglevoe(node);
        }
    }
}
