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

    void OnMouseExit ()
    {
        renderer.material = Original_Material;
    }

    void OnMouseUp ()
    {
        if (MainGameManager.Get.Floor.IsEditMode)
        {
            MainGameManager.Get.DecoEditUI.SetActive(true);
        }
    }

    public bool IsFilled ()
    {
        if (transform.childCount != 0)
            return true;
        return false;
    }

    public GameObject AddItemToGrid (GameObject prefab)
    {
        GameObject Item = GameObject.Instantiate(prefab);
        Item.transform.SetParent(gameObject.transform);
        Item.transform.localPosition = new Vector3(0,gameObject.transform.lossyScale.y * 0.5f,0);
        return Item;
    }
}
