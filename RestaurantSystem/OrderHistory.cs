using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Enumeration;
using System.Text.Json;


namespace RestaurantManagementSystem
{

    
    public class OrderHistory
    {

        private static Dictionary<string, OrderDetails> orderLists = new Dictionary<string, OrderDetails>();
        string filePath = "orderHistory.json";
        private Payment payment;  // has a payment (composition)

        private Menu menu; // has a menu (composition)

        public OrderHistory(){

            menu = new Menu();
            payment = new Payment();
            orderLists = LoadOrderFromFile(filePath);

        }

        public void TakeOrder(){
            try{
                Console.WriteLine("\n--- Taking Order ---");
                List<Order> orderItems = new List<Order>();
                bool ordering = true;
                int statusChoice;
                bool pay = false;
                OrderStatus status;
                decimal bill = 0;
                menu.ShowMenu();
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                while (ordering){

                    Console.WriteLine("\nEnter the number of the item you want to add to the order, or 0 to finish:");
                    int selection = int.Parse(Console.ReadLine());

                    if (selection == 0)
                    {
                        ordering = false;
                        break;
                    }

                    if (selection > 0 && selection <= Menu.MenuItems().Count)
                    {
                        MenuItem selectedItem = Menu.MenuItems()[selection - 1];
                        Console.WriteLine($"\nEnter the quantity of {selectedItem.Name}:");
                        int quantity = int.Parse(Console.ReadLine());
                        Order order = new Order(selectedItem.Name, selectedItem.Price, quantity);
                        orderItems.Add(order);
                        bill += selectedItem.Price * quantity;
                        Console.WriteLine($"Added {selectedItem.Name} - ${selectedItem.Price * quantity} to your order.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection. Please try again.");
                    }
                    

                }

                // Ask for the order type
                Console.WriteLine("\nSelect Order Type:");
                Console.WriteLine("1. To Go");
                Console.WriteLine("2. Dine In");
                Console.WriteLine("3. Reservation\n");

                if (!int.TryParse(Console.ReadLine(), out statusChoice) || statusChoice < 1 || statusChoice > 3)
                {
                    Console.WriteLine("Invalid selection, defaulting to To Go.");
                    statusChoice = 1;
                }

                status = (OrderStatus)(statusChoice - 1);
                if (status != OrderStatus.Reserved)
                {
                    Console.WriteLine($"\n[Process payment] \n Bill: ${bill}");
                    pay = payment.ProcessPayment(currentDate, bill);
                    
                }
                
                OrderDetails orderDetails = new OrderDetails(orderItems, status, bill, pay);
                orderLists[currentDate] = (orderDetails);
                SaveOrderToFile(filePath, orderLists);
            
                Console.WriteLine("\nOrder completed!");
                
                ViewCurrentOrder(currentDate, orderItems, status, bill, pay);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred, order & payment are cancelled");
            }

        }

