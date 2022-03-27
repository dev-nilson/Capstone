using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using static GameUtilities;

public class StoryModeScreen : MonoBehaviour
{
    public Button back;

    public Button next;
    public Button startGame;
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

        Button startGameBtn = startGame.GetComponent<Button>();
        startGameBtn.onClick.AddListener(startGameClicked);

        Button continueStoryBtn = continueStory.GetComponent<Button>();
        continueStoryBtn.onClick.AddListener(continueStoryClicked);
    }

    void backClicked()
    {
        SceneManager.LoadScene("Menu");
    }

    void nextClicked()
    {
        set1.SetActive(false);
        set2.SetActive(true);
    }
    void continueStoryClicked()
    {
        winScreen.SetActive(false);
        set3.SetActive(true);
    }

    void startGameClicked()
    {
        //if it's on set2 then get ready to have the player vs. AI easy (Peasant vs. Scribe)
        if (set2.activeInHierarchy)
        {
            setP1avatar(PlayerAvatar.PEASANT);
            setP1username("Peasant");
            setP2avatar(PlayerAvatar.SCRIBE);
            setP2username("Scribe");
            setGameType(GameType.EASY);

            Debug.Log("Peasant vs. Scribe on EASY");
            set2.SetActive(false);
            SceneManager.LoadScene("GameBoard");
        }
        //if it's on set3 slide then get ready to have the player vs. AI hard (Peasant vs. Pharoah)
        if (set3.activeInHierarchy)
        {
            setP2avatar(PlayerAvatar.PHAROAH);
            setP2username("Pharaoh");
            setGameType(GameType.DIFFICULT);

            Debug.Log("Peasant vs. Pharoah on HARD");
            set3.SetActive(false);
            SceneManager.LoadScene("GameBoard");
        }
    }
}
