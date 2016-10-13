using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CalculateScene : MonoBehaviour
{
    public Text CurrentMoney;
    public Text MaintenanceExpense;
    public Text Total;

	public void TurnOn ()
    {
        gameObject.SetActive(true);
        InGameTime.SetTimeScale(0);
        
        //input current data
        float current_money = MainGameManager.Get.playerManager.player.money;
        float maintenance_expense = 200;
        float total = current_money - maintenance_expense;
        MainGameManager.Get.playerManager.SubtractMoneyGradully(maintenance_expense);

        //delete all customers
        foreach (GameObject customer in GameObject.FindGameObjectsWithTag("Customer"))
            customer.GetComponent<CustomerLogic>().LeaveCoffeeShop();

        //display data
        CurrentMoney.text = current_money.ToString();
        MaintenanceExpense.text = maintenance_expense.ToString();
        Total.text =  total.ToString();
    }

    public void TurnOff ()
    {
        gameObject.SetActive(false);
        MainGameManager.Get.currentCanvas = 2;
        MainGameManager.Get.SceneChangeManager.SetCurrentScene(CurrentScene.Cafe);
        MainGameManager.Get.CanvasNight_UI.gameObject.SetActive(true);
        MainGameManager.Get.Canvas_UI.gameObject.SetActive(false);
        InGameTime.SetTimeScale(0);
    }
}
