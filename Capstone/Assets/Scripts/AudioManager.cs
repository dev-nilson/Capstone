/*
 *  Author: Brendon McDonald
 *  Description: ...
 */
using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

//Add code for switching music in these places
//MenuScreen.storyModeClicked() (Menu to Story)
//UIP_LobbyController.HostLobbyOnClick() and UIP_RoomButton.JoomRoomOnClick() (Join/Host to FaceOff)
//Disconnected back to Menu (Game/FaceOff)
//Game back to Menu (GameBoardScreen.okClicked() / GameOverGraphics.backToMenuClicked())


public class AudioManager : MonoBehaviour
{
    #region Variables
    public static AudioManager instance;
    public Sound[] sounds;

    Sound currentSong;
    public AudioMixer audioMixer;

    int currentScreen;
    int newScreen;

    bool isMusicOn = true;

    //MainMenuSong Variables
    int songOneRecentPlay = 0;
    int songTwoRecentPlay = 0;
    int songThreeRecentPlay = 0;

    bool firstTime = true;

    string[] songs = { "Golden Sea", "Vagueness", "Ivory Towers", "My Quiet Room",
                        "OrdinaryBankThowr","SerpentClosing", "Devil's Disgrace", 
                        "Searching Through Sand" };
    #endregion

    #region AwakeStartUpdate
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Debug.Log(firstTime);
        if (firstTime == true)
        {
            firstTime = false;
            songOneRecentPlay = 2;
            currentScreen = 1;
            newScreen = 1;
            Play("Golden Sea");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScreen != newScreen)
        {
            currentSong.source.Stop();
            Debug.Log("Stopping song: " + currentSong.name);
        }

        currentScreen = newScreen;

