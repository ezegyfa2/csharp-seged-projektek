using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HtmlAdatKigyujtes.Feltetelek
{
    public abstract class AttributumFeltetel : IHtmlNodeFeltetel
    {
        public List<Attributum> KeresendoAttributumok = new List<Attributum>();

        public AttributumFeltetel(params Attributum[] keresendoAttributumok)
        {
            KeresendoAttributumok = keresendoAttributumok.ToList();
        }

        public bool Megfelele(HtmlNode node)
        {
            foreach (Attributum attributum in KeresendoAttributumok)
                if (!meglevoAttributume(node, attributum))
                    return false;
            return true;
        }

        protected abstract bool meglevoAttributume(HtmlNode node, Attributum attributum);
    }
}
