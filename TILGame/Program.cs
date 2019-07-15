using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading;

namespace TILGame
{
    class Game {

        private ConsoleColor[,] colors;
        private const int row = 20, col = 15;

        private Point currentEl;
        private Point prevEl;

        private int countElementsX;
        private int countElementsY;

        private List<Point> same;

        public Game() {

            Console.CursorVisible = false;
            
            Console.WindowHeight = 26;
            Console.BufferHeight = 26;
            Console.WindowWidth = 45;
            Console.BufferWidth = 45;

            colors = new ConsoleColor[row, col];

            currentEl.X = 0;
            currentEl.Y = 0;
            

            same = new List<Point>();
            
            setColors();
            printField();

            printCurrent();
            
            checkElements();
            deleteElements();
            
            while (checkElements()) {
                deleteElements();
                addingElements();
                printField();
            }
            //printField();
            printCurrent();
        }

        private void printField() {
            for (int i = 0; i < row; ++i) {
                for (int j = 0; j < col; ++j) {
                    printElement(j, i);
                }
            }
        }
        
        private void printElement(int x, int y) {
            Console.SetCursorPosition(3 + x * 2, y + 1);
            Console.ForegroundColor = colors[y, x];
            Console.Write('\u25A0');
        }

        private void setColors() {
            int x;
            Random r = new Random(); 
            for (int i = 0; i < row; ++i) {
                for (int j = 0; j < col; ++j) {
                    x = r.Next(7);
                    if (x == 0) {
                        colors[i, j] = ConsoleColor.Blue;
                    }
                    if (x == 1) {
                        colors[i, j] = ConsoleColor.Green;
                    }
                    if (x == 2) {
                        colors[i, j] = ConsoleColor.DarkMagenta;
                    }
                    if (x == 3) {
                        colors[i, j] = ConsoleColor.Red;
                    }
                    if (x == 4) {
                        colors[i, j] = ConsoleColor.Yellow;
                    }
                    if (x == 5) {
                        colors[i, j] = ConsoleColor.White;
                    }
                    if (x == 6) {
                        colors[i, j] = ConsoleColor.Cyan;
                    }
                }
            }
        }

        private void printCurrent() {
            Console.SetCursorPosition(3 + currentEl.X * 2, currentEl.Y + 1);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = colors[currentEl.Y, currentEl.X];
            Console.Write('\u25A0');
            Console.ResetColor();
        }

        private void hideCurrent() {
            Console.SetCursorPosition(3 + currentEl.X * 2, currentEl.Y + 1);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = colors[currentEl.Y,currentEl.X];
            Console.Write('\u25A0');
        }

        private bool checkElements() {
            
            bool find = false;
            
            for (int i = 0; i < row; ++i) {
                for (int j = 0; j < col - 2; ++j) {
                    if (colors[i, j] == colors[i, j + 1] && colors[i, j] == colors[i, j + 2])
                    {
                        find = true;
                        same.Add(new Point(i, j));
                        same.Add(new Point(i, j + 1));
                        same.Add(new Point(i, j + 2));
                        if (j + 3 < col && colors[i, j] == colors[i, j + 3])
                        {
                            same.Add(new Point(i, j + 3));
                            countElementsX = 4;
                            if (j + 4 < col && colors[i, j] == colors[i, j + 4] && countElementsX == 4)
                            {
                                same.Add(new Point(i, j + 4));
                                countElementsX = 5;
                            }
                        }
                        else
                        {
                            countElementsX = 3;
                        }
                    }
                }
            }
            for (int i = 0; i < row - 2; ++i)
            {
                for (int j = 0; j < col; ++j)
                {
                    if (colors[i, j] == colors[i + 1, j] && colors[i, j] == colors[i + 2, j])
                    {
                        find = true;
                        same.Add(new Point(i, j));
                        same.Add(new Point(i + 1, j));
                        same.Add(new Point(i + 2, j));
                        if (i + 3 < col && colors[i, j] == colors[i + 3, j])
                        {
                            same.Add(new Point(i + 3, j));
                            countElementsY = 4;
                            if (i + 4 < col && colors[i, j] == colors[i + 4, j] && countElementsY == 4)
                            {
                                same.Add(new Point(i + 4, j));
                                countElementsY = 5;
                            }
                        }
                        else
                        {
                            countElementsY = 3;
                        }
                    }
                }
            }
            return find;
        }

        private void deleteElements() {
            foreach (Point p in same) {
                colors[p.X, p.Y] = ConsoleColor.Black;
            }
            same.Clear();
            printField();
        }
        
