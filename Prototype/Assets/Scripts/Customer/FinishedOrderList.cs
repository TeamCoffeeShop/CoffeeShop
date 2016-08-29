using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinishedOrderList : MonoBehaviour
{
    void Awake ()
    {
        //if duplicate, erase this
        if (GameObject.Find("[[Finished Orders]]") != null)
            DestroyImmediate(gameObject);

        //create saved orders
        OnLevelWasLoaded(Scenes.MainLevel);
    }

    void Start ()
    {
        //change name to specify this object
        transform.name = "[[Finished Orders]]";

        //make this saved between scenes
        DontDestroyOnLoad(this);
    }

    public void OnLevelWasLoaded(int level)
    {
        //if main level, create lists
        if (level == Scenes.MainLevel)
        {
            //create cups as much as childrens
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                Debug.Log(child);
                GameObject cup = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/cup"));
                cup.transform.SetParent(GameObject.Find("UI").transform, false);
            }
        }
    }
}
