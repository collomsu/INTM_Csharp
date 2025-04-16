using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace Semaine1
{
    internal class Labyrinthe
    {

        public Cell[][] maze;

        public Labyrinthe() { }

        public Labyrinthe(Cell[][] maze)
        {
            this.maze = maze;
        }

        public struct Cell
        {
            public bool[] stateParois;
            public bool visited;
            public Status status;

            public Cell(bool[] stateParois, bool visited, Status status)
            {
                this.stateParois = stateParois;
                this.visited = visited;
                this.status = status;
            }

            public Cell(bool haut, bool bas, bool gauche, bool droit, bool visited, Status status)
            {
                this.stateParois = new bool[] { haut, bas, gauche, droit };
                this.visited = visited;
                this.status = status;
            }
        }


        // 1) b) Pour représenter le statut j'ai choisi d'utiliser un type enuméré avec comme valeurs : Simple, Sortie, et Entree.
        public enum Status {
            Simple,
            Sortie,
            Entree
        }

        public bool IsOpen(int i, int j, int w)
        {
            return maze[i][j].stateParois[w];
        }

        public bool IsMazeStart(int i, int j)
        {
            return maze[i][j].status == Status.Entree;
        }

        public bool IsMazeEnd(int i, int j)
        {
            return maze[i][j].status == Status.Sortie;
        }

        public void Open(int i, int j, int w)
        {
            switch (w) {
                case 0:
                    if (i > 0)
                    {
                        maze[i][j].stateParois[w] = true;
                        maze[i - 1][j].stateParois[1] = true;
                    }
                    break;
                case 1:
                    if (i < maze.Length - 1)
                    {
                        maze[i][j].stateParois[w] = true;
                        maze[i + 1][j].stateParois[0] = true;
                    }
                    break;
                case 2:
                    if (j > 0)
                    {
                        maze[i][j].stateParois[w] = true;
                        maze[i][j - 1].stateParois[3] = true;
                    }
                    break;
                case 3:
                    if (j < maze[i].Length - 1)
                    {
                        maze[i][j].stateParois[w] = true;
                        maze[i][j + 1].stateParois[2] = true;
                    }
                    break;
            }
        }

        public List<KeyValuePair<int, int>> CloseNeighbors(int i, int j)
        {
            List<KeyValuePair<int, int>> poss = new List<KeyValuePair<int, int>>();
            if (i > 0)
            {
                poss.Add(new KeyValuePair<int, int>(i - 1, j));
            }

            if (i < maze.Length - 1)
            {
                poss.Add(new KeyValuePair<int, int>(i + 1, j));
            }

            if (j > 0)
            {
                poss.Add(new KeyValuePair<int, int>(i, j - 1));
            }

            if (j < maze[i].Length - 1)
            {
                poss.Add(new KeyValuePair<int, int>(i, j + 1));
            }

            return poss;
        }

        public KeyValuePair<int, int> Generate()
        {
            //Labyrinthe.Cell cell = new Labyrinthe.Cell(false, false, false, false, false, Labyrinthe.Status.Simple);
            //maze = new Cell[][]
            //{
            //    new Cell[] { cell, cell, cell, cell, cell, cell, cell, cell, cell, cell },
            //    new Cell[] { cell, cell, cell, cell, cell, cell, cell, cell, cell, cell },
            //    new Cell[] { cell, cell, cell, cell, cell, cell, cell, cell, cell, cell },
            //    new Cell[] { cell, cell, cell, cell, cell, cell, cell, cell, cell, cell },
            //    new Cell[] { cell, cell, cell, cell, cell, cell, cell, cell, cell, cell },
            //    new Cell[] { cell, cell, cell, cell, cell, cell, cell, cell, cell, cell },
            //    new Cell[] { cell, cell, cell, cell, cell, cell, cell, cell, cell, cell },
            //    new Cell[] { cell, cell, cell, cell, cell, cell, cell, cell, cell, cell },
            //    new Cell[] { cell, cell, cell, cell, cell, cell, cell, cell, cell, cell },
            //    new Cell[] { cell, cell, cell, cell, cell, cell, cell, cell, cell, cell }
            //};

            maze = new Cell[10][];
            for (int i = 0; i < maze.Length; i++)
            {
                maze[i] = new Cell[10];
                for (int j = 0; j < maze[i].Length; j++)
                {
                    maze[i][j] = new Cell(false, false, false, false, false, Status.Simple);
                }
            }

            Random rnd = new Random();
            int iNeighbors = 0;

            Stack<KeyValuePair<int, int>> cellsVisited = new Stack<KeyValuePair<int, int>>();
            KeyValuePair<int, int> cellPopped;

            cellsVisited.Push(new KeyValuePair<int, int>(rnd.Next(0, maze.Length - 1), rnd.Next(0, maze[0].Length - 1)));

            maze[cellsVisited.Peek().Key][cellsVisited.Peek().Value].visited = true;
            List<KeyValuePair<int, int>> neighbors = CloseNeighbors(cellsVisited.Peek().Key, cellsVisited.Peek().Value);
            iNeighbors = rnd.Next(0, neighbors.Count - 1);
            cellsVisited.Push(new KeyValuePair<int, int>(neighbors[iNeighbors].Key, neighbors[iNeighbors].Value));

            do
            {
                neighbors = CloseNeighbors(cellsVisited.Peek().Key, cellsVisited.Peek().Value);
                do
                {
                    iNeighbors = rnd.Next(0, neighbors.Count - 1);
                    if (!maze[neighbors.ElementAt(iNeighbors).Key][neighbors.ElementAt(iNeighbors).Value].visited)
                    {                    
                        switch ((neighbors.ElementAt(iNeighbors).Key - cellsVisited.Peek().Key, neighbors.ElementAt(iNeighbors).Value - cellsVisited.Peek().Value))
                        {
                            case (-1, 0):
                                Open(cellsVisited.Peek().Key, cellsVisited.Peek().Value, 0);
                                break;
                            case (1, 0):
                                Open(cellsVisited.Peek().Key, cellsVisited.Peek().Value, 1);
                                break;
                            case (0, -1):
                                Open(cellsVisited.Peek().Key, cellsVisited.Peek().Value, 2);
                                break;
                            case (0, 1):
                                Open(cellsVisited.Peek().Key, cellsVisited.Peek().Value, 3);
                                break;
                        }
                        maze[neighbors.ElementAt(iNeighbors).Key][neighbors.ElementAt(iNeighbors).Value].visited = true;

                        cellsVisited.Push(new KeyValuePair<int, int>(neighbors.ElementAt(iNeighbors).Key, neighbors.ElementAt(iNeighbors).Value));
                        neighbors = CloseNeighbors(cellsVisited.Peek().Key, cellsVisited.Peek().Value);
                    }
                    else
                    {
                        neighbors.RemoveAt(iNeighbors);
                    }

                } while (neighbors.Count > 0);
                cellsVisited.Pop();
            } while (cellsVisited.Count > 1);

            KeyValuePair<int, int> posEntree = RandomBorder();
            KeyValuePair<int, int> posSortie;
            do
            {
                posSortie = RandomBorder();
            } while (posSortie.Key == posEntree.Key && posSortie.Value == posEntree.Value);

            IsMazeStart(posEntree.Key, posEntree.Value);
            IsMazeEnd(posSortie.Key, posSortie.Value);

            return posEntree;
        }

        public KeyValuePair<int, int> RandomBorder()
        {
            Random rnd = new Random();

            int[] xExtremeValue = { 0, maze.Length - 1 };
            int[] yExtremeValue = { 0, maze[0].Length - 1 };

            int key = rnd.Next(0, maze.Length - 1);
            int value = rnd.Next(0, maze[key].Length - 1);

            if ((key != 0 && key != maze.Length - 1) && (value != 0 && value != maze[key].Length - 1))
            {
                if (rnd.Next(0, 1) == 0)
                {
                    key = xExtremeValue[rnd.Next(0, yExtremeValue.Length - 1)];
                }
                else
                {
                    value = yExtremeValue[rnd.Next(0, yExtremeValue.Length - 1)];
                }
            }

            return new KeyValuePair<int, int>(key, value);
        }

        // 6) Chaque intersection est entouré par au maximum 4 murs, que je définirais par HG (haut-gauche), HD (haut-droite), BG (bas-gauche) et BD (bas-droite).
        // ' ' : HG(Bas = true, Droit = true), HD(Bas = true, Gauche = true), BG(Haut = true, Droit = true), BD(Haut = true, Gauche = true).
        // '╴' : HG(Bas = false, Droit = true), HD(Bas = true, Gauche = true), BG(Haut = false, Droit = true), BD(Haut = true, Gauche = true).
        // '╷' : HG(Bas = true, Droit = true), HD(Bas = true, Gauche = true), BG(Haut = true, Droit = false), BD(Haut = true, Gauche = false).
        // '┐' : HG(Bas = false, Droit = true), HD(Bas = true, Gauche = true), BG(Haut = false, Droit = false), BD(Haut = true, Gauche = false).
        // '╶' : HG(Bas = true, Droit = true), HD(Bas = false, Gauche = true), BG(Haut = true, Droit = true), BD(Haut = false, Gauche = true).
        // '─' : HG(Bas = false, Droit = true), HD(Bas = false, Gauche = true), BG(Haut = false, Droit = true), BD(Haut = false, Gauche = true).
        // '┌' : HG(Bas = true, Droit = true), HD(Bas = false, Gauche = true), BG(Haut = true, Droit = false), BD(Haut = false, Gauche = false).
        // '┬' : HG(Bas = false, Droit = true), HD(Bas = false, Gauche = true), BG(Haut = false, Droit = false), BD(Haut = false, Gauche = false).
        // '╵' : HG(Bas = true, Droit = false), HD(Bas = true, Gauche = false), BG(Haut = true, Droit = true), BD(Haut = true, Gauche = true).
        // '┘' : HG(Bas = false, Droit = false), HD(Bas = true, Gauche = false), BG(Haut = false, Droit = true), BD(Haut = true, Gauche = true).
        // '│' : HG(Bas = true, Droit = false), HD(Bas = true, Gauche = false), BG(Haut = true, Droit = false), BD(Haut = true, Gauche = false).
        // '┤' : HG(Bas = false, Droit = false), HD(Bas = true, Gauche = false), BG(Haut = false, Droit = false), BD(Haut = true, Gauche = false).
        // '└' : HG(Bas = true, Droit = false), HD(Bas = false, Gauche = false), BG(Haut = true, Droit = true), BD(Haut = false, Gauche = true).
        // '┴' : HG(Bas = false, Droit = false), HD(Bas = false, Gauche = false), BG(Haut = false, Droit = true), BD(Haut = false, Gauche = true).
        // '├' : HG(Bas = true, Droit = false), HD(Bas = false, Gauche = false), BG(Haut = true, Droit = false), BD(Haut = false, Gauche = false).
        // '┼' : HG(Bas = false, Droit = false), HD(Bas = false, Gauche = false), BG(Haut = false, Droit = false), BD(Haut = false, Gauche = false).

        public string DisplayLine(int n)
        {
            if (n < 0 || n > maze.Length)
            {
                Console.WriteLine("Ligne hors limite du labyrinthe.");
                return "";
            }

            string ligneOut = $"{n:00} : ";

            bool[] HG, HD, BG, BD;

            if (n == 0)
            {
                bool[] outEmpty = { true, true, true, true };
                bool[] outHGHD = { true, false, true, true };
                bool[] outLeftBG = { true, true, true, false };
                bool[] outRightBD = { true, true, false, true };

                ligneOut += StateParoisInChar(outEmpty, outHGHD, outLeftBG, maze[n][0].stateParois);

                for (int i = 0; i < maze[n].Length - 1; i++)
                {
                    BG = maze[n][i].stateParois;
                    BD = maze[n][i + 1].stateParois;

                    ligneOut += StateParoisInChar(outHGHD, outHGHD, BG, BD);
                }

                ligneOut += StateParoisInChar(outHGHD, outEmpty, maze[n][maze[n].Length - 1].stateParois, outRightBD);
            }
            else if (n > 0 && n < maze.Length)
            {
                bool[] outBorder = { true, true, false, false };

                ligneOut += StateParoisInChar(outBorder, maze[n - 1][0].stateParois, outBorder, maze[n][0].stateParois);

                for (int i = 0; i < maze[n - 1].Length - 1; i++)
                {
                    HG = maze[n - 1][i].stateParois;
                    HD = maze[n - 1][i + 1].stateParois;
                    BG = maze[n][i].stateParois;
                    BD = maze[n][i + 1].stateParois;

                    ligneOut += StateParoisInChar(HG, HD, BG, BD);
                }
                
                ligneOut += StateParoisInChar(maze[n - 1][maze[n - 1].Length - 1].stateParois, outBorder, maze[n][maze[n].Length - 1].stateParois, outBorder);
            }
            else
            {
                bool[] outEmpty = { true, true, true, true };
                bool[] outBGBD = { false, true, true, true };
                bool[] outLeftHG = { true, true, true, false };
                bool[] outRightHD = { true, true, false, true };

                ligneOut += StateParoisInChar(outLeftHG, maze[n - 1][0].stateParois, outEmpty, outBGBD);

                for (int i = 0; i < maze[n - 1].Length - 1; i++)
                {
                    HG = maze[n - 1][i].stateParois;
                    HD = maze[n - 1][i + 1].stateParois;

                    ligneOut += StateParoisInChar(HG, HD, outBGBD, outBGBD);
                }

                ligneOut += StateParoisInChar(maze[n - 1][maze[n - 1].Length - 1].stateParois, outRightHD, outBGBD, outEmpty);
            }

            return ligneOut;
        }

        public char StateParoisInChar(bool[] HG, bool[] HD, bool[] BG, bool[] BD)
        {
            KeyValuePair<bool, bool> cornerHG = new KeyValuePair<bool, bool>(HG[1], HG[3]);
            KeyValuePair<bool, bool> cornerHD = new KeyValuePair<bool, bool>(HD[1], HD[2]);
            KeyValuePair<bool, bool> cornerBG = new KeyValuePair<bool, bool>(BG[0], BG[3]);
            KeyValuePair<bool, bool> cornerBD = new KeyValuePair<bool, bool>(BD[0], BD[2]);

            switch ((cornerHG.Key, cornerHG.Value, cornerHD.Key, cornerHD.Value, cornerBG.Key, cornerBG.Value, cornerBD.Key, cornerBD.Value))
            {
                case (true, true, true, true, true, true, true, true):
                    return ' ';
                case (false, true, true, true, false, true, true, true):
                    return '╴';
                case (true, true, true, true, true, false, true, false):
                    return '╷';
                case (false, true, true, true, false, false, true, false):
                    return '┐';
                case (true, true, false, true, true, true, false, true):
                    return '╶';
                case (false, true, false, true, false, true, false, true):
                    return '─';
                case (true, true, false, true, true, false, false, false):
                    return '┌';
                case (false, true, false, true, false, false, false, false):
                    return '┬';
                case (true, false, true, false, true, true, true, true):
                    return '╵';
                case (false, false, true, false, false, true, true, true):
                    return '┘';
                case (true, false, true, false, true, false, true, false):
                    return '│';
                case (false, false, true, false, false, false, true, false):
                    return '┤';
                case (true, false, false, false, true, true, false, true):
                    return '└';
                case (false, false, false, false, false, true, false, true):
                    return '┴';
                case (true, false, false, false, true, false, false, false):
                    return '├';
                case (false, false, false, false, false, false, false, false):
                    return '┼';
                default:
                    return 'x';
            }
        }

        public List<string> Display()
        {
            List<string> output = new List<string>();
            for (int i = 0; i <= maze.Length; i++)
            {
                output.Add(DisplayLine(i));
            }
            return output;
        }

        public void DisplayLabyrinthe(List<string> strings)
        {
            foreach (string line in strings)
            {
                Console.WriteLine(line);
            }
        }

        public void DisplayStateAllCells()
        {
            foreach (Cell[] cells in maze)
            {
                foreach (Cell c in cells)
                {
                    Console.WriteLine($"Haut :{c.stateParois[0]}, Bas:{c.stateParois[1]}, Gauche:{c.stateParois[2]}, Droite:{c.stateParois[3]}");
                }
            }
        }
    }
}
