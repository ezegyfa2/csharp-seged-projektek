using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegedFunkciok
{
    public class RegularisKifejezesek
    {
        public static readonly string EMAIL = "\\w+@\\w+\\.\\w+";
        public static string FileKiterjesztes
        {
            get
            {
                return "\\.\\b(" + StringFuggvenyek.Osszefuzes(fileKiterjesztesek(), "|") + ")\\b.*$";
            }
        }

        protected static List<string> fileKiterjesztesek()
        {
            List<string> kiterjesztesek = new List<string>();
            //Kepek
            kiterjesztesek.Add("jpeg");
            kiterjesztesek.Add("JPEG");
            kiterjesztesek.Add("jpg");
            kiterjesztesek.Add("JPG");
            kiterjesztesek.Add("gif");
            kiterjesztesek.Add("GIF");
            kiterjesztesek.Add("bmp");
            kiterjesztesek.Add("BMP");
            kiterjesztesek.Add("png");
            kiterjesztesek.Add("PNG");
            kiterjesztesek.Add("svg");
            kiterjesztesek.Add("SVG");
            //Osszecsomagolt file
            kiterjesztesek.Add("zip");
            kiterjesztesek.Add("ZIP");
            kiterjesztesek.Add("rar");
            kiterjesztesek.Add("RAR");
            //Office file
            //Excel
            kiterjesztesek.Add("xls");
            kiterjesztesek.Add("XLS");
            kiterjesztesek.Add("xlsx");
            kiterjesztesek.Add("XLSX");
            kiterjesztesek.Add("xlsm");
            kiterjesztesek.Add("XLSM");
            kiterjesztesek.Add("csv");
            kiterjesztesek.Add("CSV");
            //Word
            kiterjesztesek.Add("doc");
            kiterjesztesek.Add("DOC");
            kiterjesztesek.Add("docx");
            kiterjesztesek.Add("DOCX");
            kiterjesztesek.Add("docm");
            kiterjesztesek.Add("DOCM");
            kiterjesztesek.Add("txt");
            kiterjesztesek.Add("TXT");
            //Power point
            kiterjesztesek.Add("ppt");
            kiterjesztesek.Add("PPT");
            kiterjesztesek.Add("pps");
            kiterjesztesek.Add("PPS");
            kiterjesztesek.Add("pptx");
            kiterjesztesek.Add("PPTX");
            kiterjesztesek.Add("pptm");
            kiterjesztesek.Add("PPTM");
            kiterjesztesek.Add("ppsx");
            kiterjesztesek.Add("PPSX");
            kiterjesztesek.Add("ppsm");
            kiterjesztesek.Add("PPSM");
            kiterjesztesek.Add("sldx");
            kiterjesztesek.Add("SLDX");
            kiterjesztesek.Add("sldm");
            kiterjesztesek.Add("SLDM");
            return kiterjesztesek;
        }
    }
}
