using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OrderingBallonLogic : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene(3);
    } 
}
