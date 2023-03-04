using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegedFunkciok
{
    public class StringFuggvenyek
    {
        public static string Osszefuzes(List<string> szovegek, string elvalasztoSzoveg = "")
        {
            string osszefuzottSzoveg = string.Empty;
            foreach (string szoveg in szovegek)
                osszefuzottSzoveg += szoveg + elvalasztoSzoveg;
            return osszefuzottSzoveg.Substring(0, osszefuzottSzoveg.Length - elvalasztoSzoveg.Length);
        }

        public static string AdatbazisNormalizalas(string szoveg)
        {
            szoveg = szoveg.Replace("\n", "").Replace("\r", "")
                .Replace("\\\"", "\"").Replace("\"", "\\\"");
            szoveg = ElsoKarakterekLevagasa(szoveg, ' ');
            szoveg = HatsoKarakterekLevagasa(szoveg, ' ');
            return szoveg;
        }

        public static string ElsoKarakterekLevagasa(string szoveg, char levagandoKarakter)
        {
            while (szoveg.Length > 0 && szoveg.First() == levagandoKarakter)
                szoveg = szoveg.Substring(1);
            return szoveg;
        }

        public static string HatsoKarakterekLevagasa(string szoveg, char levagandoKarakter)
        {
            while (szoveg.Length > 0 && szoveg.Last() == levagandoKarakter)
                szoveg = szoveg.Substring(0, szoveg.Length - 1);
            return szoveg;
        }

        public static string UrlFileExtension(string url)
        {
            url = url.Split('?')[0];
            return Path.GetExtension(url);
        }
    }
}
