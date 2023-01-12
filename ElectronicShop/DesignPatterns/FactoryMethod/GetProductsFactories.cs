using ElectronicShop.DesignPatterns.FactoryMethod.FactoryBase;
using ElectronicShop.DesignPatterns.FactoryMethod.ProductBase;
using System.Reflection;
using ElectronicShop.DesignPatterns.FactoryMethod.ConcreteFactories;

namespace ElectronicShop.DesignPatterns.FactoryMethod
{
    public class GetProductsFactories
    {
        /*
        static public string[] PhoneNameArray = { "Iphone12", "Iphone10" }, LaptopNameArray = { "MacbookPro", "MacbookAir" },
                               PhoneProcessorArray = { "A15 Bionic", "A13 bionic" }, LaptopProcessorArray = { "Apple M1", "i7" };
        */
        /*
        static public int[] PhoneNumberOfCoresArray = { 4, 8 }, LaptopNumberOfCoresArray = { 6, 8 },
                            PhonsSreensizeArray = { 4, 5 }, LaptopSreensizeArray = { 14, 17 },
                            PhoneMemoryArray = { 4, 8 }, LaptopMemoryArray = { 8, 16 },
                            PhoneStorageArray = { 32, 64 }, LaptopStorageArray = { 256, 512 },
                            PhoneBatteryArray = { 15, 25 }, LaptopBatteryArray = { 20, 24 },
                            //PhoneNumberofproductsArray = { 5, 10 }, LaptopNumberofproductsArray = { 5, 10 },
                            PhonePriceArray = { 800, 1200 }, LaptopPriceArray = { 750, 1500 };
        */
        static public string[] PhoneNameArray = { "Iphone12" }, LaptopNameArray = { "MacbookPro" },
                               PhoneProcessorArray = { "A15 Bionic", "A13 bionic" }, LaptopProcessorArray = { "Apple M1", "i7" };

        static public int[] PhoneNumberOfCoresArray = { 4}, LaptopNumberOfCoresArray = { 6, 8 },
                            PhonsSreensizeArray = { 4 }, LaptopSreensizeArray = { 14 },
                            PhoneMemoryArray = { 4, 8 }, LaptopMemoryArray = { 8 , 32 },
                            PhoneStorageArray = { 32}, LaptopStorageArray = { 256 },
                            PhoneBatteryArray = { 15 }, LaptopBatteryArray = { 20 },
                            PhoneNumberofproductsArray = { 5 }, LaptopNumberofproductsArray = { 10 },
                            PhonePriceArray = { 800, 1200 }, LaptopPriceArray = { 750, 1500 };
        public static List<ProductElectronic> CreateManyProducts()
        {
            try
            {
                Random rnd = new Random();
                List<ProductElectronic> listofproducts = new List<ProductElectronic>();
                string name, processor;
                int numberofcores, screensize, memory, storage, battery, numberofproducts, price;

                foreach (var factory in GetFactories())
                {
                    for (int i = 0; i < 15; i++)
                    {
                        if (factory.GetType() == typeof(PhoneFactory))
                        {
                            name = PhoneNameArray[rnd.Next(PhoneNameArray.Length)];
                            processor = PhoneProcessorArray[rnd.Next(PhoneProcessorArray.Length)];
                            numberofcores = PhoneNumberOfCoresArray[rnd.Next(PhoneNumberOfCoresArray.Length)];
                            screensize = PhonsSreensizeArray[rnd.Next(PhonsSreensizeArray.Length)];
                            memory = PhoneMemoryArray[rnd.Next(PhoneMemoryArray.Length)];
                            storage = PhoneStorageArray[rnd.Next(PhoneStorageArray.Length)];
                            battery = PhoneBatteryArray[rnd.Next(PhoneBatteryArray.Length)];
                            numberofproducts = PhoneNumberofproductsArray[rnd.Next(PhoneNumberofproductsArray.Length)];
                            price = PhonePriceArray[rnd.Next(PhonePriceArray.Length)];
                        }
                        else
                        {
                            name = LaptopNameArray[rnd.Next(LaptopNameArray.Length)];
                            processor = LaptopProcessorArray[rnd.Next(LaptopProcessorArray.Length)];
                            numberofcores = LaptopNumberOfCoresArray[rnd.Next(LaptopNumberOfCoresArray.Length)];
                            screensize = LaptopSreensizeArray[rnd.Next(LaptopSreensizeArray.Length)];
                            memory = LaptopMemoryArray[rnd.Next(LaptopMemoryArray.Length)];
                            storage = LaptopStorageArray[rnd.Next(LaptopStorageArray.Length)];
                            battery = LaptopBatteryArray[rnd.Next(LaptopBatteryArray.Length)];
                            numberofproducts = LaptopNumberofproductsArray[rnd.Next(LaptopNumberofproductsArray.Length)];
                            price = LaptopPriceArray[rnd.Next(LaptopPriceArray.Length)];
                        }

                        var a = factory.CreateElectronicProduct(name, processor, numberofcores, screensize, memory, storage, battery, numberofproducts, price);
                        
                        int index = listofproducts.FindIndex(f => f.Name == a.Name && f.Type == a.Type && f.Processor == a.Processor && f.Numberofcores == a.Numberofcores
                            && f.Screensize == a.Screensize && f.Memory == a.Memory && f.Storage == a.Storage && f.Battery == a.Battery && f.Price == a.Price);
                        if (index == -1)
                        {
                            a.Numberofproducts = 1;
                            listofproducts.Add(a);
                        }
                        else listofproducts[index].Numberofproducts++;
                    }
                }
                return listofproducts;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static IEnumerable<IProductElectronicFactory> GetFactories()
        {
            
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                if (type.GetInterface(typeof(IProductElectronicFactory).ToString()) != null)
                {
                    yield return Activator.CreateInstance(type) as IProductElectronicFactory;
                }
            }
        }
    }
}