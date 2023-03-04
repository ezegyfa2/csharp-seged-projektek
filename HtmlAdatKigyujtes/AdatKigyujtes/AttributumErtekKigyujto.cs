using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAdatKigyujtes.AdatKigyujtes
{
    public class AttributumErtekKigyujto : AdatKigyujto<string>
    {
        public string AttributumNev;

        public AttributumErtekKigyujto(string attributumNev)
        {
            AttributumNev = attributumNev;
        }

        protected override string adatKigyujtese(HtmlNode node)
        {
            return node.Attributes[AttributumNev].Value;
        }
    }
}
