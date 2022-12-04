using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
class ScorePairCollection
{
    public ScorePair[] scorePairs;
}
[Serializable]
class ScorePair
{
    public string name;
    public string score;
}

public class LeaderboardViewer : MonoBehaviour
{
    public TextMeshProUGUI names1, scores1, names2, scores2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScores());
    }

    IEnumerator LoadScores()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Get("http://140.238.87.144:5000/get"))
        {
            www.timeout = 5;
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                names1.text = "Failed to connect to server";
            }
            else
            {
                ScorePairCollection jsonData = JsonUtility.FromJson<ScorePairCollection>(www.downloadHandler.text);
                ScorePair[] leaderboard = jsonData.scorePairs;

                names1.text = "";

                for (int i = 0; i < leaderboard.Length; i++)
                {
                    if (i<5)
                    {
                        names1.text += leaderboard[i].name + "\n";
                        scores1.text += leaderboard[i].score + "\n";
                    }
                    else
                    {
                        names2.text += leaderboard[i].name + "\n";
                        scores2.text += leaderboard[i].score + "\n";
                    }
                }
            }
        }
    }
}
