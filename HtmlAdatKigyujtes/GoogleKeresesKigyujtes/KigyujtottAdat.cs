using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAdatKigyujtes.GoogleKeresesKigyujtes
{
    public class KigyujtottAdat
    {
        public object Adat;
        public string Link;

        public KigyujtottAdat(object adat, string link)
        {
            Adat = adat;
            Link = link;
        }
    }
}
