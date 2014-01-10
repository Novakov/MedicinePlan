using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontend.ViewModels
{
    public class ChangeDosageViewModel
    {
        public string Name { get; set; }

        public int NewDosage { get; set; }

        public DateTime Since { get; set; }

        public ChangeDosageViewModel()
        {
            this.Since = DateTime.Today;
        }
    }
}
