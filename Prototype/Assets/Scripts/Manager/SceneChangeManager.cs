using UnityEngine;
using System.Collections;

public enum CurrentScene
{
    Cafe = 0, Make_Order = 1, Dialogue = 2
}

public class SceneChangeManager : MonoBehaviour
{
    private CurrentScene CS;

    public CurrentScene currentScene
    {
        get
        {
            return CS;
        }
    }

    public void SetCurrentScene(int scene)
    {
        if (scene == (int)CS)
            return;

        CS = (CurrentScene)scene;
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
                break;
            case CurrentScene.Make_Order:
                MainGameManager.Get.Canvas_UI.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_Dialogue.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_OrderHUD.gameObject.SetActive(false);
                MinigameManager.Get.Canvas_UI.SetActive(true);
                MinigameManager.Get.MakeOrderCamera.gameObject.SetActive(true);
                MainGameManager.Get.CafeCamera.gameObject.SetActive(false);
                break;
            case CurrentScene.Dialogue:
                MainGameManager.Get.CafeCamera.LookingAtDialogue();
                MainGameManager.Get.DialogueManager.RunDialogue();
                MainGameManager.Get.Canvas_UI.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_OrderHUD.gameObject.SetActive(false);
                MainGameManager.Get.Canvas_Dialogue.gameObject.SetActive(true);
                MinigameManager.Get.Canvas_UI.SetActive(false);
                break;
        }
    }
}
