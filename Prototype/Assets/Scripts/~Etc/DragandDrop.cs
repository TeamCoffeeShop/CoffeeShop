using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragandDrop : MonoBehaviour
{
    public bool active = true;
    public bool pullToCamera = true;
    public Vector2 Xbound = new Vector2(-225, -177);
    public Vector2 Ybound = new Vector2(0.5f, 6);
    public GameObject[] Target;
    public OutlineHighlighter[] Highlight;
    public float MoveSpeed = 10;

    private bool pActive;
    private int InTarget_Return = 0;
    private int InTarget = 0;
    private bool Grab = false;
    private bool EndOfGrab = false;
    private LayerMask Interactable;
    private Vector3 OriginalPosition;
    private bool FinishedReturning = false;

    GameObject arrowForGrinder;
    GameObject arrowForMachine;
    GameObject arrowForInstantiator;
    GameObject arrowForPlate;

    public int inTarget
    {
        get
        {
            return InTarget_Return;
        }
    }

    void OnMouseDown ()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Grab = true;
        GetComponent<Rigidbody>().isKinematic = true;

        if (active)
        {
            //highlight on targets
            foreach (OutlineHighlighter target in Highlight)
            {
                target.active = true;
            }
            foreach (GameObject obj in Target)
            {
                if (obj.transform.parent.name == "HandGrinder")
                {
                    arrowForGrinder = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefab/Arrow"));
                    arrowForGrinder.transform.position = MinigameManager.Get.handGrinder.transform.position;
                    arrowForGrinder.transform.Translate(new Vector3(0, 5, 0), Space.Self);
                }
                if (obj.transform.parent.name == "CoffeeMachine")
                {
                    arrowForMachine = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefab/Arrow"));
                    arrowForMachine.transform.position = MinigameManager.Get.coffeeMachine.transform.position;
                    arrowForMachine.transform.Translate(new Vector3(0, 6, 0), Space.Self);
                }

                if (obj.transform.parent.name == "Instantiator")
                {
                    arrowForInstantiator = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefab/Arrow"));
                    arrowForInstantiator.transform.position = MinigameManager.Get.instantiator.transform.position;
                    arrowForInstantiator.transform.Translate(new Vector3(0, 2, 0), Space.Self);
                }

                if (obj.transform.parent.name == "Plate")
                {
                    if (this.name == "Mug(Clone)" || this.name == "Standard" || this.name == "Cappucino")
                    {
                        arrowForPlate = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefab/Arrow"));
                        arrowForPlate.transform.position = MinigameManager.Get.plate.transform.position;
                        arrowForPlate.transform.Translate(new Vector3(0, 2, 0), Space.Self);
                    }
                }
            }
        }

    }

    void OnMouseDrag ()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (active)
        {
            if(pullToCamera)
            {
                Vector3 TargetPos = Camera.main.transform.position + Camera.main.transform.forward * 5 - Camera.main.transform.up * 2;

                transform.position += (TargetPos - transform.position) * Time.deltaTime * MoveSpeed;
            }
            else
            {
                //moving object according to mouse coordinate
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane p = new Plane(new Vector3(0, 0, -1), 3.51f);
                float d;
                p.Raycast(ray, out d);
                Vector3 curPosition = Camera.main.transform.position + ray.direction * d;

                //X bound
                if (curPosition.x < Xbound.x)
                    curPosition.x = Xbound.x;
                else if (curPosition.x > Xbound.y)
                    curPosition.x = Xbound.y;

                //Y bound
                if (curPosition.y < Ybound.x)
                    curPosition.y = Ybound.x;
                else if (curPosition.y > Ybound.y)
                    curPosition.y = Ybound.y;

                transform.position = curPosition;
            }
        }
    }

    void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Grab = false;
        EndOfGrab = true;

        if (arrowForGrinder)
            Destroy(arrowForGrinder);

        if (arrowForMachine)
            Destroy(arrowForMachine);

        if (arrowForInstantiator)
            Destroy(arrowForInstantiator);

        if (arrowForPlate)
            Destroy(arrowForPlate);

        if (active)
        {
            if (pullToCamera)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 20, Interactable))
                {
                    int i = 1;
                    foreach (GameObject obj in Target)
                    {
                        if (obj == hit.collider.gameObject)
                        {
                            InTarget = i;
                            break;
                        }
                        ++i;
                    }
                }
            }
            else
            {
                GetComponent<Rigidbody>().isKinematic = false;
            }

            if (InTarget != 0)
            {
                InTarget_Return = InTarget;
            }

            //highlight off targets
            foreach (OutlineHighlighter target in Highlight)
                target.active = false;       
        }   
    }

    void OnTriggerEnter(Collider col)
    {
        int i = 1;
        if(Grab && !pullToCamera)
            foreach (GameObject obj in Target)
            {
                if(obj == col.gameObject)
                {
                    InTarget = i;
                    break;
                }
                ++i;
            }
    }

    void OnTriggerExit(Collider col)
    {
        if (Grab && !pullToCamera)
            foreach (GameObject obj in Target)
            {
                if (obj == col.gameObject)
                {
                    InTarget = 0;
                    InTarget_Return = 0;
                    break;
                }
            }
    }

    void Start ()
    {
        Interactable = LayerMask.GetMask("Interactable");
        pActive = !active;
        OriginalPosition = transform.position;
    }

    void Update ()
    {
        if (active)
        {
            if (pullToCamera)
            {
                if (EndOfGrab)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Plane p = new Plane(new Vector3(0, 0, -1), 3.51f);
                    float d;
                    p.Raycast(ray, out d);
                    Vector3 curPosition = Camera.main.transform.position + ray.direction * d;

                    //X bound
                    if (curPosition.x < Xbound.x)
                        curPosition.x = Xbound.x;
                    else if (curPosition.x > Xbound.y)
                        curPosition.x = Xbound.y;

                    //Y bound
                    if (curPosition.y < Ybound.x)
                        curPosition.y = Ybound.x;
                    else if (curPosition.y > Ybound.y)
                        curPosition.y = Ybound.y;

                    OriginalPosition = curPosition;

                    EndOfGrab = false;
                    FinishedReturning = false;
                }

                else if (!Grab && !FinishedReturning)
                {
                    Vector3 Gap = OriginalPosition - transform.position;
                    transform.position += Gap * Time.deltaTime * MoveSpeed;

                    //if close enough, finish moving.
                    if (Gap.sqrMagnitude < 0.1f)
                    {
                        GetComponent<Rigidbody>().isKinematic = false;
                        FinishedReturning = true;
                    }
                }
            }
        }

        if (pActive != active)
        {
            pActive = active;

            InTarget = 0;
            InTarget_Return = 0;
            //Grab = false;
            FinishedReturning = true;
            //EndOfGrab = false;
        }
    }
}
