using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OrderLogic : MonoBehaviour
{
    public bool CompletedOrder = false;

	void Start ()
    {
        //saved between multi-scene
        DontDestroyOnLoad(this);
	}

    void OnLevelWasLoaded(int index)
    {
        //make it invisible when not in neither minigame nor mainlevel
        if (index != Scenes.MainLevel && index != Scenes.Minigame)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);

            //make it follow camera (ortho)
            //if(index == Scenes.MainLevel)
            //{
            //    GameObject Camera = GameObject.Find("Main Camera");

            //    if(Camera != null)
            //    {
            //        //transform.position = Camera.transform.position;
            //        //transform.position += Camera.transform.forward;
            //        //transform.parent = Camera.transform;
            //        transform.parent = GameObject.Find("UI").transform;
            //    }
            //}
        }
    }
}
