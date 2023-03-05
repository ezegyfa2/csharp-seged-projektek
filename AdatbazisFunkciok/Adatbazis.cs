using SegedFunkciok;

using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace AdatbazisFunkciok
{
    public class Adatbazis
    {
        public static AdatbazisBeallitas beallitas;
        public static Dictionary<object, MySqlConnection> Kapcsolatok = new Dictionary<object, MySqlConnection>();
        public static List<MySqlConnection> connections = new List<MySqlConnection>();

        public static void Beallitas(string configFileEleresiUtvonal = null)
        {
            if (configFileEleresiUtvonal == null)
            {
                configFileEleresiUtvonal = "config.txt";
            }
            beallitas = new AdatbazisBeallitas(configFileEleresiUtvonal);
        }

        public static int FeltoltesHaNemLetezo(string tablaNev, List<string> oszlopNevek, List<string> oszlopErtekek)
        {
            int megfeleloId = ElemIdLekeres(tablaNev, oszlopNevek, oszlopErtekek);
            if (megfeleloId == -1)
                return Feltoltes(tablaNev, oszlopNevek, oszlopErtekek);
            else
                return megfeleloId;
        }

        public static int Feltoltes(string tablaNev, List<string> oszlopNevek, List<string> oszlopErtekek)
        {
            oszlopErtekek = oszlopErtekek.Select(oszlopErtek => StringFuggvenyek.AdatbazisNormalizalas(oszlopErtek)).ToList();
            string oszlopErtekekString = StringFuggvenyek.Osszefuzes(oszlopErtekek, "\", \"") + "\"";
            string query = "INSERT INTO " + tablaNev + "(" + StringFuggvenyek.Osszefuzes(oszlopNevek, ", ") + ") VALUES (\""
                + oszlopErtekekString.Replace("\"NULL\"", "NULL") + ")";
            MySqlConnection kapcsolat = KapcsolatKeszites();
            MySqlCommand feltoltes = new MySqlCommand(query, kapcsolat);
            try
            {
                feltoltes.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.log("Error: " + query);
            }
            kapcsolat.Close();
            return ElemIdLekeres(tablaNev, oszlopNevek, oszlopErtekek);
        }

        public static void CommandVegrehajtas(string command)
        {
            MySqlConnection kapcsolat = KapcsolatKeszites();
            MySqlCommand feltoltes = new MySqlCommand(command, kapcsolat);
            feltoltes.ExecuteNonQuery();
            kapcsolat.Close();
        }

        public static bool LetezoEleme(string tablaNev, List<string> oszlopNevek, List<string> oszlopErtekek)
        {
            List<string> lekerendoOszlopNevek = new List<string>();
            lekerendoOszlopNevek.Add("id");
            MySqlDataReader beolvaso = Lekeres(tablaNev, lekerendoOszlopNevek, oszlopNevek, oszlopErtekek);
            bool letezoEleme = beolvaso.HasRows;
            beolvaso.Close();
            KapcsolatBezaras(beolvaso);
            return letezoEleme;
        }

        public static int ElemIdLekeres(string tablaNev, List<string> oszlopNevek, List<string> oszlopErtekek)
        {
            List<string> lekerendoOszlopNevek = new List<string>();
            lekerendoOszlopNevek.Add("id");
            MySqlDataReader lekeres = Lekeres(tablaNev, lekerendoOszlopNevek, oszlopNevek, oszlopErtekek);
            int lekertId = -1;
            if (lekeres.HasRows)
            {
                lekeres.Read();
                lekertId = lekeres.GetInt32(0);
                if (lekeres.NextResult())
                    throw new Exception("Tul sok talalat");
            }
            lekeres.Close();
            KapcsolatBezaras(lekeres);
            return lekertId;
        }

        public static List<LekertAdatTipus> ListaLekeres<LekertAdatTipus>(string query) where LekertAdatTipus : TarolhatoAdat
        {
            MySqlDataReader beolvaso = Lekeres(query);
            List<LekertAdatTipus> lekertAdatok = new List<LekertAdatTipus>();
            while (beolvaso.Read())
            {
                lekertAdatok.Add((LekertAdatTipus)Activator.CreateInstance(typeof(LekertAdatTipus), beolvaso));
            }
            beolvaso.Close();
            KapcsolatBezaras(beolvaso);
            return lekertAdatok;
        }

        public static MySqlDataReader Lekeres(string tablaNev, List<string> lekerendoOszlopNevek, List<string> feltetelOszlopNevek = null, List<string> feltelOszlopErtekek = null)
        {
            return Lekeres(LekeresQuery(tablaNev, lekerendoOszlopNevek, feltetelOszlopNevek, feltelOszlopErtekek));
        }

        public static MySqlDataReader Lekeres(string query)
        {
            MySqlConnection kapcsolat = KapcsolatKeszites();
            MySqlCommand select = new MySqlCommand(query, kapcsolat);
            MySqlDataReader beolvaso = select.ExecuteReader();
            Kapcsolatok[beolvaso] = kapcsolat;
            return beolvaso;
        }

        public static string LekeresQuery(string tablaNev, List<string> lekerendoOszlopNevek, List<string> feltetelOszlopNevek = null, List<string> feltelOszlopErtekek = null)
        {
            string selectQuery = LekeresQuery(tablaNev, lekerendoOszlopNevek) + " WHERE ";
            if (feltetelOszlopNevek != null)
                for (int i = 0; i < feltetelOszlopNevek.Count; ++i)
                {
                    string feltetelOszlopErtek = StringFuggvenyek.AdatbazisNormalizalas(feltelOszlopErtekek[i]);
                    if (feltetelOszlopErtek == "NULL")
                    {
                        selectQuery += feltetelOszlopNevek[i] + " IS NULL AND ";
                    }
                    else
                    {
                        selectQuery += feltetelOszlopNevek[i] + " = \"" + feltetelOszlopErtek + "\" AND ";
                    }
                }
            selectQuery = selectQuery.Substring(0, selectQuery.Length - 5);
            return selectQuery;
        }

        public static string LekeresQuery(string tablaNev, List<string> lekerendoOszlopNevek)
        {
            return "SELECT " + StringFuggvenyek.Osszefuzes(lekerendoOszlopNevek, ", ") + " FROM " + tablaNev;
        }

        public static MySqlConnection KapcsolatKeszites()
        {
            MySqlConnection kapcsolat = new MySqlConnection(beallitas.ToString());
            kapcsolat.Open();
            return kapcsolat;
        }

        public static void Torles(string tablaNev, List<string> feltetelOszlopNevek, List<string> feltelOszlopErtekek)
        {
            string torlesQuery = "DELETE FROM " + tablaNev + " WHERE ";
            for (int i = 0; i < feltetelOszlopNevek.Count; ++i)
                torlesQuery += feltetelOszlopNevek[i] + " = \"" + feltelOszlopErtekek[i] + "\" AND ";
            torlesQuery = torlesQuery.Substring(0, torlesQuery.Length - 5);
            MySqlConnection kapcsolat = KapcsolatKeszites();
            MySqlCommand torles = new MySqlCommand(torlesQuery, kapcsolat);
            torles.ExecuteNonQuery();
            kapcsolat.Close();
        }

        public static void KapcsolatBezaras(object kapcsolatKulcs)
        {
            Kapcsolatok[kapcsolatKulcs].Close();
            Kapcsolatok.Remove(kapcsolatKulcs);
        }
    }
}
