using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MedicalUTP.Models
{
    public class FlyoutItems
    {
        public required string Title { get; set; }
        public required string IconSource { get; set; }
        public Type? TargetType { get; set; }
        public bool IsLogout { get; set; } = false;

        public Color BackgroundColor { get; set; }
        public bool IsSelected { get; set; } = false;


    }
}
