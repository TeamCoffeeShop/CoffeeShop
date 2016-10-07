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
    bool pactive;
    Renderer R;

    void Awake ()
    {
        phighlightOn = highlightOn;
        R = GetComponent<Renderer>();
        pactive = !active;
    }

    void Start ()
    {
        if(highlightOn == HighlightOn.alwaysAndOver)
            R.material.SetColor("_OutlineColor", Color.green);
    }

    void OnMouseEnter ()
    {
        if(active)
            if (highlightOn == HighlightOn.mouseOver)
                R.material.SetColor("_OutlineColor", Color.yellow);
            else if(highlightOn == HighlightOn.alwaysAndOver)
                R.material.SetColor("_OutlineColor", Color.green);
    }

    void OnMouseExit ()
    {
        if(active)
            if (highlightOn == HighlightOn.mouseOver)
                R.material.SetColor("_OutlineColor", Color.black);
            else if (highlightOn == HighlightOn.alwaysAndOver)
                R.material.SetColor("_OutlineColor", Color.yellow);
    }

    void Update ()
    {
        if(active)
            if (highlightOn != phighlightOn)
            {
                phighlightOn = highlightOn;

                if (highlightOn == HighlightOn.none || highlightOn == HighlightOn.mouseOver)
                    R.material.SetColor("_OutlineColor", Color.black);
                else if (highlightOn == HighlightOn.always || highlightOn == HighlightOn.alwaysAndOver)
                    R.material.SetColor("_OutlineColor", Color.yellow);
            }

        if (active != pactive)
        {
            pactive = active;

            if(pactive)
            {
                if (highlightOn == HighlightOn.none || highlightOn == HighlightOn.mouseOver)
                    R.material.SetColor("_OutlineColor", Color.black);
                else if (highlightOn == HighlightOn.always || highlightOn == HighlightOn.alwaysAndOver)
                    R.material.SetColor("_OutlineColor", Color.yellow);
            }
            else
                R.material.SetColor("_OutlineColor", Color.black);
        }
            
    }
}
