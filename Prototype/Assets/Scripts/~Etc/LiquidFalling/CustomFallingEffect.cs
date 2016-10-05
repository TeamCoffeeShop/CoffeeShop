using UnityEngine;
using System.Collections;

public class CustomFallingEffect : MonoBehaviour
{
    public enum DropType
    {
        liquid
    }

    public DropType[] type;
    public Material[] material;
    public Vector3 beginPosition;
    public Vector3 endPosition;
    
    bool fall;

    GameObject liquidPrefab;
    GameObject currLiquid;

    void Start ()
    {
        //liquidPrefab = Resources.Load<>();
    }

    public void StartFalling ()
    {
        if(!fall)
        {
            fall = true;
        }
    }

    public void EndFalling ()
    {
        if (fall)
        {
            fall = true;
        }
    }

    public void ToggleFalling ()
    {
        if (fall) EndFalling();
        else StartFalling();
    }
}
