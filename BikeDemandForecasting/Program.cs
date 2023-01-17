using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.TimeSeries;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Range = Microsoft.Office.Interop.Excel.Range;

MLContext mlContext = new MLContext();

    //create the Application object we can use in the member functions.
    Microsoft.Office.Interop.Excel.Application _excelApp = new Microsoft.Office.Interop.Excel.Application();
_excelApp.Visible = true;

string fileName = @"C:\Users\alexo\Downloads\daily_energy_2022.xlsx";
Debug.WriteLine(fileName);

//open the workbook
Workbook workbook = _excelApp.Workbooks.Open(fileName,
    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
    Type.Missing, Type.Missing);

//select the first sheet        
Worksheet worksheet = (Worksheet)workbook.Worksheets[1];

//find the used range in worksheet
Range excelRange = worksheet.UsedRange;

if (worksheet != null)
    excelRange = worksheet.get_Range("A1", Missing.Value);

string A1 = String.Empty;

if (excelRange != null)
    A1 = excelRange.Text.ToString();
else
    A1 = "it is empty";

Debug.WriteLine("A1 value: " + A1);

//clean up stuffs
workbook.Close(false, Type.Missing, Type.Missing);
Marshal.ReleaseComObject(workbook);

_excelApp.Quit();
Marshal.FinalReleaseComObject(_excelApp);




public class ModelInput
{
    public DateTime RentalDate { get; set; }
    public float Year { get; set; }
    public float TotalRentals { get; set; }
}

public class ModelOutput
{
    public float[] ForecastedRentals { get; set; }
    public float[] LowerBoundRentals { get; set; }
    public float[] UpperBoundRentals { get; set; }
}