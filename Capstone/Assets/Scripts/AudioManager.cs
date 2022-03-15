using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    bool firstTime = true;

    //MainMenuSong Variables
    int songOneRecentPlay = 0;
    int songTwoRecentPlay = 0;
    int songThreeRecentPlay = 0;

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

        //Debug.Log(firstTime);
        if (firstTime == true)
        {
            firstTime = false;
            songOneRecentPlay = 2;
            Play("Golden Sea");
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
        StartCoroutine(waitAudio(s));
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator waitAudio(Sound s)
    {
        yield return new WaitForSeconds(s.clip.length);
        //Debug.Log("end of sound");

        Play(MainMenuSongs());
    }

    //Main Menu, Quick Game, Tutorial, And Multiplayer will all have same songs
    private string MainMenuSongs()
    {
        string songOne = "Golden Sea";
        string songTwo = "Vagueness";
        string songThree = "Ivory Towers";

        System.Random r = new System.Random();
        int randInt = r.Next(0, 10);
        //Debug.Log("The random generator chose the number: " + randInt);

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
}
