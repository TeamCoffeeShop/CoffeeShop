using UnityEngine;
using System.Collections;

public class DragandDrop : MonoBehaviour
{
    public bool active = true;
    public bool pullToCamera = true;
    public Vector2 Xbound = new Vector2(-225, -177);
    public Vector2 Ybound = new Vector2(0.5f, 6);
    public GameObject[] Target;
    public OutlineHighlighter[] Highlight;

    private bool pActive;
    private int InTarget_Return = 0;
    private int InTarget = 0;
    private bool Grab = false;
    private bool EndOfGrab = false;
    private LayerMask Interactable;
    private Vector3 OriginalPosition;
    private float MoveSpeed = 10;
    private bool FinishedReturning = false;

    public GameObject arrow;

    public int inTarget
    {
        get
        {
            return InTarget_Return;
        }
    }

    void OnMouseDown ()
    {
        Grab = true;
        GetComponent<Rigidbody>().isKinematic = true;

        if (active)
        {
            //highlight on targets
            foreach (OutlineHighlighter target in Highlight)
            {
                target.active = true;
                if (target.name == "HandGrinder")
                {
                    arrow.GetComponent<Renderer>().enabled = false;
                }
            }
        }
    }

    void OnMouseDrag ()
    {
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
        Grab = false;
        EndOfGrab = true;

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
