using System;
using System.Collections.Generic;
using System.Text;

namespace PROAtas.Core.Model
{
    public class Settings
    {
        public Settings() { }

        public Settings(Settings model)
        {
            User = model.User;
            Organization = model.Organization;
            FontSize = model.FontSize;
            FontFamily = model.FontFamily;

            MarginLeft = model.MarginLeft;
            MarginTop = model.MarginTop;
            MarginRight = model.MarginRight;
            MarginBottom = model.MarginBottom;
        }

        public string User { get; set; }
        public string Organization { get; set; }
        public double FontSize { get; set; }
        public string FontFamily { get; set; }

        public double MarginLeft { get; set; }
        public double MarginTop { get; set; }
        public double MarginRight { get; set; }
        public double MarginBottom { get; set; }
    }
}
