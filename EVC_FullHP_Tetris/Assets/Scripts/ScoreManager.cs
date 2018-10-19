using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public int[] scores = new int[10];
    public string[] names = new string[10];
    TextAsset rankList;
    string rankstring;
    public Text scoreTable;
    string playerName = "Eugene";
    int myPlace = 11;
    int storedScore = 0;


	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("RankList") == false) // если списка рекордов еще нет, то загружается дефолтный из текстового файла
        {
            rankList = (TextAsset)Resources.Load("Data/rankings");
            rankstring = rankList.text;
            PlayerPrefs.SetString("RankList", rankstring);
        }
        ReadScores();
        SetScoreTable();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Записываем счёт и имена топ-10 рекордсменов в соответствующие массивы из PlayerPrefs
    void ReadScores()
    {
        rankstring = PlayerPrefs.GetString("RankList");
        int numOfRec = 0;
        string memory = "";
        for (int i = 0; i < 9000; i++)
        {
            if (rankstring[i] != '(' && rankstring [i] != ')' && rankstring [i] != ';') memory += rankstring[i];
            if (rankstring[i] == '(')
            {
                memory = "";
            }
            if (rankstring[i] == ')')
            {
                int x = int.Parse(memory);
                scores[numOfRec] = x;
                memory = "";
            }
            if (rankstring[i] == ';')
            {
                names[numOfRec] = memory;
                memory = "";
                numOfRec++;
            }
            if (numOfRec > 9) break;
        }
    }

    public void WriteScores()
    {
        string extractRankList = "";
        for (int i = 0; i < 10; i++)
        {
            extractRankList += "(" + scores[i] + ")" + names[i] + "; ";
        }
        PlayerPrefs.SetString("RankList", extractRankList);
        SetScoreTable();
    }

    public void SetScoreTable()
    {
        string extractRankList = "";
        for (int i = 0; i < 10; i++)
        {
            extractRankList += i+1 + "." + names[i] + " - " + scores[i] + "\n";
        }
        scoreTable.text = extractRankList;
    }

    public void CompareMyScore(int myScore)
    {
        for (int i = 9; i >= 0; i--)
        {
            if (myScore <= scores[i]) break;
            myPlace = i + 1;
        }
        Debug.Log("Mesto - " + myPlace);
        if (myPlace <= 10) // игрок попал в топ-10
        {
            storedScore = myScore;
            GameManager.GM.nameEntering.enabled = true;
        }
    }

    public void NameInserting(string name)
    { 
            for (int i = 9; i > myPlace - 1; i--)
            {
                scores[i] = scores[i - 1];
                names[i] = names[i - 1];
            }
            scores[myPlace - 1] = storedScore;
            names[myPlace - 1] = name;
            WriteScores();
            myPlace = 11;
    }
}
