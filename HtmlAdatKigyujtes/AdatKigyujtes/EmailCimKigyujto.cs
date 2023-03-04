using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAdatKigyujtes;
using System.Text.RegularExpressions;
using HtmlAdatKigyujtes.Feltetelek;
using SegedFunkciok;

namespace HtmlAdatKigyujtes
{
    public class EmailCimKigyujto : AdatKigyujto<string>
    {
        Attributum emailAttributum = new Attributum(".*", RegularisKifejezesek.EMAIL);
        InnerTextKigyujto emailInnerTextKigyujto = new InnerTextKigyujto(new InnerTextFeltetel(RegularisKifejezesek.EMAIL));

        public EmailCimKigyujto(params IHtmlNodeFeltetel[] kigyujtesiFeltetelek) : base(new VagyFeltetel(
            new TeljesAttributumFeltetel(new Attributum(".*", RegularisKifejezesek.EMAIL)),
            new InnerTextFeltetel(RegularisKifejezesek.EMAIL)
        ))
        {
            KigyujtesiFeltetelek.AddRange(kigyujtesiFeltetelek);
        }

        protected override string adatKigyujtese(HtmlNode node)
        {
            List<string> talaltEmailtTartalmazoSzovegek = emailAttributum.MegfeleloHtmlAttributumok(node).Select(htmlAttributum => htmlAttributum.Value).ToList();
            talaltEmailtTartalmazoSzovegek.AddRange(emailInnerTextKigyujto.AdatokKigyujtese(node));
            return emailkivagasa(talaltEmailtTartalmazoSzovegek.First());
        }

        private string emailkivagasa(string emailtTartalmazoSzoveg)
        {
            Match emailTalalat = Regex.Match(emailtTartalmazoSzoveg, RegularisKifejezesek.EMAIL);
            if (emailTalalat.Success)
                return emailTalalat.Value;
            else
                throw new Exception("A szuresi talalatnak tartalmaznia kell egy emailt. Hibas szuresi talalat: " + emailtTartalmazoSzoveg);
        }
    }
}
