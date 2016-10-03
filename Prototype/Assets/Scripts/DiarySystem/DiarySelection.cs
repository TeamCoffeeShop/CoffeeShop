using UnityEngine;
using System.Collections;

public class DiarySelection : MonoBehaviour
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
