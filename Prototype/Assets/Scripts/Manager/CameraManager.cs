using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public GameObject CoffeeCup;
    public CoffeeCupType CoffeeCupType;
    public Vector3 NextPos;
    public Vector3 NextCoffeeCupPos;

    CameraLogic MainCamera;
    Vector3 CoffeeMakingPosition;
    Vector3 CoffeeSpawnPosition;

    //variable to access coffeeCup
    GameObject coffeeCup;

    void Awake()
    {
        //if none is selected, load default cup
        if(CoffeeCup == null)
           CoffeeCup = Resources.Load<GameObject>("Prefab/CoffeeCup");

        MainCamera = GameObject.Find("Main Camera").GetComponent<CameraLogic>();

        CoffeeMakingPosition = new Vector3(3f, 13f, -10f);
        CoffeeSpawnPosition = new Vector3(3f, 1f, 1f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Check if there is a coffeecup selected in the level
        if (coffeeCup == null)
        {
            coffeeCup = GameObject.FindGameObjectWithTag("CoffeeCup");
        }
    }

    void OnMouseDown()
    {
        //cup selection
        MainCamera.TargetPosition = CoffeeMakingPosition;
        coffeeCup = Instantiate(CoffeeCup, CoffeeSpawnPosition, Quaternion.identity) as GameObject;
        coffeeCup.name = "CoffeeCup";
        coffeeCup.GetComponent<CoffeeCupBehavior>().type = CoffeeCupType;
        CoffeeBehaviourSetup.SetCoffeeCup(ref coffeeCup);
        
        if (gameObject.tag == "Next")
        {
            coffeeCup.transform.position = NextCoffeeCupPos;
            MainCamera.TargetPosition = NextPos;
        }

        //when the player clicks the reset icon
        if (gameObject.tag == "Reset")
        {
            GameObject.Find("ResetManager").GetComponent<ResetManager>().Reset();
        }
    }
}
