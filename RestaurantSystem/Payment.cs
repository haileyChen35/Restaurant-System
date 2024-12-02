using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.XPath;

namespace RestaurantManagementSystem
{
    public class Payment
    {
        bool paying = true;
        int option;
        decimal card = 0;
        decimal cash = 0;
        decimal result = 0;
        decimal payment = 0;
        
        public bool ProcessPayment(string date, decimal bill){

            while(paying){
            Console.WriteLine("Select Payment Option:");
            Console.WriteLine("1. Cash");
            Console.WriteLine("2. Card");

            if (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 2)
            {
                Console.WriteLine("Invalid selection, please try again.");
                continue; // Restart the loop
            }
                
            if(option == 1){ // pay by cash
                Console.WriteLine("Enter the amount of cash: ");
                payment = decimal.Parse(Console.ReadLine());
                result = Compare(bill, payment);
                if(result == 0){ 
                    cash += payment;
                    Console.WriteLine("Order is paid successfully!");
                    break;
                }else if(result == -1){
                    cash += payment;
                    Console.WriteLine($"Remaining Bill: ${bill-payment}");
                    bill -= payment;
                }else if(result == 1){
                    cash += bill;
                    Console.WriteLine($"Change: ${payment-bill}");
                    Console.WriteLine("Order is paid successfully!");
                    break;
                }

            }else if(option == 2){ // pay by card
                Console.WriteLine("Enter the amount of card: ");
                payment = decimal.Parse(Console.ReadLine());
                result = Compare(bill, payment);
                if(result == 0){ 
                    card += payment;
                    Console.WriteLine("Order is paid successfully!");
                    break;
                }else if(result == -1){
                    card += payment;
                    Console.WriteLine($"Remaining Bill: ${bill-payment}");
                    bill -= payment;
                }else if(result == 1){
                    card += bill;
                    Console.WriteLine($"Change: ${payment-bill}");
                    Console.WriteLine("Order is paid successfully!");
                    break;
                }
            }


            }

            FinancialReport report = new FinancialReport(date, cash, card);
            return true;
        }


        public int Compare(decimal bill, decimal money){
            if(bill == money){
                return 0;  // payment completed
            }else if(bill > money){
                return -1; // split the bill 
            }
            return 1;  // give the change
        }

        

     


    }
}

