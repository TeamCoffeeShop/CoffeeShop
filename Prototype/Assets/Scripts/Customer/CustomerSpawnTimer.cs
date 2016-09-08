using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CustomerSpawnTimer : MonoBehaviour {

    public Transform customer;

    int CurrentScene;

    public void OnLevelWasLoaded(int level)
    {
        CurrentScene = level;
        Visibility();
    }

    // Use this for initialization
    void Start()
    {
        CurrentScene = Scenes.asInt(SceneManager.GetActiveScene());
        Visibility();

        UpdatePosition();
    }

    void UpdatePosition()
    {
        //follow link position
        if (customer != null)
        {
            // WORLD TO CANVAS CODE ///////////////////////////////////////////////////////////////////////////////////
            RectTransform rt = GetComponent<RectTransform>();

            //offset
            Vector3 newPos = customer.transform.position - new Vector3(0, 10, 0);

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

    void Visibility()
    {
        if (CurrentScene == Scenes.MainLevel)
        {
            GetComponent<Image>().enabled = true;
            UpdatePosition();
        }
        else
            GetComponent<Image>().enabled = false;
    }
}
