using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Text
using System.Collections.Generic; //List

public class DialogueManager : MonoBehaviour {

    //Dialogue box
    private Dialogue dia = new Dialogue();
    private GameObject dialogue_window; //Dialogue Panel
    private GameObject npc_text; //NPC Dialogue
    private GameObject option1; //Option 1
    private GameObject option2; //Option 2

    public GameObject DialogueWindowPrefab; //Dialogue Panel Prefab

    private int selected_option = -2;

    // .txt file 
    public TextAsset textFile;

    public bool isActive;

    private bool isTyping = false;
    private bool cancelTyping = false;

    public float typeSpeed;

	// Use this for initialization
	void Start () {

        string[] textLines;

        //If there is text file, get strings
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
            ClassifyDialogue(textLines);
            Debug.Log("Classify");
        }

        Debug.Log("Start function");
	}
	
	// Update is called once per frame
	void Update () {

        ////##CHANGE## The key set enter button temporaily, it will change
        //if(Input.GetMouseButtonDown(0)/*Input.GetTouch*/)
        //{
        //    if (!isActive)
        //        EnableDialogueBox();


        //    else if( isTyping && !cancelTyping)
        //    {
        //        cancelTyping = true;

        //    }         
        //}

        //if (!isActive)
        //{
        //    return;
        //}
    }

    private void ClassifyDialogue(string[] texts)
    {
        int order = 0;
        for (int i = 0; i < texts.Length; ++i)
        {
            if ( texts[i].Contains("\n") == false)
            {
                DialogueNode diaNode = new DialogueNode();
                int bracketIndex = texts[i].IndexOf("]");
                //Clear dialogue Node
                diaNode.tempText = "";
                diaNode.NodeID = order;
                ++order;
                diaNode.mi = null;
                diaNode.options = null;

                // Special action
                if (texts[i].Contains("["))
                {
                    // Get function Name
                    string functionName = texts[i].Substring(1, bracketIndex - 1);
                    diaNode.mi = this.GetType().GetMethod(functionName);
                }
                else // Simple narration
                {
                    if (texts[i].Contains("CHOICES:"))
                    {
                        List<DialogueOption> options = new List<DialogueOption>();
                        int nodeid = i - 1;
                        for (int j = 0; j < 2; ++j)
                        {
                            int arrowIndex = texts[order + 1].IndexOf(">");

                            DialogueOption option = new DialogueOption();
                            option.tempText = texts[i + 1].Substring(arrowIndex + 1, texts[i + 1].Length - 1);
                            option.destinationNodeID = order + 1;
                            Debug.Log(option.tempText);

                            options.Add(option);
                            ++i;
                        }
                        dia.Nodes[nodeid].options = options;
                        Debug.Log(nodeid);
                    }
                    else
                    {
                        diaNode.tempText = texts[i];
                        dia.Nodes.Add(diaNode);
                        Debug.Log(diaNode.tempText);
                        Debug.Log(dia.Nodes.Count - 1);

                    }
                }
            }
        }
    }

    private void display_node(DialogueNode node)
    {
        StartCoroutine(TextScroll(npc_text.GetComponent<Text>().text));

        option1.SetActive(false);
        option2.SetActive(false);

        for (int i = 0; i < node.options.Count && i < 2; ++i)
        {
            switch (i)
            {
                case 0:
                    set_option_button(option1, node.options[i]);
                    break;
                case 1:
                    set_option_button(option2, node.options[i]);
                    break;
            }
        }

    }

    private IEnumerator TextScroll (string lineOfText)
    {
        int letter = 0;
        npc_text.GetComponent<Text>().text = "";
        isTyping = true;
        cancelTyping = false;

        // Type text one letter at a time by speed
        while(isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            npc_text.GetComponent<Text>().text += lineOfText[letter];
            ++letter;
            yield return new WaitForSeconds(typeSpeed);
        }

        npc_text.GetComponent<Text>().text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }

    public void EnableDialogueBox()
    {
        dialogue_window.SetActive(true);
    }

    public void DisableDialogueBox()
    {
        dialogue_window.SetActive(false);
    }

    // Make dialogue box
    private void MakeDialogueBox()
    {
        var canvas = GameObject.Find("Canvas");

        dialogue_window = Instantiate<GameObject>(DialogueWindowPrefab);
        dialogue_window.transform.SetParent(canvas.transform, false);

        RectTransform dia_window_transform = (RectTransform)dialogue_window.transform;
        dia_window_transform.anchoredPosition = new Vector3(0, 80, 0);
        dia_window_transform.offsetMin = new Vector2(80, dia_window_transform.offsetMin.y);
        dia_window_transform.offsetMax = new Vector2(-80, dia_window_transform.offsetMax.y);

        npc_text = GameObject.Find("NPCDialogue_Text");
        option1 = GameObject.Find("Option1");
        option2 = GameObject.Find("Option2");

        dialogue_window.SetActive(false);
    }

    public void RunDialogue()
    {
        StartCoroutine(run());
    }

    private void SetSelectedOption(int x)
    {
        selected_option = x;
    }

    private void set_option_button(GameObject button, DialogueOption opt)
    {
        button.SetActive(true);
        button.GetComponentInChildren<Text>().text = opt.tempText;
        button.GetComponent<Button>().onClick.AddListener(delegate { SetSelectedOption(opt.destinationNodeID); });

    }

    private IEnumerator run()
    {
        dialogue_window.SetActive(true);

        //create an indexer, set it to 0 - the start node
        int node_id = 0;

        //while the next node is not an exit node, traverse the dialogue tree based on user input
        while (node_id != -1)
        {
            display_node(dia.Nodes[node_id]);
            selected_option = -2;
            while (selected_option == -2)
            {
                if (dia.Nodes[node_id].options.Count == 0)
                {
                    if (Input.GetMouseButtonDown(0)/*Input.GetTouch*/)
                    {
                        selected_option = node_id + 1;
                        yield return null;
                    }
                }
                else
                    yield return null;
            }
            node_id = selected_option;
        }
        // When there is no dialogue, disable dialogue box
        dialogue_window.SetActive(false);
        
    }
}
