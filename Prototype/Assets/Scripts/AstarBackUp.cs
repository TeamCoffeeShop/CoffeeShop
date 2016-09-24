using UnityEngine;
using System.Collections.Generic;

class Backup
{
    List<Vector3> MoveGridList = new List<Vector3>();
    class A_GRID
    {
        public int X, Z;

        public float F, G, H;
        public bool closed;
        public A_GRID Parent = null;
    }
    public Grid Begin;
    public Grid End;
    private FloorGridLogic Floor;

    void FindPathToSeat()
    {
        Floor = MainGameManager.Get.Floor;
        A_GRID[,] Grids = new A_GRID[Floor.X, Floor.Z];
        Grids[Begin.X, Begin.Z] = new A_GRID();

        AstarAlgorithm(Grids, Begin.X, Begin.Z, false);

        //convert from A_GRID to GRID
        A_GRID grid = Grids[End.X, End.Z];
        while (grid != Grids[Begin.X, Begin.Z])
        {
            MoveGridList.Add(Floor.Grids[grid.X, grid.Z].transform.position);
            grid = grid.Parent;
        }
        MoveGridList.Reverse();

    }

    bool AstarAlgorithm(A_GRID[,] Grids, int X, int Z, bool shortest_path)
    {
        //if arrived to goal
        if (X == End.X && Z == End.Z)
        {
            Debug.Log("GOAL! " + End.X + " " + End.Z);
            return true;
        }

        //make this A_GRID closed
        Grids[X, Z].closed = true;

        Debug.Log(X + " " + Z);

        int loop = 0;
        A_GRID best_grid;
        bool new_shortest_path = false;

        do
        {
            A_GRID grid;
            best_grid = null;

            if (X > 0)
            {
                // - - -
                // X O -
                // - - -
                grid = OpenGrid(Grids, X, Z, X - 1, Z, shortest_path);
                CheckShortestGrid(Grids[X, Z], grid, ref best_grid, ref new_shortest_path);

                if (Z + 1 < Floor.Z)
                {
                    // X - -
                    // - O -
                    // - - -
                    grid = OpenGrid(Grids, X, Z, X - 1, Z + 1, shortest_path);
                    CheckShortestGrid(Grids[X, Z], grid, ref best_grid, ref new_shortest_path);
                }

                if (Z > 0)
                {
                    // - - -
                    // - O -
                    // X - -
                    grid = OpenGrid(Grids, X, Z, X - 1, Z - 1, shortest_path);
                    CheckShortestGrid(Grids[X, Z], grid, ref best_grid, ref new_shortest_path);
                }
            }

            if (X + 1 < Floor.X)
            {
                // - - -
                // - O X
                // - - -
                grid = OpenGrid(Grids, X, Z, X + 1, Z, shortest_path);
                CheckShortestGrid(Grids[X, Z], grid, ref best_grid, ref new_shortest_path);

                if (Z + 1 < Floor.Z)
                {
                    // - - X
                    // - O -
                    // - - -
                    grid = OpenGrid(Grids, X, Z, X + 1, Z + 1, shortest_path);
                    CheckShortestGrid(Grids[X, Z], grid, ref best_grid, ref new_shortest_path);
                }

                if (Z > 0)
                {
                    // - - -
                    // - O -
                    // - - X
                    grid = OpenGrid(Grids, X, Z, X + 1, Z - 1, shortest_path);
                    CheckShortestGrid(Grids[X, Z], grid, ref best_grid, ref new_shortest_path);
                }
            }

            if (Z + 1 < Floor.Z)
            {
                // - X -
                // - O -
                // - - -
                grid = OpenGrid(Grids, X, Z, X, Z + 1, shortest_path);
                CheckShortestGrid(Grids[X, Z], grid, ref best_grid, ref new_shortest_path);
            }

            if (Z > 0)
            {
                // - - -
                // - O -
                // - X -
                grid = OpenGrid(Grids, X, Z, X, Z - 1, shortest_path);
                CheckShortestGrid(Grids[X, Z], grid, ref best_grid, ref new_shortest_path);
            }

            //if all blocked, return failed
            if (best_grid == null)
            {
                Debug.Log("NO EXIT!");
                return false;
            }

            if (++loop > 5)
            {
                Debug.Log("maximum loop reached!");
                return true;
            }

        } while (AstarAlgorithm(Grids, best_grid.X, best_grid.Z, new_shortest_path));

        Debug.Log("TRUE! " + X + " " + Z);
        return true;
    }

    A_GRID OpenGrid(A_GRID[,] Grids, int X, int Z, int nX, int nZ, bool shortest_path)
    {
        float G = Grids[X, Z].G;

        //if there is already openGrid
        if (Grids[nX, nZ] != null)
            if (!Grids[nX, nZ].closed)
            {
                if (shortest_path)
                {
                    Grids[nX, nZ].Parent = Grids[X, Z];

                    //calculate G
                    //diagonal
                    if ((nX != X) == (nZ != Z))
                        G += 14;
                    //straight
                    else
                        G += 10;

                    Grids[nX, nZ].G = G;
                    Grids[nX, nZ].F = Grids[nX, nZ].G + Grids[nX, nZ].H;
                }
                return Grids[nX, nZ];
            }
            else
                //if the openGrid is closed , return null
                return null;

        //if the grid is impenetrable, return null
        Transform rGrid = Floor.Grids[nX, nZ].transform;
        if (rGrid.childCount != 0)
            if (rGrid.GetChild(0).GetComponent<CafeDeco>().Type == CafeDecoType.impenetrable)
                return null;

        A_GRID grid = Grids[nX, nZ] = new A_GRID();

        //calculate G
        //diagonal
        if ((nX != X) == (nZ != Z))
            G += 14;
        //straight
        else
            G += 10;
        grid.G = G;

        //calculate H
        grid.H = Mathf.Abs(End.X - nX) + Mathf.Abs(End.Z - nZ);
        grid.H *= 10;

        //calculte F
        grid.F = grid.G + grid.H;

        //set Parent
        grid.Parent = Grids[X, Z];

        //set X,Z
        grid.X = nX;
        grid.Z = nZ;

        return grid;
    }

    void CheckShortestGrid(A_GRID current_grid, A_GRID toCompare, ref A_GRID best_grid, ref bool shortest_path)
    {
        //if the grid is impenetrable, return
        if (toCompare == null)
            return;

        //if shortest path, return
        if (shortest_path)
            return;

        //if it is shorter path
        if (toCompare.Parent != current_grid)
        {
            //if it is not shorter, return
            if (toCompare.G >= current_grid.G)
                return;

            shortest_path = true;
            best_grid = toCompare;
        }
        //if this is first grid to compare, change
        else if (best_grid == null)
        {
            best_grid = toCompare;
        }
        //if this grid is better, change
        else if (best_grid.F > toCompare.F)
        {
            best_grid = toCompare;
        }
    }
}