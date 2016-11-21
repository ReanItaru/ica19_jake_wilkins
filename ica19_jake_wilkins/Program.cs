using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GDIDrawer;
using Utility_Library;

namespace ica19_jake_wilkins
{
    class Program
    {
        static void Main(string[] args)
        {
            //variables
            string choice = "";
            CDrawer sky = new CDrawer(1,1);
            int height = 0;
            int width = 0;
            bool[,] stars = new bool[0, 0];
            int numberOfStars = 0;

            //title
            Console.WriteLine("\t\tStarry Night\n");
            sky.Close();

            //inputs
            do
            {
                Console.Write("Please choose one of the following:\nM - Make a New Sky\nA - Add Stars\nR - Remove All Stars\nD - Draw Sky\nX - Exit\nYour Choice: ");
                choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case ("M"):
                        sky = MakeSky();
                        height = sky.m_ciHeight;
                        width = sky.m_ciWidth;                        
                        break;
                    case ("A"):
                        if (width * height == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("\t\tYou haven't drawn a sky yet, do that first please\n");
                        }
                        else
                        {
                            stars = MakeStars(width, height);
                            numberOfStars = AddStars(stars, (width * height));                            
                        }
                        break;
                    case ("R"):
                        RemoveStars(stars);
                        break;
                    case ("D"):
                        if (width * height == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("\t\tYou haven't drawn a sky yet, do that first please\n");
                        }
                        else
                        {
                            DrawSky(sky, stars);                            
                        }                                                    
                        break;
                    case ("X"):
                        continue;
                    default:
                        Console.Clear();
                        Console.WriteLine("You have entered an invalid choice, try again\n");
                        break;
                }
            } while (choice != "X");
        }
        static public CDrawer MakeSky()
        {
            int width = 0;
            int height = 0;

            width = Utilities.GetInt("Enter width of sky: ", 200, 800);
            height = Utilities.GetInt("Enter height of sky: ", 100, 600);
            CDrawer gdi = new CDrawer(width, height, false);
            Console.Clear();
            Console.WriteLine("\tSky Successfully made\n");
            return gdi;
        }
        static public bool[,] MakeStars(int width, int height)
        {
            bool[,] stars = new bool[height, width];
            return stars;
        }
        static public void DrawSky(CDrawer gdi, bool[,] stars)
        {
            int row = 0;
            int column = 0;

            for (row = 0; row < stars.GetLength(0); row++)
                for (column = 0; column < stars.GetLength(1); column++)
                    if (stars[row, column])
                        gdi.SetBBPixel(column, row, RandColor.GetColor());
                    else
                        gdi.SetBBPixel(column, row, Color.Black);
            gdi.Render();
            Console.Clear();
            Console.WriteLine("\t\t\tSky updated\n");
        }
        static public int AddStars(bool[,] stars, int maxStars)
        {
            Random rng = new Random();
            int inputStars;
            int row = 0;
            int column = 0;
            int drawnStars = 0;            

            maxStars = (maxStars < 10000) ? maxStars : 10000;

            inputStars = Utilities.GetInt("Enter number of stars you'd like to see: ", 1, maxStars);

            while (drawnStars < inputStars)
            {
                row = rng.Next(stars.GetLength(0));
                column = rng.Next(stars.GetLength(1));

                if (stars[row, column] == false)
                {
                    stars[row, column] = true;
                    drawnStars++;
                }
            }
            Console.Clear();
            Console.WriteLine("\tStars have been created\n");
            return drawnStars;
        }
        static public void RemoveStars(bool[,] stars)
        {
            int row = 0;
            int column = 0;

            for (row = 0; row < stars.GetLength(0); row++)
                for (column = 0; column < stars.GetLength(1); column++)
                    if (stars[row, column])
                        stars[row, column] = false;

            Console.Clear();
            Console.WriteLine("\t\tStars removed\n");
        }
    }
}