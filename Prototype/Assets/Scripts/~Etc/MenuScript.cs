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

    public void GotoLevel(int level)
    {
        //stop time
        if(level == 5)
        {
            MainGameManager.Get.TimeOfDay.enabled = false;
        }
        SceneManager.LoadScene(level);
    }

}
