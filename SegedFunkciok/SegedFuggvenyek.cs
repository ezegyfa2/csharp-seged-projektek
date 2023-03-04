﻿using Aspose.Imaging;
using Aspose.Imaging.ImageOptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace SegedFunkciok
{
    public class SegedFuggvenyek
    {
        public static void KepLetoltes(string link, string fileUtvonal)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(link), fileUtvonal);
            }
        }

        public static void JpgPngKonvertalas(string eredetiKepEleresiUtvonal, string celKepEleresiUtvonal = null)
        {
            if (celKepEleresiUtvonal == null)
            {
                celKepEleresiUtvonal = Path.Combine(Path.GetDirectoryName(eredetiKepEleresiUtvonal), Path.GetFileNameWithoutExtension(eredetiKepEleresiUtvonal) + ".png");
            }
            using (Image image = Image.Load(eredetiKepEleresiUtvonal))
            {
                // Create PNG options
                PngOptions options = new PngOptions() { ColorType = Aspose.Imaging.FileFormats.Png.PngColorType.TruecolorWithAlpha };

                // Save image as JPG
                image.Save(celKepEleresiUtvonal, options);
            }
        }

        public static List<string> ObjektumAdattagNevek(object objektum)
        {
            return objektum.GetType().GetFields().Select(adatTag => adatTag.Name).ToList();
        }

        public static List<string> ObjektumAdattagErtekek(object objektum)
        {
            List<string> adattagErtekek = new List<string>();
            FieldInfo[] fieldInfok = objektum.GetType().GetFields();
            foreach (FieldInfo fieldInfo in fieldInfok)
                adattagErtekek.Add(fieldInfo.GetValue(objektum).ToString());
            return adattagErtekek;
        }
    }
}