using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AdatbazisFunkciok
{
    abstract public class TarolhatoAdat
    {
        public TarolhatoAdat(int id)
        {
            MySqlDataReader beolvaso = LekeresIdAlapjan(id);
            AdatokBeallitasaReaderbol(beolvaso);
            beolvaso.Close();
        }

        public TarolhatoAdat(MySqlDataReader reader)
        {
            AdatokBeallitasaReaderbol(reader);
        }

        public TarolhatoAdat()
        {
        }

        public virtual int Tarolas()
        {
            if (LetezoEleme())
            {
                return ID;
            }
            else
            {
                foreach (TarolhatoAdat eloreTarolandoAdat in eloreTarolandoAdatok)
                {
                    if (eloreTarolandoAdat != null)
                    {
                        eloreTarolandoAdat.Tarolas();
                    }
                }
                return Adatbazis.FeltoltesHaNemLetezo(TablaNev, AdatbazisOszlopNevek, AdatbazisOszlopErtekek);
            }
        }

        protected int id = -1;
        public int ID
        {
            get
            {
                if (id == -1)
                {
                    id = Adatbazis.ElemIdLekeres(TablaNev, AdatbazisOszlopNevek, AdatbazisOszlopErtekek);
                }
                return id;
            }
        }

        public bool LetezoEleme()
        {
            return Adatbazis.LetezoEleme(TablaNev, AdatbazisOszlopNevek, AdatbazisOszlopErtekek);
        }

        public MySqlDataReader LekeresIdAlapjan(int id)
        {
            MySqlDataReader beolvaso = Adatbazis.Lekeres(TablaNev, AdatbazisOszlopNevek, new List<string>() { "id" }, new List<string>() { id.ToString() });
            beolvaso.Read();
            return beolvaso;
        }

        protected virtual List<TarolhatoAdat> eloreTarolandoAdatok
        {
            get
            {
                return new List<TarolhatoAdat>();
            }
        }

        abstract public void AdatokBeallitasaReaderbol(MySqlDataReader reader);
        public abstract string TablaNev { get; }
        abstract public List<string> AdatbazisOszlopNevek { get; }
        abstract public List<string> AdatbazisOszlopErtekek { get; }
    }
}
