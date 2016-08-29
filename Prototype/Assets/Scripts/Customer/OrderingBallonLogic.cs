using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OrderingBallonLogic : MonoBehaviour
{
    public Transform link;

    void Start ()
    {
        //follow link position
        if(link != null)
        {
            // WORLD TO CANVAS CODE ///////////////////////////////////////////////////////////////////////////////////
            RectTransform rt = GetComponent<RectTransform>();

            //offset
            Vector3 newPos = link.transform.position + new Vector3(0,10,0);

            rt.position = rt.worldToLocalMatrix * newPos;

            RectTransform CanvasRt = transform.parent.GetComponent<RectTransform>();

            Vector2 vPos = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToViewportPoint(newPos);
            Vector2 result = new Vector2(
            ((vPos.x * CanvasRt.sizeDelta.x) - (CanvasRt.sizeDelta.x * 0.5f)),
            ((vPos.y * CanvasRt.sizeDelta.y) - (CanvasRt.sizeDelta.y * 0.5f)));

            rt.anchoredPosition = result;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            //custom movement
            //rt.Translate(0, 0, 0);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "CompletedOrder")
        {
            if(Input.GetMouseButtonUp(0))
            {
                //check if the order is correct one


                //if correct, give correct respond (ex. customer leaving the cafe, paying, etc...)
                DestroyObject(other.gameObject);
                DestroyObject(link.gameObject);
                DestroyObject(this.gameObject);
                Cursor.visible = true;
            }
            

        }
    }
}
