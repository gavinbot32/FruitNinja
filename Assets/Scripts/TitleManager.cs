using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public TextMeshProUGUI highscoreTxt;
    public int highscore;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            highscore = PlayerPrefs.GetInt("Highscore", 0);
        }
        highscoreTxt.text = highscore.ToString();
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetHighscore()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", 0);
            highscoreTxt.text = "0";
        }
    }

}
