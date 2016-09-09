using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Center
{
    middle, left, right
}

public class MiniDialogue : MonoBehaviour
{
    public GameObject TextBox;

    public RectTransform CreateMiniDialogue ()
    {
        GameObject miniDialogue = GameObject.Instantiate(TextBox);
        miniDialogue.transform.SetParent(gameObject.transform);
        RectTransform rt = miniDialogue.GetComponent<RectTransform>();
        rt.localPosition = new Vector3(0, 0, 0);
        rt.localScale = new Vector3(1, 1, 1);

        return rt;
    }

    public RectTransform CreateMiniDialogue(string text, Vector2 scale)
    {
        RectTransform rt = CreateMiniDialogue();
        rt.GetChild(0).GetComponent<Text>().text = text;
        rt.sizeDelta = scale;

        return rt;
    }

    public RectTransform CreateMiniDialogue (string text, Vector2 scale, Vector3 worldposition, Center center = Center.middle)
    {
        RectTransform rt = CreateMiniDialogue(text, scale);
        UIEffect.WorldToCanvas(gameObject, worldposition, rt);

        switch (center)
        {
            case Center.left:
                rt.localPosition += new Vector3(rt.sizeDelta.x * 0.5f,0,0);
                break;
            case Center.right:
                rt.localPosition -= new Vector3(rt.sizeDelta.x * 0.5f,0,0);
                break;
        }
        return rt;
    }
}