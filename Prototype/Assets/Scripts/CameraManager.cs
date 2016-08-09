﻿using UnityEngine;
using UnityEditor; //for assetdatabase
using System.Collections;

public class CameraManager : MonoBehaviour
{
    CameraLogic MainCamera;

    GameObject CoffeeCup1;
    GameObject CoffeeCup2;
    GameObject CoffeeCup3;

    public Vector3 NextPos;
    public Vector3 NextCoffeeCupPos;

    Vector3 StartPosition;
    Vector3 CoffeeMakingPosition;
    Vector3 CoffeeSpawnPosition;
    Vector3 DrawingPosition;

    //variable to access coffeeCup
    GameObject coffeeCup;

    void Awake()
    {
        CoffeeCup1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Prefab/CoffeeCup1.prefab");
        CoffeeCup2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Prefab/CoffeeCup2.prefab");
        CoffeeCup3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Prefab/CoffeeCup3.prefab");

        MainCamera = GameObject.Find("Main Camera").GetComponent<CameraLogic>();

        StartPosition = new Vector3(-18f, 13f, -10f);
        CoffeeMakingPosition = new Vector3(3f, 13f, -10f);
        CoffeeSpawnPosition = new Vector3(5f, 1f, -1f);
        DrawingPosition = new Vector3(24f, 13f, -10f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (coffeeCup == null)
        {
            coffeeCup = GameObject.FindGameObjectWithTag("CoffeeCup");
        }
    }

    void OnMouseDown()
    {
        if (gameObject.name == "Cup1")
        {
            MainCamera.TargetPosition = CoffeeMakingPosition;

            GameObject CoffeeCup = (GameObject)Instantiate(CoffeeCup1, CoffeeSpawnPosition, Quaternion.identity);
            CoffeeCup.name = "CoffeeCup1";
        }

        if (gameObject.name == "Cup2")
        {
            MainCamera.TargetPosition = CoffeeMakingPosition;

            GameObject CoffeeCup = (GameObject)Instantiate(CoffeeCup2, CoffeeSpawnPosition, Quaternion.identity);
            CoffeeCup.name = "CoffeeCup2";
        }

        if (gameObject.name == "Cup3")
        {
            MainCamera.TargetPosition = CoffeeMakingPosition;

            GameObject CoffeeCup = (GameObject)Instantiate(CoffeeCup3, CoffeeSpawnPosition, Quaternion.identity);
            CoffeeCup.name = "CoffeeCup3";
        }

        if (gameObject.name == "BacktoStart" )
        {
            MainCamera.TargetPosition = StartPosition;
        }

        if (gameObject.tag == "Next")
        {
            if (coffeeCup != null)
            {
                coffeeCup.transform.position = NextCoffeeCupPos;
            }

            MainCamera.TargetPosition = NextPos;
        }

        if (gameObject.tag == "Reset")
        {
            if (GameObject.FindGameObjectWithTag("CoffeeCup") != null)
            {
                Destroy(GameObject.FindGameObjectWithTag("CoffeeCup"));              
            }
            MainCamera.TargetPosition = StartPosition;
        }
    }
}
