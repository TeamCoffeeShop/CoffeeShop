using UnityEngine;
using System.Collections;

public class CoffeeCupSelector : MonoBehaviour
{
    public GameObject CoffeeCupPrefab;
    public GameObject CupInStock;
    public float MaxTime = 1;
    float time = 1;
    

    void Update()
    {
        //if empty, start counter
        if (CupInStock == null)
        {
            time += Time.deltaTime;

            //if time is ready, create a cup
            if (time >= MaxTime)
            {
                time = 0;
                CupInStock = GameObject.Instantiate(CoffeeCupPrefab);
                CupInStock.transform.position = transform.position;
                CupInStock.GetComponent<CoffeeCupBehavior>().CoffeeCupShelfLink = this;
            }
        }
    }
}
