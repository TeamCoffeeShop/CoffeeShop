using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OrderLogic : MonoBehaviour
{
    public bool CompletedOrder = false;
    public float SelectCancelSpeed = 5;

    Vector3 OriginalPosition;
    bool dragging = false;
    RectTransform rt;

    void Awake ()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }

    void Start ()
    {
        OriginalPosition = rt.position;
    }

    //void Start ()
    //{
    //    //saved between multi-scene
    //    DontDestroyOnLoad(this);
    //}

    //void OnLevelWasLoaded(int index)
    //{
    //    //make it invisible when not in neither minigame nor mainlevel
    //    if (index != Scenes.MainLevel && index != Scenes.Minigame)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        gameObject.SetActive(true);

    //        //make it follow camera (ortho)
    //        //if(index == Scenes.MainLevel)
    //        //{
    //        //    GameObject Camera = GameObject.Find("Main Camera");

    //        //    if(Camera != null)
    //        //    {
    //        //        //transform.position = Camera.transform.position;
    //        //        //transform.position += Camera.transform.forward;
    //        //        //transform.parent = Camera.transform;
    //        //        transform.parent = GameObject.Find("UI").transform;
    //        //    }
    //        //}
    //    }
    //}

    public void DragSelectedCup()
    {
        dragging = true;
        Cursor.visible = false;


        //drag the object
        rt.position = Input.mousePosition;
        Vector3 size = rt.localToWorldMatrix * rt.sizeDelta;
        rt.position -= new Vector3(size.x * 0.5f, size.y * 0.5f, 0);
    }

    public void EndDraggingCup()
    {
        Cursor.visible = true;
        dragging = false;
    }

    void Update ()
    {
        if(!dragging)
        {
            //return to its original position
            rt.position += (OriginalPosition - rt.position) * Time.deltaTime * SelectCancelSpeed;
        }
    }
}
