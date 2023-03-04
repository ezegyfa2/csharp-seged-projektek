using AdatbazisFunkciok;

using MySql.Data.MySqlClient;
using SegedFunkciok;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace HtmlAdatKigyujtes
{
    public class LetoltottKep: TarolhatoAdat
    {
        public string EleresiUtvonal
        {
            get
            {
                return Path.Combine(MappaEleresiUtvonal, FileNev).Replace("\\", "/");
            }
        }
        public string MappaEleresiUtvonal;
        public string FileNev;
        public string Link;

        public LetoltottKep(string mappaEleresiUtvonal, string link): 
            this(mappaEleresiUtvonal, GeneraltFileNev(mappaEleresiUtvonal, link), link)
        {
        }

        public LetoltottKep(string mappaEleresiUtvonal, string fileNev, string link)
        {
            MappaEleresiUtvonal = mappaEleresiUtvonal;
            FileNev = fileNev + "." + getExtension(link);
            Link = link;
            SegedFuggvenyek.KepLetoltes(link, EleresiUtvonal);
            if (getExtension(link) == "jpg" || getExtension(link) == "jpeg")
            {
                SegedFuggvenyek.JpgPngKonvertalas(EleresiUtvonal);
                File.Delete(EleresiUtvonal);
                FileNev = Path.GetFileNameWithoutExtension(FileNev) + ".png";
            }
        }

        public LetoltottKep(int id): base(id)
        {
        }

        public LetoltottKep(MySqlDataReader reader): base(reader)
        {
        }

        public override string TablaNev
        {
            get 
            { 
                return "images"; 
            }
        }

        public override List<string> AdatbazisOszlopNevek 
        {
            get 
            { 
                return new List<string>() 
                { 
                    "file_path", 
                    "link"
                }; 
            }
        }

        public override List<string> AdatbazisOszlopErtekek
        {
            get
            {
                return new List<string>()
                {
                    EleresiUtvonal,
                    Link
                };
            }
        }

        public static string GeneraltFileNev(string mappaEleresiUtvonal, string link)
        {
            return Directory.GetFiles(mappaEleresiUtvonal).Length.ToString();
        }

        public override void AdatokBeallitasaReaderbol(MySqlDataReader reader)
        {
            MappaEleresiUtvonal = Path.GetDirectoryName(reader.GetString("file_path"));
            FileNev = Path.GetFileName(reader.GetString("file_path"));
            Link = reader.GetString("link");
        }

        protected virtual string getExtension(string link)
        {
            return StringFuggvenyek.UrlFileExtension(link);
        }
    }
}
