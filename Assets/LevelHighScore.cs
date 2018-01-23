using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelHighScore : MonoBehaviour {

    public int maxScoresOnBoard = 10;

    public Text scoreBoard;

    private List<int> highScores;
    private string saveFilePath;



    private void Start()
    {
        saveFilePath = Application.persistentDataPath +
                       "/" + SceneManager.GetActiveScene().name + ".save";
        LoadScores();
    }

    public void AddHighScore(int score)
    {
        highScores.Add(score);
    }

    public void UpdateScoreboard()
    {
        scoreBoard.text = "HIGH SCORES\n";

        List<int> scores = new List<int>(highScores);
        int[] highs = new int[maxScoresOnBoard];
        int max = 0;
        int idx;
        for(int n = 0; n < maxScoresOnBoard; ++n)
        {
            max = scores[0];
            idx = 0;
            for(int i = 0; i < scores.Count; ++i)
            {
                if(max < scores[i])
                {
                    max = scores[i];
                    idx = i;
                }
            }
            highs[n] = max;
            scores[idx] = int.MinValue;
        }

        for(int i = 0; i < highs.Length; ++i)
        {
            scoreBoard.text = scoreBoard.text + highs[i] + "\n";
        }
    }

    public void LoadScores()
    {
        if(File.Exists(saveFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveFilePath, FileMode.Open);
            highScores = (List<int>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            highScores = new List<int>();
        }

        UpdateScoreboard();
    }

    public void SaveScores()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath);
        bf.Serialize(file, highScores);
        file.Close();
    }

}
