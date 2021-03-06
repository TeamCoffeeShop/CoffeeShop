﻿using UnityEngine;
using System.Collections;

public class Customer : MonoBehaviour
{

    public CustomerData data = new CustomerData();

    public string _name = "Customer";
    public OrderType order = new OrderType();

    public void StoreData()
    {
        data.name = _name;
        Vector3 pos = transform.position;
        data.posx = pos.x;
        data.posy = pos.y;
        data.posz = pos.z;
        data.order = order;
    }
}

public class CustomerData
{
    //[XmlAttribute("Name")]
    public string name;

    //[XmlElement("PosX")]
    public float posx;

    //[XmlElement("PosY")]
    public float posy;

    //[XmlElement("PosZ")]
    public float posz;

    //[XmlElement("Order")]
    public OrderType order;
}