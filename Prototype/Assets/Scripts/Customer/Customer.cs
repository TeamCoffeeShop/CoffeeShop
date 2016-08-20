using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Customer : MonoBehaviour {

    public CustomerData data = new CustomerData();

    public string _name;

    public Coffee order = new Coffee();

    public void StoreData()
    {
        data.name = _name;
        Vector3 pos = transform.position;
        data.posx = pos.x;
        data.posy = pos.y;
        data.posz = pos.z;
        data.order = order;   
    }

    public void LoadData()
    {
        _name = data.name;
        transform.position = new Vector3(data.posx, data.posy, data.posz);
        order = data.order;
    }

    void OnEnable()
    {
        CustomerSaveLoad.OnLoaded += delegate { LoadData(); };
        CustomerSaveLoad.OnBeforeSave += delegate { StoreData(); };
        CustomerSaveLoad.OnBeforeSave += delegate { CustomerSaveLoad.AddCustomerData(data); };
    }

    void OnDisable()
    {
        CustomerSaveLoad.OnLoaded -= delegate { LoadData(); };
        CustomerSaveLoad.OnBeforeSave -= delegate { StoreData(); };
        CustomerSaveLoad.OnBeforeSave -= delegate { CustomerSaveLoad.AddCustomerData(data); };
    }
}

public class CustomerData
{
    [XmlAttribute("Name")]
    public string name;

    [XmlElement("PosX")]
    public float posx;

    [XmlElement("PosY")]
    public float posy;

    [XmlElement("PosZ")]
    public float posz;

    [XmlElement("Order")]
    public Coffee order;
}