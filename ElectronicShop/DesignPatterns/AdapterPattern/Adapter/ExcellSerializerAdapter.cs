using ElectronicShop.DesignPatterns.AdapterPattern.Target;
using OfficeOpenXml;
using System.Data;

namespace ElectronicShop.DesignPatterns.AdapterPattern.Adapter
{
    public class ExcellSerializerAdapter : ISerializerAdapter
    {
        public string Serialize<T>(object objToSerialize)
        {
            try
            {
                string excellfilename = "Products", currentDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    filepath = Path.Combine(currentDirectory, excellfilename.Trim() + ".xlsx"), xls = AppDomain.CurrentDomain.BaseDirectory + excellfilename.Trim() + ".xlsx";

                if(objToSerialize.GetType() == typeof(DataTable))
                {
                    DataTable? table = objToSerialize as DataTable;
                    var format = new ExcelTextFormat();
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Products");
                        ws.Cells["A1"].LoadFromDataTable(table, true);
                        pck.SaveAs(new FileInfo(xls));
                    }
                }
                
                return "";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}