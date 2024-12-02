using System;

namespace RestaurantManagementSystem
{
    class Program
    {

        static void Main(string[] args)
        {

            OrderHistory order = new OrderHistory();
            FinancialReport report = new FinancialReport();
            Menu menu = new Menu();
            
            while(true){

                Console.WriteLine("\nWelcome to the Restaurant Management System!\n");
                Authentication auth = new Authentication();
                int userRole = auth.Login();  // Get user role (1: Owner, 2: Employee, 3: Invalid)

                if(userRole == 3){
                    continue;  // go back to login
                }
                

                while (userRole != 3)  // Continue the loop as long as the user is authenticated
                {
                    if (userRole == 1) // Owner role
                    {
                        Console.WriteLine("\nSelect an option:");
                        Console.WriteLine("1. Take an order or Save a reservation");
                        Console.WriteLine("2. Pay reserved order");
                        Console.WriteLine("3. Remove reserved order");
                        Console.WriteLine("4. View Financial Report");
                        Console.WriteLine("5. View Menu");
                        Console.WriteLine("6. Modify Menu");
                        Console.WriteLine("7. View Order History");
                    }
                    else if (userRole == 2) // Employee role
                    {
                        Console.WriteLine("\nSelect an option:");
                        Console.WriteLine("1. Take an order or Save a reservation");
                        Console.WriteLine("2. Pay reserved order");
                        Console.WriteLine("3. View Menu");
                        Console.WriteLine("4. View Order History");
                    }

                    Console.WriteLine("Enter your choice (or 'exit' to quit): ");
                    string choice = Console.ReadLine();

                    if (choice == "exit")
                    {
                        break; // Exit the loop
                    }

                    // Handle user choices based on their role
                    if (userRole == 1) // Owner
                    {
                        switch (choice)
                        {
                            case "1":
                                order.TakeOrder();
                                break;
                            case "2":
                                order.PayReservation();
                                break;
                            case "3":
                                order.RemoveUnpaidReservation();
                                break;
                            case "4":
                                report.ViewReport();
                                break;
                            case "5":
                                menu.ShowMenu();
                                break;
                            case "6":
                                menu.ModifyMenu();
                                break;
                            case "7":
                                order.ViewOrderHistory();
                                break;
                            default:
                                Console.WriteLine("\nInvalid choice. Please try again.");
                                continue;
                        }
                    }
                    else if (userRole == 2) // Employee
                    {
                        switch (choice)
                        {
                            case "1":
                                order.TakeOrder();
                                break;
                            case "2":
                                order.PayReservation();
                                break;
                            case "3":
                                menu.ShowMenu();
                                break;
                            case "4":
                                order.ViewOrderHistory();
                                break;
                            default:
                                Console.WriteLine("\nInvalid choice. Please try again.");
                                continue;
                        }
                    }
                }

                break;
        }
            Console.WriteLine("\nExiting the system. Goodbye!");
   

        }
    }
}
