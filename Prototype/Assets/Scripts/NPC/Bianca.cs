using UnityEngine;
using System.Collections;

public class Bianca : MonoBehaviour
{
    public RuntimeAnimatorController DeapBreath;
    Animator anim;

    void Awake ()
    {
        anim = transform.root.GetComponent<Animator>();
    }

    void OnMouseUp ()
    {
        MainGameManager.Get.SceneChangeManager.SetCurrentScene(CurrentScene.Dialogue);
    }

    void Update ()
    {
        //Temporary code!!!
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.runtimeAnimatorController = DeapBreath;
            anim.Play(0);
        }
    }
}
