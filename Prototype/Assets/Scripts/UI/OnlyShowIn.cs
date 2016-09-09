using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnlyShowIn : MonoBehaviour
{
    public int Level;

    public void OnLevelWasLoaded (int level)
    {
        if (level == Level)
        {
            GetComponent<RawImage>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            GetComponent<RawImage>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
