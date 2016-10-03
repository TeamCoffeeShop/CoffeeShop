using UnityEngine;
using System.Collections;
using System.Collections.Generic; // for list
using UnityEngine.UI;


public class HandGrinderScript : MonoBehaviour
{
    //for object rotation following mouse
    ////////////////////////////////////
    ////////////////////////////////////
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    ////////////////////////////////////
    ////////////////////////////////////

    public BarScript coffeeBar;
    //bool type for rotation
    bool CheckGrind = false;

    //bool type for checking coffee grinder game
    bool CheckGameStop = false;

    //UI image for rotation
    public Image rotationImage;
    public List<CoffeeBean> CoffeeBeans = new List<CoffeeBean>();

    //bool type for rotation check
    Vector3 oldEulerAngles;
    //how much grinder's rotated
    float totalRotation;
    //standard degree for coffee bean grind check
    public float stanDegree;
    //standard rotation for coffee powder to be made
    public float stanRotation;

    //coffee powder variables
    public GameObject CoffeePowder1;
    public GameObject CoffeePowder2;

    private bool CameraRotateCheck;
    //rotation degree function variables
    ////////////////////////////////////
    ////////////////////////////////////
    /*Vector3 mouseClickPos;
    Vector3 dir;
    float angle;
    float lastAngle = 0;
    int fullRotations = 0;
    float rotationChecker;*/
    ////////////////////////////////////
    ////////////////////////////////////

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        playerRigidbody = GetComponent<Rigidbody>();

        oldEulerAngles = transform.GetChild(0).rotation.eulerAngles;
        coffeeBar.Value = totalRotation;
        coffeeBar.MaxValue = stanRotation;
        coffeeBar.GetComponent<Image>().enabled = false;
        rotationImage.enabled = false;
    }

    void FixedUpdate()
    {
        RotationCheck();

        //track down the hand grinder rotation based on the mouse position
        ////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////
        /*mouseClickPos = Input.mousePosition;
        dir = Vector3.Normalize(mouseClickPos - Camera.main.WorldToScreenPoint(transform.position));
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= 90;
        if (lastAngle - angle > 270)
        {
            fullRotations++;
        }
        else if (angle - lastAngle > 270)
        {
            fullRotations--;
        }
 
        rotationChecker = 360 * fullRotations + angle;
        
        if(rotationChecker < 0)
        {
            rotationChecker = Mathf.Abs(rotationChecker);
            rotationChecker = 360 - rotationChecker;
        }
        if (rotationChecker > 360)
        {
            fullRotations = 0;
            rotationChecker = 0;
        }*/
        ////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////

        if(CheckGameStop)//if (totalRotation > stanRotation)
        {
            Camera.main.GetComponent<CameraLogic>().TargetPosition = Camera.main.GetComponent<CameraLogic>().PreviousPosition;
            Camera.main.transform.Rotate(-90, 0, 0);

            coffeeBar.gameObject.SetActive(false);

            if (CoffeeBeans[0].CBean == 1)
            {
                GameObject coffeepowder1 = (GameObject)Instantiate(CoffeePowder1, transform.position, Quaternion.identity);
                coffeepowder1.name = "CoffeePowder1";
            }

            if (CoffeeBeans[0].CBean == 2)
            {
                GameObject coffeepowder2 = (GameObject)Instantiate(CoffeePowder2, transform.position, Quaternion.identity);
                coffeepowder2.name = "CoffeePowder2";
            }

            totalRotation = 0;
            CoffeeBeans.Clear();
            CheckGameStop = false;
        }

        if (CheckGrind == true)
        {
            //GrindMotion();
            NewGrindMotion();
        }        
    }

    void RotationCheck()
    {
        //when the list is not empty
        if (CoffeeBeans.Count != 0)
        {
            if (CameraRotateCheck == false)
            {
                //Rotate Camera
                Camera.main.GetComponent<CameraLogic>().PreviousPosition = Camera.main.GetComponent<CameraLogic>().TargetPosition;
                Camera.main.GetComponent<CameraLogic>().TargetPosition = new Vector3(-6, 60, 5);
                Camera.main.transform.Rotate(90, 0, 0);
                coffeeBar.GetComponent<Image>().enabled = true;
                CameraRotateCheck = true;
            }
            //check what kind of coffee bean is in the grinder,
            //make coffee powder using the coffee bean
            if (CoffeeBeans[0].Check == true)
            {
                // Save coffee content
                coffeeBar.Value = totalRotation;
                coffeeBar.MaxValue = stanRotation;
                CoffeeBeans[0].coffeecontent = (int)totalRotation;

                if (oldEulerAngles != transform.GetChild(0).rotation.eulerAngles)
               {
                    //player should rotate at least certain degree to grind the coffee bean
                   if (Mathf.Abs(transform.GetChild(0).rotation.eulerAngles.y - oldEulerAngles.y) >= stanDegree)
                    {
                        oldEulerAngles = transform.GetChild(0).rotation.eulerAngles;
                        totalRotation += 1;
                    }
                   
               }
            }

        }
    }

    void OnMouseDown()
    {
        CheckGrind = true;
        rotationImage.enabled = true;
        //Cursor.visible = false;
    }

    void OnMouseUp()
    {
        CheckGrind = false;
        CheckGameStop = true;
        rotationImage.enabled = false;
        //Cursor.visible = true;
    }

    //for coffee grinding motion
    void GrindMotion()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    Vector3 MousePos;

    void NewGrindMotion ()
    {
        Vector3 GrinderPos = Camera.main.WorldToScreenPoint(transform.GetChild(0).transform.position + new Vector3(0,3,0));

        float angle = Mathf.Rad2Deg * GetAngleInRadian(new Vector2(MousePos.x, MousePos.y), new Vector2(GrinderPos.x, GrinderPos.y), new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        transform.GetChild(0).Rotate(0, -angle, 0);

        MousePos = Input.mousePosition;
    }

    public static float GetAngleInRadian(Vector2 vertex1, Vector2 pivotpoint, Vector2 vertex2)
    {
        Vector2 Edge1 = (vertex1 - pivotpoint);
        Vector2 Edge2 = (vertex2 - pivotpoint);

        float angle1 = Mathf.Atan2(Edge1.y, Edge1.x);
        float angle2 = Mathf.Atan2(Edge2.y, Edge2.x);

        return angle2 - angle1;
    }
}
