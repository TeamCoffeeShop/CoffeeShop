using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CalculateScene : MonoBehaviour
{
    private GameObject gameobject;

    public GameObject incomeText;
	// Use this for initialization
	void Start () {

        gameobject = GameObject.Find("CustomerList(Clone)");
        Destroy(gameobject);
        Destroy(MainGameManager.Get.TimeOfDay.gameObject);

        incomeText = GameObject.Find("income");
        incomeText.GetComponent<Text>().text = "Income : " + PlayerPrefs.GetFloat("income").ToString();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
