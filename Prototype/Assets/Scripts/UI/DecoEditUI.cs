using UnityEngine;
using System.Collections;

public class DecoEditUI : MonoBehaviour
{
    public Grid EditingGrid;
    bool Dragging = false;
    Transform MovingCafeDeco;

    float CameraAngle;
    float MoveSpeed = 0.7f;

    public void Selected (Grid grid)
    {
        gameObject.SetActive(true);

        //grid
        EditingGrid = grid;
        EditingGrid.renderer.material = EditingGrid.Selected_Material;

        //CafeDeco
        MovingCafeDeco = grid.transform.GetChild(0);
        MovingCafeDeco.GetComponent<CafeDeco>().Selected = true;

        //camera
        MainGameManager.Get.maincamera.LookingAt(grid.transform.position + new Vector3(0, 5, 0));
        MainGameManager.Get.Floor.IsEditMode = EditMode.selected;
    }

    public void DisableSelected ()
    {
        if(EditingGrid != null)
        {
            //camera
            MainGameManager.Get.Floor.IsEditMode = EditMode.off;
            MainGameManager.Get.maincamera.Return();

            //CafeDeco
            MovingCafeDeco.GetComponent<CafeDeco>().Selected = false;
            MovingCafeDeco = null;

            //grid
            EditingGrid.renderer.material = EditingGrid.Original_Material;
            EditingGrid = null;

            gameObject.SetActive(false);
        }
    }

    public void Rotate ()
    {
        EditingGrid.transform.GetChild(0).GetComponent<CafeDeco>().Rotate();
    }

    public void Delete ()
    {
        Grid tempGrid = EditingGrid;
        DisableSelected();
        DestroyObject(tempGrid.transform.GetChild(0).gameObject);
    }

    public void MoveBegin ()
    {
        Dragging = true;
        Cursor.lockState = CursorLockMode.Locked;

        //unlink parent from grid
        MovingCafeDeco.SetParent(null);
    }

    public void MoveEnd ()
    {
        //cursor option
        Dragging = false;
        Cursor.lockState = CursorLockMode.None;
        
        //calculate closest grid to be moved
        GameObject closestgrid = EditingGrid.gameObject;
        float closest = (closestgrid.transform.position - MovingCafeDeco.transform.position).sqrMagnitude;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Floor");
        
        //find the closest grid
        foreach (GameObject grid in objs)
        {
            float dist = (grid.transform.position - MovingCafeDeco.transform.position).sqrMagnitude;
            
            //if one is closer
            if(dist < closest)
            {
                closest = dist;
                closestgrid = grid;
            }
        }

        //successfully move the CafeDeco into Grid
        MovingCafeDeco.transform.SetParent(closestgrid.transform);
        MovingCafeDeco.transform.localPosition = new Vector3(0, MovingCafeDeco.GetComponent<CafeDeco>().Float, 0);

        //change selected grid
        EditingGrid.renderer.material = EditingGrid.Original_Material;
        EditingGrid = closestgrid.GetComponent<Grid>();
        EditingGrid.renderer.material = EditingGrid.Selected_Material;

        //camera
        MainGameManager.Get.maincamera.LookingAt(MovingCafeDeco.position + new Vector3(0, 5, 0));
    }

    void Start ()
    {
        CameraAngle = -Quaternion.ToEulerAngles(MainGameManager.Get.maincamera.transform.rotation).y;
    }

    void Update ()
    {
        if (Dragging)
        {
            Vector2 Move = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            Vector2 newMove;
            newMove.x = Mathf.Cos(CameraAngle) * Move.x + -Mathf.Sin(CameraAngle) * Move.y;
            newMove.y = Mathf.Sin(CameraAngle) * Move.x + Mathf.Cos(CameraAngle) * Move.y;

            MovingCafeDeco.transform.position += new Vector3(newMove.x * MoveSpeed, 0, newMove.y * MoveSpeed);

            MainGameManager.Get.maincamera.LookingAt(MovingCafeDeco.position + new Vector3(0, 5, 0));
        }
    }
}
