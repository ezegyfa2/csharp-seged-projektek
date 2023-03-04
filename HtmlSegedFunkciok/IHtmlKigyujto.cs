using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSegedFunkciok
{
    public interface IWeboldalForraskodKigyujto
    {
        HtmlNode Kigyujtes(string url);
        string Url { get; }
    }
}
