using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;

public class PlayerManager : MonoBehaviour {

    public Player player;

    //xp UI
    public BarScript bar;

    //money UI
    public GameObject money_text;

    public string PlayerDataFilePath; // Xml file

	// Use this for initialization
	void Start () {

        player = load_player("Assets/Resources/PlayerInfo/" + PlayerDataFilePath);
        var canvas = GameObject.Find("Canvas");

        money_text.GetComponent<Text>().text = player.money.ToString();
        
    }
	
	// Update is called once per frame
	void Update () {
        bar.Value = player.xp_currentVal;
        bar.MaxValue = player.xp_maxVal;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.xp_currentVal += 10;
            bar.Value = player.xp_currentVal;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.xp_currentVal -= 10;
            bar.Value = player.xp_currentVal;
        }
    }

    private static Player load_player(string path)
    {
        XmlSerializer serz = new XmlSerializer(typeof(Player));
        StreamReader reader = new StreamReader(path);

        Player player = (Player)serz.Deserialize(reader);
        return player;
    }

    private void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(Player));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }
}
