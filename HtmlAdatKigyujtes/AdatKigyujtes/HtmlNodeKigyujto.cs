using HtmlAdatKigyujtes.Feltetelek;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAdatKigyujtes
{
    public class HtmlNodeKigyujto : AdatKigyujto<HtmlNode>
    {
        public HtmlNodeKigyujto(params IHtmlNodeFeltetel[] kigyujtesiFeltetelek) : base(kigyujtesiFeltetelek)
        {
        }

        protected override HtmlNode adatKigyujtese(HtmlNode node)
        {
            return node;
        }
    }
}
