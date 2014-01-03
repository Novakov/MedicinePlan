using System;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.ViewModels
{
    public class RefillAllMedicinesViewModel
    {
        public DateTime Date { get; set; }
        public List<Item> Stocks { get; set; }

        public RefillAllMedicinesViewModel(IEnumerable<string> medicines)
        {
            this.Stocks = medicines.Select(x => new Item(x)).ToList();
            this.Date = DateTime.Now;
        }

        public RefillAllMedicinesViewModel()
            : this(new[] { "VitaminuC", "VitaminuD", "VitaminuE" })
        {

        }

        public class Item
        {
            public string MedicineName { get; private set; }
            public int Count { get; set; }

            public Item(string medicineName)
            {
                this.MedicineName = medicineName;
                this.Count = 0;
            }
        }
    }
}