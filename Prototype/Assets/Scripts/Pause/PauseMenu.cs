using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public bool Pause = false;
    private CurrentScene PrevScene;

    public void TogglePauseMenu ()
    {
        SetPauseMenu(!Pause);
    }

    public void SetPauseMenu (bool set)
    {
        Pause = set;

        if(Pause)
        {
            PrevScene = MainGameManager.Get.SceneChangeManager.currentScene;
            MainGameManager.Get.SceneChangeManager.SetCurrentScene(CurrentScene.PauseMenu);
        }
        else
        {
            MainGameManager.Get.SceneChangeManager.SetCurrentScene(PrevScene);
        }
    }
}
