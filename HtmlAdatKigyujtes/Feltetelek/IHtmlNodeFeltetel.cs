﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAdatKigyujtes.Feltetelek
{
    public interface IHtmlNodeFeltetel
    {
        bool Megfelele(HtmlNode node);
    }
}
