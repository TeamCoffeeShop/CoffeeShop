using UnityEngine;
using System.Collections;

public class MinigameManager : MonoBehaviour
{
    //public link
    public static MinigameManager Get;

    //shortcuts
    public ResetManager ResetManager;
    public Minigame_CoffeeManager CoffeeManager;
    public CameraManager CameraManager;
    public Canvas UI;

    void Awake ()
    {
        Get = this;
    }
}
