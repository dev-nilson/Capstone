using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using static GameUtilities;

public class StoryModeScreen : MonoBehaviour
{
    public Button back;

    public Button next;
    public Button startGame_One;
    public Button startGame_Two;
    public Button continueStory;

    public Button settings;
    public Button exitSettings;
    public GameObject settingsPopUp;

    public GameObject set1;
    public GameObject set2;
    public GameObject set3;

    public GameObject winScreen;
    public GameObject loseScreen;

    void Start()
    {
        settingsPopUp.SetActive(false);
        set1.SetActive(true);
        set2.SetActive(false);
        set3.SetActive(false);

        Button backBtn = back.GetComponent<Button>();
        backBtn.onClick.AddListener(backClicked);

        Button nextBtn = next.GetComponent<Button>();
        nextBtn.onClick.AddListener(nextClicked);

        Button startGameOneBtn = startGame_One.GetComponent<Button>();
        startGameOneBtn.onClick.AddListener(startGameOneClicked);

        Button startGameTwoBtn = startGame_Two.GetComponent<Button>();
        startGameTwoBtn.onClick.AddListener(startGameTwoClicked);

        Button continueStoryBtn = continueStory.GetComponent<Button>();
        continueStoryBtn.onClick.AddListener(continueStoryClicked);

        if (GameOverScreen.readyForStoryModeSetThree == true)
        {
            set1.SetActive(false);
            set2.SetActive(false);
            set3.SetActive(true);
        }
    }

    void backClicked()
    {
        SceneManager.LoadScene("Menu");
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
    }

    void nextClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        set1.SetActive(false);
        set2.SetActive(true);
    }
    void continueStoryClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        winScreen.SetActive(false);
        set3.SetActive(true);
    }

    void startGameOneClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
       
            setP1avatar(PlayerAvatar.PEASANT);
            setP1username("Peasant (You)");
            setP2avatar(PlayerAvatar.SCRIBE);
            setP2username("Scribe");
            setGameType(GameType.EASY);

            Debug.Log("Peasant vs. Scribe on EASY");
            set2.SetActive(false);
            SceneManager.LoadScene("GameBoard");
            FindObjectOfType<AudioManager>().StopCurrentSong(6);
    }

    void startGameTwoClicked()
    {
        setP2avatar(PlayerAvatar.PHAROAH);
        setP2username("Pharaoh");
        setGameType(GameType.DIFFICULT);

        Debug.Log("Peasant vs. Pharoah on HARD");
        set3.SetActive(false);
        SceneManager.LoadScene("GameBoard");
        FindObjectOfType<AudioManager>().StopCurrentSong(6);
    }
}