        public void PayReservation(){
            Console.WriteLine("\n=== Pay for Reservation ===");
            // Find all reserved orders
            var reservedOrders = orderLists
            .Where(order => (OrderStatus)order.Value.Status == (OrderStatus.Reserved)&&(order.Value.Pay == false))
            .ToList();    

            if (reservedOrders.Count == 0)
            {
                Console.WriteLine("No reservations found.");
                return;
            }

            // Display reserved orders
            Console.WriteLine("\nReservations:");
            int index = 1;
            foreach (var order in reservedOrders)
            {
                Console.WriteLine($"[{index}] Date: {order.Key}, Bill: ${order.Value.Bill:F2}, Payment Status: {(order.Value.Pay ? "Paid" : "Unpaid")}");
                index++;
            }

            // Ask the user to select a reservation
            Console.Write("\nSelect a reservation to pay (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > reservedOrders.Count)
            {
                Console.WriteLine("Invalid selection. Returning to the main menu.");
                return;
            }

            var selectedReservation = reservedOrders[choice - 1];
            var orderDate = selectedReservation.Key;
            var orderDetails = selectedReservation.Value;

            // Display the selected reservation details
            Console.WriteLine($"\nSelected Reservation - Date: {orderDate}, Bill: ${orderDetails.Bill:F2}, Status: {(orderDetails.Pay ? "Paid" : "Unpaid")}");

            //process payment
            bool pay = payment.ProcessPayment(orderDate, orderDetails.Bill);

            //save change to orderDetails object
            //since both reference to same object, saves change to the orderLists dictionary too
            orderDetails.Pay = pay;

            //save to file
            SaveOrderToFile(filePath, orderLists);

            Console.WriteLine("Reservation Payment processed successfully.");
            
        }

        public void RemoveUnpaidReservation(){
            Console.WriteLine("\n=== Remove Unpaid Reservation ===");
            // Find all unpaid reserved orders
            var UnpaidReservedOrders = orderLists
            .Where(order => ((OrderStatus)order.Value.Status == OrderStatus.Reserved)&&(order.Value.Pay == false))
            .ToList();    

            if (UnpaidReservedOrders.Count == 0)
            {
                Console.WriteLine("No reservations found.");
                return;
            }

            // Display reserved orders
            Console.WriteLine("\nReservations:");
            int index = 1;
            foreach (var order in UnpaidReservedOrders)
            {
                Console.WriteLine($"[{index}] Date: {order.Key}, Bill: ${order.Value.Bill:F2}, Payment Status: {(order.Value.Pay ? "Paid" : "Unpaid")}");
                index++;
            }

            // Ask the user to select index to remove order
            Console.Write("\nSelect an unpaid reservation to remove (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > UnpaidReservedOrders.Count)
            {
                Console.WriteLine("Invalid selection. Returning to the main menu.");
                return;
            }

            var selectedReservation = UnpaidReservedOrders[choice - 1];
            var orderDate = selectedReservation.Key;
            orderLists.Remove(orderDate); // remove unpaid orders by their keys

            //save to file
            SaveOrderToFile(filePath, orderLists);

            // Display remaining orders
            Console.WriteLine("Remaining Orders:");
            foreach (var order in orderLists)
            {
                Console.WriteLine($"Order ID: {order.Key}, Bill: {order.Value.Bill}, Paid: {order.Value.Pay}");
            }

            Console.WriteLine("Remove Unpaid Reservation processed successfully.");



        }

        public void ViewOrderHistory()
        {
            Console.WriteLine("\n=== Current Order History ===");

            if (orderLists.Count == 0)
            {
                Console.WriteLine("No orders found.");
                return;
            }

            int orderIndex = 1; // Index for orders
            foreach (var entry in orderLists)
            {
                // Extract details
                string orderDate = entry.Key;
                OrderStatus status = (OrderStatus)entry.Value.Status;
                decimal bill = entry.Value.Bill;
                bool isPaid = entry.Value.Pay;
                string paymentStatus = isPaid ? "Paid" : "Unpaid";

                // Order Summary Header
                Console.WriteLine($"\n[Order #{orderIndex}]");
                Console.WriteLine($"Order Date   : {orderDate}");
                Console.WriteLine($"Order Type   : {status}");
                Console.WriteLine($"Payment Status: {paymentStatus}");
                Console.WriteLine($"Total Bill   : ${bill:F2}");
                Console.WriteLine(new string('-', 40)); // Separator for readability

                // Item Details
                Console.WriteLine("Item Details:");
                int itemIndex = 1; // Index for items
                foreach (var order in entry.Value.Orders)
                {
                    Console.WriteLine($"    {itemIndex}. {order.Name}");
                    Console.WriteLine($"       Price   : ${order.Price:F2}");
                    Console.WriteLine($"       Quantity: {order.Quantity}");
                    itemIndex++;
                }

                Console.WriteLine(new string('=', 40)); // End of order section
                orderIndex++;
                }
        }

        public void ViewCurrentOrder(string currentDate, List<Order> orderItems, OrderStatus status, decimal bill, bool pay){
            
            if (currentDate == null || orderItems == null)
            {
                Console.WriteLine("No order details available.");
                return;
            }

            string paymentStatus = pay ? "Paid" : "Unpaid";

            // Order Header
            Console.WriteLine("\n=== Current Order Details ===");
            Console.WriteLine($"Order Date     : {currentDate}");
            Console.WriteLine($"Order Type     : {status}");
            Console.WriteLine($"Payment Status : {paymentStatus}");
            Console.WriteLine($"Total Bill     : ${bill:F2}");
            Console.WriteLine(new string('-', 40)); // Separator for readability

            // Item Details
            if (orderItems.Count > 0)
            {
                Console.WriteLine("Items Ordered:");
                int itemIndex = 1;
                foreach (var order in orderItems)
                {
                    Console.WriteLine($"    {itemIndex}. {order.Name}");
                    Console.WriteLine($"       Price   : ${order.Price:F2}");
                    Console.WriteLine($"       Quantity: {order.Quantity}");
                    itemIndex++;
                }
            }
            else
            {
                Console.WriteLine("No items in the order.");
            }

            Console.WriteLine(new string('=', 40)); // End of order section
        }


        public Dictionary<string, OrderDetails> LoadOrderFromFile(string filePath){

            
            try
            {
                string json = File.ReadAllText(filePath);

                return JsonSerializer.Deserialize<Dictionary<string, OrderDetails>>(json) ?? new Dictionary<string, OrderDetails>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading order file: {ex.Message}");
                return new Dictionary<string, OrderDetails>();
            }
        }

        public void SaveOrderToFile(string filePath, Dictionary<string, OrderDetails> orderLists){

            if(File.Exists(filePath)){

                string jsonData = JsonSerializer.Serialize(orderLists, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonData);

            }else{
                Console.WriteLine("Menu file not found. Loading an empty menu.");
            }
        }

    }

    
}
