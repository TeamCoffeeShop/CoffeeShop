﻿using UnityEngine;
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

        AkSoundEngine.PostEvent("Play_CoffeeGrinder", gameObject);
    }

    void OnMouseDrag()
    {
        if (machine.totalRotation > machine.stanRotation)
        {
            machine.CheckGrind = false;
        }
    }

    void OnMouseUp ()
    {
        machine.CheckGrind = false;
        if (machine.IsFilled)
        {
            machine.CheckGameStop = true;
        }
        machine.coffeeBar.gameObject.SetActive(false);
        AkSoundEngine.PostEvent("Stop_CoffeeGrinder", gameObject);

    }
}