        if (!isMusicOn)
        {
            currentSong.source.Pause();
        }
        else if (isMusicOn)
        {
            currentSong.source.UnPause();
        }
    }
    #endregion

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void turnMusicOff()
    {
        isMusicOn = false;
    }

    public void turnMusicOn()
    {
        isMusicOn = true;
    }

    public void StopCurrentSong(int screen)
    {
        if (isMusicOn == false)
        {
            return;
        }
        
        //will update newScreen in order to make the current song being played stop
        newScreen = screen;
        StartCoroutine(waitSong(screen));
        
    }

    public void nextSong(int screen)
    {
        switch (screen)
        {
            case 1: //MAIN
                Debug.Log("MAIN");

                Play(MainMenuSongs());
                break;
            case 2: //STORY_FIRST_HALF
                Debug.Log("STORY_FIRST_HALF");

                Play("Searching Through Sand");         
                break;
            case 3: //STORY_SECOND_HALF
                Debug.Log("STORY_SECOND_HALF");

                Play("Devil's Disgrace");
                break;
            case 4: //FACEOFF
                Debug.Log("FACEOFF");

                Play("SerpentClosing"); //needs to play both divine serpent and closing in
                break;
            case 5: //ART
                Debug.Log("ART");

                Play("My Quiet Room"); //on a loop
                break;
            case 6: //GAME
                Debug.Log("GAME");

                Play("OrdinaryBankThowr"); //add in code to switch between all 3 songs
                break;
        }

        Debug.Log("exiting switch statement");
    }

    public void Play(string name)
    {
        List<string> gameSongs = new List<string>();
        gameSongs.AddRange(songs);

        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        for (int i = 0; i < songs.Length; i++)
        {
            if (name == songs[i])
            {
                currentSong = s;
                Debug.Log("Current song set to: " + currentSong.name);
            }
            if (name == songs[i] && currentScreen == 1) //MAINMENU
            {
                StartCoroutine(MainMenuSwitch(s));
            }
            else if (currentScreen == 2) //STORY
            {
                StopCoroutine(MainMenuSwitch(s));
            }
            else if (currentScreen == 4) //FACEOFF
            {
                StopCoroutine(MainMenuSwitch(s));
            }
            else if (currentScreen == 5) //ART
            {
                StopCoroutine(MainMenuSwitch(s));
            }
            else if (name == songs[i] && currentScreen == 6) //GAMEBOARD
            {
                StopCoroutine(MainMenuSwitch(s));
                //StartCoroutine(GameBoardSwitch(s));
            }
        }

        s.source.outputAudioMixerGroup = s.group;
        s.source.Play();
    }

    #region Coroutines
    private IEnumerator waitSong(int screen)
    {
        yield return new WaitForSecondsRealtime(1);

        Debug.Log("Calling nextSong");
        nextSong(screen);
    }

    private IEnumerator MainMenuSwitch(Sound s)
    {
        Debug.Log("Starting Coroutine MainMenuSwitch");

        
        yield return new WaitForSeconds(s.clip.length);

        if (currentScreen == 1)
        {
            Play(MainMenuSongs());
        }
    }
    #endregion

    #region Main Menu Song Stuff
    //Main Menu, Quick Game, Tutorial, And Multiplayer will all have same songs
    private string MainMenuSongs()
    {
        string songOne = "Golden Sea";
        string songTwo = "Vagueness";
        string songThree = "Ivory Towers";

        System.Random r = new System.Random();
        int randInt = r.Next(0, 10);
        Debug.Log("The random generator chose the number: " + randInt);

            if (randInt == 0)
            {
                    if (songOneRecentPlay == 0)
                    {
                        songOneRecentPlay = 2;

                        songTwoRecentPlay = PlayedRecently(songTwoRecentPlay);
                        songThreeRecentPlay = PlayedRecently(songThreeRecentPlay);
                        return songOne;
                    }
                    else if (songTwoRecentPlay == 0)
                    {
                        songTwoRecentPlay = 2;

                        songOneRecentPlay = PlayedRecently(songOneRecentPlay);
                        songThreeRecentPlay = PlayedRecently(songThreeRecentPlay);
                        Debug.Log("Song 1 was chosen, but has played recently. Playing song 2");
                        return songTwo;
                    }
                    else if (songThreeRecentPlay == 0)
                    {
                        songThreeRecentPlay = 2;

                        songOneRecentPlay = PlayedRecently(songOneRecentPlay);
                        songTwoRecentPlay = PlayedRecently(songTwoRecentPlay);
                        Debug.Log("Song 1 was chosen, but has played recently. Playing song 3");
                        return songThree;
                    }
            }
            if (randInt > 0 && randInt < 5)
            {
                    if (songTwoRecentPlay == 0)
                    {
                        songTwoRecentPlay = 2;

                        songOneRecentPlay = PlayedRecently(songOneRecentPlay);
                        songThreeRecentPlay = PlayedRecently(songThreeRecentPlay);
                        return songTwo;
                    }
                    else if (songThreeRecentPlay == 0)
                    {
                        songThreeRecentPlay = 2;

                        songOneRecentPlay = PlayedRecently(songOneRecentPlay);
                        songTwoRecentPlay = PlayedRecently(songTwoRecentPlay);
                        Debug.Log("Song 2 was chosen, but has played recently. Playing song 3");
                        return songThree;
                    }
                    else if (songOneRecentPlay == 0)
            {
                        songOneRecentPlay = 2;

                        songTwoRecentPlay = PlayedRecently(songTwoRecentPlay);
                        songThreeRecentPlay = PlayedRecently(songThreeRecentPlay);
                        Debug.Log("Song 2 was chosen, but has played recently. Playing song 1");
                        return songOne;

                    }
            }
            if (randInt >= 5 && randInt <= 10)
            {
                    if (songThreeRecentPlay == 0)
                    {
                        songThreeRecentPlay = 2;

                        songOneRecentPlay = PlayedRecently(songOneRecentPlay);
                        songTwoRecentPlay = PlayedRecently(songTwoRecentPlay);
                        return songThree;

                    }
                    else if (songOneRecentPlay == 0)
                    {
                        songOneRecentPlay = 2;

                        songTwoRecentPlay = PlayedRecently(songTwoRecentPlay);
                        songThreeRecentPlay = PlayedRecently(songThreeRecentPlay);
                        Debug.Log("Song 3 was chosen, but has played recently. Playing song 1");
                        return songOne;
                    }
                    else if (songTwoRecentPlay == 0)
            {
                        songTwoRecentPlay = 2;

                        songOneRecentPlay = PlayedRecently(songOneRecentPlay);
                        songThreeRecentPlay = PlayedRecently(songThreeRecentPlay);
                        Debug.Log("Song 3 was chosen, but has played recently. Playing song 2");
                        return songTwo;
                    }
            }

        return songOne;
    }

    private int PlayedRecently(int songCountdown)
    {
        if (songCountdown == 1)
        {
            songCountdown -= 1;
        }
        else if (songCountdown == 2)
        {
            songCountdown -= 1;
        }

        return songCountdown;
    }
    #endregion
}
