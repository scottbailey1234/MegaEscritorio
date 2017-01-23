using System;

namespace MegaEscritorio
{
    internal class Desk
    {
        private double width;
        private double length;
        private int numDrawers;
        private string material;

        //Default Constructor
        public Desk()
        {
            setWidth(0);
            setLength(0);
            setNumDrawers(0);
            setMaterial("Null");
        }

        //Constructor that takes 4 arguments
        public Desk(double width, double length, int numDrawers, string material)
        {
            setWidth(width);
            setLength(length);
            setMaterial(material);
            setNumDrawers(numDrawers);
        }

        // Getters
        public double getWidth()
        {
            return width;
        }

        public double getLength()
        {
            return length;
        }

        public double getSurfaceArea()
        {
            return getLength() * getWidth();
        }

        public int getNumDrawers()
        {
            return numDrawers;
        }

        public string getMaterial()
        {
            return material;
        }

        // Setters
        public void setWidth(double width)
        {
            this.width = width;
        }

        public void setLength(double length)
        {
            this.length = length;
        }

        public void setNumDrawers(int numDrawers)
        {
            this.numDrawers = numDrawers;
        }

        public void setMaterial(string material)
        {
            this.material = material;
        }

        // Displays the table specifications to the Console
        public void displayTableSpec()
        {
            Console.Out.WriteLine("\nTable Specifications\n--------------------");
            Console.Out.WriteLine("Width: " + getWidth() + " in\nLength: " + getLength()  + " in\nSurface Area: " + getSurfaceArea() + " sq/in\nNumber of Drawers: " + getNumDrawers() + "\nMaterial: " + getMaterial());
        }
    }
}