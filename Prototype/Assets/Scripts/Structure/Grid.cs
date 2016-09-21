using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    public Material Selected_Material;
           Material Original_Material;
           Renderer renderer;
    void Awake ()
    {
        renderer = GetComponent<Renderer>();
        Original_Material = renderer.material;
    }

    void OnMouseEnter()
    {
        if(MainGameManager.Get.Floor.IsEditMode)
            renderer.material = Selected_Material;
    }

    void OnMouseExit()
    {
        renderer.material = Original_Material;
    }
}
