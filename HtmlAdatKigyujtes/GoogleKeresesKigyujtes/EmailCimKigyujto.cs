using HtmlSegedFunkciok;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAdatKigyujtes.GoogleKeresesKigyujtes
{
    public class EmailCimKigyujto : AdatKigyujto<string>
    {
        public EmailCimKigyujto(LinkSzuro linkSzuro = null, int atnezendoWeblapokSzama = 50) 
            : base(
                  new WeboldalAdatKigyujto<string>(
                      new HtmlAdatKigyujtes.EmailCimKigyujto(),
                      atnezendoWeblapokSzama, 
                      linkSzuro
                    )
                )
        {
        }
    }
}
