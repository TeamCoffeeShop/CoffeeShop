﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Minigame_InputManager : MonoBehaviour
{
    public bool CameraMove;
    public GameObject HotCheat;
    public GameObject ColdCheat;

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

        //set pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainGameManager.Get.Canvas_PauseMenu.TogglePauseMenu();
        }

        //cheat code
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
            Instantiate(HotCheat).transform.position = new Vector3(-203.23f,0,3.76f);

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V))
            Instantiate(ColdCheat).transform.position = new Vector3(-201.23f, 0, 3.76f);

        if (Input.GetKeyDown(KeyCode.T))
            InGameTime.SetTimeScale(InGameTime.timeScale * 10);
        if (Input.GetKeyUp(KeyCode.T))
            InGameTime.SetTimeScale(InGameTime.timeScale * 0.1f);

        if (Input.GetKeyDown(KeyCode.D))
            MainGameManager.Get.SceneChangeManager.SetCurrentScene(CurrentScene.Dialogue);
        if (Input.GetKeyDown(KeyCode.A))
            MainGameManager.Get.SceneChangeManager.SetCurrentScene(CurrentScene.Cafe_Day);
    }

    public void GoBackToMainLevel ()
    {
        MainGameManager.Get.SceneChangeManager.SetCurrentScene(CurrentScene.Cafe_Day);
        gameObject.GetComponent<RecipeManager>().Close();
    }
}
