using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.SceneManagement;

public class BiancaDialogue : MonoBehaviour
{
    private Dialogue dia;
    private GameObject dialogue_window; //Dialogue Panel
    private GameObject npc_text; //NPC Dialogue
    private GameObject option1; //Option 1
    private GameObject option2; //Option 2

    private GameObject gamemanager;

    private int selected_option = -2;

    public string DialoguePath; //Xml file

    public GameObject DialogueWindowPrefab; //Dialogue Panel Prefab

    //Use this for initialization
    void Start()
    {
        gamemanager = GameObject.Find("MainGameManager");

        dia = load_dialogue("Assets/Resources/Xmls/" + DialoguePath);

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

    public void SetSelectedOption(int x)
    {
        selected_option = x;
    }

    public IEnumerator run()
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
                //gamemanager.GetComponent<MainGameManager>().OnDialogue = false;
                yield return null;
            }
            node_id = selected_option;
        }

        dialogue_window.SetActive(false);
        gamemanager.GetComponent<MainGameManager>().OnDialogue = false;
    }

    private void display_node(DialogueNode node)
    {
        npc_text.GetComponent<Text>().text = node.tempText;

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

    private void set_option_button(GameObject button, DialogueOption opt)
    {
        button.SetActive(true);
        button.GetComponentInChildren<Text>().text = opt.tempText;
        button.GetComponent<Button>().onClick.AddListener(delegate { SetSelectedOption(opt.destinationNodeID); });

    }

    private static Dialogue load_dialogue(string path)
    {
        XmlSerializer serz = new XmlSerializer(typeof(Dialogue));
        StreamReader reader = new StreamReader(path);

        Dialogue dia = (Dialogue)serz.Deserialize(reader);
        return dia;
    }

    void OnMouseDown()
    {
        gamemanager.GetComponent<MainGameManager>().OnDialogue = true;
        RunDialogue();
    }
}