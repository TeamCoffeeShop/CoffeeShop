using UnityEngine;
using UnityEditor;
using System.Collections;

public class WhippingCream : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    float DragHeight = 2;

    public Vector3 StandPosition;
    public float ResetSpeed = 3;
    public Vector3 CreamStartPosition;
    public Vector3 CreamStartVelocity;
    public float CreamDelay = 0.1f;
    private float CurrentCreamDelay = 0;

    CameraLogic MainCamera;
    ParticleSystem CreamEmitter;
    bool Grab = false;
    bool Spray = false;
    GameObject Cream;

    void Awake()
    {
        MainCamera = GameObject.Find("Main Camera").GetComponent<CameraLogic>();
        CreamEmitter = gameObject.GetComponent<ParticleSystem>();
        Cream = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Prefab/Cream.prefab");
        CreamEmitter.Stop();
    }

    void Update()
    {
        if(Grab)
        {
            //for drag and drop
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            curPosition.y = DragHeight;
            transform.position = curPosition;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
            //press right click to cancel
            if (Input.GetMouseButtonDown(1))
            {
                Grab = false;
                Cursor.visible = true;

                //reset camera zoom
                //MainCamera.ResetPosition();
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CreamEmitter.Play();
                    Spray = true;
                }
                else if(Input.GetMouseButtonUp(0))
                {
                    CreamEmitter.Stop();
                    Spray = false;
                    GameObject.Find("Creaming Area").GetComponent<CreamingGradingLogic>().CalculateScore();

                    CurrentCreamDelay = 0;
                }

                //spray cream
                if(Spray)
                {
                    CurrentCreamDelay += Time.deltaTime;

                    if (CurrentCreamDelay > CreamDelay)
                    {
                        GameObject cream = (GameObject)Instantiate(Cream, transform.position + CreamStartPosition, Quaternion.identity);
                        cream.name = "cream";
                        //cream.rigidbody.

                        CurrentCreamDelay = 0;
                    }
                }
            }
        }
        else
        {
            //return to where it belongs.
            gameObject.transform.position += ((StandPosition - gameObject.transform.position) * ResetSpeed * Time.deltaTime);
        }
    }

    void OnMouseUp()
    {
        if (!Grab)
        {
            //grab check
            Grab = true;
            //cursor invisible
            Cursor.visible = !Grab;

            //camera zoom in
            //Vector3 targetPos = new Vector3(7.1f, 11.4f, -11.47f);
            //MainCamera.TargetPosition = targetPos * 0.7f;

            //for drag and drop
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }        
    }
}
