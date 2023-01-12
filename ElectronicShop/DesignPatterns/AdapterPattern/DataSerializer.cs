using ElectronicShop.DesignPatterns.AdapterPattern.Target;
using ElectronicShop.DesignPatterns.FactoryMethod.ProductBase;
using ElectronicShop.ShopModels.DTOModel.DTO;
using OfficeOpenXml;
using System.Data;
using System.Xml;

namespace ElectronicShop.DesignPatterns.AdapterPattern
{
    public class DataSerializer
    {
        private readonly ISerializerAdapter _serializer;

        static internal string currentDirectory = AppDomain.CurrentDomain.BaseDirectory,
                        jsonfilepath = Path.Combine(currentDirectory, "Products.json"),
                        xmlfilepath = Path.Combine(currentDirectory, "Products.xml");
        static FileInfo fi = new FileInfo(currentDirectory);
        static DirectoryInfo? di = fi.Directory;
        static FileInfo[]? files = di?.GetFiles();

        public DataSerializer(ISerializerAdapter serializer)
        {
            _serializer = serializer;
        }

        public void CreateJSONFileOfElectronicProducts(List<ElectronicProductDTO> l)
        {
            try
            {
                foreach (FileInfo f in files!)
                {
                    if (f.Extension == ".json") f.Delete();
                }
                string s = _serializer.Serialize<ProductElectronic>(l) + "\n";
                File.WriteAllText(jsonfilepath, s);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateXMLFileOfElectronicProducts(List<ElectronicProductDTO> l)
        {
            try
            {
                foreach (FileInfo f in files!)
                {
                    if (f.Extension == ".xml") f.Delete();
                }
                int i = 1;
                using (XmlWriter writer = XmlWriter.Create(xmlfilepath))
                {
                    writer.WriteStartElement("ElectronicProducts");
                    foreach (var x in l)
                    {
                        writer.WriteElementString("ElectronicProduct" + i, _serializer.Serialize<ElectronicProductDTO>(x));
                        i++;
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateExcellFileOfElectronicProducts(List<ElectronicProductDTO> l)
        {
            try
            {

                DataTable table = new DataTable();

                table.Columns.Add("ID", typeof(string));                    table.Columns.Add("Name", typeof(string)); 
                table.Columns.Add("Type", typeof(string));                  table.Columns.Add("Processor", typeof(string));
                table.Columns.Add("Numberofcores", typeof(string));         table.Columns.Add("Screensize", typeof(string)); 
                table.Columns.Add("Memory", typeof(string));                table.Columns.Add("Storage", typeof(string)); 
                table.Columns.Add("Battery", typeof(string));               table.Columns.Add("NumberOfProducts", typeof(string)); 
                table.Columns.Add("Price", typeof(string));

                int id = 1;

                foreach (var m in l)
                {
                    table.Rows.Add(id++, m.name, m.type, m.processor, m.numberofcores, m.screensize, m.memory, m.storage, m.battery, m.numberofproducts, m.price);
                }

                deleteExcelAndCreateAnotherFile();
                _serializer.Serialize<ElectronicProductDTO>(table);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void deleteExcelAndCreateAnotherFile()
        {
            try
            {
                string excellfilename = "Products", filepath = Path.Combine(currentDirectory, excellfilename.Trim() + ".xlsx");

                foreach (FileInfo f in files!)
                {
                    if (f.Extension == ".xlsx") f.Delete();
                }

                if (!File.Exists(filepath))
                {
                    var myfile = File.CreateText(filepath);
                    myfile.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
