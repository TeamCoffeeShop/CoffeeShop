﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinishedOrderList : MonoBehaviour
{
    void Awake ()
    {
        //if duplicate, erase this
        if (GameObject.Find("[[Finished Orders]]") != null)
            DestroyImmediate(gameObject);

        //create saved orders
        //CreateOrdersInUI();
    }

    void Start ()
    {
        //change name to specify this object
        transform.name = "[[Finished Orders]]";

        //make this saved between scenes
        DontDestroyOnLoad(this);
    }

    public void OnLevelWasLoaded(int level)
    {
        //if main level, create lists
        if (level == Scenes.MainLevel)
            CreateOrdersInUI();
    }

    void CreateOrdersInUI()
    {
        //create cups as much as childrens
        int size = transform.childCount;
        float xPos = 0;
        for (int i = 0; i < size; ++i)
        {
            //create cup
            GameObject cup = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/cup"));
            cup.transform.SetParent(GameObject.Find("UI").transform, false);

            //set cup details here


            //set Transform (to not stack in one place)
            RectTransform rt = cup.GetComponent<RectTransform>();

            rt.Translate(xPos, 0, 0);
            xPos += (rt.localToWorldMatrix * rt.sizeDelta).x + 20;
        }
    }
}