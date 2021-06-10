using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveHighScoreData(string aLevelName, int[] aHighScore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/highscore_" + aLevelName + ".bin";

        //If file doesnt exist it will get created otherwise it will be overriden (truncate)
        FileStream stream = new FileStream(path, FileMode.Create);

        HighScoreData highScoreData = new HighScoreData(aLevelName, aHighScore);

        formatter.Serialize(stream, highScoreData);
        stream.Close();
    }

    public static HighScoreData LoadHighScoreData(string aLevelName)
    {
        string path = Application.persistentDataPath + "/highscore_" + aLevelName + ".bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);
            HighScoreData highScoreData = (HighScoreData)formatter.Deserialize(stream);
            stream.Close();
            return highScoreData;
        }
        else
        {
            Debug.LogErrorFormat("Save file not found in {0}", path);
            return null;
        }
    }
}
