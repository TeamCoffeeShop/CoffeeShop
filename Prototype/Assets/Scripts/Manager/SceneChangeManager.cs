using UnityEngine;
using System.Collections;

public enum CurrentScene
{
    Null = 0, Cafe = 1, Make_Order = 2, Dialogue = 3
}

public class SceneChangeManager : MonoBehaviour
{
    private CurrentScene CS = CurrentScene.Make_Order;

    public CurrentScene currentScene
    {
        get
        {
            return CS;
        }
    }
    
    void Start ()
    {
        SetCurrentScene(CurrentScene.Cafe);
    }

    public void SetCurrentScene(int scene)
    {
        SetCurrentScene((CurrentScene)scene);
    }

    public void SetCurrentScene(CurrentScene scene)
    {
        if (scene == CS)
            return;

        CS = scene;
        switch (CS)
        {
            case CurrentScene.Cafe:
                MainGameManager.Get.Canvas_UI.gameObject.SetActive(true);
                MainGameManager.Get.Canvas_Dialogue.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_OrderHUD.gameObject.SetActive(true);
                MinigameManager.Get.Canvas_UI.SetActive(false);
                MinigameManager.Get.MakeOrderCamera.gameObject.SetActive(false);
                MainGameManager.Get.CafeCamera.gameObject.SetActive(true);
                MainGameManager.Get.CafeCamera.Return();
                InGameTime.SetTimeScale(1);
                break;
            case CurrentScene.Make_Order:
                MainGameManager.Get.Canvas_UI.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_Dialogue.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_OrderHUD.gameObject.SetActive(false);
                MinigameManager.Get.Canvas_UI.SetActive(true);
                MinigameManager.Get.MakeOrderCamera.gameObject.SetActive(true);
                MainGameManager.Get.CafeCamera.gameObject.SetActive(false);
                InGameTime.SetTimeScale(0.1f);
                break;
            case CurrentScene.Dialogue:
                MainGameManager.Get.CafeCamera.LookingAtDialogue();
                MainGameManager.Get.DialogueManager.RunDialogue();
                MainGameManager.Get.Canvas_UI.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_OrderHUD.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_Dialogue.gameObject.SetActive(true);
                MinigameManager.Get.Canvas_UI.SetActive(false);
                InGameTime.SetTimeScale(0);
                break;
        }
    }
}
