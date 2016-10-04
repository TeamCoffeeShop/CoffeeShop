using UnityEngine;
using System.Collections;

public class MinigameManager : MonoBehaviour
{
    //public link
    public static MinigameManager Get;

    //Canvas
    public GameObject Canvas_UI;

    //camera
    public MakeOrderCameraLogic MakeOrderCamera;

    //shortcuts
    public ResetManager ResetManager;
    public Minigame_CoffeeManager CoffeeManager;

    void Awake ()
    {
        Get = this;
    }
}
