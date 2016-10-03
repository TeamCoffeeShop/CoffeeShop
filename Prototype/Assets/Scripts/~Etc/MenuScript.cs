using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GotoMainGmae()
    {
        SceneManager.LoadScene(Scenes.MainLevel);
    }
    public void GotoLevel(int level)
    {
        //stop time
        if (level == Scenes.NextDay || level == Scenes.Diary)
        {
            InGameTime.SetTimeScale(0);
        }
        else
            InGameTime.SetTimeScale(1);

        SceneManager.LoadScene(level);
    }

}
