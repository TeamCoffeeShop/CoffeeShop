using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Minigame_InputManager : MonoBehaviour
{
    public void GoBackToMainLevel ()
    {
        MinigameManager.Get.CoffeeManager.SaveFinishedOrder();
        SceneManager.LoadScene(Scenes.MainLevel);
        Cursor.visible = true;
    }
}
