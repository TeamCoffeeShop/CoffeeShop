using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CustomerLogic : MonoBehaviour
{
    public GameObject OrderingBallon;
    public GameObject SpawnTimer;

    bool arrived = false;
    float walkSpeed = 20;

    private GameObject OB;
    private GameObject ST;

    private float timer = 0.0f;
    private float customerspawntime = 5.0f;
    private int direction = -1;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnLevelWasLoaded(int level)
    {
        //if not main level, deactivate customers
        if (level != Scenes.MainLevel)
            GetComponent<Animator>().enabled = false;
        else
            GetComponent<Animator>().enabled = true;
    }

    void Start ()
    {        
        //Find Path to the seat
        FindPathToSeat();
    }

    void Update()
    {
        //if not arrived, walk
        if (!arrived)
        {
            if (WalkTowardsSeat())
            {
                //after arriving, make order
                OrderStart();
                arrived = true;
            }
        }
        else
        {
            if (GetComponent<Animator>().enabled == true)
            {
                timer += (Time.deltaTime / MainGameManager.Get.TimeOfDay.secondInFullDay) * 24.0f;
                
                if(ST)
                {
                    ST.GetComponent<BarScript>().Value = timer;

                    ////Delete customer when spawn time has passed
                    if (timer >= ST.GetComponent<BarScript>().MaxValue)
                    {
                        LeaveCoffeeShop();
                    }
                }
            }
        }
    }

    void OrderStart ()
    {
        OB = Instantiate(OrderingBallon);
        OB.transform.SetParent(GameObject.Find("[OrderHUD]").transform, false);
        OB.GetComponent<OrderingBallonLogic>().customer = transform;

        ST = Instantiate(SpawnTimer);
        ST.transform.SetParent(GameObject.Find("[OrderHUD]").transform, false);
        ST.GetComponent<CustomerSpawnTimer>().customer = transform;
        ST.GetComponent<BarScript>().MaxValue = customerspawntime;

        OB.GetComponent<OrderingBallonLogic>().SpawnBar = ST;

        //custom cup display
        CoffeeOrderSetup.SetOrder(ref OB, GetComponent<Customer>().data.order);
    }

    void LeaveCoffeeShop ()
    {
        DestroyObject(ST.GetComponent<CustomerSpawnTimer>().customer.gameObject);
        DestroyObject(ST);
        DestroyObject(OB);
        DestroyObject(this.gameObject);
    }

    List<Vector3> MoveGridList;
    List<A_GRID> OpenList;
    A_GRID[,] Grids;
    bool ReachedEnd = false;

    bool WalkTowardsSeat()
    {
        if (MoveGridList.Count != 0)
        {
            Vector3 direction = (MoveGridList[0] - transform.position).normalized;

            //walk to the seat
            transform.position += direction * walkSpeed * Time.deltaTime;

            //if past the seat
            Vector3 newdir = (MoveGridList[0] - transform.position).normalized;
            if (newdir.x * direction.x < 0 || newdir.z * direction.z < 0)
                MoveGridList.RemoveAt(0);

            //rotate
            transform.forward = direction;

            return false;
        }
        return true;
    }

    public Grid Begin;
    public Grid End;

    class A_GRID
    {
        public int X = -1, Z = -1;

        public float F, G, H;
        public bool closed;
        public A_GRID Parent = null;
    }

    private FloorGridLogic Floor;

    void FindPathToSeat ()
    {
        MoveGridList = new List<Vector3>();
        Floor = MainGameManager.Get.Floor;
        Grids = new A_GRID[Floor.X, Floor.Z];
        OpenList = new List<A_GRID>();
        ReachedEnd = false;

        AstarAlgorithm();

        if (!ReachedEnd)
            Debug.Log("Failed to find seat!");
        else
        {
            A_GRID grid = Grids[End.X, End.Z];
            while (grid != Grids[Begin.X, Begin.Z])
            {
                MoveGridList.Add(Floor.Grids[grid.X, grid.Z].transform.position);
                grid = grid.Parent;
            }
            MoveGridList.Reverse();
        }
    }

    void InsertInOrder(List<A_GRID> OpenList, A_GRID toInsert)
    {
        if (toInsert == null)
            return;

        int i;
        for (i = 0; i < OpenList.Count; ++i)
        {
            if (OpenList[i].F >= toInsert.F)
                break;
        }

        OpenList.Insert(i, toInsert);
    }

    void AstarAlgorithm ()
    {
        //Begin Grid
        A_GRID CurrentGrid = Grids[Begin.X, Begin.Z] = new A_GRID();
        CurrentGrid.X = Begin.X;
        CurrentGrid.Z = Begin.Z;

        FillAdjacentGrid(CurrentGrid);

        do
        {
            //find lowest grid
            CurrentGrid = OpenList[0];
            CurrentGrid.closed = true;
            OpenList.Remove(CurrentGrid);

            CheckAdjacentGrid(CurrentGrid);

        } while (OpenList.Count != 0 && !ReachedEnd);
    }
    
    void FillAdjacentGrid (A_GRID current_grid)
    {
        int X = current_grid.X, Z = current_grid.Z;
        A_GRID new_grid = null;

        if (X > 0)
        {
            OpenGrid(current_grid, X - 1, Z, ref new_grid);
            InsertInOrder(OpenList, new_grid);
            if (Z + 1 < Floor.Z)
            {
                OpenGrid(current_grid, X - 1, Z + 1, ref new_grid);
                InsertInOrder(OpenList, new_grid);
            }
            if (Z > 0)
            {
                OpenGrid(current_grid, X - 1, Z - 1, ref new_grid);
                InsertInOrder(OpenList, new_grid);
            }
        }

        if (X + 1 < Floor.X)
        {
            OpenGrid(current_grid, X + 1, Z, ref new_grid);
            InsertInOrder(OpenList, new_grid);
            if (Z + 1 < Floor.Z)
            {
                OpenGrid(current_grid, X + 1, Z + 1, ref new_grid);
                InsertInOrder(OpenList, new_grid);
            }
            if (Z > 0)
            {
                OpenGrid(current_grid, X + 1, Z - 1, ref new_grid);
                InsertInOrder(OpenList, new_grid);
            }
        }

        if (Z + 1 < Floor.Z)
        {
            OpenGrid(current_grid, X, Z + 1, ref new_grid);
            InsertInOrder(OpenList, new_grid);
        }
        if (Z > 0)
        {
            OpenGrid(current_grid, X, Z - 1, ref new_grid);
            InsertInOrder(OpenList, new_grid);
        }
    }

    bool OpenGrid (A_GRID current_grid, int nX, int nZ, ref A_GRID new_grid)
    {
        //if there is already OpenGrid, return null
        if (Grids[nX, nZ] != null)
        {
            if(Grids[nX, nZ].closed)
                return false;
            new_grid = Grids[nX, nZ];
            return true;
        }

        //if the grid is impenetrable, return null
        Transform rGrid = Floor.Grids[nX, nZ].transform;
        if (rGrid.childCount != 0)
            if (rGrid.GetChild(0).GetComponent<CafeDeco>().Type == CafeDecoType.impenetrable)
                return false;

        //calculate G
        new_grid = Grids[nX, nZ] = new A_GRID();

        //set X,Z
        new_grid.X = nX;
        new_grid.Z = nZ;

        //set Parent
        new_grid.Parent = Grids[current_grid.X, current_grid.Z];

        new_grid.G = CalculateG(current_grid, new_grid);

        //calculate H
        new_grid.H = Mathf.Abs(End.X - nX) + Mathf.Abs(End.Z - nZ);
        new_grid.H *= 10;

        //calculte F
        new_grid.F = new_grid.G + new_grid.H;

        return false;
    }

    void CheckAdjacentGrid (A_GRID current_grid)
    {
        int X = current_grid.X, Z = current_grid.Z;
        A_GRID new_grid = null;

        if (X > 0)
        {
            CalculateGrid(current_grid, ref new_grid, X - 1, Z);
            if (Z + 1 < Floor.Z)
                CalculateGrid(current_grid, ref new_grid, X - 1, Z + 1);                
            if (Z > 0)
                CalculateGrid(current_grid, ref new_grid, X - 1, Z - 1);                
        }

        if (X + 1 < Floor.X)
        {
            CalculateGrid(current_grid, ref new_grid, X + 1, Z);
            if (Z + 1 < Floor.Z)
                CalculateGrid(current_grid, ref new_grid, X + 1, Z + 1);
            if (Z > 0)
                CalculateGrid(current_grid, ref new_grid, X + 1, Z - 1);   
        }

        if (Z + 1 < Floor.Z)
            CalculateGrid(current_grid, ref new_grid, X, Z + 1);
        if (Z > 0)
            CalculateGrid(current_grid, ref new_grid, X, Z - 1);
    }

    void CalculateGrid (A_GRID current_grid, ref A_GRID new_grid, int X, int Z)
    {
        if (ReachedEnd)
            return;

        if (OpenGrid(current_grid, X, Z, ref new_grid))
        {
            //compare G
            if (new_grid.G + CalculateG(new_grid, current_grid) < current_grid.G)
            {
                //change
                current_grid.Parent = new_grid.Parent;

                //recalculat G and F
                current_grid.G = CalculateG(current_grid.Parent, new_grid.Parent);
                current_grid.F = current_grid.G + current_grid.H;

                //re-order in list
                //OpenList.Remove(current_grid);
                //InsertInOrder(OpenList, current_grid);
            }
        }
        else
        {
            InsertInOrder(OpenList, new_grid);

            //if reached end, return true
            if (End.X == X && End.Z == Z)
                ReachedEnd = true;
        }
    }

    float CalculateG(A_GRID current_grid, A_GRID new_grid)
    {
        //calculate G
        //diagonal
        if ((new_grid.X != current_grid.X) == (new_grid.Z != current_grid.Z))
            return current_grid.G + 14;
        //straight
        else
            return current_grid.G + 10;
    }
}
