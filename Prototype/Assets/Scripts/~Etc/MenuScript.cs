using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(Scenes.MainLevel);
    }
    public void QuitGame()
    {
        //SceneManager.LoadScene(Scenes.MainLevel);
        Application.Quit();
    }
}
