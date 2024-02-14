using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    private Text HighScore;

    private GameObject HowToPlayScreen;
    private GameObject Menu;

    void Awake ()
    {

        //Screen stuff
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;

        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        HighScore = GameObject.Find("Score").GetComponent<Text>();
        HowToPlayScreen = GameObject.Find("How to play screen");
        Menu = GameObject.Find("Menu");

        HowToPlayScreen.SetActive(false);
        Menu.SetActive(true);

        HighScore.text = "High score:\n" + DataManager.LoadScore();

	}
	
    public void Quit ()
    {
        Application.Quit();
    }
    public void StartPlaying()
    {
        SceneManager.LoadScene("Loading");
    }
    public void ShowHowToPlay()
    {
        HowToPlayScreen.SetActive(true);
        Menu.SetActive(false);
    }
    public void HideHowToPlay()
    {
        HowToPlayScreen.SetActive(false);
        Menu.SetActive(true);
    }

}
