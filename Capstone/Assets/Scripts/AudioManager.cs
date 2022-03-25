/*
 *  Author: Brendon McDonald
 *  Description: ...
 */
using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//Add code for switching music in these places
//QuickGameScreen.startGameClicked() (Quick to Game)
//MenuScreen.artBookClicked() (Menu to Art)
//MenuScreen.storyModeClicked() (Menu to Story)
//UIP_LobbyController.HostLobbyOnClick() and UIP_RoomButton.JoomRoomOnClick() (Join/Host to FaceOff)
//UIP_RoomController.StartGameOnClick() (FaceOff to Game)
//Disconnected back to Menu (Game/FaceOff)
//Game back to Menu (GameBoardScreen.okClicked() / GameOverGraphics.backToMenuClicked())


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    bool firstTime = true;

    //MainMenuSong Variables
    int songOneRecentPlay = 0;
    int songTwoRecentPlay = 0;
    int songThreeRecentPlay = 0;

    int currentScreen;
    int newScreen;

    Sound currentSong;

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
        //if this is very first time, always play Golden Sea
        //else send to randomizeMainMenuSong

        Debug.Log(firstTime);
        if (firstTime == true)
        {
            firstTime = false;
            songOneRecentPlay = 2;
            Play("Golden Sea"); //this would be a call to nextMusic function, play will be called from nextMusic
        }
    }

    // Update is called once per frame
    void Update()
    {
        //update will set a temp value equal to the current int, this int will represent the current screen
        //it will test to see if the temp is different than the int and if so will call nextMusic
        if (currentScreen != newScreen)
        {
            currentSong.source.Stop();
            Debug.Log("Stopping song: " + currentSong.name);
        }

        currentScreen = newScreen;
    }

    public void StopCurrentSong(int screen)
    {
        //will update newScreen in order to make the current song being played stop
        newScreen = screen;
        StartCoroutine(waitSong(screen));
    }

    public void nextSong(int screen)
    {
        switch (screen)
        {
            case 1: //MAIN
                //Play();
                Debug.Log("MAIN");
                break;
            case 2: //STORY
                //Play();
                Debug.Log("STORY");
                break;
            case 3: //QUICK
                //Play();
                Debug.Log("QUICK");
                break;
            case 4: //FACEOFF
                //Play();
                Debug.Log("FACEOFF");
                break;
            case 5: //ART
                    Play("My Quiet Room");
                    Debug.Log("ART");
                break;
            case 6: //GAME
                Play("An Ordinary Day");
                Debug.Log("GAME");
                break;
        }    
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
        currentSong = s; //this has to go somewhere else, sounds will mess it up
    }

    private IEnumerator waitSong(int screen)
    {
        yield return new WaitForSecondsRealtime(1);

        Debug.Log("Calling nextSong");
        nextSong(screen);
    }

    #region Main Menu Randomize Stuff
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
                    else
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
                    else
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
                    else
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
