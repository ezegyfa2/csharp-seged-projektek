//using CsvFunkciok;
using HtmlSegedFunkciok;
using SegedFunkciok;

using Slugify;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAdatKigyujtes.GoogleKeresesKigyujtes
{
    public class AdatKigyujto<KigyujtendoAdatTipus>
    {
        public WeboldalAdatKigyujto<KigyujtendoAdatTipus> WeboldalAdatKigyujto;
        protected SlugHelper sulgosito = new SlugHelper();

        public AdatKigyujto(WeboldalAdatKigyujto<KigyujtendoAdatTipus> adatKigyujto)
        {
            WeboldalAdatKigyujto = adatKigyujto;
        }

        /*public List<KigyujtottAdat> TeljesKigyujtes(string keresesiSzoveg, int oldalakSzama = 3)
        {
            List<KigyujtottAdat> kigyujtottAdatok = new List<KigyujtottAdat>();
            List<string> talaltLinkek = talalatiLinkek(keresesiSzoveg, oldalakSzama);
            foreach (string talaltLink in talaltLinkek)
            {
                try
                {
                    List<KigyujtottAdat> adatok = WeboldalAdatKigyujto.Kigyujtes(talaltLink)
                        .Select(kigyujtottAdat => new KigyujtottAdat(kigyujtottAdat, talaltLink)).ToList();
                    using (CsvKiiro kiiro = new CsvKiiro("D:\\debug.csv", true, ';'))
                        kiiro.KiirObjektumok(adatok);
                    kigyujtottAdatok.AddRange(adatok);
                }
                catch (Exception e)
                {
                    Logger.log("Exception: " + e.Message);
                }
            }
            return kigyujtottAdatok;
        }*/

        public List<KigyujtendoAdatTipus> Kigyujtes(string keresesiSzoveg, int oldalakSzama = 3)
        {
            return talalatiLinkek(keresesiSzoveg, oldalakSzama)
                .SelectMany(link => WeboldalAdatKigyujto.Kigyujtes(link))
                .ToList();
        }

        public List<string> talalatiLinkek(string keresesiSzoveg, int oldalakSzama = 3)
        {
            FormosWeboldalForraskodKigyujtes.Kigyujto forraskodKigyujto = new FormosWeboldalForraskodKigyujtes.Kigyujto();
            TalalatiLinkKigyujto kereso = new TalalatiLinkKigyujto();
            return kereso.TalalatiLinkekKigyujtese(sulgosito.GenerateSlug(keresesiSzoveg), oldalakSzama, WeboldalAdatKigyujto.AdatKigyujto.ForraskodKigyujto);
        }
    }
}
