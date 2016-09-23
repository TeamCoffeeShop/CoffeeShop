using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CalculateScene : MonoBehaviour
{
    private GameObject gameobject;

    public GameObject CurrentMoney;
    public GameObject MaintenanceExpense;
    public GameObject Total;
	// Use this for initialization
	void Start () {
        MainGameManager.Get.TimeOfDay.enabled = false;
        float current_money = MainGameManager.Get.playerManager.player.money;
        float maintenance_expense = 200;
        float total = current_money - maintenance_expense;
        MainGameManager.Get.playerManager.SubtractMoneyGradully(maintenance_expense);

        gameobject = GameObject.Find("CustomerList(Clone)");
        Destroy(gameobject);


        CurrentMoney = GameObject.Find("CurrentMoney");
        CurrentMoney.GetComponent<Text>().text = "Current Money : " + current_money;

        MaintenanceExpense = GameObject.Find("MaintenanceExpense");
        MaintenanceExpense.GetComponent<Text>().text = "Maintenance Expense : " + maintenance_expense;

        Total = GameObject.Find("Total");
        Total.GetComponent<Text>().text = "Total : " + total;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
