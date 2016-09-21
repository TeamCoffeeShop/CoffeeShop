using UnityEngine;
using System.Collections;

public class FloorGridLogic : MonoBehaviour
{
    [Range(10, 30)]
    public int X;

    [Range(10, 30)]
    public int Z;

    public GameObject FloorPrefab;
    public GameObject FloorPrefab2;

    public bool IsEditMode = false;

    GameObject[,] Grids;

    void Start ()
    {
        bool Floor = false;

        Grids = new GameObject[X,Z];

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

    public void ToggleEditMode ()
    {
        IsEditMode = !IsEditMode;
    }
}
