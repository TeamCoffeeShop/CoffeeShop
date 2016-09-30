using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class LookAt : MonoBehaviour
{
    float Distance = 81.5f;
    float OriginalSize;
    Vector3 TargetPosition;
    Vector3 OriginalPosition;
    Quaternion OriginalRotation;
    bool follow = false;
    Camera cam;

    public float Speed = 10; 
    //public float 

    void Awake ()
    {
        OriginalRotation = transform.rotation;
        TargetPosition = OriginalPosition = transform.position;
        OriginalSize = GetComponent<Camera>().orthographicSize;
        cam = GetComponent<Camera>();
    }

    void Update ()
    {
        transform.position += ((follow ? TargetPosition : OriginalPosition) - transform.position) * Time.deltaTime * Speed;
        cam.orthographicSize += ((follow ? OriginalSize * 0.5f: OriginalSize) - cam.orthographicSize) * Time.deltaTime * Speed;
    }

    public void LookingAt (Vector3 worldPos)
    {
        TargetPosition = worldPos - transform.forward * Distance;
        follow = true;
    }

    public void LookingAtDialogue ()
    {
        GetComponent<Camera>().orthographic = false;
        GetComponent<BlurOptimized>().enabled = true;
        transform.position = TargetPosition = new Vector3(-11.64f, 17.21f, -48.69f);
        transform.rotation = Quaternion.Euler(0.65f,27.69f,-0.93f);
        MainGameManager.Get.UI.SetActive(false);
        MainGameManager.Get.OrderHUD.gameObject.SetActive(false);
        follow = true;
    }

    public void Return ()
    {
        transform.rotation = OriginalRotation;
        GetComponent<Camera>().orthographic = true;
        GetComponent<BlurOptimized>().enabled = false;
        MainGameManager.Get.UI.SetActive(true);
        MainGameManager.Get.OrderHUD.gameObject.SetActive(true);
        follow = false;
    }
}
