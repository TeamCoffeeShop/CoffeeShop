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

    List<Vector2> MoveGridList = new List<Vector2>();

    class AstarGrid
    {
        public AstarGrid Parent = null;
        public float F;
        public float G;
        public float H;
        public bool Closed;
    }

    AstarGrid FillGridData(AstarGrid[,] map, AstarGrid parent, int X, int Z)
    {
        Debug.Log("Adding " + X + ", " + Z + "...");

        //if the Data already exists, return
        if (map[X,Z] != null)
            if (!map[X,Z].Closed)
                return map[X,Z];
            else
                return null;

        //if the grid is impenetrable, return
        Transform realGrid = MainGameManager.Get.Floor.Grids[X, Z].transform;
        if (realGrid.childCount != 0)
            if (realGrid.GetChild(0).GetComponent<CafeDeco>().Type == CafeDecoType.impenetrable)
                return null;

        AstarGrid grid = map[X, Z] = new AstarGrid();

        //distance from begin
        grid.G = (Begin.transform.position - MainGameManager.Get.Floor.Grids[X, Z].transform.position).sqrMagnitude;

        //distance from goal
        grid.H = (Goal.transform.position - MainGameManager.Get.Floor.Grids[X, Z].transform.position).sqrMagnitude;

        grid.F = grid.G + grid.H;
        grid.Parent = parent;

        return grid;
    }

    // a really costly A* pathfinding algorithm.
    // consider using thread with this code.
    bool FindPathToSeat()
    {
        //create new open map
        AstarGrid[,] map = new AstarGrid[MainGameManager.Get.Floor.X,MainGameManager.Get.Floor.Z];

        map[Begin.X, Begin.Z] = new AstarGrid();
        map[Begin.X, Begin.Z].Closed = true;
        AstarAlgorithm(map, Begin.X, Begin.Z);

        return false;
    }

    void AstarAlgorithm(AstarGrid[,] map, int X, int Z)
    {
        Debug.Log("Grid " + X + ", " + Z + " selected!");

        float lowest_Cost = -1;
        int Next_X = -1, Next_Z = -1;
        AstarGrid grid;

        //Check grids near the current grid
        if (X - 1 >= 0)
        {
            // - - -
            // X O -
            // - - -
            grid = FillGridData(map, map[X, Z], X - 1, Z);
            if(grid != null)
            {
                lowest_Cost = grid.F;
                Next_X = X - 1;
                Next_Z = Z;
            }

            if (Z + 1 < MainGameManager.Get.Floor.Z)
            {
                // X - -
                // - O -
                // - - -
                grid = FillGridData(map, map[X, Z], X - 1, Z + 1);
                if (grid != null)
                    if(lowest_Cost > grid.F)
                    {
                        lowest_Cost = grid.F;
                        Next_X = X - 1;
                        Next_Z = Z + 1;
                    }
            }

            if(Z - 1 >= 0)
            {
                // - - -
                // - O -
                // X - -
                grid = FillGridData(map, map[X, Z], X - 1, Z - 1);
                if (grid != null)
                    if (lowest_Cost > grid.F)
                    {
                        lowest_Cost = grid.F;
                        Next_X = X - 1;
                        Next_Z = Z - 1;
                    }
            }
        }

        if (X + 1 < MainGameManager.Get.Floor.X)
        {
            // - - -
            // - O X
            // - - -
            grid = FillGridData(map, map[X, Z], X + 1, Z);
            if (grid != null)
                if (lowest_Cost > grid.F)
                {
                    lowest_Cost = grid.F;
                    Next_X = X + 1;
                    Next_Z = Z;
                }

            if (Z + 1 < MainGameManager.Get.Floor.Z)
            {
                // - - X
                // - O -
                // - - -
                grid = FillGridData(map, map[X, Z], X + 1, Z + 1);
                if (grid != null)
                    if (lowest_Cost > grid.F)
                    {
                        lowest_Cost = grid.F;
                        Next_X = X + 1;
                        Next_Z = Z + 1;
                    }
            }

            if (Z - 1 >= 0)
            {
                // - - -
                // - O -
                // - - X
                grid = FillGridData(map, map[X, Z], X + 1, Z - 1);
                if (grid != null)
                    if (lowest_Cost > grid.F)
                    {
                        lowest_Cost = grid.F;
                        Next_X = X + 1;
                        Next_Z = Z - 1;
                    }
            }
        }

        if (Z + 1 < MainGameManager.Get.Floor.Z)
        {
            // - X -
            // - O -
            // - - -
            grid = FillGridData(map, map[X, Z], X, Z + 1);
            if (grid != null)
                if (lowest_Cost > grid.F)
                {
                    lowest_Cost = grid.F;
                    Next_X = X;
                    Next_Z = Z + 1;
                }
        }

        if (Z - 1 >= 0)
        {
            // - - -
            // - O -
            // - X -
            grid = FillGridData(map, map[X, Z], X, Z - 1);
            if (grid != null)
                if (lowest_Cost > grid.F)
                {
                    lowest_Cost = grid.F;
                    Next_X = X;
                    Next_Z = Z - 1;
                }
        }

        Debug.Log("Next Grid is " + Next_X + ", " + Next_Z + ".");
    }


    bool WalkTowardsSeat ()
    {
        if (MoveGridList.Count != 0)
        {
            //walk to the seat


            return true;
        }

        return false;
    }

    //bool WalkTowardsSeatPrev ()
    //{
    //    //walk x coord first
    //    if (transform.localPosition.x < TargetSeat.x)
    //    {
    //        transform.localPosition += new Vector3(walkSpeed * Time.deltaTime,0,0);

    //        //if over, stop
    //        if (transform.localPosition.x > TargetSeat.x)
    //            transform.localPosition = new Vector3(TargetSeat.x, transform.position.y, transform.position.z);
    //    }
    //    else if (transform.localPosition.x > TargetSeat.x)
    //    {
    //        transform.localPosition += new Vector3(-walkSpeed * Time.deltaTime, 0, 0);

    //        //if over, stop
    //        if (transform.localPosition.x < TargetSeat.x)
    //            transform.localPosition = new Vector3(TargetSeat.x, transform.position.y, transform.position.z);
    //    }
    //    //if x finished, walk y
    //    else if (transform.localPosition.z < TargetSeat.z)
    //    {
    //        transform.localPosition += new Vector3(0,0,walkSpeed * Time.deltaTime);

    //        if(direction == -1)
    //        {
    //            transform.Rotate(0,90,0);
    //            direction = 0;
    //        }

    //        //if over, stop
    //        if (transform.localPosition.z > TargetSeat.z)
    //            transform.localPosition = new Vector3(transform.position.x, transform.position.y, TargetSeat.z);
    //    }
    //    else if (transform.localPosition.z > TargetSeat.z)
    //    {
    //        transform.localPosition += new Vector3(0, 0, -walkSpeed * Time.deltaTime);

    //        if (direction == -1)
    //        {
    //            transform.Rotate(0, -90, 0);
    //            direction = 1;
    //        }

    //        //if over, stop
    //        if (transform.localPosition.z < TargetSeat.z)
    //            transform.localPosition = new Vector3(transform.position.x, transform.position.y, TargetSeat.z);
    //    }
    //    else
    //        return true;
    //    return false;
    //}
}
