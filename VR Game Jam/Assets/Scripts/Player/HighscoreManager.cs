using UnityEngine;

public static class HighscoreManager
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="minigame"></param>
    /// <param name="highscore"></param>
    public static void SetHighscore(string minigame, float highscore)
    {
        PlayerPrefs.SetFloat(minigame, highscore);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="minigame"></param>
    /// <returns></returns>
    public static float GetHighscore(string minigame)
    {
        if (PlayerPrefs.GetFloat(minigame) == 0)
            PlayerPrefs.SetFloat(minigame, Mathf.Infinity);

        return PlayerPrefs.GetFloat(minigame);
    }
}
