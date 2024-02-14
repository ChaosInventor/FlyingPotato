using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    public float ConstantPushPower;
    public float Speed;
    public float AbilityCharge = 0;
    public float AbilityIncrease;

    public bool Alive;
    public bool SpeedUp = false;
    public bool Immune = false;
    public bool AbiltiyReady = false;
    public bool PcMode = true;
    public bool InAbility = false;
    private bool GoingUp = false;
    private bool GoingDown = false;
    public bool CanMove = true;

    private Rigidbody2D rb2d;
    private GameManager GM;
    private GuiManager GUIManager;
    private Animator Anim;

    public Sprite[] AbilityIcons;

	// Use this for initialization
	void Awake ()
    {
        Alive = true;

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        GM = GameObject.Find("Game manager").GetComponent<GameManager>();
        GUIManager = GameObject.Find("GUI manager").GetComponent<GuiManager>();
        Anim = gameObject.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        //No cheating (spikes where too hard to make lmao)
        if (transform.position.y > 2.871f && !Immune || transform.position.y < -2.9f && !Immune)
        {
            Alive = false;
        }
        //It's alive
        if (Alive)
        {
            GM.WhilePlayerAlive();
            if (CanMove || !CanMove && InAbility)
            {
                gameObject.transform.position += new Vector3(ConstantPushPower, 0f, 0f);
            }
        }
        else
        {
            GM.OnPlayerDeath();
        }
        //Abiltiy stuff
        #region
        if (AbilityCharge > 0 && AbilityCharge < 25)
        {
            GUIManager.ChageAbility(AbilityIcons[0]);
        }
        if(AbilityCharge > 25 && AbilityCharge < 50)
        {
            GUIManager.ChageAbility(AbilityIcons[1]);
        }
        if(AbilityCharge > 50 && AbilityCharge < 75)
        {
            GUIManager.ChageAbility(AbilityIcons[2]);
        }
        if(AbilityCharge > 75 && AbilityCharge < 100)
        {
            GUIManager.ChageAbility(AbilityIcons[3]);
        }
        if(AbilityCharge > 100)
        {
            GUIManager.ChageAbility(AbilityIcons[4]);
            AbiltiyReady = true;
        }
        #endregion
    }

    void FixedUpdate()
    {
        //Basic controls for testing on pc
        if (Alive && PcMode && CanMove)
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            {
                AbilityCharge += AbilityIncrease;
                if(!SpeedUp)
                {
                    ConstantPushPower = ConstantPushPower * 2;
                    GM.ScorePlus = GM.ScorePlus * 2;
                    SpeedUp = true;
                }
            }
            else
            {
                if(SpeedUp)
                {
                    ConstantPushPower = ConstantPushPower / 2;
                    GM.ScorePlus = GM.ScorePlus / 2;
                    SpeedUp = false;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    transform.position += new Vector3(0f, Speed * Time.deltaTime, 0f);
                }
                if(Input.GetKey(KeyCode.S))
                {
                    transform.position += new Vector3(0f, -Speed * Time.deltaTime, 0f);
                }
            }
        }
        //Basic controls for moble
        if(Alive && !PcMode && CanMove)
        {
            if(GoingUp && GoingDown)
            {
                AbilityCharge += AbilityIncrease;
                if (!SpeedUp)
                {
                    ConstantPushPower = ConstantPushPower * 2;
                    GM.ScorePlus = GM.ScorePlus * 2;
                    SpeedUp = true;
                }
            }
            else
            {
                if (SpeedUp)
                {
                    ConstantPushPower = ConstantPushPower / 2;
                    GM.ScorePlus = GM.ScorePlus / 2;
                    SpeedUp = false;
                }
                if (GoingUp)
                {
                    transform.position += new Vector3(0f, Speed * Time.deltaTime, 0f);
                }
                if (GoingDown)
                {
                    transform.position += new Vector3(0f, -Speed * Time.deltaTime, 0f);
                }
            }
        }

    }
    //Touch movment stuff
    #region
    public void GoUp()
    {
        GoingUp = true;
    }

    public void StopGoingUp()
    {
        GoingUp = false;
    }

    public void GoDown()
    {
        GoingDown = true;
    }

    public void StopGoingDown()
    {
        GoingDown = false;
    }
    #endregion

    public void Revive()
    {
        GameObject PlayerDead = GameObject.FindGameObjectWithTag("P dead");
        Destroy(PlayerDead);

        Alive = true;
        GuiManager GIUManager = GameObject.Find("GUI manager").GetComponent<GuiManager>();
        GUIManager.HideDeathScreen();
        Anim.enabled = true;
        GM.OnlyDoOnce = true;
        transform.position += new Vector3(0f,-transform.position.y);
    }

    public void StartImmune()
    {
        Immune = true;
        Anim.SetTrigger("Immune");
    }

    public void EndImmune()
    {
        Immune = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //If it hits some thing bad it dies.
        if (coll.gameObject.tag == "Bad" && !Immune)
        {
            Alive = false;
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.tag == "Bad" && Immune)
        {
            Destroy(coll.gameObject);
            GM.Score += 25f;
        }

    }

    public void ActivateAbility()
    {
        if (AbiltiyReady)
        {
            Immune = true;
            CanMove = false;
            ConstantPushPower = ConstantPushPower * 3;
            Anim.SetTrigger("Ability");
            AbilityCharge = 0f;
            AbiltiyReady = false;
            InAbility = true;
        }
    }
    
    public void EndAbility()
    {
        Immune = false;
        CanMove = true;
        ConstantPushPower = ConstantPushPower / 3;
        StartImmune();
        InAbility = false;
    }

}