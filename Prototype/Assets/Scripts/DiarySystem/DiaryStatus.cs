using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiaryStatus : MonoBehaviour
{
    public int currentStatus;
    GameObject gmObject;
    bool diaryStatus;

    private float ratio;

    // Use this for initialization
    void Start()
    {
        ratio = MainGameManager.Get.playerManager.player.xp_currentVal / MainGameManager.Get.playerManager.player.xp_maxVal;

        switch (currentStatus)
        {
            case 1:
                gmObject = GameObject.Find("StatusNameGender");
                gmObject.GetComponent<Text>().text = "Player Name : " + "Gender : ";
                break;
            case 2:
                gmObject = GameObject.Find("StatusCafeName");
                gmObject.GetComponent<Text>().text = "Cafe Name : ";
                break;
            case 3:
                gmObject = GameObject.Find("StatusLevel");
                gmObject.GetComponent<Text>().text = "Level " + MainGameManager.Get.playerManager.player.level;
                break;
            case 4:
                gmObject = GameObject.Find("StatusXPBackground");
                gmObject.GetComponent<Image>().enabled = true;                
                break;
            case 5:
                gmObject = GameObject.Find("StatusXP");
                gmObject.transform.localScale = new Vector3(gmObject.transform.localScale.x * ratio, gmObject.transform.localScale.y, gmObject.transform.localScale.z);
                gmObject.GetComponent<Image>().enabled = true;
                break;
            case 6:
                gmObject = GameObject.Find("StatusMoney");
                gmObject.GetComponent<Text>().text = "Money " + MainGameManager.Get.playerManager.player.money;
                //change color depending on the current money
                if(MainGameManager.Get.playerManager.player.money < 0)
                {
                    gmObject.GetComponent<Text>().color = Color.red;
                }
                else
                {
                    gmObject.GetComponent<Text>().color = Color.black;
                }
                break;
            default:
                break;
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        diaryStatus = GameObject.Find("DiaryManager").GetComponent<DiaryManager>().StatusBool;

        if (diaryStatus)
        {
            if (currentStatus == 1 || currentStatus == 2 || currentStatus == 3 || currentStatus == 6)
            {
                gameObject.GetComponent<Text>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<Image>().enabled = true;
            }
        }
        else
        {
            if (currentStatus == 1 || currentStatus == 2 || currentStatus == 3 || currentStatus == 6)
            {
                gameObject.GetComponent<Text>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<Image>().enabled = false;
            }
        }
    }
}
