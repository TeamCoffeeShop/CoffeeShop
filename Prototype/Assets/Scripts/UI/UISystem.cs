using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISystem : MonoBehaviour {

    public BarScript bar;
    //money UI
    public GameObject money_text;
    //Level UI
    public GameObject level_text;

    private float xp_currentVal;
    private float xp_maxVal;

    private float money;
    private int level;

    // Use this for initialization
    void Start() {

        xp_currentVal = MainGameManager.Get.playerManager.player.xp_currentVal;
        xp_maxVal = MainGameManager.Get.playerManager.player.xp_maxVal;

        money = MainGameManager.Get.playerManager.player.money;
        level = MainGameManager.Get.playerManager.player.level;

    }
	
	// Update is called once per frame
	void Update () {

        bar.Value = MainGameManager.Get.playerManager.player.xp_currentVal;
        bar.MaxValue = MainGameManager.Get.playerManager.player.xp_maxVal;

        money_text.GetComponent<Text>().text = MainGameManager.Get.playerManager.player.money.ToString("N0");
        level_text.GetComponent<Text>().text = MainGameManager.Get.playerManager.player.level.ToString();
    }
}
