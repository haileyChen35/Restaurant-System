using System;

namespace RestaurantManagementSystem{

    public class User
    {
        public string Passcode { get; set; }

        public User(string passcode)
        {
            Passcode = passcode;
        }

        // public virtual bool Authenticate(string inputPasscode) //method can be overridden in a derived class
        // {
        //     return Passcode == inputPasscode;
        // }

    }

}