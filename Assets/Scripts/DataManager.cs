using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //Saves the highscore
    public static void SaveHighScore (float Score)
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileStream = new FileStream(Application.persistentDataPath + "/HighScore.banana",FileMode.Create);

        HighScore data = new HighScore();
        data.Score = Score;

        bf.Serialize(fileStream, data);
        fileStream.Close();

    }
    //Loads the highscore.
    public static float LoadScore()
    {
        //Checks if the file exists.
        if (File.Exists (Application.persistentDataPath + "/HighScore.banana"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = new FileStream(Application.persistentDataPath + "/HighScore.banana", FileMode.Open);

            HighScore data = bf.Deserialize(fileStream) as HighScore;

            fileStream.Close();
            return data.Score;
        }else
        {
            return 0f;
        }

    }

}

[Serializable]
public class HighScore
{

    public float Score;

}

