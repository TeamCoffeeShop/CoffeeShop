using UnityEngine;
using System.Collections;
using System.Collections.Generic; // for list

public enum EditMode
{
    off, selecting, selected
}

public class FloorGridLogic : MonoBehaviour
{
    [Range(3, 30)]
    public int X;

    [Range(3, 30)]
    public int Z;

    public GameObject FloorPrefab;
    public GameObject FloorPrefab2;

    public EditMode IsEditMode;

    public GameObject[,] Grids;
    public List<CafeDeco> EmptySeats;

    public void ToggleEditMode()
    {
        SetEditMode(IsEditMode != EditMode.off ? EditMode.off : EditMode.selecting, null);
    }

    public void SetEditMode(EditMode mode, Grid grid)
    {
        switch (mode)
        {
            case EditMode.off:
                MainGameManager.Get.DecoEditUI.DisableSelected();
                break;
            case EditMode.selected:
                MainGameManager.Get.DecoEditUI.Selected(grid);
                break;
            case EditMode.selecting:
                break;
        }

        IsEditMode = mode;
    }

    void Start ()
    {
        CreateGrid();
        LoadItemsInCafe();
    }

    void CreateGrid ()
    {
        bool Floor = false;

        Grids = new GameObject[X, Z];

        //starting location
        Vector2 BeginLocation = new Vector2(FloorPrefab.transform.localScale.x * 0.5f * (1 - X), 0);

        //create grids
        for (int i = 0; i < X; ++i)
        {
            BeginLocation.y = FloorPrefab.transform.localScale.z * 0.5f * (1 - Z);
            Floor = !Floor;
            for (int j = 0; j < Z; ++j)
            {
                Grids[i, j] = GameObject.Instantiate(Floor ? FloorPrefab : FloorPrefab2);
                Floor = !Floor;
                Grids[i, j].transform.SetParent(gameObject.transform);
                Grids[i, j].GetComponent<Grid>().X = i;
                Grids[i, j].GetComponent<Grid>().Z = j;

                Grids[i, j].transform.position = new Vector3(BeginLocation.x, 0, BeginLocation.y);
                BeginLocation.y += FloorPrefab.transform.localScale.z;
            }
            BeginLocation.x += FloorPrefab.transform.localScale.x;
        }
    }

    //this one requires XML depository save.
    void LoadItemsInCafe ()
    {
        //XML load here

        //!!TEMPORARY!!
        //loads custom items instead of loading from XML.
        GameObject Seat = Resources.Load<GameObject>("Prefab/CafeDeco/CafeDeco_Seat");
        GameObject Seat2 = Resources.Load<GameObject>("Prefab/CafeDeco/CafeDeco_Seat 2");
        GameObject Table01 = Resources.Load<GameObject>("Prefab/CafeDeco/CafeDeco_Table 1");
        GameObject Table02 = Resources.Load<GameObject>("Prefab/CafeDeco/CafeDeco_Table 2");
        GameObject Bamboo = Resources.Load<GameObject>("Prefab/CafeDeco/CafeDeco_Plant_bamboo");
        GameObject Cactus = Resources.Load<GameObject>("Prefab/CafeDeco/CafeDeco_Plant_cactus");
        GameObject Tikitorch = Resources.Load<GameObject>("Prefab/CafeDeco/CafeDeco_Tikitorch");
        GameObject Counter01 = Resources.Load<GameObject>("Prefab/CafeDeco/CafeDeco_Counter 1");
        GameObject Divider01 = Resources.Load<GameObject>("Prefab/CafeDeco/CafeDeco_Divider 1");


        Grids[7, 1].GetComponent<Grid>().AddItemToGrid(Seat);
        Grids[6, 2].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, 90, 0);
        Grids[7, 3].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, 180, 0);
        Grids[8, 2].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, -90, 0);
        Grids[7, 2].GetComponent<Grid>().AddItemToGrid(Table01);

        Grids[2, 6].GetComponent<Grid>().AddItemToGrid(Seat2);
        Grids[1, 7].GetComponent<Grid>().AddItemToGrid(Seat2).transform.Rotate(0, 90, 0);
        Grids[2, 8].GetComponent<Grid>().AddItemToGrid(Seat2).transform.Rotate(0, 180, 0);
        Grids[3, 7].GetComponent<Grid>().AddItemToGrid(Seat2).transform.Rotate(0, -90, 0);
        Grids[2, 7].GetComponent<Grid>().AddItemToGrid(Table02);

        Grids[9, 9].GetComponent<Grid>().AddItemToGrid(Tikitorch);
        Grids[9, 7].GetComponent<Grid>().AddItemToGrid(Tikitorch);


        Grids[0, 1].GetComponent<Grid>().AddItemToGrid(Counter01).transform.Rotate(0, 180, 0);
        Grids[1, 1].GetComponent<Grid>().AddItemToGrid(Counter01).transform.Rotate(0, 180, 0);
        Grids[2, 0].GetComponent<Grid>().AddItemToGrid(Bamboo);

        Grids[5, 6].GetComponent<Grid>().AddItemToGrid(Cactus);
        Grids[5, 7].GetComponent<Grid>().AddItemToGrid(Divider01).transform.Rotate(0, -90, 0);
        Grids[5, 8].GetComponent<Grid>().AddItemToGrid(Divider01).transform.Rotate(0, -90, 0);
        Grids[5, 9].GetComponent<Grid>().AddItemToGrid(Divider01).transform.Rotate(0, -90, 0);
    }
}
