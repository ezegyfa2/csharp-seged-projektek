using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HtmlSegedFunkciok
{
    public class HtmlNodeFactory
    {
        public static HtmlNode UjNodeFilebol(string fileUrl)
        {
            HtmlDocument d = new HtmlDocument();
            d.Load(fileUrl);
            return d.DocumentNode;
        }

        public static HtmlNode UjNode(string html)
        {
            HtmlDocument d = new HtmlDocument();
            d.LoadHtml(html);
            return d.DocumentNode;
        }
    }
}
