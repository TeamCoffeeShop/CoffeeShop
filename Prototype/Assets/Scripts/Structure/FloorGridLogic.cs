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
    public List<CafeDeco> Seats;

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
        GameObject TestBox = Resources.Load<GameObject>("Prefab/CafeDeco/CafeDeco_TestBox");
        GameObject Table01 = Resources.Load<GameObject>("Prefab/CafeDeco/CafeDeco_Table 1");
        GameObject Table02 = Resources.Load<GameObject>("Prefab/CafeDeco/CafeDeco_Table 2");
        GameObject Carpet01 = Resources.Load<GameObject>("Prefab/CafeDeco/CafeDeco_Carpet 1");


        Grids[2, 1].GetComponent<Grid>().AddItemToGrid(Seat);
        Grids[1, 2].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, 90, 0);
        Grids[2, 3].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, 180, 0);
        Grids[3, 2].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, -90, 0);
        Grids[2, 2].GetComponent<Grid>().AddItemToGrid(Table01);
        Grids[2, 2].GetComponent<Grid>().AddItemToGrid(Carpet01).transform.Translate(0, 0.3f, 0);

        Grids[2, 6].GetComponent<Grid>().AddItemToGrid(Seat2);
        Grids[1, 7].GetComponent<Grid>().AddItemToGrid(Seat2).transform.Rotate(0, 90, 0);
        Grids[2, 8].GetComponent<Grid>().AddItemToGrid(Seat2).transform.Rotate(0, 180, 0);
        Grids[3, 7].GetComponent<Grid>().AddItemToGrid(Seat2).transform.Rotate(0, -90, 0);
        Grids[2, 7].GetComponent<Grid>().AddItemToGrid(Table02);
        Grids[2, 7].GetComponent<Grid>().AddItemToGrid(Carpet01).transform.Translate(0, 0.3f, 0);



        Grids[6, 4].GetComponent<Grid>().AddItemToGrid(TestBox);
        Grids[5, 4].GetComponent<Grid>().AddItemToGrid(TestBox);
        Grids[5, 5].GetComponent<Grid>().AddItemToGrid(TestBox);
        Grids[5, 6].GetComponent<Grid>().AddItemToGrid(TestBox);
        Grids[5, 7].GetComponent<Grid>().AddItemToGrid(TestBox);
        Grids[6, 7].GetComponent<Grid>().AddItemToGrid(TestBox);
    }
}
