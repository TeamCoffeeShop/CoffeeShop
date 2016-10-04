using UnityEngine;
using System.Collections;

public class OutlineHighlighter : MonoBehaviour
{
    public enum HighlightOn
    {
        none, mouseOver, always, alwaysAndOver
    }

    public HighlightOn highlightOn = HighlightOn.mouseOver;
    HighlightOn phighlightOn;
    public bool active = true;
    Renderer R;

    void Awake ()
    {
        phighlightOn = highlightOn;
        R = GetComponent<Renderer>();
    }

    void Start ()
    {
        if(highlightOn == HighlightOn.alwaysAndOver)
            R.material.SetColor("_OutlineColor", Color.green);
    }

    void OnMouseEnter ()
    {
        if (highlightOn == HighlightOn.mouseOver)
            R.material.SetColor("_OutlineColor", Color.green);
        else if(highlightOn == HighlightOn.alwaysAndOver)
            R.material.SetColor("_OutlineColor", Color.yellow);
    }

    void OnMouseExit ()
    {
        if (highlightOn == HighlightOn.mouseOver)
            R.material.SetColor("_OutlineColor", Color.black);
        else if (highlightOn == HighlightOn.alwaysAndOver)
            R.material.SetColor("_OutlineColor", Color.green);
    }

    void Update ()
    {
        if(highlightOn != phighlightOn)
        {
            phighlightOn = highlightOn;

            if(highlightOn == HighlightOn.none || highlightOn == HighlightOn.mouseOver)
                R.material.SetColor("_OutlineColor", Color.black);
            else if(highlightOn == HighlightOn.always || highlightOn == HighlightOn.alwaysAndOver)
                R.material.SetColor("_OutlineColor", Color.green);
        }
    }
}
