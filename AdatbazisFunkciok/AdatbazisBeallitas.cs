using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdatbazisFunkciok
{
    public class AdatbazisBeallitas
    {
        public string Server;
        public string Port;
        public string Adatbazis;
        public string FelhasznaloNev;
        public string Jelszo;
        public string Charset;

        public AdatbazisBeallitas(string server, string port, string adatbazis, string felhasznaloNev, string jelszo, string charset)
        {
            Server = server;
            Port = port;
            Adatbazis = adatbazis;
            FelhasznaloNev = felhasznaloNev;
            Jelszo = jelszo;
            Charset = charset;
        }

        public AdatbazisBeallitas(string configFileEleresiUtvonal)
        {
            using (StreamReader configBeolvaso = new StreamReader(configFileEleresiUtvonal))
            {
                string beolvasottSor = configBeolvaso.ReadLine();
                while (beolvasottSor != null)
                {
                    if (beolvasottSor.Contains("="))
                    {
                        string beallitandoElemNev = beolvasottSor.Split('=')[0];
                        switch (beallitandoElemNev)
                        {
                            case "SERVER":
                                Server = beolvasottSor.Split('=')[1];
                                break;
                            case "PORT":
                                Port = beolvasottSor.Split('=')[1];
                                break;
                            case "DATABASE":
                                Adatbazis = beolvasottSor.Split('=')[1];
                                break;
                            case "USERNAME":
                                FelhasznaloNev = beolvasottSor.Split('=')[1];
                                break;
                            case "PASSWORD":
                                Jelszo = beolvasottSor.Split('=')[1];
                                break;
                            case "CHARSET":
                                Charset = beolvasottSor.Split('=')[1];
                                break;
                        }
                    }
                    beolvasottSor = configBeolvaso.ReadLine();
                }
            }
        }

        public override string ToString()
        {
            return "SERVER=" + Server + ";DATABASE=" + Adatbazis + ";PORT=" + Port + ";UID=" + FelhasznaloNev + ";PASSWORD=" + Jelszo + ";CHARSET=" + Charset + "Convert Zero Datetime=True;";
        }
    }
}
