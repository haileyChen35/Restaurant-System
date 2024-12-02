using System;
using System.Dynamic;
using System.Text.Json;

namespace RestaurantManagementSystem{

    public class FinancialReport{

        private static Dictionary<string, List<decimal>> financialReport;
        List<decimal> report;

        string filePath = "financialReport.json";

        public FinancialReport(){

            financialReport = LoadOrderFromFile(filePath);
            
        }

        public FinancialReport(string date, decimal cash, decimal card){

            financialReport = LoadOrderFromFile(filePath);
            
            report = new List<decimal> { cash, card };

            // Add or update the report for the given date
            financialReport[date] = report;

            // Save updated data back to the file
            SaveOrderToFile(filePath, financialReport);

        }

        public void ViewReport(){ 

            decimal totalCash = 0;
            decimal totalCard = 0;
            decimal total = 0;
            decimal dailyAverage = 0;
            Console.WriteLine("\n--- Financial Report ---\n");
            foreach (var entry in financialReport)
            {
                string timestamp = entry.Key;
                decimal cash = entry.Value[0];
                decimal card = entry.Value[1];

                totalCash += cash;
                totalCard += card;

                Console.WriteLine($"Timestamp: {timestamp}, Cash: ${cash:F2}, Card: ${card:F2}, Total: ${cash+card}");
            }
            total = totalCash + totalCard;
            dailyAverage = total / financialReport.Count;
            Console.WriteLine("\n--- Summary ---");
            Console.WriteLine($"Total Cash: ${totalCash:F2}");
            Console.WriteLine($"Total Card: ${totalCard:F2}");
            Console.WriteLine($"Grand Total: ${total:F2}");
            Console.WriteLine($"Daily Average: ${dailyAverage:F2}");
        }

        public Dictionary<string, List<decimal>> LoadOrderFromFile(string filePath){

            
            try
            {
                string json = File.ReadAllText(filePath);

                return JsonSerializer.Deserialize<Dictionary<string, List<decimal>>>(json) ?? new Dictionary<string, List<decimal>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading order file: {ex.Message}");
                return new Dictionary<string, List<decimal>>();
            }
        }

        public void SaveOrderToFile(string filePath, Dictionary<string, List<decimal>> financialReport){

            if(File.Exists(filePath)){

                string jsonData = JsonSerializer.Serialize(financialReport, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonData);

            }else{
                Console.WriteLine("Menu file not found. Loading an empty menu.");
            }
        }




    }



}
