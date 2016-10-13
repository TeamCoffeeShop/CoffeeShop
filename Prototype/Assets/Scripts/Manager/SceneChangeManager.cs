using UnityEngine;
using System.Collections;

public enum CurrentScene
{
    Null = 0, Cafe_Day = 1, Make_Order = 2, Dialogue = 3, Cafe_Night = 4, Upgrade = 5
    ,PauseMenu = 6
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
        SetCurrentScene(CurrentScene.Cafe_Day);
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
            case CurrentScene.Cafe_Day:
                MainGameManager.Get.Canvas_UI.gameObject.SetActive(true);
                MainGameManager.Get.CanvasNight_UI.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_Dialogue.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_OrderHUD.gameObject.SetActive(true);
                MinigameManager.Get.Canvas_UI.SetActive(false);
                MinigameManager.Get.MakeOrderCamera.gameObject.SetActive(false);
                MainGameManager.Get.CafeCamera.gameObject.SetActive(true);
                MainGameManager.Get.Canvas_PauseMenu.gameObject.SetActive(false);
                MainGameManager.Get.DialogueCamera.SetActive(false);
                MainGameManager.Get.CafeCamera.Return();
                AkSoundEngine.PostEvent("Set_Pause_Status_Off", gameObject);
                InGameTime.SetTimeScale(1);
                break;
            case CurrentScene.Make_Order:
                AkSoundEngine.PostEvent("Play_Select", gameObject);
                MainGameManager.Get.Canvas_UI.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_Dialogue.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_OrderHUD.gameObject.SetActive(true);
                MinigameManager.Get.Canvas_UI.SetActive(true);
                MinigameManager.Get.MakeOrderCamera.gameObject.SetActive(true);
                MainGameManager.Get.CafeCamera.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_PauseMenu.gameObject.SetActive(false);
                MainGameManager.Get.DialogueCamera.SetActive(false);
                InGameTime.SetTimeScale(0.1f);
                break;
            case CurrentScene.Dialogue:
                MainGameManager.Get.CafeCamera.LookingAtDialogue();
                MainGameManager.Get.DialogueManager.RunDialogue();
                MainGameManager.Get.Canvas_UI.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_OrderHUD.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_Dialogue.gameObject.SetActive(true);
                MainGameManager.Get.Canvas_PauseMenu.gameObject.SetActive(false);
                MinigameManager.Get.Canvas_UI.SetActive(false);
                InGameTime.SetTimeScale(0);
                break;
            case CurrentScene.Cafe_Night:
                MainGameManager.Get.CanvasNight_UI.gameObject.SetActive(true);
                MainGameManager.Get.Canvas_UI.gameObject.SetActive(false);
                MinigameManager.Get.Canvas_UI.SetActive(false);
                MainGameManager.Get.Canvas_OrderHUD.gameObject.SetActive(false);
                MinigameManager.Get.MakeOrderCamera.gameObject.SetActive(false);
                MainGameManager.Get.CafeCamera.gameObject.SetActive(true);
                MainGameManager.Get.DialogueCamera.SetActive(false);
                MainGameManager.Get.CafeCamera.Return();
                InGameTime.SetTimeScale(0);
                break;

            case CurrentScene.PauseMenu:
                MainGameManager.Get.Canvas_PauseMenu.gameObject.SetActive(true);
                AkSoundEngine.PostEvent("Set_Pause_Status_On", gameObject);

                InGameTime.SetTimeScale(0);
                break;
        }
    }
}
