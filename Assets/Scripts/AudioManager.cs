using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public bool SimpleSong;

    public CompliteSong[] Songs;
    public AudioClip[] SimpleSongs;
    public AudioClip DeathSF;

    public float StartMidAtDis;
    public float StartFastAtDis;

    private int CurSong;

    private bool IntroSlow = false;
    private bool IntroMid = false;
    private bool IntroFast = false;
    public bool DoOnce = false;

    private GameManager GM;
    private AudioSource AudioPlayer;
    private PlayerControler Player;

    void Awake()
    {

        AudioPlayer = GetComponent<AudioSource>();
        
        if (!SimpleSong)
        {
            GM = GameObject.Find("Game manager").GetComponent<GameManager>();
            CurSong = RandomSong();
            Player = GameObject.Find("Player").GetComponent<PlayerControler>();
        }

    }

    void Update()
    {
        //Complecated stuff
        if (!SimpleSong &&Player.Alive)
        {

            if (GM.Dis < StartMidAtDis)
            {
                //Plays the intro once
                if (!IntroSlow)
                {
                    PlayIntro(Songs[CurSong].Slow.Intro);
                    IntroSlow = true;
                }
                //Plays the loop intill the dis chages
                if(!AudioPlayer.isPlaying)
                {
                    PlayLoop(Songs[CurSong].Slow.Loop);
                }
            }

            if(GM.Dis > StartMidAtDis && GM.Dis < StartFastAtDis)
            {
                if (!IntroMid)
                {
                    AudioPlayer.loop = false;
                    if (!AudioPlayer.isPlaying)
                    {
                        StopPlaying();
                        PlayIntro(Songs[CurSong].Mid.Intro);
                        IntroMid = true;
                    }
                }

                if(!AudioPlayer.isPlaying && IntroMid)
                {
                    PlayLoop(Songs[CurSong].Mid.Loop);
                }
            }

            if(GM.Dis > StartFastAtDis)
            {
                if(!IntroFast)
                {
                    AudioPlayer.loop = false;
                    if (AudioPlayer.isPlaying)
                    {
                        StopPlaying();
                        PlayIntro(Songs[CurSong].Fast.Intro);
                        IntroFast = true;
                    }
                }

                if(!AudioPlayer.isPlaying && IntroFast)
                {
                    PlayLoop(Songs[CurSong].Fast.Loop);
                }
            }

        }
        else if(!SimpleSong &&!Player.Alive)
        {
            if (!DoOnce)
            {
                StopPlaying();
                AudioPlayer.clip = DeathSF;
                AudioPlayer.Play();
                DoOnce = true;
            }
        }
        else
        {

            AudioPlayer.loop = false;
            int RanSimpleSong = Random.Range(0, SimpleSongs.Length);

            if(!AudioPlayer.isPlaying)
            {
                AudioPlayer.clip = SimpleSongs[RanSimpleSong];
                AudioPlayer.Play();
            }

        }
    }

    private int RandomSong()
    {
        return Random.Range(0, Songs.Length);
    }

    private void PlayIntro(AudioClip Intro)
    {

        AudioPlayer.clip = Intro;
        AudioPlayer.Play();

    }

    private void PlayLoop(AudioClip Loop)
    {
        AudioPlayer.clip = Loop;
        AudioPlayer.loop = true;
        AudioPlayer.Play();
    }

    public void StopPlaying()
    {
        AudioPlayer.loop = false;
        AudioPlayer.Stop();

    }

    public void Mute()
    {
        AudioPlayer.Pause();
    }

    public void UnMute()
    {
        AudioPlayer.UnPause();
    }

}
