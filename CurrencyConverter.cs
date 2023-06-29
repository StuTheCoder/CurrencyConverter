using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


public class CurrencyConverter
{
    private const string ApiKey = "78839b268de046ef895c8f360c0ee6ba";

    public void Run()
    {
        Console.WriteLine("Welcome to Currency Converter");

        while (true)
        {
            Console.WriteLine();
            Console.Write("Enter the amount to convert (or 'exit' to Exit)");
            string amountInput = Console.ReadLine();

            if (amountInput.ToLower() == "exit")
            {
                Console.WriteLine("Thank you for using Currency Converter");
                break;
            }

            if (double.TryParse(amountInput, out double amount))
            {
                Console.Write("Enter the base currency code: ");
                string baseCurrency = Console.ReadLine().ToUpper();

                Console.Write("Enter the target currency code: ");
                string targetCurrency = Console.ReadLine().ToUpper();

                double convertedAmount = ConvertCurrency(amount, baseCurrency, targetCurrency);

                if (convertedAmount != -1)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{amount} {baseCurrency} = {convertedAmount} {targetCurrency}");
                }
                else
                {
                     Console.WriteLine("Failed to retrieve exchange rates.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount");
            }
        }
    }

    private double ConvertCurrency(double amount, string baseCurrency, string targetCurrency)
    {
        try
        {
            string url = $"https://openexchangerates.org/api/latest.json?app_id={ApiKey}";

            using (WebClient webClient = new WebClient())
            {
                string json = webClient.DownloadString(url);
                ExchangeRates exchangeRates = JsonConvert.DeserializeObject<ExchangeRates>(json);

                double baseRate = GetRateForCurrency(exchangeRates, baseCurrency);
                double targetRate = GetRateForCurrency(exchangeRates, targetCurrency);

                if (baseRate != -1 && targetRate != -1)
                {
                    double convertedAmount = (amount / baseRate) * targetRate;
                    return convertedAmount;
                }
                else
                {
                    return -1;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return -1;
        }
    }

    private double GetRateForCurrency(ExchangeRates exchangeRates, string currency)
    {
        switch (currency)
        {
            case "USD":
                return exchangeRates.Rates.USD;
            case "EUR":
                return exchangeRates.Rates.EUR;
            default:
                Console.WriteLine($"Currency code '{currency}' not supported.");
                return -1;
        }
    }
}