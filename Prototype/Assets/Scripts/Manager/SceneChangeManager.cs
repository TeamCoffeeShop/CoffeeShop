using UnityEngine;
using System.Collections;

enum CurrentScene
{
    Cafe, Make_Order, Dialogue
}

public class SceneChangeManager : MonoBehaviour
{
    private static CurrentScene CS;

    public static CurrentScene currentScene
    {
        get
        {
            return CS;
        }
    }


}
