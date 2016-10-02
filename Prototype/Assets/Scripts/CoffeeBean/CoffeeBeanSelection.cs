using UnityEngine;
using System.Collections;

public class CoffeeBeanSelection : MonoBehaviour
{
    public void On()
    {
        gameObject.SetActive(true);
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }
}
