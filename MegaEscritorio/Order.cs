using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;



namespace MegaEscritorio
{
    internal class Order
    {
        private Desk myDesk;
        private bool rushOrder;
        private int rushDays;
        private double[,] priceRushOrderTable = new double[3, 3];

        public Order(Desk myDesk, bool rushOrder, int rushDays)
        {
            this.myDesk = myDesk;
            setRushOrder(rushOrder);
            setRushDays(rushDays);
            readFile();
        }
        // Getters
        public int getRushDays()
        {
            return rushDays;
        }

        public bool isRushOrder()
        {
            return rushOrder;
        }

        public double getTotalPrice()
        {
            double totalPrice = calculateTotal();
            return totalPrice;
        }
        // Setters
        public void setRushOrder(bool rushDelivery)
        {
            this.rushOrder = rushDelivery;
        }

        public void setRushDays(int days)
        {
            rushDays = days;
        }

       // This function reads the price table for delivery options from a file
        private void readFile()
        {
            int x = 0;
            int y = 0;
             
            StreamReader reader = new StreamReader("deliveryPrices.txt");

            // Store the information in the text file into a two dimensional array
            while (reader.EndOfStream == false)
            {
                string value = reader.ReadLine();
                priceRushOrderTable[x, y] = Convert.ToDouble(value);
                                             //           Desk size
                x++;                         // day# | <1000 | 1000 to 1999 | >2000
                if (x % 3 == 0)              //  3   |  $60  |      $70     |  $80
                {                            //  5   |  $40  |      $50     |  $60
                    x = 0;                   //  7   |  #30  |      $30     |  $40
                    y++;
                }
            }
            reader.Close();
        }

        // This method will write the order information to JSON file
        public void writeFile()
        {
            // Create a Dictionary to store the order information
            Dictionary<string, string> orderDetails = new Dictionary<string, string>();
            orderDetails.Add("width", Convert.ToString(myDesk.getWidth()));
            orderDetails.Add("length", Convert.ToString(myDesk.getLength()));
            orderDetails.Add("material", myDesk.getMaterial());
            orderDetails.Add("drawers", Convert.ToString(myDesk.getNumDrawers()));
            orderDetails.Add("rushDays", Convert.ToString(getRushDays()));
            orderDetails.Add("totalPrice", Convert.ToString(getTotalPrice()));

            // Using the third party library JSON.Net serialize the Dictionary into a string with json formatting
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(orderDetails);
            
            // Append the text file to the json.txt file
            File.AppendAllText("json.txt", "\n");
            File.AppendAllText("json.txt", json);
        }

        // This method will display the delivery price table to the Console
        public void displayDeliveryPriceTable()
        {
            int j;
            for (int i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    Console.Out.Write(priceRushOrderTable[j,i] + " ");
                }

                if (j % 3 == 0)
                {
                    Console.Out.WriteLine();
                }
            }
        }

        // This method calculates the total price of the order
        public double calculateTotal()
        {
            double orderTotal = 200.00;  // Add desks have a base price of 200
            double area = myDesk.getSurfaceArea();

            // If surface area is greater than 1000 square inches charge 5 dollars per extra square inch
            if (area > 1000.00)
            {
                orderTotal += (area - 1000) * 5;
            }

            // Calculate cost per number of drawers.
            // Drawers are 50 dollars each
            if (myDesk.getNumDrawers() >= 1)
            {
                orderTotal += myDesk.getNumDrawers() * 50.00;
            }

            // Calculate desk material cost
            switch (myDesk.getMaterial())
            {
                case "oak":
                    orderTotal += 200.00;
                    break;
                case "laminate":
                    orderTotal += 100.00;
                    break;
                case "pine":
                    orderTotal += 50.00;
                    break;
            }

            // Calculate the order charge using the price rush order table 
            double orderCharge = 0;

            if (isRushOrder() == true)
            {
                if (area < 1000.00)
                {
                    switch (getRushDays())
                    {
                        case 3:
                            orderCharge = priceRushOrderTable[0, 0];
                            break;
                        case 5:
                            orderCharge = priceRushOrderTable[0, 1];
                            break;
                        case 7:
                            orderCharge = priceRushOrderTable[0, 2];
                            break;
                    }
                }
                else if (area >= 1000.00 && area <= 1999.00)
                {
                    switch (getRushDays())
                    {
                        case 3:
                            orderCharge = priceRushOrderTable[1, 0];
                            break;
                        case 5:
                            orderCharge = priceRushOrderTable[1, 1];
                            break;
                        case 7:
                            orderCharge = priceRushOrderTable[1, 2];
                            break;
                    }
                }
                else if (area >= 2000.00)
                {
                    switch (getRushDays())
                    {
                        case 3:
                            orderCharge = priceRushOrderTable[2, 0];
                            break;
                        case 5:
                            orderCharge = priceRushOrderTable[2, 1];
                            break;
                        case 7:
                            orderCharge = priceRushOrderTable[2, 2];
                            break;
                    }
                }

            }
            orderTotal += orderCharge;

            return orderTotal ;
        }

        // This method displays the order specifications
        public void displayOrderSpec()
        {
            Console.Out.WriteLine("\nOrder Specifications\n--------------------");
            Console.Out.WriteLine("Rush Order Days: " + getRushDays());
        }

    }
}