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
        if (level == Scenes.NextDay)
        {
            MainGameManager.Get.TimeOfDay.enabled = false;
        }
        else
            MainGameManager.Get.TimeOfDay.enabled = true;

        SceneManager.LoadScene(level);
    }

}
