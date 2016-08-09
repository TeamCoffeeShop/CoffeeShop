using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot("CutomerCollection")]
public class CustomerContainer{

    [XmlArray("Customer")]
    [XmlArrayItem("Customer")]
    public List<CustomerData> customers = new List<CustomerData>();
}
