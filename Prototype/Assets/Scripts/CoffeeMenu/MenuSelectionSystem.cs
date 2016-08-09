using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuSelectionSystem : MonoBehaviour {

    public int menuNum;

    public Button speechBallon;
    public Text menuText;

    public void SelectMenu()
    {
        int randomNum = Random.Range(0, menuNum);
        // Americano
        if (randomNum == 0)
        {
            menuText.text = "Americano";
        }
        else if (randomNum == 1)
        {
            menuText.text = "Caffe latte";
        }
        else if(randomNum == 2)
        {
            menuText.text = "Caffe Mocha";
        }
    }
}
