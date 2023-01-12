using ElectronicShop.DesignPatterns.FactoryMethod;
using ElectronicShop.ShopModels.Models;
using Microsoft.EntityFrameworkCore;
using ElectronicShop.DesignPatterns.FactoryMethod.ProductBase;

namespace ElectronicShop.ShopModels
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }
        public DbSet<electronicproductmodel>? electronicproducts { get; set; }  //Creating the table in the database
        public DbSet<usermodel>? users { get; set; }
        public DbSet<orderdetailsmodel>? ordersdetails { get; set; }
        public DbSet<ordersmodel>? orders { get; set; }
        public DbSet<ElectronicShopExceptionTable>? AllExceptionsTable { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Giving Initial Data to the tables in the database
            string[] usernames = { "Joe", "John", "Maria" };
            string[] lastnames = { "Joe", "John", "Maria" };
            int[] totalprices = { 2100, 2500, 3500 };
            int j = 1;


            List<ProductElectronic> listproducts = GetProductsFactories.CreateManyProducts();
            foreach (var a in listproducts)
            {
                electronicproductmodel p = new electronicproductmodel(j++, a.Name!, a.Type!, a.Processor, a.Numberofcores, a.Screensize, a.Memory, a.Storage, a.Battery, a.Numberofproducts, a.Price);
                builder.Entity<electronicproductmodel>().HasData(p);
            }

            for (int i = 0; i < usernames.Length; i++)
            {
                builder.Entity<usermodel>().HasData(new usermodel { userID = i+1 , firstname = usernames[i], lastname = lastnames[i], age = (new Random()).Next(18,50),
                password = generatepassword(), phonenumber = generatephonenumber(), balance = (float)((new Random()).Next(7500,20000))});
            }

            for (int i = 0; i < usernames.Length; i++)
            {
                builder.Entity<ordersmodel>().HasData(new ordersmodel
                {
                    orderID = i + 1,
                    userID = (new Random()).Next(1, usernames.Length+1),
                    totalnumberofobjectsbought = (new Random()).Next(1, usernames.Length+1) * (new Random()).Next(1, usernames.Length+1),
                    totalprice = totalprices[(new Random()).Next(totalprices.Length)],
                });
            }
            
            for (int i = 0; i < usernames.Length; i++)
            {
                builder.Entity<orderdetailsmodel>().HasData(new orderdetailsmodel
                {
                    orderdetailID = i + 1,
                    orderID = (new Random()).Next(1, usernames.Length + 1),
                    electronicproductID = (new Random()).Next(1, usernames.Length + 1),
                    boughtdate = DateTime.Now,
                    quantity = (new Random()).Next(totalprices.Length) + 1,
                    price = totalprices[(new Random()).Next(totalprices.Length)]  / 3,
                });
            }

        }

        static string generatepassword()
        {
            Random rnd = new Random();
            int k, j = rnd.Next(3, 10);
            char u;
            string code = string.Empty;
            for (int i = 0; i < j; i++)
            {
                k = rnd.Next(0, 2);
                if (k == 0)
                {
                    k = rnd.Next(65, 91);
                    u = (char)k;
                    code = code + u;
                }
                else
                {
                    k = rnd.Next(97, 123);
                    u = (char)k;
                    code = code + u;
                }
            }
            return code;
        }

        static int generatephonenumber()
        {
            string[] numbers = { "1", "4", "6", "9" };
            Random rnd = new Random();
            string number = "7";
            number += numbers[rnd.Next(numbers.Length)];
            for (int i = 0; i < 6; i++)
            {
                number += rnd.Next(0, 10);
            }
            return Convert.ToInt32(number);
        }
    }
}