using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Minigame_InputManager : MonoBehaviour
{
    public bool CameraMove;
    
    void Update ()
    {
        if(CameraMove)
        {
            if(Input.GetKey(KeyCode.Mouse0))
            {
                //move camera
                Vector3 dP = new Vector3(-Input.GetAxis("Mouse X") * 0.5f, 0, 0);
                MinigameManager.Get.MakeOrderCamera.Slide(dP);
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
                CameraMove = false;
        }
    }

    public void GoBackToMainLevel ()
    {
        MinigameManager.Get.CoffeeManager.SaveFinishedOrder();
        MainGameManager.Get.Canvas_OrderHUD.CreateOrdersInUI();
    }
}
