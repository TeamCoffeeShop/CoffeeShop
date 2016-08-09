using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreamingGradingLogic : MonoBehaviour
{
    public Component[] CreamDetectors;

    public int MinimumCreamCount = 10;

    public float Perfect = 100;
    public float Great = 90;
    public float Nice = 80;
    public float Poor = 60;

    private List<GameObject> CreamObjects = new List<GameObject>();

    void Update()
    {
        //Temporary method. Checks grade by pressing a key.
         if(Input.GetKeyDown(KeyCode.Return))
             CalculateScore();
    }

    void OnTriggerEnter(Collider Other)
    {
        //if the object is not in the list
        if (!CreamObjects.Contains(Other.gameObject))
            //add to the list
            CreamObjects.Add(Other.gameObject);
    }

     void OnTriggerExit(Collider Other)
     {
         //if the object is in the list
         if (CreamObjects.Contains(Other.gameObject))
             //remove it from the list
             CreamObjects.Remove(Other.gameObject);
     }

    public void CalculateScore()
    {
        //count total cream
        int TotalCream = CreamObjects.Count;
        int CorrectCream = 0;
        float CreamingScore = 0;

        //count cream that is inside the boundary
        for (int i = 0; i < CreamObjects.Count; ++i)
            if (CreamObjects[i].GetComponent<CreamLogic>().InsideDrawLine)
                ++CorrectCream;

        //Debug.Log(CorrectCream + " / " + TotalCream);

        //if there's not enough cream, make a penalty
        if (TotalCream < MinimumCreamCount)
            TotalCream = MinimumCreamCount;

        //calculate score
        if (TotalCream != 0)
        {
            CreamingScore = (float)CorrectCream / (float)TotalCream;
            CreamingScore *= 100;
            CreamingScore = (int)CreamingScore;
            Debug.Log("Result: " + CreamingScore + "/ 100");

            if (CreamingScore <= Poor)
                Debug.Log("POOR");
            else if (CreamingScore <= Nice)
                Debug.Log("NICE");
            else if (CreamingScore <= Great)
                Debug.Log("GREAT");
            else if (CreamingScore <= Perfect)
                Debug.Log("PERFECT");
        }
    }
}
