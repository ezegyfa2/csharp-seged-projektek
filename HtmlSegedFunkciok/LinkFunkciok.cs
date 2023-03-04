using Slugify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSegedFunkciok
{
    public class LinkFunkciok
    {
        public static string LinkParameterekNelkul(string link)
        {
            UriBuilder uri = new UriBuilder(link);
            return uri.Uri.GetLeftPart(UriPartial.Path);
        }

        public static bool BetolthetoUrle(string url)
        {
            UriBuilder uri = new UriBuilder(url);
            return url == SlugositottUrl(url);
        }

        public static string SlugositottUrl(string url)
        {
            return SlugositottUrlPath(url) + SlugositottUrlParameterek(url);
        }

        public static string SlugositottUrlParameterek(string url)
        {
            SlugHelper slug = new SlugHelper();
            UriBuilder uri = new UriBuilder(url);
            string parameterSzoveg = uri.Query;
            string[] urlParameterek = uri.Uri.DecodeQueryParameters().Values.ToArray();
            foreach (string urlParameter in urlParameterek)
            {
                string slugositottParameter = slug.GenerateSlug(urlParameter);
                if (urlParameter != slugositottParameter)
                    parameterSzoveg.Replace(urlParameter, slugositottParameter);
            }
            return parameterSzoveg;
        }
        
        public static string SlugositottUrlPath(string url)
        {
            SlugHelper slug = new SlugHelper();
            UriBuilder uri = new UriBuilder(url);
            string[] urlReszek = uri.Path.Split('/');
            string path;
            if (uri.Query.Length == 0)
                path = url;
            else
                path = url.Replace(uri.Query, "");
            foreach (string urlResz in urlReszek)
            {
                string slugositottResz = slug.GenerateSlug(urlResz);
                if (urlResz != slugositottResz)
                    path = path.Replace(urlResz, slugositottResz);
            }
            return path;
        }
    }
}
