/* SalesApp2.cs
 * This application tallies sales for four sales areas 
 * selling three different types of dessert pies, then
 * displays the total sales for each city.
 * Sales are input by the user. Any number of sales can 
 * be entered. Note we are tallying up the sales not the
 * counts.
*/

using System;
using static System.Console;

namespace SalesApp
{
    class SalesApp
    {
        static void Main(string[] args)
        {
            const int COUNT_OF_SALES_AREAS = 4;
            const int COUNT_OF_PRODUCTS = 4;

            double[,] sales = new double[COUNT_OF_SALES_AREAS, COUNT_OF_PRODUCTS];

            string[] salesArea = { "Victoria", "Nanaimo", "Kamloops", "Vancouver" };
            string[] productName = { "Lemon", "Apple", "Pumpkin", "Total Sales" };

            InitializeArray(sales);

            GetSalesFigures(sales, salesArea, productName);

            CalculateTotalSales(sales, salesArea, productName);

            DisplayResults(sales, salesArea, productName);
            ReadKey();

        }

        public static void InitializeArray(double[,] sales)
        {
            /*  Set all sales values for each sales area and product to zero.
             */

            for (int row = 0; row < sales.GetLength(0); row++)
            {
                for (int col = 0; col < sales.GetLength(1); col++)
                {
                    sales[row, col] = 0;
                }
            }
        }

        public static void GetSalesFigures(double[,] sales, string[] salesmenName, string[] productName)
        {

            int salesNo,
                productNo;
            string inputValue;
            bool moreData = true;
            while (moreData)
            {
                salesNo = GetSalesNumber(salesmenName);
                productNo = GetProductNumber(productName);
                sales[salesNo, productNo] += GetSalesFigures();

                Write("Are there more sales to record (Yes/No)? ");
                inputValue = ReadLine();
                if (inputValue == "No" || inputValue == "no" || inputValue == "n" || inputValue == "N")
                {
                    moreData = false;
                }
            }
        }

        public static int GetSalesNumber(string[] salesAreaName)
        {
            int salesNo = -1;
            bool validNumber = false;
            while (salesNo > -2 && salesNo < 0)
            {
                Clear();
                WriteLine("Sales Registry\n\n");

                for (int i = 0; i < salesAreaName.Length; i++)
                {
                    WriteLine(" {0}. {1}", (i + 1), salesAreaName[i]);
                }

                Write("\nSales are for which sales area? (1-4):  ");

                while (!validNumber)
                {
                    if (!int.TryParse(ReadLine(), out salesNo) || salesNo < 1 || salesNo > 4)
                    {
                        Write("Sorry, that number is not valid. Please try again: ");
                    }
                    else
                    {
                        validNumber = true;
                    }
                }
            }
            return salesNo - 1;
        }

        public static int GetProductNumber(string[] productName)
        {
            int productNo = -1;
            bool validNumber = false;
            while (productNo > -2 && productNo < 0)
            {
                Clear();
                WriteLine("Products\n\n");

                for (int i = 0; i < (productName.Length - 1); i++)
                {
                    WriteLine(" {0}. {1}", (i + 1), productName[i]);
                }

                Write("\nSales are for which product?  ");

                while (!validNumber)
                {
                    if (!int.TryParse(ReadLine(), out productNo) || productNo > 3 || productNo < 1)
                    {
                        Write("Sorry, that number is not valid. Please try again: ");
                    }
                    else
                    {
                        validNumber = true;
                    }
                }
            }
            return productNo - 1;
        }

        public static double GetSalesFigures()
        {
            double salesAmt = 0;
            bool validValue = false;
            Write("What was the sales amount? ");

            while (!validValue)
            {
                if (!double.TryParse(ReadLine(), out salesAmt))
                {
                    Write("Sorry, that is not a valid number. Please try again: ");
                }
                else
                {
                    validValue = true;
                }
            }
            return salesAmt;
        }

        public static void DisplayResults(double[,] sales, string[] salesAreaName, string[] productName)
        {
            Clear();
            WriteLine("\t\tSales Summary\n\n");
            Write("{0, -15}", "Sales Area");
            for (int i = 0; i < productName.Length; i++)
            {
                Write("{0, 15}", productName[i]);
            }
            WriteLine();

            for (int row = 0; row < salesAreaName.Length; row++)
            {
                Write("\n{0}  \t", salesAreaName[row]);
                for (int col = 0; col < sales.GetLength(1); col++)
                {
                    Write("{0,15:F2}", sales[row, col]);
                }
            }
        }

        private static void CalculateTotalSales(double[,] sales, string[] salesArea, string[] productName)
        {
            /* Calculates the sum of all sales for each sales region,
             * then places these sales into their own column dedicated
             * to sales totals.
             */

            double sum = 0;

            for (int row = 0; row < sales.GetLength(0); row++)
            {
                for (int col = 0; col < (sales.GetLength(1) - 1); col++)
                {
                    sum += sales[row, col];
                }
                sales[row, 3] = sum;
                sum = 0;
            }
        }
    }
}