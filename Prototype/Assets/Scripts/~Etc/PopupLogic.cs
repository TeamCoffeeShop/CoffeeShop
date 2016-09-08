using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupLogic : MonoBehaviour
{
    public float Duration = 1;
    public float MovingSpeed = 10;

    RectTransform rt;
    Text text;

    float time;

    void Awake ()
    {
        rt = GetComponent<RectTransform>();
        text = GetComponent<Text>();
        text.CrossFadeAlpha(0, Duration, false);
        time = Duration;
    }

	// Update is called once per frame
	void Update ()
    {
        time -= Time.deltaTime;

        rt.Translate(0, MovingSpeed * Time.deltaTime, 0);

        //destroy if alpha is done
        if (time <= 0)
            DestroyObject(gameObject);
	}
}
