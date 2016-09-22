using UnityEngine;
using System.Collections;

public class DecoEditUI : MonoBehaviour
{
    public Grid EditingGrid;

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
}
