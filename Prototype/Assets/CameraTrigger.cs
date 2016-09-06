using UnityEngine;
using System.Collections;

public class CameraTrigger : MonoBehaviour
{
    public CamMType ActionType;
    public CoffeeCupType CupType;

    CameraManager CameraManager;

    void Awake ()
    {
        CameraManager = GameObject.Find("Manager").transform.Find("CameraManager").GetComponent<CameraManager>();
    }

    void OnMouseDown ()
    {
        ActivateAction();
    }

    public void ActivateAction ()
    {
        CameraManager.ActivateAction(ActionType, CupType);
    }

}
