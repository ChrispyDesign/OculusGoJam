using UnityEngine;

public static class HighscoreManager
{
    /// <summary>
    /// set the new highscore of a minigame
    /// </summary>
    /// <param name="minigame">the name of the minigame</param>
    /// <param name="highscore">the new highscore</param>
    public static void SetHighscore(string minigame, float highscore)
    {
        PlayerPrefs.SetFloat(minigame, highscore);
    }

    /// <summary>
    /// get the current highscore of a minigame
    /// </summary>
    /// <param name="minigame">the name of the minigame</param>
    /// <returns>the current highscore</returns>
    public static float GetHighscore(string minigame)
    {
        if (PlayerPrefs.GetFloat(minigame) == 0)
            PlayerPrefs.SetFloat(minigame, Mathf.Infinity); // initialise to infinity

        return PlayerPrefs.GetFloat(minigame);
    }
}
