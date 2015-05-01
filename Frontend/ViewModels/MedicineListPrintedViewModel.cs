using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontend.ViewModels
{
    public class MedicineListPrintedViewModel
    {
        public List<MedicineStatus> Medicines { get; set; }

        public DateTime AsOfDate { get; set; }

        public MedicineListPrintedViewModel()
        {
            this.Medicines = new List<MedicineStatus>();
        }
    }
}
