using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HtmlAdatKigyujtes.Feltetelek
{
    class TagadasiFeltetel : IHtmlNodeFeltetel
    {
        public IHtmlNodeFeltetel TagadandoFeltetel;

        public TagadasiFeltetel(IHtmlNodeFeltetel tagadandoFeltetel)
        {
            TagadandoFeltetel = tagadandoFeltetel;
        }

        public bool Megfelele(HtmlNode node)
        {
            return !TagadandoFeltetel.Megfelele(node);
        }
    }
}
