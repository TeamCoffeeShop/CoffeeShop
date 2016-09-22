using UnityEngine;
using System.Collections;

public class DecoEditUI : MonoBehaviour
{
    public Grid EditingGrid;
    bool Dragging = false;
    Vector3 PrevMousePosition;
    GameObject DecoCopy;

    float CameraAngle;
    float MoveSpeed = 0.1f;

    public void Selected (Grid grid)
    {
        gameObject.SetActive(true);
        EditingGrid = grid;
        grid.transform.GetChild(0).GetComponent<CafeDeco>().Selected = true;
        MainGameManager.Get.maincamera.LookingAt(grid.transform.position + new Vector3(0, 5, 0));
        MainGameManager.Get.Floor.IsEditMode = EditMode.selected;
    }

    public void DisableSelected ()
    {
        if(EditingGrid != null)
        {
            MainGameManager.Get.Floor.IsEditMode = EditMode.off;
            MainGameManager.Get.maincamera.Return();
            EditingGrid.transform.GetChild(0).GetComponent<CafeDeco>().Selected = false;
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
        Cursor.visible = false;
        PrevMousePosition = Input.mousePosition;

        //temporary. will change soon.
        DecoCopy = Instantiate(EditingGrid.transform.GetChild(0).gameObject);
        DecoCopy.transform.localScale = EditingGrid.transform.GetChild(0).lossyScale;
        DecoCopy.transform.position = EditingGrid.transform.GetChild(0).position;
    }

    public void MoveEnd ()
    {
        Dragging = false;
        Cursor.visible = true;
        DestroyObject(DecoCopy);
    }

    void Start ()
    {
        CameraAngle = -Quaternion.ToEulerAngles(MainGameManager.Get.maincamera.transform.rotation).y;
    }

    void Update ()
    {
        if (Dragging)
        {
            Vector3 Move = Input.mousePosition - PrevMousePosition;
            PrevMousePosition = Input.mousePosition;

            Vector2 newMove;
            newMove.x = Mathf.Cos(CameraAngle) * Move.x + -Mathf.Sin(CameraAngle) * Move.y;
            newMove.y = Mathf.Sin(CameraAngle) * Move.x + Mathf.Cos(CameraAngle) * Move.y;

            DecoCopy.transform.position += new Vector3(newMove.x * MoveSpeed, 0, newMove.y * MoveSpeed);

        }
    }
}
