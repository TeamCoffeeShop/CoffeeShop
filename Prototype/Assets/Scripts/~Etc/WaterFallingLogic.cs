using UnityEngine;
using System.Collections;

public class WaterFallingLogic : MonoBehaviour
{
    public float fallingSpeed = 10;
    public bool filling = true;
    public float MaxY = 0;

	void Update ()
    {
        if(filling)
        {
            transform.localScale += new Vector3(0, fallingSpeed * Time.deltaTime, 0);
            transform.Translate(0, -fallingSpeed * Time.deltaTime * 0.5f, 0);
        }
        else
        {
            transform.Translate(0, -fallingSpeed * Time.deltaTime, 0);
        }

        if (transform.position.y + transform.localScale.y * 0.5f < MaxY)
            DestroyObject(gameObject);
	}
}
