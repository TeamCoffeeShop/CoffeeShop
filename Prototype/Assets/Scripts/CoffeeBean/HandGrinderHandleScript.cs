using UnityEngine;
using System.Collections;

public class HandGrinderHandleScript : MonoBehaviour
{
    public HandGrinderScript machine;

    void OnMouseDown ()
    {
        machine.CheckGrind = true;
        machine.rotationImage.enabled = false;
        machine.totalRotation = 0;
        machine.PowderContent = 0;
    }

    void OnMouseUp ()
    {
        machine.CheckGrind = false;
        if (machine.IsFilled)
        {
            machine.CheckGameStop = true;
        }
        machine.coffeeBar.gameObject.SetActive(false);
        //Cursor.visible = true;
    }
}
