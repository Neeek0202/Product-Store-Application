using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore
{
    public class Product
    {
        // Public arrays for category codes and corresponding names 
        public static string[] categoryCodes = { "e-", "b-", "a-", "t-", "o-" };
        public static string[] categoryNames = { "Electronics", "Books", "Apparel", "Toys", "Others" };

        private string productID;
        private string productCategoryName;

        // Autoimplemented properties 
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public double ProductPrice { get; set; }

        // Properties for ProductID for validation 
        public string ProductID
        {
            get { return productID; }
            set
            { if (value.Length == 4 && char.IsLower(value[0]) && value[1] == '-' && char.IsDigit(value[2]) && char.IsDigit(value[3])) //
                {
                    productID = value; //if condition above is met, assign the value to productID 
                    productCategoryName = GetCategoryName(value); //calls GetCategoryName() method to get category name based on product ID 
                }
            else
                {
                    productID = "o-" + value.Substring(2); //otherwise returns 'o-' and .Substring(2) retains last 2 numbers assigned starting from position 2 of string  
                    productCategoryName = "Others"; 
                }
            }
        }
      
        // ProductCategoryName read only properly 
        public string ProductCategoryName { 
            get { return productCategoryName; } 
        }
        // Set constructors 
        public Product() // Default constructor with no parameter 
        {

        }
        public Product(string id, string name, int quantity, double price) // constructor with parameter for all fields 
        {
            ProductID = id;
            ProductName = name;
            ProductQuantity = quantity; 
            ProductPrice = price;   
        }
        private static string GetCategoryName(string id)
        {
            string code = id.Substring(0, 2); //.substring (0,2) - we want to extract the first 2 letters of the string id starting from position 0 
            for (int i = 0; i < categoryCodes.Length; i++) //loop created to match categoryCode to categoryNames 
            {
                if (categoryCodes[i] == code)
                {
                    return categoryNames[i]; //returns corresponding categoryName
                }
            }
            return "Others";
        }
        public override string ToString() 
        {
            return $"Product ID: {ProductID}, Product Category: {ProductCategoryName}, Name: {ProductName}, Quantity: {ProductQuantity}, Price: {ProductPrice:C}";
        }
    }
}



