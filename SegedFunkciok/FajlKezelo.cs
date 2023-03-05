using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegedFunkciok
{
    public class FajlKezelo
    {
        public static string MAPPA_ELERESI_UTVONAL = "D:\\Projektek\\Catalyst\\";

        public static string FileNevValtoztatas(string fileUrl, string elotag)
        {
            return Path.Combine(Path.GetDirectoryName(fileUrl), elotag + Path.GetFileName(fileUrl));
        }

        public static string Mappa(string alapMappaUrl, string alMappaNev)
        {
            string mappaUrl = Path.Combine(alapMappaUrl, alMappaNev);
            Directory.CreateDirectory(mappaUrl);
            return mappaUrl;
        }

        public static string FileUrlElotaggal(string fileUrl, string elotag)
        {
            return Path.Combine(Path.GetDirectoryName(fileUrl), elotag + Path.GetFileName(fileUrl));
        }

        public static string FileUrlUtotaggal(string fileUrl, string utotag)
        {
            return Path.Combine(Path.GetDirectoryName(fileUrl), Path.GetFileNameWithoutExtension(fileUrl) + utotag + Path.GetExtension(fileUrl));
        }

        public static string BeolvasFileTartalom(string fileUrl)
        {
            using (StreamReader reader = new StreamReader(fileUrl))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
