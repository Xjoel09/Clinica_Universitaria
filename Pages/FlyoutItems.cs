﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MedicalUTP.Pages
{
    public class FlyoutItems
    {
        public required string Title { get; set; }
        public required string IconSource { get; set; }
        public required Type? TargetType { get; set; }
        public bool IsLogout { get; set; } = false;

        

        public string FlyoutDisplayOptions { get; set; } 
        

        public Color BackgroundColor { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
