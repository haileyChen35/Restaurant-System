using System;
using System.Collections.Generic;
using System.Text.Json;

namespace RestaurantManagementSystem
{
    public class Menu
    {
        private static List<MenuItem> menuItems = new List<MenuItem>();

        string filePath = "menu.json";

        /*[

        { 
            "Name": "Burger", 
            "Price": 5.99 
        },
        {
            "Name": "Pizza",
            "Price": 8.99
        },
        {
            "Name": "Salad",
            "Price": 4.99
        }
        ]*/

        // public record struct MenuItem(
        //     string Name,
        //     decimal Price
        // );

        public Menu()
        {
            // Initialize menu items
            menuItems = LoadMenuFromFile(filePath);
            // menuItems.Add(new MenuItem("Burger", 5.99m));
            // menuItems.Add(new MenuItem("Pizza", 8.99m));
            // menuItems.Add(new MenuItem("Pasta", 7.99m));
            // menuItems.Add(new MenuItem("Salad", 4.99m));
        }

        public static List<MenuItem> MenuItems(){

            return menuItems;
        }

        public void ShowMenu()
        {
            Console.WriteLine("===== Menu =====");
            if (menuItems.Count == 0)
            {
                Console.WriteLine("The menu is currently empty.");
                return;
            }

            for (int i = 0; i < menuItems.Count; i++)
            {
                var menuItem = menuItems[i];
                Console.WriteLine($"{i + 1}. {menuItem.Name,-20} - ${menuItem.Price:F2}");
            }
            Console.WriteLine("================");
        }

        public void RemoveMenuItemByIndex()
        {
            ShowMenu();
            try{
                Console.WriteLine("\nEnter the number of the menu item to remove:");
                int menuIndex = int.Parse(Console.ReadLine())-1;
                if (menuIndex >= 0 && menuIndex < menuItems.Count)
                {
                    // console before RemoveAt() method to print the right index && prevent index out of bound
                    Console.WriteLine($"Removing: {menuItems[menuIndex].Name}"); 
                    menuItems.RemoveAt(menuIndex);
                    SaveMenuToFile(filePath,menuItems);
                }
                else
                {
                    Console.WriteLine("Invalid index. No item removed.");
                }
            }catch(Exception ex){
                Console.WriteLine("Invalid input. No item removed.");
            }
        }

        public void AddMenuItem(){
            ShowMenu();
            try{
                Console.WriteLine("\nEnter the name of the new menu item:");
                string name = Console.ReadLine();

                Console.WriteLine($"Enter the price of {name}:");
                decimal price = decimal.Parse(Console.ReadLine());
                if(price >= 0){
                menuItems.Add(new MenuItem(name, price));
                SaveMenuToFile(filePath,menuItems);
                Console.WriteLine($"Menu item '{name}' added successfully with price ${price:F2}.");
                }
                else{       
                    Console.WriteLine("Invalid price. The menu item was not added.");
                }
            }catch(Exception ex){
                Console.WriteLine("Invalid input. New Item was not added.");
            }

        }

        public void UpdateMenuItem(){
            
            ShowMenu();
            try{
                Console.Write("\nEnter the index of the menu item to update: ");
                int index = int.Parse(Console.ReadLine());
                if(index >= 1 && index <= menuItems.Count){

                    MenuItem itemToUpdate = menuItems[index-1];
                    Console.WriteLine($"You are updating: {itemToUpdate.Name} - ${itemToUpdate.Price}");

                    // Get new name input
                    Console.Write($"Enter a new name for '{itemToUpdate.Name}' (or press Enter to keep the same): ");
                    string newName = Console.ReadLine();

                    // Get new price input
                    Console.Write($"Enter a new price for '{itemToUpdate.Name}' (or press Enter to keep the same): ");
                    string newPriceInput = Console.ReadLine();

                    // Update name if a new name is provided
                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        itemToUpdate.Name = newName;
                    }

                    // Update price if a new price is provided
                    if (!string.IsNullOrWhiteSpace(newPriceInput))
                    {
                        // Update price if a price input is valid 
                        if(decimal.TryParse(newPriceInput, out decimal newPrice) && newPrice >= 0)
                            itemToUpdate.Price = newPrice;
                        else
                            Console.WriteLine("Invalid price. The item price was not updated.");
                    }

                    SaveMenuToFile(filePath,menuItems);
                    Console.WriteLine($"Menu item at index {index} has been updated.");
                    
                    
                }
                else
                {
                    Console.WriteLine("Invalid index. No item updated.");
                }
            }catch(Exception ex){
                Console.WriteLine("Invalid input. No item updated.");
            }

        }

        public void ModifyMenu(){

            Console.WriteLine("===== Modify Menu =====");
            Console.WriteLine("Please select an option from the menu below:");

            // Display menu options
            Console.WriteLine("1. Add a new item");
            Console.WriteLine("2. Update an existing item");
            Console.WriteLine("3. Remove an existing item");

            // Prompt the user for input
            Console.Write("Enter your choice (1, 2, or 3):");
            string choice = Console.ReadLine();

            // Process the user's choice
            switch (choice)
            {
                case "1":
                    Console.WriteLine("\n Adding new item...");
                    AddMenuItem();
                    break;
                case "2":
                    Console.WriteLine("\n Update existing item...");
                    UpdateMenuItem();
                    break;
                case "3":
                    Console.WriteLine("\n Remove existing item...");
                    RemoveMenuItemByIndex();
                    break;
                default:
                    Console.WriteLine("\nInvalid choice. Please select a valid option (1, 2, or 3).");
                    break;
            }

            Console.WriteLine("=========================");
        }

        // Method to load menu items from a JSON file
        public List<MenuItem> LoadMenuFromFile(string filePath){

            
            // StreamReader r = new StreamReader(filePath);

            // Check if the file exists
            if(File.Exists(filePath)){

                // Read all json file into string
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<MenuItem>>(json) ?? new List<MenuItem>();

            }else{
                Console.WriteLine("Menu file not found. Loading an empty menu.");
                return new List<MenuItem>();
            }

        }


        public void SaveMenuToFile(string filePath, List<MenuItem> menuItems){

            // Check if the file exists
            if(File.Exists(filePath)){

                string jsonData = JsonSerializer.Serialize(menuItems, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonData);

            }else{
                Console.WriteLine("Menu file not found. Loading an empty menu.");
            }
        }

       



    }
}
