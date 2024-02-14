using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Not fixed var
    public float Dis;
    public float Score;
    public float ScorePlus = 1f;
    //Increse var
    public float SpeedIncrease;
    public float PushPowerIncrease;
    public float IncreseEveryTimeByDis;
    public float IncreseWhenToIncrese;
    public float ScoreIncrese;
    private float LastIncrease;
    //Refrences
    private PlayerControler Player;
    private GameObject StartingPoint;
    private GuiManager GuiManager;
    //GameObjects that need to be assined to some thing
    public GameObject PlayerDead;
    //Other stuff
    public bool OnlyDoOnce = true;

    //At the start of the game
    void Awake ()
    {

        //Screen stuff
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;

        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        //Finds all requered objects
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>();
        StartingPoint = GameObject.Find("Start");
        GuiManager = GameObject.Find("GUI manager").GetComponent<GuiManager>();
        //Sets the increse
        LastIncrease = IncreseEveryTimeByDis;

	}

    //Every few fixed frames
    private void Update()
    {
        
        if (Input.touchCount > 0 && GuiManager.Paused || Input.GetMouseButtonDown(0) && GuiManager.Paused)
        {
            GuiManager.UnPauseGame();
        }
        if (Input.GetKey(KeyCode.Escape) && !GuiManager.Paused || Input.GetKey(KeyCode.Escape) && !GuiManager.Paused)
        {
            GuiManager.PauseGame();
        }

    }

    //Every frame the player is alive
    public void WhilePlayerAlive()
    {
        if (!GuiManager.Paused)
        {
            //Checks the distance and adds score
            Dis = Vector2.Distance(Player.gameObject.transform.position, StartingPoint.transform.position);
            Score += ScorePlus;
            //Increses the speed of the player
            if (Dis > LastIncrease && !Player.SpeedUp && !Player.InAbility)
            {
                Player.Speed += SpeedIncrease;
                Player.ConstantPushPower += PushPowerIncrease;
                ScorePlus += ScoreIncrese;

                LastIncrease = Dis + IncreseEveryTimeByDis + IncreseWhenToIncrese;
            }
        }

    }

    //When the player dies
    public void OnPlayerDeath()
    {
        //Only preforms this stuff once.
        if (OnlyDoOnce)
        {
            Animator Anim = Player.GetComponent<Animator>();
            Anim.enabled = false;
            Player.GetComponent<SpriteRenderer>().sprite = null;
            Instantiate(PlayerDead, Player.transform.position, Player.transform.rotation);
            AudioManager Audio = gameObject.GetComponentInChildren<AudioManager>();
            Audio.DoOnce = false;
            AdManager AdManager = gameObject.GetComponentInChildren<AdManager>();
            AdManager.LoadAd();

            if (gameObject.GetComponentInChildren<AdManager>().AdsPerGame <= 0)
            {
                GuiManager.Revive.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "No more revives left";
            }
            float LastScore = DataManager.LoadScore();
            if (Score > LastScore)
            {
                DataManager.SaveHighScore(Score);
                GuiManager.ShowDeathScreen(DataManager.LoadScore());
            }
            else
            {
                GuiManager.ShowDeathScreen(LastScore);
            }

            OnlyDoOnce = false;
        }

    }

}
