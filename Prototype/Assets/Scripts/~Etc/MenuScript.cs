using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void GotoMainGmae()
    {
        SceneManager.LoadScene(Scenes.MainLevel);
    }

}
