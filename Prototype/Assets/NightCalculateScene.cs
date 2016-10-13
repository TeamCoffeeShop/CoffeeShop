using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NightCalculateScene : MonoBehaviour {

    public Text Money;
    public Text Expense;
    public Text Total;

    public void TurnOn()
    {
        gameObject.SetActive(true);
        InGameTime.SetTimeScale(0);

        //input current data
        float money = MainGameManager.Get.playerManager.player.money;
        float expense = 0;
        float total = money - expense;
        MainGameManager.Get.playerManager.SubtractMoneyGradully(expense);

        //display data
        Money.text = "money : " + money.ToString();
        Expense.text = "expense : " + expense.ToString();
        Total.text = "total : " + total.ToString();
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
        MainGameManager.Get.SceneChangeManager.SetCurrentScene(CurrentScene.Cafe_Day);
        MainGameManager.Get.CanvasNight_UI.gameObject.SetActive(false);
        MainGameManager.Get.Canvas_UI.gameObject.SetActive(true);
        InGameTime.SetTimeScale(1);
    }

}
