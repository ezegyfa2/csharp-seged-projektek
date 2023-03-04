using HtmlAdatKigyujtes.Feltetelek;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAdatKigyujtes
{
    public class LinkNodeKigyujto : HtmlNodeKigyujto
    {
        public static LinkNodeKigyujto Kigyujto = new LinkNodeKigyujto();

        public LinkNodeKigyujto() : base(NevFeltetel.a())
        {
        }

        public static List<HtmlNode> LinkekKigyujtese(HtmlNode node)
        {
            return Kigyujto.AdatokKigyujtese(node);
        }
    }
}
