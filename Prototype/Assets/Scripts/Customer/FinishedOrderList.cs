using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinishedOrderList : MonoBehaviour
{
    public GameObject orderDisplayButton;

    void Awake ()
    {
        //if duplicate, erase this
        if (GameObject.Find("[[Finished Orders]]") != null)
            DestroyImmediate(gameObject);
    }

    void Start ()
    {
        //change name to specify this object
        transform.name = "[[Finished Orders]]";

        //make this saved between scenes
        DontDestroyOnLoad(this);
    }

    public void OnLevelWasLoaded(int level)
    {
        //if main level, create lists
        if (level == Scenes.MainLevel)
            CreateOrdersInUI();
    }

    public void SetTrashVisible (bool visible)
    {
        GameObject.Find("UI").transform.Find("Trash").gameObject.SetActive(visible);
    }

    void CreateOrdersInUI()
    {
        //create cups as much as childrens
        int size = transform.childCount;
        float xPos = 0;
        OrderLogic prevCup = null;
        for (int i = 0; i < size; ++i)
        {
            //create cup
            GameObject cup = GameObject.Instantiate(orderDisplayButton);
            cup.transform.SetParent(GameObject.Find("UI").transform, false);

            //set cup details here
            CoffeeCupBehavior finishedCup = transform.GetChild(i).GetComponent<CoffeeCupBehavior>();
            OrderLogic currentCup = cup.GetComponent<OrderLogic>();
            currentCup.originalCup = finishedCup;
            currentCup.OrderManager = this;
            if(prevCup)
                prevCup.NextFinishedOrder = currentCup;
            prevCup = currentCup;

            //distinguish image by the droptype
            CoffeeOrderSetup.SetOrder(ref cup, finishedCup.DistinguishedMenuName);

            //after distinguishing, deactivate original cup
            finishedCup.gameObject.SetActive(false);  

            //set Transform (to not stack in one place)
            RectTransform rt = cup.GetComponent<RectTransform>();
            float gap = (rt.localToWorldMatrix * rt.sizeDelta).x + 20;
            
            rt.Translate(xPos, 0, 0);
            xPos += gap;
            currentCup.Gap = gap;
        }
    }
}
