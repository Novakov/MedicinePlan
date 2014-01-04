using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using Frontend.ViewModels;

namespace Frontend.Reports
{
    public class ExcelReportBuilder
    {
        private static readonly DosageHelper Helper = new DosageHelper();

        public static void CreateExcelReport(IList<MedicineStatus> medicines, DateTime asOfDate, string outputFile)
        {
            var doc = new XLWorkbook();
            var sheet = doc.AddWorksheet("Leki");
            sheet.Cell("A1").Value = "Data";
            sheet.Cell("A2").Value = "Lek";
            sheet.Cell("A3").Value = "Liczba tabletek";
            sheet.Cell("A4").Value = "Dawkowanie";
            sheet.Cell("A5").Value = "Starczy na dni";
            sheet.Cell("A6").Value = "Starczy do dnia";
            sheet.Cell("A8").Value = "Trzeba kupiæ";

            for (int i = 0; i < medicines.Count; i++)
            {
                sheet.Cell(1, 2 + i).SetValue(asOfDate);
                sheet.Cell(2, 2 + i).SetValue(medicines[i].Name.ToUpper());
                sheet.Cell(3, 2 + i).SetValue(medicines[i].Remaining);
                sheet.Cell(4, 2 + i).SetValue(Helper.GetDescription(medicines[i].Dosage));
                sheet.Cell(5, 2 + i).SetValue((int) medicines[i].ToExhaustion.TotalDays);
                sheet.Cell(6, 2 + i).SetValue(medicines[i].ExhaustionDate);
            }

            sheet.Cell("B8").Value = medicines.Min(x => x.ExhaustionDate).AddDays(-7);

            sheet.Range(1, 1, 6, 1 + medicines.Count).Style.Font.SetFontSize(14);
            sheet.Range(1, 2, 2, 1 + medicines.Count).Style
                .Font.SetBold(true)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            sheet.Range(4, 2, 4, 1 + medicines.Count).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

            sheet.Range(6, 1, 6, 1 + medicines.Count).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

            sheet.Cell("A6").Style.Font.SetBold(true);
            sheet.Cell("A8").Style
                .Font.SetBold(true)
                .Font.SetFontSize(14)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                .Alignment.SetVertical(XLAlignmentVerticalValues.Center);

            sheet.Cell("B8").Style
                .Font.SetBold(true)
                .Font.SetFontSize(24)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                .Alignment.SetVertical(XLAlignmentVerticalValues.Center);

            sheet.Range(6, 2, 6, 1 + medicines.Count)
                .AddConditionalFormat()
                .WhenIsBottom(1, XLTopBottomType.Items)
                .Font.SetBold(true);

            sheet.Rows().AdjustToContents();
            sheet.Columns().AdjustToContents();

            sheet.Row(6).Height = 45;
            sheet.Row(8).Height = 39.75;

            sheet.Range(8, 3, 8, 5)
                .Merge()
                .SetValue("7 dni przed koñcem najwczeœniejszego")
                .Style.Alignment.SetWrapText(true)
                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

            doc.SaveAs(outputFile);
        }
    }
}