using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HtmlAdatKigyujtes.Feltetelek
{
    public class NevFeltetel : IHtmlNodeFeltetel
    {
        public string KeresendoNodeNev;

        public NevFeltetel(string keresendoNodeNev)
        {
            KeresendoNodeNev = keresendoNodeNev;
        }

        public bool Megfelele(HtmlNode node)
        {
            return node.Name == KeresendoNodeNev;
        }
        
        public static NevFeltetel a()
        {
            return new NevFeltetel("a");
        }

    }
}
