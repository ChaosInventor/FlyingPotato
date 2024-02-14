using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiManager : MonoBehaviour
{
    //Stuff
    private Text ScoreTxt;
    private GameManager GM;
    private GameObject YouDiedScreen;
    private Image Abitly;
    private AdManager AM;
    private AudioManager AUM;
    public Button Revive;
    private GameObject Pause;

    public bool Paused = false;

    void Awake ()
    {
        //Finds all of the needed game objects.
        ScoreTxt = GameObject.Find("Score").GetComponent<Text>();
        GM = GameObject.Find("Game manager").GetComponent<GameManager>();
        YouDiedScreen = GameObject.Find("You died");
        Abitly = GameObject.Find("Ability").GetComponent<Image>();
        AM = GameObject.Find("Ad Manager").GetComponent<AdManager>();
        AUM = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        Revive = GameObject.Find("Revive").GetComponent<Button>();
        Revive.gameObject.GetComponentInChildren<Text>().text = "Watch an ad to get revived" + " ( " + AM.AdsPerGame.ToString() + " )";
        Pause = GameObject.Find("Pause");

        YouDiedScreen.SetActive(false);
        Pause.SetActive(false);

	}
	
	void Update ()
    {
        //Updates the score
        ScoreTxt.text = "Score: " + GM.Score;

	}
    //Death screen stuff.
    public void ShowDeathScreen(float HighScore)
    {

        YouDiedScreen.SetActive(true);
        GameObject HighScoreText = GameObject.Find("High score");
        Image FadeIn = GameObject.Find("FadeIn").GetComponent<Image>();

        HighScoreText.GetComponent<Text>().text = "High score:\n" + HighScore;
        FadeIn.gameObject.GetComponent<Animator>().SetTrigger("Fade in");

    }
    public void HideDeathScreen()
    {
        YouDiedScreen.SetActive(false);
    }
    //Ability icon update.
    public void ChageAbility(Sprite Icon)
    {
        Abitly.sprite = Icon;
    }
    //Death screen buttons.
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void WatchAdForRevive()
    {
        if (AM.AdsPerGame > 0)
        {
            Revive.gameObject.GetComponentInChildren<Text>().text = "Watch an ad to get revived" + " ( " + (AM.AdsPerGame - 1f) + " )";
            AM.StartVideoAd();
        }

    }
    //Pause and unpause the game.
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Pause.SetActive(true);
        PlayerControler Player = GameObject.Find("Player").GetComponent<PlayerControler>();
        Player.CanMove = false;
        Paused = true;
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        Pause.SetActive(false);
        PlayerControler Player = GameObject.Find("Player").GetComponent<PlayerControler>();
        Player.CanMove = true;
        Paused = false;
    }


}