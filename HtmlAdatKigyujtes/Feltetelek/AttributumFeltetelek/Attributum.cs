using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HtmlAdatKigyujtes.Feltetelek
{
    public class Attributum
    {
        public string Nev;
        public string Ertek;

        public Attributum(string nev, string ertek)
        {
            Nev = nev;
            Ertek = ertek;
        }

        public bool Meglevoe(HtmlNode node)
        {
            return MegfeleloHtmlAttributumok(node).Count > 0;
        }

        public List<HtmlAttribute> MegfeleloHtmlAttributumok(HtmlNode node)
        {
            if (node.Attributes["href"] != null && node.Attributes["href"].Value == "mailto:nimhinfo@nih.gov")
                ;
            return node.Attributes.Where(htmlAttributum => megegyezoAttributume(htmlAttributum)).ToList();
        }
        
        private bool megegyezoAttributume(HtmlAttribute htmlAttributum)
        {
            Match nevTalalat = Regex.Match(htmlAttributum.Name, Nev);
            if (nevTalalat.Success && nevTalalat.Index == 0)
            {
                Match ertekTalalat = Regex.Match(htmlAttributum.Value, Ertek);
                return ertekTalalat.Success && ertekTalalat.Index == 0;
            }
            else
                return false;
        }

        public bool ReszbenMeglevoe(HtmlNode node)
        {
            foreach (HtmlAttribute nodeAttributum in node.Attributes)
                if (Nev == nodeAttributum.Name && nodeAttributum.Value.Contains(Ertek))
                    return true;
            return false;
        }

        public static bool operator==(Attributum a1, HtmlAttribute a2)
        {
            return a1.Nev == a2.Name && a1.Ertek == a2.Value;
        }

        public static bool operator !=(Attributum a1, HtmlAttribute a2)
        {
            return !(a1 == a2);
        }
    }
}
