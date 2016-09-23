using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CustomerLogic : MonoBehaviour
{
    public GameObject OrderingBallon;
    public GameObject SpawnTimer;

    public Grid Begin;
    public Grid Goal;

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
        if (FindPathToSeat())
            Debug.Log("Finding Path Failed!");
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

    List<Vector3> MoveGridList = new List<Vector3>();

    class AstarGrid
    {
        public AstarGrid Parent = null;
        public float F;
        public float G;
        public float H;
        public bool Closed;

        public int X;
        public int Z;
    }

    AstarGrid FillGridData(AstarGrid[,] map, AstarGrid parent, int X, int Z)
    {
        //if the Data already exists, return
        if (map[X,Z] != null)
            if (!map[X,Z].Closed)
                return map[X,Z];
            else
            {
                //Debug.Log(X + ", " + Z + " already used!");
                return null;
            }

        //if the grid is impenetrable, return
        Transform realGrid = MainGameManager.Get.Floor.Grids[X, Z].transform;
        if (realGrid.childCount != 0)
            if (realGrid.GetChild(0).GetComponent<CafeDeco>().Type == CafeDecoType.impenetrable)
            {
                //Debug.Log(X + ", " + Z + " impenetrable!");
                return null;
            }

        //Debug.Log("Adding " + X + ", " + Z + "...");

        AstarGrid grid = map[X, Z] = new AstarGrid();

        //distance from begin
        grid.G = (Begin.transform.position - MainGameManager.Get.Floor.Grids[X, Z].transform.position).sqrMagnitude;

        //distance from goal
        grid.H = (Goal.transform.position - MainGameManager.Get.Floor.Grids[X, Z].transform.position).sqrMagnitude;

        grid.F = grid.G + grid.H;
        grid.Parent = parent;
        grid.X = X;
        grid.Z = Z;

        return grid;
    }

    // a really costly A* pathfinding algorithm.
    // consider using thread with this code.
    bool FindPathToSeat()
    {
        //create new open map
        AstarGrid[,] map = new AstarGrid[MainGameManager.Get.Floor.X,MainGameManager.Get.Floor.Z];

        map[Begin.X, Begin.Z] = new AstarGrid();
        AstarAlgorithm(map, Begin.X, Begin.Z);

        AstarGrid grid = map[Goal.X, Goal.Z];
        while (grid != map[Begin.X, Begin.Z])
        {
            MoveGridList.Add(MainGameManager.Get.Floor.Grids[grid.X, grid.Z].transform.position);
            grid = grid.Parent;
        }
        MoveGridList.Reverse();

        return false;
    }

    bool AstarAlgorithm(AstarGrid[,] map, int X, int Z)
    {
        //if arrived to goal
        if(MainGameManager.Get.Floor.Grids[X,Z] == Goal.gameObject)
            return true;

        //close the data since it is selected
        map[X, Z].Closed = true;

        AstarGrid grid;

        //int loop = 0;
        float lowest_Cost = 100000;
        int Next_X = -1, Next_Z = -1;

        do
        {
            //Debug.Log("Grid " + X + ", " + Z + " selected!");

            lowest_Cost = 100000;
            Next_X = -1;
            Next_Z = -1;

            //Check grids near the current grid
            if (X - 1 >= 0)
            {
                // - - -
                // X O -
                // - - -
                grid = FillGridData(map, map[X, Z], X - 1, Z);
                CheckAstarGrid(map[X, Z], grid, ref lowest_Cost, ref Next_X, ref Next_Z, X - 1, Z);


                if (Z + 1 < MainGameManager.Get.Floor.Z)
                {
                    // X - -
                    // - O -
                    // - - -
                    grid = FillGridData(map, map[X, Z], X - 1, Z + 1);
                    CheckAstarGrid(map[X,Z], grid, ref lowest_Cost, ref Next_X, ref Next_Z, X - 1, Z + 1);
                }

                if(Z - 1 >= 0)
                {
                    // - - -
                    // - O -
                    // X - -
                    grid = FillGridData(map, map[X, Z], X - 1, Z - 1);
                    CheckAstarGrid(map[X, Z], grid, ref lowest_Cost, ref Next_X, ref Next_Z, X - 1, Z - 1);

                }
            }

            if (X + 1 < MainGameManager.Get.Floor.X)
            {
                // - - -
                // - O X
                // - - -
                grid = FillGridData(map, map[X, Z], X + 1, Z);
                CheckAstarGrid(map[X, Z], grid, ref lowest_Cost, ref Next_X, ref Next_Z, X + 1, Z);


                if (Z + 1 < MainGameManager.Get.Floor.Z)
                {
                    // - - X
                    // - O -
                    // - - -
                    grid = FillGridData(map, map[X, Z], X + 1, Z + 1);
                    CheckAstarGrid(map[X, Z], grid, ref lowest_Cost, ref Next_X, ref Next_Z, X + 1, Z + 1);
                }

                if (Z - 1 >= 0)
                {
                    // - - -
                    // - O -
                    // - - X
                    grid = FillGridData(map, map[X, Z], X + 1, Z - 1);
                    CheckAstarGrid(map[X, Z], grid, ref lowest_Cost, ref Next_X, ref Next_Z, X + 1, Z - 1);
                }
            }

            if (Z + 1 < MainGameManager.Get.Floor.Z)
            {
                // - X -
                // - O -
                // - - -
                grid = FillGridData(map, map[X, Z], X, Z + 1);
                CheckAstarGrid(map[X, Z], grid, ref lowest_Cost, ref Next_X, ref Next_Z, X, Z + 1);
            }

            if (Z - 1 >= 0)
            {
                // - - -
                // - O -
                // - X -
                grid = FillGridData(map, map[X, Z], X, Z - 1);
                CheckAstarGrid(map[X, Z], grid, ref lowest_Cost, ref Next_X, ref Next_Z, X, Z - 1);
            }

            //if blocked, return to 
            if (Next_X == -1 || Next_Z == -1)
            {
                //Debug.Log("End of road! returning...");
                return false;
            }

            //Debug.Log("Next grid is " + Next_X + ", " + Next_Z + "!");

            //loop++;
            //if (loop > 4)
            //{
            //    Debug.Log("max loop!");
            //    return true;
            //}

        } while (!AstarAlgorithm(map, Next_X, Next_Z));



        return true;
    }

    void CheckAstarGrid(AstarGrid curr_grid, AstarGrid grid, ref float lowest_Cost, ref int X, ref int Z, int Next_X, int Next_Z)
    {
        //change the target grid
        if (grid != null)
        {
            if (curr_grid != grid.Parent)
                //if have same parent
                if(curr_grid.Parent == grid.Parent)
                {
                    if (curr_grid.F < grid.F)
                        return;
                }

            if (lowest_Cost > grid.F)
            {
                lowest_Cost = grid.F;
                X = Next_X;
                Z = Next_Z;
            }
        }
    }

    bool WalkTowardsSeat ()
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
}
