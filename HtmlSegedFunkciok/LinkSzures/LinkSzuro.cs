using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSegedFunkciok
{
    public class LinkSzuro
    {
        public Dictionary<string, int> EngedelyezettOldalSzamok = new Dictionary<string, int>();
        public int AlapertelmezettenEngedelyezettOldalSzam = 0;

        public LinkSzuro(int alapertelmezettenEngedelyezettOldalSzam = 0) : this(new Dictionary<string, int>(), alapertelmezettenEngedelyezettOldalSzam)
        {
        }

        public LinkSzuro(Dictionary<string, int> engedelyezettOldalSzamok, int alapertelmezettenEngedelyezettOldalSzam = 0)
        {
            EngedelyezettOldalSzamok = engedelyezettOldalSzamok;
            AlapertelmezettenEngedelyezettOldalSzam = alapertelmezettenEngedelyezettOldalSzam;
        }

        public bool engedelyezettLinke(string link)
        {
            UriBuilder uri = new UriBuilder(link);
            Dictionary<string, string> linkParameterek = uri.Uri.DecodeQueryParameters();
            if (linkParameterek.ContainsKey("page"))
            {
                int linkOldalSzam;
                try
                {
                    linkOldalSzam = Convert.ToInt32(linkParameterek["page"]);
                }
                catch(Exception)
                {
                    return true;
                }
                return linkOldalSzam <= engedelyezettOldalSzam(link);
            }
            else
                return true;
        }

        public int engedelyezettOldalSzam(string link)
        {
            UriBuilder uri = new UriBuilder(link);
            if (EngedelyezettOldalSzamok.ContainsKey(uri.Host))
                return EngedelyezettOldalSzamok[uri.Host];
            else
                return AlapertelmezettenEngedelyezettOldalSzam;
        }
    }
}