        private void addingElements() {
            int x;
            Random r = new Random();
            for (int i = 0; i < row; ++i) {
                for (int j = 0; j < col; ++j) {
                    x = r.Next(7);
                    if (colors[i, j] == ConsoleColor.Black) {
                        if (x == 0)
                        {
                            colors[i, j] = ConsoleColor.Blue;
                        }
                        if (x == 1)
                        {
                            colors[i, j] = ConsoleColor.Green;
                        }
                        if (x == 2)
                        {
                            colors[i, j] = ConsoleColor.DarkMagenta;
                        }
                        if (x == 3)
                        {
                            colors[i, j] = ConsoleColor.Red;
                        }
                        if (x == 4)
                        {
                            colors[i, j] = ConsoleColor.Yellow;
                        }
                        if (x == 5)
                        {
                            colors[i, j] = ConsoleColor.White;
                        }
                        if (x == 6)
                        {
                            colors[i, j] = ConsoleColor.Cyan;
                        }
                    }
                }
            }
        }
        
        public void run()
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            ConsoleColor bufferColor;
            bool selected = false;

            while (true) {

                key = Console.ReadKey(true);
                

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
                if (key.Key == ConsoleKey.Enter)
                {
                    if (selected && currentEl == prevEl) {
                        selected = false;
                    }

                    else if (!selected)//!false == true
                    {
                        prevEl = currentEl;
                        selected = true;
                    }
                    else if (prevEl.X != currentEl.X || prevEl.Y != currentEl.Y) {

                        bufferColor = colors[currentEl.Y, currentEl.X];
                        colors[currentEl.Y, currentEl.X] = colors[prevEl.Y, prevEl.X];
                        colors[prevEl.Y, prevEl.X] = bufferColor;

                        if (checkElements()) {
                            printElement(prevEl.X, prevEl.Y);
                            
                            deleteElements();
                            while (checkElements())
                            {
                                deleteElements();
                                addingElements();
                                printField();
                            }
                            printCurrent();
                        }
                        else {
                            bufferColor = colors[currentEl.Y, currentEl.X];
                            colors[currentEl.Y, currentEl.X] = colors[prevEl.Y, prevEl.X];
                            colors[prevEl.Y, prevEl.X] = bufferColor;
                        }

                        selected = false;
                    }
                }
                if (key.Key == ConsoleKey.UpArrow)
                {
                    hideCurrent();
                    if (!selected)
                    {
                        if (currentEl.Y == 0)
                        {
                            hideCurrent();
                            currentEl.Y = (row - 1);
                            printCurrent();
                        }
                        else
                        {
                            hideCurrent();
                            currentEl.Y--;
                            printCurrent();
                        }
                    }
                    else if (prevEl.Y != 0) {
                        currentEl = prevEl;
                        hideCurrent();
                        currentEl.Y--;
                        printCurrent();
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    hideCurrent();
                    if (!selected) {
                        if (currentEl.Y == (row - 1))
                        {
                            hideCurrent();
                            currentEl.Y = 0;
                            printCurrent();
                        }
                        else
                        {
                            hideCurrent();
                            currentEl.Y += 1;
                            printCurrent();
                        }
                    }
                    else if (prevEl.Y != row - 1)
                    {
                        currentEl = prevEl;
                        hideCurrent();
                        currentEl.Y++;
                        printCurrent();
                    }
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    hideCurrent();
                    if (!selected) {
                        if (currentEl.X == 0)
                        {
                            hideCurrent();
                            currentEl.X = (col - 1);
                            printCurrent();
                        }
                        else
                        {
                            hideCurrent();
                            currentEl.X -= 1;
                            printCurrent();
                        }
                    }
                    else if (prevEl.X != 0)
                    {
                        currentEl = prevEl;
                        hideCurrent();
                        currentEl.X--;
                        printCurrent();
                    }
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    hideCurrent();
                    if (!selected) {
                        if (currentEl.X == (col - 1))
                        {
                            hideCurrent();
                            currentEl.X = 0;
                            printCurrent();
                        }
                        else
                        {
                            hideCurrent();
                            currentEl.X += 1;
                            printCurrent();
                        }
                    }
                    else if (prevEl.X != col - 1)
                    {
                        currentEl = prevEl;
                        hideCurrent();
                        currentEl.X++;
                        printCurrent();
                    }
                }
            }
        }
    }
    
    class Program {

        static void Main(string[] args)
        {
            Game g = new Game();
            g.run();
        }
    }
}