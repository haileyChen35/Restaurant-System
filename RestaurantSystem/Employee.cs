using System;

namespace RestaurantManagementSystem{

    public class Employee : User 
    {

        public Employee() : base("456"){
             // Employee has a predefined passcode "456"
        }

        // // Override the Authenticate method 
        // public override bool Authenticate(string inputPasscode)
        // {
        //     return Passcode == inputPasscode;
        // }

    }

}