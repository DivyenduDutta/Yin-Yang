using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class HighScoreData
{
    public int levelIndex;
    public string levelName;
    public int[] highScores; //top 5

    public HighScoreData(string aLevelName)
    {
        levelIndex = ((int)System.Enum.Parse(typeof(GameConstants.LevelNames), aLevelName)) + 1;
        levelName = aLevelName;
        highScores = new int[5];
    }

    public HighScoreData(string aLevelName, int[] aHighScore)
    {
        levelIndex = ((int)System.Enum.Parse(typeof(GameConstants.LevelNames), aLevelName)) + 1;
        levelName = aLevelName;
        highScores = aHighScore;
    }
}
