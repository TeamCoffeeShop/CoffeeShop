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
            UIEffect.WorldToCanvas(transform.parent.gameObject, customer.transform.position + new Vector3(0, -1, 0), GetComponent<RectTransform>());

        }
    }

    void Visibility()
    {
        if (CurrentScene == Scenes.MainLevel)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Image>().enabled = true;
            UpdatePosition();
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<Image>().enabled = false;
        }
    }
}
