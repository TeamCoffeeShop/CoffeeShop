using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Minigame_CameraMover : MonoBehaviour
{
    void OnMouseDown ()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
            MinigameManager.Get.InputManager.CameraMove = true;
    }
}
