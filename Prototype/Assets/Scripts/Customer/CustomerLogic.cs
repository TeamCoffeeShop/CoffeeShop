using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CustomerLogic : MonoBehaviour
{
    public RuntimeAnimatorController Seated_Motion;
    public GameObject OrderingBallon;
    public GameObject SpawnTimer;
    //public Grid Seat;
    public int SeatX = -1, SeatZ = -1;

    bool arrived = false;
    float walkSpeed = 20;

    private GameObject OB;
    private GameObject ST;

    private float timer = 0.0f;
    private float customerspawntime = 5.0f;
    private int direction = -1;
    private Billboard billboard;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnLevelWasLoaded(int level)
    {
        //if not main level, deactivate customers
        //if (level != Scenes.MainLevel)
        //    GetComponent<Animator>().enabled = false;
        //else
        //    GetComponent<Animator>().enabled = true;
    }

    void Start ()
    {
        billboard = GetComponent<Billboard>();
        
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
                transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = Seated_Motion;
                arrived = true;
            }
        }
        else
        {
            // TEMPORARY
            //if (GetComponent<Animator>().enabled == true)
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

    public void LeaveCoffeeShop ()
    {
        MainGameManager.Get.Floor.Grids[SeatX,SeatZ].transform.GetChild(0).GetComponent<CafeDeco>().Filled = false;
        DestroyObject(ST.GetComponent<CustomerSpawnTimer>().customer.gameObject);
        DestroyObject(ST);
        DestroyObject(OB);
        DestroyObject(this.gameObject);
    }

    class A_GRID
    {
        public int X = -1, Z = -1;

        public float F, G, H;
        public bool closed;
        public A_GRID Parent = null;
    }

    List<Vector3> MoveGridList;
    List<A_GRID> OpenList;
    A_GRID[,] Grids;
    bool ReachedEnd = false;

    public Grid Begin;
    public Grid End;
    private FloorGridLogic Floor;

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
            if (direction.z > direction.x * 1.01f)
                billboard.invert = false;
            else
                billboard.invert = true;

            return false;
        }
        return true;
    }

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

    void AstarAlgorithm ()
    {
        //Begin Grid
        A_GRID CurrentGrid = Grids[Begin.X, Begin.Z] = new A_GRID();
        CurrentGrid.X = Begin.X;
        CurrentGrid.Z = Begin.Z;
        CurrentGrid.closed = true;

        CheckAdjacentGrid(CurrentGrid);

        do
        {
            //find lowest grid
            CurrentGrid = OpenList[0];
            CurrentGrid.closed = true;
            OpenList.Remove(CurrentGrid);

            //Debug.Log("[" + CurrentGrid.X + ", " + CurrentGrid.Z + "]" + " is selected!");

            CheckAdjacentGrid(CurrentGrid);

        } while (OpenList.Count != 0 && !ReachedEnd);
    }

    void CheckAdjacentGrid (A_GRID current_grid)
    {
        int X = current_grid.X, Z = current_grid.Z;

        if (X > 0)
        {
            CalculateGrid(current_grid, X - 1, Z);
            if (Z + 1 < Floor.Z)
                CalculateGrid(current_grid, X - 1, Z + 1);                
            if (Z > 0)
                CalculateGrid(current_grid, X - 1, Z - 1);                
        }

        if (X + 1 < Floor.X)
        {
            CalculateGrid(current_grid, X + 1, Z);
            if (Z + 1 < Floor.Z)
                CalculateGrid(current_grid, X + 1, Z + 1);
            if (Z > 0)
                CalculateGrid(current_grid, X + 1, Z - 1);   
        }

        if (Z + 1 < Floor.Z)
            CalculateGrid(current_grid, X, Z + 1);
        if (Z > 0)
            CalculateGrid(current_grid, X, Z - 1);
    }

    void CalculateGrid(A_GRID current_grid, int nX, int nZ)
    {
        if (ReachedEnd)
            return;

        //Debug.Log("Checking [" + nX + ", " + nZ + "] ...");

        A_GRID new_grid = null;

        if (OpenGrid(current_grid, ref new_grid, nX, nZ))
        {
            //Debug.Log("There's already open grid! comparing G..");

            //compare G
            if (new_grid.G > CalculateG(current_grid, new_grid))
            {
                //change
                new_grid.Parent = current_grid;

                //recalculat G and F
                new_grid.G = CalculateG(current_grid, new_grid);
                new_grid.F = new_grid.G + new_grid.H;

                //Debug.Log("Lower cost path detected! redirecting...");
                OpenList.Remove(new_grid);
                InsertInOrder(OpenList, new_grid);


            }
        }
        else
        {
            InsertInOrder(OpenList, new_grid);

            //if reached end, return true
            if (End.X == nX && End.Z == nZ)
                ReachedEnd = true;
        }

        //if (new_grid != null)
        //    Debug.Log("F: " + new_grid.F + " G: " + new_grid.G + " H: " + new_grid.H);
        //else
        //    Debug.Log("Grid is impenetrable or closed.");
    }

    bool OpenGrid(A_GRID current_grid, ref A_GRID new_grid, int nX, int nZ)
    {
        //if there is already OpenGrid, return null
        if (Grids[nX, nZ] != null)
        {
            if (Grids[nX, nZ].closed)
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

    float CalculateG(A_GRID from, A_GRID to)
    {
        //calculate G
        //diagonal
        if ((to.X != from.X) == (to.Z != from.Z))
            return from.G + 14;
        //straight
        else
            return from.G + 10;
    }
}
