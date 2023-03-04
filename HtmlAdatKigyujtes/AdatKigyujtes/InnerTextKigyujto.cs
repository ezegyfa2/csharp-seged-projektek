using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAdatKigyujtes.Feltetelek;

namespace HtmlAdatKigyujtes
{
    public class InnerTextKigyujto : AdatKigyujto<string>
    {
        public InnerTextKigyujto(params IHtmlNodeFeltetel[] kigyujtesiFeltetelek) : base(kigyujtesiFeltetelek) { }

        public string AdatokOsszegzettKigyujtese(HtmlNode node)
        {
            return AdatokKigyujtese(node).Aggregate((aktNode, kovNode) => aktNode + kovNode);
        }

        protected override string adatKigyujtese(HtmlNode node)
        {
            return node.InnerText;
        }
    }
}
