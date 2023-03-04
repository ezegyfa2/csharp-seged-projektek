using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAdatKigyujtes.Feltetelek.AttributumFeltetelek.OsztalyFeltetelek
{
    abstract public class OsztalyFeltetel: AttributumFeltetel
    {
        public OsztalyFeltetel(params string[] osztalyNevek): base(osztalyNevek.Select(osztalyNev => new Attributum("class", osztalyNev)).ToArray())
        {
        }
    }
}
