using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace MegaEscritorio
{
    class Program
    {
        static void Main(string[] args)
        {
            // Greet the user
            Console.Out.WriteLine("Welcome to Mega Escritorio!\nPlease specify your desired desk below.\n");

            Desk myDesk = new Desk(); // Create a new Desk object
            // Prompt user for desk specifications
            promptSize(myDesk);
            promptNumDrawers(myDesk);
            promptMaterials(myDesk);

            Order newOrder = new Order(myDesk, false, 14); // Create a new order object
            // Prompt user for order specifications
            promptRushOrder(newOrder);

            //Display final order
            myDesk.displayTableSpec(); 
            newOrder.displayOrderSpec();
            double orderTotal = newOrder.getTotalPrice();
            Console.Out.WriteLine("\nTotal Cost: ${0:0.00}", orderTotal);

            // Write order to a file called json.txt
            newOrder.writeFile();

            // Close the program
            Console.Out.WriteLine("\nPress any key to exit");
            Console.ReadKey();
        }

        // Get size specifications
        static void promptSize(Desk myDesk)
        {
            try
            {
                // Prompt user for the width and length of their desired desk
                Console.Out.Write("Width of the desk (inches): ");
                string width = Console.ReadLine();
                Console.Out.Write("Length of the desk (inches): ");
                string length = Console.ReadLine();

                // store data in myDesk
                myDesk.setWidth(Convert.ToDouble(width));
                myDesk.setLength(Convert.ToDouble(length));
            }
            catch (FormatException) // If the user input above is not a double catch this exception
            {
                Console.Out.WriteLine("ERROR: Your input is invalid. Please enter the width and length of your desired desk in inches. (ex: 50)\n");
                promptSize(myDesk); // Prompt the user again
            }
        }

        // Get drawer specification
        static void promptNumDrawers(Desk myDesk)
        {
            try
            {
                // Prompt user for the number of drawers they want built into their desk
                Console.Out.Write("Number of drawers (0-7): ");
                string numDrawers = Console.ReadLine();

                if ((Convert.ToInt16(numDrawers) > 7) || (Convert.ToInt16(numDrawers) < 0)) // If the input is outside the range of 0-7, prompt again
                {
                    Console.Out.WriteLine("ERROR: You may only select up to 7 drawers.\n");
                    promptNumDrawers(myDesk);
                }
                else
                {
                    myDesk.setNumDrawers(Convert.ToInt16(numDrawers));
                }
            }
            catch (FormatException)
            {
                Console.Out.WriteLine("Error: Please enter the number of drawers your would like built. (ex: 3)\n");
                promptNumDrawers(myDesk);
            }
     
        }

        // Get material specification
        static void promptMaterials(Desk myDesk)
        {
            //Prompt user for material type
            Console.Out.Write("Type of Material (Oak, Laminate, Pine): ");
            string material = Console.ReadLine();
            material = material.ToLower();
            if (material.Equals("oak") || material.Equals("laminate") || material.Equals("pine"))
            {
                myDesk.setMaterial(material);
                
            }
            else
            {
                Console.Out.WriteLine("ERROR: Please type either oak, laminate, or pine as the surface material for your desk.\n");
                promptMaterials(myDesk);
            }
        }

        // get rush delivery specifications
        static void promptRushOrder(Order newOrder)
        {
            // Prompt user for rush delivery
            Console.Out.Write("\nNormal production time is 14 days, would you like a rush order at an extra charge? (y/n) ");
            string answer = Console.ReadLine();
            if (answer.ToLower() == "y" || answer.ToLower() == "yes")
            {
                try
                {
                    newOrder.setRushOrder(true);
                    Console.Out.Write("How quickly would you like your order produced? Select one of the 3 options 3, 5, or 7 days: ");
                    string days = Console.ReadLine();
                    if (Convert.ToInt16(days) == 3 || Convert.ToInt16(days) == 5 || Convert.ToInt16(days) == 7)
                    {
                        newOrder.setRushDays(Convert.ToInt16(days));
                    }
                    else
                    {
                        Console.Out.WriteLine("ERROR: We only have rush order options of 3, 5 or 7 days.");
                        promptRushOrder(newOrder);
                    }
                }
                catch (FormatException)
                {
                    Console.Out.WriteLine("ERROR: Please select a rush order option of 3, 5, or 7 days");
                    promptRushOrder(newOrder);
                }
            }
            else if (answer.ToLower() == "n" || answer.ToLower() == "no")
            {
                newOrder.setRushOrder(false);
                newOrder.setRushDays(0);
            }
            else
            {
                Console.Out.WriteLine("ERROR: Please select if you would like to make it a rush order. (ex: yes, no)");
                promptRushOrder(newOrder);
            }
        }
    }
}
