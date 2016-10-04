using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Minigame_InputManager : MonoBehaviour
{
    public void GoBackToMainLevel ()
    {
        MinigameManager.Get.CoffeeManager.SaveFinishedOrder();
        MainGameManager.Get.SceneChangeManager.SetCurrentScene(CurrentScene.Cafe);
    }
}
