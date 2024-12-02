using System;

namespace RestaurantManagementSystem
{
    public class Authentication
    
    {
        Owner owner;
        Employee employee;

        public Authentication(){

            owner = new Owner();
            employee = new Employee();
        }

        // Method to authenticate a user based on the input passcode
        public int Login()
        
        {

            Console.WriteLine("Enter passcode (123 for owner, 456 for employee): ");
            string inputPasscode = Console.ReadLine();
            // Check if the passcode matches Owner or Employee
            if (inputPasscode == owner.Passcode)
            {
                Console.WriteLine("Success! You are Owner.");
                return 1; // owner
            }
            else if (inputPasscode == employee.Passcode)
            {
                Console.WriteLine("Success! You are Employee.");
                return 2; // employee
            }
          
            // Invalid passcode
            Console.WriteLine("Invalid passcode. Authentication failed. Please try again.");
            return 3; // invalid
            
        }

        
    }
}
