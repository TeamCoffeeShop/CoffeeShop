using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

public class CustomerSaveLoad : MonoBehaviour
{
    public GameObject customerList;

    public static CustomerContainer customerContainer = new CustomerContainer();

    public delegate void SerializaAction();
    public static event SerializaAction OnLoaded;
    public static event SerializaAction OnBeforeSave;



    public void Load(string path)
    {
        //customerContainer = LoadCustomers(path);

        //foreach(CustomerData data in customerContainer.customers)
        //{
        //    CustomerSystem.CreateCustomer(data, CustomerSystem.customerPath,
        //        new Vector3(data.posx, data.posy, data.posz), Quaternion.identity);
        //}

        OnLoaded();

    }

    public void save(string path, CustomerContainer customers)
    {
        OnBeforeSave();

        DontDestroyOnLoad(customerList);
        //SaveCustomers(path, customers);

        ClearCustomers();
    }

    public static void AddCustomerData(CustomerData data)
    {
        customerContainer.customers.Add(data);
    }

    public static void ClearCustomers()
    {
        customerContainer.customers.Clear();
    }

    //private static CustomerContainer LoadCustomers(string path)
    //{
    //    XmlSerializer serializer = new XmlSerializer(typeof(CustomerContainer));

    //    FileStream stream = new FileStream(path, FileMode.Open);

    //    CustomerContainer customers = serializer.Deserialize(stream) as CustomerContainer;

    //    stream.Close();

    //    return customers;
    //}

    //private static void SaveCustomers(string path, CustomerContainer customers)
    //{
    //    XmlSerializer serializer = new XmlSerializer(typeof(CustomerContainer));

    //    FileStream stream = new FileStream(path, FileMode.Truncate);

    //    serializer.Serialize(stream, customers);

    //    stream.Close();
    //}

}
