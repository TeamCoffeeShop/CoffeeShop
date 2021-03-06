﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;

public class PlayerManager : MonoBehaviour {

    public Player player = new Player();
    public float MoneyIncreasingSpeed = 10;
    float ExpectedMoney;

    public float income;

    public string PlayerDataFilePath; // Xml file

	// Use this for initialization
	void Start () {

            //player = load_player(PlayerDataFilePath);
        player.money = 1000;
            ExpectedMoney = player.money;
        player.xp_currentVal = 0;
        player.xp_maxVal = 100;
        player.level = 1;
        player.income = 0;

        PlayerPrefs.SetFloat("xp_currentVal", player.xp_currentVal);
        PlayerPrefs.SetFloat("xp_maxVal", player.xp_maxVal);
        PlayerPrefs.SetFloat("money", player.money);
        PlayerPrefs.SetFloat("income", player.income);
        PlayerPrefs.SetInt("level", player.level);     
    }
	
	// Update is called once per frame
	void Update () {

        if (player.xp_currentVal >= player.xp_maxVal)
            PlayerLevelUp();

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
        income += money;
    }

    public void AddMoneyGradually(float money)
    {
        ExpectedMoney = player.money + money;
        income += money;

    }
    public void SubtractMoneyGradully(float money)
    {
        ExpectedMoney = player.money - money;
        income -= money;
    }
    public void AddXP(float xp)
    {
        player.xp_currentVal += xp;
    }

    public void SubtractXP(float xp)
    {
    }

    void PlayerLevelUp()
    {
        player.xp_maxVal += 2 * player.xp_maxVal;
        player.xp_currentVal = 0;

        player.level += 1;

    }

    void MoneyUpdate()
    {
        float difference = ExpectedMoney - player.money;
        if (difference > 1)
            player.money += difference * Time.deltaTime * MoneyIncreasingSpeed;
        else
            player.money = ExpectedMoney;

    }

}
