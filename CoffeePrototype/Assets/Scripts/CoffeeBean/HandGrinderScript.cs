﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic; // for list

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

    //bool type for rotation
    bool CheckGrind = false;

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

        oldEulerAngles = transform.rotation.eulerAngles;
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

        if (totalRotation > stanRotation)
        {
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
        }

        if (CheckGrind == true)
        {
            GrindMotion();
        }        
    }

    void RotationCheck()
    {
        //when the list is not empty
        if (CoffeeBeans.Count != 0)
        {
            //check what kind of coffee bean is in the grinder,
            //make coffee powder using the coffee bean
            if (CoffeeBeans[0].Check == true)
            {
               if (oldEulerAngles != transform.rotation.eulerAngles)
               {
                    //player should rotate at least certain degree to grind the coffee bean
                    if(Mathf.Abs(transform.rotation.eulerAngles.y - oldEulerAngles.y) >= stanDegree)
                    {
                        oldEulerAngles = transform.rotation.eulerAngles;
                        totalRotation += 1;
                    }
                   
               }
            }

        }
    }

    void OnMouseDown()
    {
        CheckGrind = true;
        Cursor.visible = false;
    }

    void OnMouseUp()
    {
        CheckGrind = false;
        Cursor.visible = true;
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
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }
}