using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using static GameUtilities;

public class StoryModeScreen : MonoBehaviour
{
    public Button back;

    public Button next;
    public Button startGame;

    public Button settings;
    public Button exitSettings;
    public GameObject settingsPopUp;

    public GameObject set1;
    public GameObject set2;
    public GameObject set3;

    void Start()
    {
        settingsPopUp.SetActive(false);
        set1.SetActive(true);
        set2.SetActive(false);
        set3.SetActive(false);

        Button backBtn = back.GetComponent<Button>();
        backBtn.onClick.AddListener(backClicked);

        Button settingsBtn = settings.GetComponent<Button>();
        settingsBtn.onClick.AddListener(settingsClicked);

        Button exitSettingsBtn = exitSettings.GetComponent<Button>();
        exitSettingsBtn.onClick.AddListener(exitSettingsClicked);

        Button nextBtn = next.GetComponent<Button>();
        nextBtn.onClick.AddListener(nextClicked);

        Button startGameBtn = startGame.GetComponent<Button>();
        startGameBtn.onClick.AddListener(startGameClicked);

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

    /*NOTES!!!
     * 
     * if they lose then make them redo the level
     * LAURA GRACE set up the game to where we can skip the quick game and hard code in the difficulty and player aliens
     * LAURA GRACE depending on if a player wins or loses they will need to either move on or stay on that level
     * take them back to the 
     */
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

    void settingsClicked()
    {
        Debug.Log("settings game");
        settingsPopUp.SetActive(true); // false to hide, true to show
    }

    void exitSettingsClicked()
    {
        Debug.Log("exit");
        settingsPopUp.SetActive(false); // false to hide, true to show
    }

}
