using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Xps;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Frontend.Reports;
using MedicinePlan;
using Microsoft.Win32;
using Paragraph = System.Windows.Documents.Paragraph;
using Run = System.Windows.Documents.Run;

namespace Frontend.ViewModels
{
    public class MainViewModel
    {        
        private readonly Supplies supplies;
        private DateTime asOfDate;

        public ObservableCollection<MedicineStatus> Medicines { get; set; }

        public DateTime AsOfDate
        {
            get { return this.asOfDate; }
            set { this.asOfDate = value; this.DumpSuppliesStatus(); }
        }

        public ICommand AddMedicineCommand { get; set; }
        public ICommand RefillCommand { get; set; }
        public ICommand RefillAllCommand { get; set; }
        public ICommand ExcelReportCommand { get; set; }
        public ICommand ChangeDosageCommand { get; set; }
        public ICommand RemoveMedicineCommand { get; set; }
        public ICommand PrintCommand { get; set; }

        public event EventHandler SuppliesChanged;

        public MainViewModel(Supplies supplies)
        {
            this.supplies = supplies;

            this.asOfDate = DateTime.Today;

            this.AddMedicineCommand = new DelegateCommand(AddMedicine);
            this.RefillCommand = new DelegateCommand<string>(RefillMedicine);
            this.ChangeDosageCommand = new DelegateCommand<string>(ChangeDosage);
            this.RefillAllCommand = new DelegateCommand(RefillAllMedicine);
            this.ExcelReportCommand = new DelegateCommand(ExcelReport);
            this.RemoveMedicineCommand = new DelegateCommand<string>(RemoveMedicine);
            this.PrintCommand = new DelegateCommand(Print);

            this.Medicines = new ObservableCollection<MedicineStatus>();

            this.DumpSuppliesStatus();
        }

        private void Print()
        {         
            PrintDocumentImageableArea ia = null;

            var writer = PrintQueue.CreateXpsDocumentWriter(ref ia);

            if (writer == null)
            {
                return;
            }

            var report = new MedicineListPrinted(new MedicineListPrintedViewModel()
            {
                Medicines = this.Medicines.ToList(),
                AsOfDate = this.AsOfDate
            });

            var paginator = ((IDocumentPaginatorSource)report).DocumentPaginator;

            paginator.PageSize = new Size(ia.MediaSizeWidth, ia.MediaSizeHeight);

            report.ColumnWidth = double.PositiveInfinity;

            writer.Write(paginator);
        }

        private void RemoveMedicine(string medicineName)
        {
            this.supplies.RemoveMedicine(new Medicine(medicineName));

            this.DumpSuppliesStatus();
        }

        private void ChangeDosage(string medicineName)
        {
            var medicine = new Medicine(medicineName);

            var viewModel = new ChangeDosageViewModel
            {
                Name = medicineName,
                NewDosage = ((CountPerDayDosage) this.supplies.CurrentDosage(medicine, this.asOfDate)).CountPerDay
            };

            var window = new ChangeDosage(viewModel);

            if (window.ShowDialog() == true)
            {
                this.supplies.AddDosage(medicine, viewModel.Since, new CountPerDayDosage(viewModel.NewDosage));

                this.OnSuppliesChanged();

                this.DumpSuppliesStatus();
            }
        }

        private void ExcelReport()
        {
            var saveDialog = new SaveFileDialog
            {
                DefaultExt = ".xlsx",
                FileName = "Leki.xlsx",
                Filter = "Pliki Excel (*.xlsx)|*.xlsx"
            };

            if (saveDialog.ShowDialog() == true)
            {
                ExcelReportBuilder.CreateExcelReport(this.Medicines.ToArray(), this.asOfDate, saveDialog.FileName);
            }
        }

        private void RefillAllMedicine()
        {
            var viewModel = new RefillAllMedicinesViewModel(this.supplies.GetMedicines().Select(x => x.Name));

            var window = new RefillAllMedicinesWindow(viewModel);

            if (window.ShowDialog() == true)
            {
                this.supplies.Refill(viewModel.Stocks.Where(x => x.Count > 0).ToDictionary(x => new Medicine(x.MedicineName), x => new Stock(x.Count, viewModel.Date.Date)));

                this.OnSuppliesChanged();

                this.DumpSuppliesStatus();
            }
        }

        private void RefillMedicine(string medicineName)
        {
            var viewModel = new RefillMedicineViewModel { MedicineName = medicineName };

            var window = new RefillMedicineWindow(viewModel);

            if (window.ShowDialog() == true)
            {
                this.supplies.Refill(new Medicine(medicineName), new Stock(viewModel.Count, viewModel.Date.Date));

                this.OnSuppliesChanged();

                this.DumpSuppliesStatus();
            }
        }

        private void AddMedicine()
        {
            var viewModel = new AddMedicineViewModel();

            var addDosage = new AddMedicineWindow(viewModel);

            if (addDosage.ShowDialog() == true)
            {
                this.supplies.AddDosage(new Medicine(viewModel.Name), viewModel.Date, new CountPerDayDosage(viewModel.CountPerDay));

                this.OnSuppliesChanged();

                this.DumpSuppliesStatus();
            }
        }

        private void DumpSuppliesStatus()
        {
            this.Medicines.Clear();

            foreach (var medicine in this.supplies.GetMedicines())
            {
                this.Medicines.Add(new MedicineStatus
                {
                    AsOfDate = this.asOfDate,
                    Name = medicine.Name,
                    Dosage = this.supplies.CurrentDosage(medicine, this.asOfDate),
                    ExhaustionDate = this.supplies.ExhaustionOf(medicine),
                    Remaining = this.supplies.RemainingStock(medicine, this.asOfDate).Count,
                });
            }
        }

        private void OnSuppliesChanged()
        {
            if (this.SuppliesChanged != null)
            {
                this.SuppliesChanged(this, EventArgs.Empty);
            }
        }
    }
}
