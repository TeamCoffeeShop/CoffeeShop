using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;

public class PlayerManager : MonoBehaviour {

    public Player player;
    public float MoneyIncreasingSpeed = 10;
    float ExpectedMoney;

    //xp UI
    public BarScript bar;

    //money UI
    public GameObject money_text;

    public string PlayerDataFilePath; // Xml file

	// Use this for initialization
	void Start () {

        player = load_player("Assets/Resources/Xmls/" + PlayerDataFilePath);

        ExpectedMoney = player.money;
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

        MoneyUpdate();
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

    public void AddMoneyInstantly(float money)
    {
        player.money += money;
        ExpectedMoney = player.money;
    }

    public void AddMoneyGradually(float money)
    {
        ExpectedMoney = player.money + money;
    }

    void MoneyUpdate ()
    {
        float difference = ExpectedMoney - player.money;
        if (difference > 1)
            player.money += difference * Time.deltaTime * MoneyIncreasingSpeed;
        else
            player.money = ExpectedMoney;
        money_text.GetComponent<Text>().text = player.money.ToString("N0");
    }
}
