﻿using UnityEngine;
using System.Collections;

public class TutorialTemporary : MonoBehaviour
{
    MiniDialogue DialogueManager;
    CameraManager CameraManager;
    public int Order = 0;
    int prevOrder = -1;
    bool Done = false;

    public GameObject cup2;
    public GameObject cup3;
    public GameObject beanbag2;
    public GameObject reset;
    public GameObject finish;

    RectTransform CurrentDialogue;

    void Awake()
    {
        if (GameObject.Find("[Tutorial]"))
            DestroyObject(gameObject);

        gameObject.name = "[Tutorial]";
        DontDestroyOnLoad(gameObject);

        LoadPointers();
    }

    void LoadPointers ()
    {
        DialogueManager = GameObject.Find("MiniDialogue").transform.GetComponent<MiniDialogue>();
        CameraManager = GameObject.Find("Manager").transform.FindChild("CameraManager").GetComponent<CameraManager>();
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == Scenes.Minigame)
        {
            LoadPointers();
        }
    }

    void Update()
    {
        if (prevOrder != Order)
        {
            NextDialogue();
            prevOrder = Order;
        }

        //go to next step
        switch (Order)
        {
            case 0:
                if(CameraManager.step == 1)
                    ++Order;
                break;
            case 1:
                if (Camera.main.GetComponent<CameraLogic>().Stopped)
                    ++Order;
                break;
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
                if (Input.GetKeyDown(KeyCode.Z))
                    ++Order;
                break;
        }

        if(!Done)
        {
            reset.SetActive(false);
            finish.SetActive(false);
        }

    }

    void NextDialogue()
    {
        switch (Order)
        {
            case 0:
                CurrentDialogue = DialogueManager.CreateMiniDialogue("Press this mug to get things started!", new Vector2(250, 36), new Vector3(-19.5f, 3, 3));
                cup2.GetComponent<Collider>().enabled = false;
                cup3.GetComponent<Collider>().enabled = false;
                beanbag2.GetComponent<Collider>().enabled = false;
                break;
            case 1:
                DestroyObject(CurrentDialogue.gameObject);
                break;
            case 2:
                CurrentDialogue = DialogueManager.CreateMiniDialogue("Press first Coffee Bean Bag to get some coffee beans.", new Vector2(365, 36), new Vector3(-12, 4.2f, 4), Center.left);
                break;
            case 3:
                DestroyObject(CurrentDialogue.gameObject);
                CurrentDialogue = DialogueManager.CreateMiniDialogue("Now, put the coffee bean into the grinder.", new Vector2(280, 36), new Vector3(-6, -2, 3.13f));
                break;
            case 4:
                DestroyObject(CurrentDialogue.gameObject);
                CurrentDialogue = DialogueManager.CreateMiniDialogue("Great! Next, hold and rotate the handle.", new Vector2(275, 36), new Vector3(-6, -2, 3.13f));
                break;
            case 5:
                DestroyObject(CurrentDialogue.gameObject);
                CurrentDialogue = DialogueManager.CreateMiniDialogue("Drag the grinded powders into coffee machine.", new Vector2(320, 36), new Vector3(-2.5f, -2, 3.13f));
                break;
            case 6:
                DestroyObject(CurrentDialogue.gameObject);
                break;
            case 7:
                CurrentDialogue = DialogueManager.CreateMiniDialogue("Lastly, Select the hot water and hold the handle to add water.", new Vector2(550, 36), new Vector3(5, -2, 3.13f));
                break;
            case 8:
                DestroyObject(CurrentDialogue.gameObject);
                CurrentDialogue = DialogueManager.CreateMiniDialogue("Nicely Done! Press \"Finish\" to bring coffee back to the cafe!", new Vector2(550, 36), new Vector3(5, -2, 3.13f));
                FinishDialogue();
                break;
        }
    }

    void FinishDialogue()
    {
        cup2.GetComponent<MeshCollider>().enabled = true;
        cup3.GetComponent<MeshCollider>().enabled = true;
        beanbag2.GetComponent<Collider>().enabled = true;
        Done = true;
        finish.SetActive(true);
    }
}