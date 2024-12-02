# itsc3112_project

# Restaurant Management System - Console Application

## Overview

The Restaurant Management System is a console-based application designed to streamline operations in a restaurant. The system provides functionality for both owners and employees, enabling role-based access to key features such as order management, menu management, payment management, financial reporting, and order history tracking. Integration with JSON files as a database for persistent storage of order history, menu, and financial reports.

## Features

### Owner Features:
1. **Take Orders or Save Reservations**  
   Add new customer orders or save reservations for future processing.
2. **Payment Processing**
   Choose a payment method (cash or card), and the system will calculate change or handle split bills for partial payments.
3. **Pay Reserved Orders**  
   Pay and mark reserved orders as paid during customer checkout.
4. **Remove Reserved Orders**  
   Delete unpaid reservations that are no longer valid.
5. **View Financial Report**  
   Access a financial report for the restaurant, detailing total cash payments, total card payments, and the combined total for each transaction. The report also provides a cumulative summary, including the grand total and daily average revenue.
6. **View Menu**  
   Display the current menu items.
7. **Modify Menu**  
   Add, update, or remove menu items.
8. **View Order History**  
   Review the history of all processed orders.

### Employee Features:
1. **Take Orders or Save Reservations**  
   Similar to the owner, employees can manage customer orders or reservations.
2. **Pay Reserved Orders**  
   Process payments for reservations.
3. **View Menu**  
   Check the current menu offerings.
4. **View Order History**  
   Review processed orders for reference.

## Role-Based Authentication
The system provides secure role-based access control. Upon login, users are assigned roles:  
- **Owner**: Full access to all features.  
- **Employee**: Limited access to operational features.  

Invalid login attempts will prompt re-authentication.

## How to Use

The application features a console-based user interface where all interactions occur directly in the console.
1. **Login**  
   The system prompts users to log in. Based on their authentication, they are granted access to relevant features.  
   
2. **Select Role-Specific Options**  
   - Owners and employees see different menus based on their roles.
   - Enter the index corresponding to the desired feature or type "exit" to terminate the program.  

3. **Exit the System**  
   Type "exit" to terminate the program.

## Prerequisites

- **Development Environment:** .NET Framework or .NET Core.  
- **Language:** C#.  
- **IDE:** Visual Studio (recommended).  

## Getting Started

1. Clone the repository to your local system:
   ```
   git clone https://github.com/haileyChen35/Restaurant-System.git
   ```
2. Open the project in your preferred IDE (e.g., Visual Studio).
3. Ensure the terminal is navigated to the RestaurantSystem directory:
   ```
   cd RestaurantSystem
   ```
4. Run the program by entering **dotnet run** in the terminal.

## Code Overview

The application is modular, consisting of the following components:
- **Authentication:** Handles login and user role determination.
- **OrderHistory:** Manages order creation, payment, removal, and history tracking.
- **FinancialReport:** Displays the restaurant’s financial data.
- **Menu:** Facilitates viewing and modifying menu items.

## Future Enhancements

Potential features to expand the system:
- Enhanced security mechanisms.
- Mobile or web-based interface for greater accessibility.

---

Enjoy using the **Restaurant Management System Console App** to improve your restaurant's efficiency and customer satisfaction!
