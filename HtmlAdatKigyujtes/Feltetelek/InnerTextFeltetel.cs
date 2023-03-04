using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace HtmlAdatKigyujtes.Feltetelek
{
    public class InnerTextFeltetel : IHtmlNodeFeltetel
    {
        public string KeresendoSzoveg;

        public InnerTextFeltetel(string keresendoSzoveg)
        {
            KeresendoSzoveg = keresendoSzoveg;
        }

        public bool Megfelele(HtmlNode node)
        {
            return node.ChildNodes.Count == 0 && Regex.IsMatch(node.InnerText, KeresendoSzoveg);
        }
    }
}
