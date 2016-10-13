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

    private GameObject DialogueWindowPrefab; //Dialogue Panel Prefab

    private int selected_option = -2;

    // .txt file 
    public TextAsset textFile;

    // canvas
    public bool isActive;

    //npc to move.
    public Animator NPC;
    public RuntimeAnimatorController[] anim;

    private bool isTyping = false;
    private bool cancelTyping = false;

    public float typeSpeed;

	// Use this for initialization
	void Start ()
    {
        DialogueWindowPrefab = Resources.Load<GameObject>("Prefab/Dialogue_Prefab");
        MakeDialogueBox();
        string[] textLines;

        //If there is text file, get strings
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
            ClassifyDialogue(textLines);
        }

        //RunDialogue();
	}

    private void ClassifyDialogue(string[] texts)
    {
        int order = 0;
        for (int i = 0; i < texts.Length; ++i)
        {
           //int bracketIndex = texts[i].IndexOf("]");

            // Special action
            if (texts[i].Contains("["))
            {
                // Get function Name
                //string functionName = texts[i].Substring(1, bracketIndex - 1);
                //diaNode.mi = this.GetType().GetMethod(functionName);
            }
            else // Simple narration
            {
                if (texts[i].Contains("CHOICES:"))
                {
                    int nodeid = i - 1;
                    order = i;
                    for (int j = 0; j < 2; ++j)
                    {
                        i = i + 1;
                        int arrowIndex = texts[i].IndexOf(">");

                        DialogueOption option = new DialogueOption();
                        option.tempText = texts[i].Substring(arrowIndex + 1, texts[i].Length - 1);
                        option.destinationNodeID = order;

                        if (j == 0)
                        {
                            dia.Nodes[nodeid].Noption1 = option;
                        }
                        else
                            dia.Nodes[nodeid].Noption2 = option;

                    }
                }
                else
                {
                    // Make Node
                    DialogueNode diaNode = new DialogueNode();                   
                    diaNode.NodeID = order;
                    DialogueOption nextoption = new DialogueOption();
                    nextoption.destinationNodeID = order + 1;
                    nextoption.tempText = "NEXT";
                    diaNode.Noption1 = nextoption;
                    diaNode.Noption2 = null;
                    diaNode.tempText = texts[i];
                    dia.Nodes.Add(diaNode);
                    ++order;
                }
            }
        }
    }

    private void display_node(DialogueNode node)
    {
        StartCoroutine(TextScroll(node.tempText));
        
        option2.SetActive(false);

        if (node.Noption2 != null)
        {
            set_option_button(option1, node.Noption1);
            set_option_button(option2, node.Noption2);
        }
        else
        {
            set_option_button(option1, node.Noption1);
        }

    }

    private IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
        npc_text.GetComponent<Text>().text = "";
        isTyping = true;
        cancelTyping = false;

        // Type text one letter at a time by speed
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            npc_text.GetComponent<Text>().text += lineOfText[letter];
            ++letter;
            AkSoundEngine.PostEvent("Play_Talk", gameObject);
            yield return new WaitForSeconds(typeSpeed);
        }

        npc_text.GetComponent<Text>().text = lineOfText;
        isTyping = false;
        cancelTyping = false;
        //Debug.Log("End Text Scroll");
    }

    // Make dialogue box
    private void MakeDialogueBox()
    {
        dialogue_window = Instantiate<GameObject>(DialogueWindowPrefab);
        dialogue_window.transform.SetParent(MainGameManager.Get.Canvas_Dialogue.transform, false);

        RectTransform dia_window_transform = (RectTransform)dialogue_window.transform;
        dia_window_transform.anchoredPosition = new Vector3(0, 20, 0);
        dia_window_transform.offsetMin = new Vector2(40, dia_window_transform.offsetMin.y);
        dia_window_transform.offsetMax = new Vector2(-40, dia_window_transform.offsetMax.y);

        npc_text = dialogue_window.transform.FindChild("NPCDialogue_Text").gameObject;
        option1 = dialogue_window.transform.FindChild("Option1").gameObject;
        option2 = dialogue_window.transform.FindChild("Option2").gameObject;

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
        //Debug.Log("SetOption");
        //Debug.Log(opt.destinationNodeID);
        button.SetActive(true);
        button.GetComponentInChildren<Text>().text = opt.tempText;
        button.GetComponent<Button>().onClick.AddListener(delegate { SetSelectedOption(opt.destinationNodeID); cancelTyping = true; });

    }

    private IEnumerator run()
    {
        dialogue_window.SetActive(true);

        //create an indexer, set it to 0 - the start node
        int node_id = 0;

        //while the next node is not an exit node, traverse the dialogue tree based on user input
        while (node_id != dia.Nodes.Count)
        {
            display_node(dia.Nodes[node_id]);
            selected_option = -2;
            while (selected_option == -2)
            {
                yield return new WaitForSeconds(0.25f);
                        //if (Input.GetMouseButtonDown(0)/*Input.GetTouch*/)
            }
            node_id = selected_option;
            NPC.runtimeAnimatorController = anim[Random.Range(0,3)];
            NPC.Play(0);
        }
        // When there is no dialogue, disable dialogue box
        dialogue_window.SetActive(false);       
    }
}
