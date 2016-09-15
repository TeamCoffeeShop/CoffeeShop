using UnityEngine;
using System.Collections;

public class CameraTrigger : MonoBehaviour
{
    public CamMType ActionType;
    public CoffeeCupType CupType;

    public void Activate ()
    {
        MinigameManager.Get.CameraManager.ActivateAction(ActionType, CupType);
    }

    void OnMouseDown()
    {
        Activate();
    }
}
