using UnityEngine;
using System.Collections;

//this class enables to make the owner always face forward to camera.
public class Billboard : MonoBehaviour
{
    public enum BillboardOption
    {
        inactive, active, atBegin
    }

    public BillboardOption option = BillboardOption.inactive;
    public bool invert = false;

    void Start()
    {
        if (option != BillboardOption.inactive)
        {
            transform.forward = Camera.main.transform.forward * (invert ? 1 : -1);
        }
    }

    void Update()
    {
        if (option == BillboardOption.active)
        {
            transform.forward = -Camera.main.transform.forward * (invert ? 1 : -1);
        }
    }
}