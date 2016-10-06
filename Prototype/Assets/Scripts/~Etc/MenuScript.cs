using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void QuitGame()
    {
        //SceneManager.LoadScene(Scenes.MainLevel);
        Application.Quit();
    }
}
