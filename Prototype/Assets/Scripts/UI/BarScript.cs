using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarScript : MonoBehaviour {

    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private Image content;

    public float MaxValue { get; set; }

    int CurrentScene;

    public float Value
    {
        set
        {
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

    public void OnLevelWasLoaded(int level)
    {
        CurrentScene = level;
        Visibility();
    }

	void Start ()
    {
        CurrentScene = Scenes.asInt(SceneManager.GetActiveScene());
        Visibility();
	}
	
	// Update is called once per frame
	void Update () {

        HandleBar();
	}

    private void HandleBar()
    {
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = fillAmount;
        }
    }

    private float Map(float value,float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    void Visibility()
    {
        if (CurrentScene == Scenes.MainLevel)
            GetComponent<Image>().enabled = true;
        else
            GetComponent<Image>().enabled = false;
    }
}
