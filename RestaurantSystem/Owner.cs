using System;

namespace RestaurantManagementSystem{

    public class Owner : User 
    {

        public Owner() : base("123"){  // Owner has a predefined passcode "123"
           
        }

        // // Override the Authenticate method 
        // public override bool Authenticate(string inputPasscode)
        // {
        //     return Passcode == inputPasscode;
        // }

    }

}