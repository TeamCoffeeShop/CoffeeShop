using UnityEngine;
using System.Collections;

public class Bianca : MonoBehaviour
{
    void OnMouseUp ()
    {
        MainGameManager.Get.SceneChangeManager.SetCurrentScene(CurrentScene.Dialogue);
    }
}
