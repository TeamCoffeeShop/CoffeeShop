﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CustomerSpawnTimer : MonoBehaviour {

    public Transform customer;

    // Use this for initialization
    void Start()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        //follow link position
        if (customer != null)
        {
            UIEffect.WorldToCanvas(transform.parent.gameObject, customer.transform.position + new Vector3(0, -1, 0), GetComponent<RectTransform>());

        }
    }
}
