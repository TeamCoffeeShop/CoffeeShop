using UnityEngine;
using System.Collections;

public class Bianca : MonoBehaviour
{
    public RuntimeAnimatorController DeepBreath;
    public RuntimeAnimatorController ExcitedTalk;
    public RuntimeAnimatorController HipTalkIdle;
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
            if (anim.runtimeAnimatorController == DeepBreath)
            {
                anim.runtimeAnimatorController = ExcitedTalk;
            }
            else if (anim.runtimeAnimatorController == ExcitedTalk)
            {
                anim.runtimeAnimatorController = HipTalkIdle;
            }
            else
                anim.runtimeAnimatorController = DeepBreath;
            anim.Play(0);
        }
    }
}
