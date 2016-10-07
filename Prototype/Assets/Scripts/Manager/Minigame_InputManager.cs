using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Minigame_InputManager : MonoBehaviour
{
    public bool CameraMove;
    private float prevX;
    private Plane P;
    private float d;

    void Awake ()
    {
        P = new Plane(new Vector3(0, 0, -1), 3.51f);
    }

    void Update ()
    {
        if(CameraMove)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray newRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                P.Raycast(newRay, out d);
                prevX = (Camera.main.transform.position + newRay.direction * d).x;
            }

            if(Input.GetKey(KeyCode.Mouse0))
            {
                Ray newRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                P.Raycast(newRay, out d);
                float currX = (Camera.main.transform.position + newRay.direction * d).x;
                float dX = prevX - currX;
                prevX = currX + dX;

                //move camera
                Vector3 dP = new Vector3(dX, 0, 0);
                //Vector3 dP = new Vector3(-Input.GetAxis("Mouse X") * 0.5f, 0, 0);
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
        MinigameManager.Get.ResetManager.Reset();
        MainGameManager.Get.SceneChangeManager.SetCurrentScene(CurrentScene.Cafe);
    }
}
