using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore
{
    internal class Program
    {
        public static void Main(string[] args) 
        {
            int numberOfProducts = InputValue(1, 30); //Get the number of products from user input 

            Product[] products = new Product[numberOfProducts]; //Initialize product array 
            GetProductData(products); //Populates the product data

            DisplayAllProducts(products); //Dispalys product details

            double totalRevenue = CalculateRevenue(products); //Calculates total revenue
            Console.Write($"Total Revenue is: ${totalRevenue}");

            GetProductLists(products); //Returns product list based on category input 
        }
        public static int InputValue(int min, int max)
        {
            int value;
            Console.Write($"Enter the number of products between the range: {min} and {max}: ");
            while (!(int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)) /*while loops continues to prompt user until input value is valid,
            int.TryParse converts the user inputted string into an int, if successful, the parsed interger is assigned to value. Value must also be within the min-max range*/
            {
                Console.WriteLine($"Invalid input. Please enter a valid number between {min} and {max}.");
            }
            return value;
        }
        public static bool CheckString(string id)
        {
            if (id.Length == 4 && char.IsLower(id[0]) && id[1] == '-' && char.IsDigit(id[2]) && char.IsDigit(id[3])) //checks if inputted id length is 4, first letter is lower case, 2nd letter '-', 3rd and 4th are digits 
            {
                return true;
            }
         return false;
        }
        private static void GetProductData(Product[] products)
        {
            Console.WriteLine("Valid Product Categories:");
            for (int i = 0; i < Product.categoryCodes.Length; i++) //Lists the valid product categories using a for loop to display each element of the categoryCodes and categoryNames array 
            {
                Console.WriteLine($"{Product.categoryCodes[i]}: {Product.categoryNames[i]}"); //displays matching category code to names according to position in array
            }

            for (int i = 0; i < products.Length; i++)
            {
                Console.WriteLine($"Enter details for product {i + 1}:"); //prompts user to enter details for product 1, for loop allows input of multiple products 
                string id;

                Console.Write("Enter Product ID (valid format: a-22): "); //prompts user to enter ID parameter 
                id = Console.ReadLine();
                while (!CheckString(id)) //while loop to make sure the ID format is valid using CheckString() 
                {
                    Console.WriteLine("Invalid format. Please enter a valid product ID.");
                    Console.Write("Enter Product ID (valid format: a-22): ");
                    id = Console.ReadLine();
                }

                Console.Write("Enter Product Name: "); //prompts user to enter name parameter of Product constructor 
                string name = Console.ReadLine();

                Console.Write("Enter Product Quantity: "); //prompts user to enter quantity parameter of Product constructor 
                int quantity; 
                while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid quantity.");
                }     

                Console.Write("Enter Product Price: "); //prompts user to enter price parameter of Product constructor 
                double price;
                while (!double.TryParse(Console.ReadLine(), out price) || price < 0) //.tryparse converts user inputted string to double, if valid then assigned to price. price must be a positive integer
                {
                    Console.WriteLine("Invalid input. Please enter a valid price.");
                }
                products[i] = new Product(id, name, quantity, price); //new object product array of Class Product created with user inputted parameters 
            }
        }
        public static void DisplayAllProducts(Product[] products)
            {
                for (int i = 0; i < products.Length; i++) //loops through each product in the array
                {
                    Console.WriteLine(products[i].ToString()); //for each product of the array, the ToString() method of the Product class is called. Displaying the inputted array in a string format. 
                }
            }
        public static double CalculateRevenue(Product[] products)
        {
            double totalRevenue = 0;
            for (int i = 0; i < products.Length; i++)
            {
                totalRevenue += products[i].ProductQuantity * products[i].ProductPrice; //loops through each product and calculates the price by multiplying quanty to price then adding/storing the total in totalRevenue 
            }
            return totalRevenue;
        }
        private static void GetProductLists(Product[] products)
        {
            Console.WriteLine("\nValid product categories:");
            for (int i = 0; i < Product.categoryCodes.Length; i++)
            {
                Console.WriteLine($"{Product.categoryCodes[i]}: {Product.categoryNames[i]}"); //loops through and lists the category codes and corresponding category names 
            }

            Console.WriteLine("Enter a category code to see the products in that category (include the '-'), or type 'exit' to quit:");
            string input = Console.ReadLine();

            while (input.ToLower() != "exit") //if user doesn't input exit
            {
                bool isValidCode = false; //initiate a boolean 
                for (int i = 0; i < Product.categoryCodes.Length; i++) 
                {
                    if (input == Product.categoryCodes[i]) //if user input code matches the required format in categoryCode[] then isValidCode = true 
                    {
                        isValidCode = true;
                    }
                }

                if (isValidCode)
                {
                    DisplayProductsInCategory(products, input); //if matched then displays the products in correct format with DisplayProductInCategory() method 
                }
                else
                {
                    Console.WriteLine("Invalid category code. Please try again.");
                }

                Console.WriteLine("Enter another category code to see the products, or type 'exit' to quit:"); //prompts user to enter another category or exit to quit 
                input = Console.ReadLine();
            }
        }

        private static void DisplayProductsInCategory(Product[] products, string categoryCode)
        {
            int count = 0;
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].ProductID.Length >= 2 && products[i].ProductID.Substring(0, 2) == categoryCode) // checks that the inputted ID is at least 2 characters long and that the first two characters matches categoryCode 
                {
                    Console.WriteLine(products[i].ToString()); //if condition met, display inputted product array using ToString method 
                    count++; //stores number of products found in the specific category 
                }
            }

            if (count == 0)
            {
                Console.WriteLine($"No products found in category {categoryCode}."); //if count = 0, then displays that no products are found 
            }
            else
            {
                Console.WriteLine($"Total products in category {categoryCode}: {count}"); //displays categoryCode + the number of products in the category 
            }
            }
        }
    }


