using UnityEngine;
using System.Collections;
using System.Collections.Generic; // for list

public enum EditMode
{
    off, selecting, selected
}

public class FloorGridLogic : MonoBehaviour
{
    [Range(10, 30)]
    public int X;

    [Range(10, 30)]
    public int Z;

    public GameObject FloorPrefab;
    public GameObject FloorPrefab2;

    public EditMode IsEditMode;

    GameObject[,] Grids;
    public List<CafeDeco> Seats;

    public void ToggleEditMode()
    {
        SetEditMode(IsEditMode != EditMode.off ? EditMode.off : EditMode.selecting);
    }

    public void SetEditMode(EditMode mode)
    {
        switch (mode)
        {
            case EditMode.off:
                MainGameManager.Get.DecoEditUI.SetActive(false);
                break;
            case EditMode.selected:
                MainGameManager.Get.DecoEditUI.SetActive(true);
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

        Grids[2, 1].GetComponent<Grid>().AddItemToGrid(Seat);
        Grids[1, 2].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, 90, 0);
        Grids[2, 3].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, 180, 0);
        Grids[3, 2].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, -90, 0);

        Grids[7, 1].GetComponent<Grid>().AddItemToGrid(Seat);
        Grids[6, 2].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, 90, 0);
        Grids[7, 3].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, 180, 0);
        Grids[8, 2].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, -90, 0);

        Grids[7, 6].GetComponent<Grid>().AddItemToGrid(Seat);
        Grids[6, 7].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, 90, 0);
        Grids[7, 8].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, 180, 0);
        Grids[8, 7].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, -90, 0);

        Grids[2, 6].GetComponent<Grid>().AddItemToGrid(Seat);
        Grids[1, 7].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, 90, 0);
        Grids[2, 8].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, 180, 0);
        Grids[3, 7].GetComponent<Grid>().AddItemToGrid(Seat).transform.Rotate(0, -90, 0);
    }
}
