using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    public Material  Selected_Material;
    public Material Selecting_Material;
    public Material  Original_Material;
    public Renderer           Renderer;

    public int X, Z;

    void Awake ()
    {
        Renderer = GetComponent<Renderer>();
        Original_Material = Renderer.material;
    }

    void OnMouseEnter()
    {
        if(MainGameManager.Get.Floor.IsEditMode == EditMode.selecting)
            Renderer.material = Selecting_Material;
    }

    void OnMouseExit ()
    {
        if (MainGameManager.Get.Floor.IsEditMode == EditMode.selecting)
            Renderer.material = Original_Material;
    }

    void OnMouseUp ()
    {
        if (MainGameManager.Get.Floor.IsEditMode == EditMode.selecting)
        {
            if(transform.childCount != 0)
            {
                MainGameManager.Get.Floor.SetEditMode(EditMode.selected, this);
            }
        }
    }

    public bool IsFilled ()
    {
        if (transform.childCount != 0)
            return true;
        return false;
    }

    public GameObject AddItemToGrid (GameObject prefab)
    {
        GameObject Item = GameObject.Instantiate(prefab);
        Item.transform.SetParent(gameObject.transform);
        Item.transform.localPosition = new Vector3(0,gameObject.transform.lossyScale.y * 0.5f,0);
        return Item;
    }

    public static Grid FindClosestGrid (Vector3 pos)
    {
        //calculate closest grid to be moved
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Floor");

        GameObject closestgrid = objs[0];
        float closest = (closestgrid.transform.position - pos).sqrMagnitude;
        //find the closest grid
        foreach (GameObject grid in objs)
        {
            float dist = (grid.transform.position - pos).sqrMagnitude;

            //if one is closer
            if (dist < closest)
            {
                closest = dist;
                closestgrid = grid;
            }
        }

        return closestgrid.GetComponent<Grid>();
    }
}
