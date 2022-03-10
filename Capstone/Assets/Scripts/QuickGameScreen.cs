using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameUtilities;

public class QuickGameScreen : MonoBehaviour
{
    public Button startGame;
    public Button settings;
    public Button exitSettings;
    public Button exitQuickGame;

    public Button pharoah;
    public GameObject pharoah_H;
    public GameObject pharoah_figure;

    public Button scribe;
    public GameObject scribe_H;
    public GameObject scribe_figure;

    public Button peasant;
    public GameObject peasant_H;
    public GameObject peasant_figure;

    public Button worker;
    public GameObject worker_H;
    public GameObject worker_figure;

    public InputField usernameInputField;

    public GameObject scroll1;
    public GameObject scroll2;
    public GameObject scroll3;
    public GameObject scroll4;
    public GameObject scroll5;
    public GameObject scroll6;

    // Checks if there is anything entered into the input field.
    void LockInput(InputField input)
    {
        if (input.text.Length > 0)
        {
            Debug.Log(input.text);

            setP1username(input.text);
        }
        else if (input.text.Length == 0)
        {
            input.text = "Player 1";
            Debug.Log(input.text);

            setP1username(input.text);
        }

        //SAVE USERNAME HERE!!!
        setP2username("CPU");
    }

    // Start is called before the first frame update
    void Start()
    {
        Button startGameBtn = startGame.GetComponent<Button>();
        startGameBtn.onClick.AddListener(startGameClicked);

        Button exitQuickGameBtn = exitQuickGame.GetComponent<Button>();
        exitQuickGameBtn.onClick.AddListener(exitQuickGameClicked);

        Button pharoahBtn = pharoah.GetComponent<Button>();
        pharoahBtn.onClick.AddListener(pharoahClicked);

        Button scribeBtn = scribe.GetComponent<Button>();
        scribeBtn.onClick.AddListener(scribeClicked);

        Button peasantBtn = peasant.GetComponent<Button>();
        peasantBtn.onClick.AddListener(peasantClicked);

        Button workerBtn = worker.GetComponent<Button>();
        workerBtn.onClick.AddListener(workerClicked);

        //Adds a listener that invokes the "LockInput" method when the player finishes editing the main input field.
        //Passes the main input field into the method when "LockInput" is invoked
        usernameInputField.onEndEdit.AddListener(delegate { LockInput(usernameInputField); });

        Button settingsBtn = settings.GetComponent<Button>();
        settingsBtn.onClick.AddListener(settingsClicked);

        Button exitSettingsBtn = exitSettings.GetComponent<Button>();
        exitSettingsBtn.onClick.AddListener(delayDisplay);
    }

    void startGameClicked()
    {
        Debug.Log("here");

        // GameType and PlayerTurn are set in "ToggleGroup.cs"

        SceneManager.LoadScene("GameBoard");
    }

    void exitQuickGameClicked()
    {
        SceneManager.LoadScene("Menu");
    }

    void resetAll()
    {
        pharoah_H.SetActive(false);
        pharoah_figure.SetActive(false);

        scribe_H.SetActive(false);
        scribe_figure.SetActive(false);

        peasant_H.SetActive(false);
        peasant_figure.SetActive(false);

        worker_H.SetActive(false);
        worker_figure.SetActive(false);
    }

    void pharoahClicked()
    {
        resetAll();
        pharoah_H.SetActive(true);
        pharoah_figure.SetActive(true);

        //save the alien name LAURA GRACE lol
        setP1avatar(PlayerAvatar.PHAROAH);
    }

    void scribeClicked()
    {
        resetAll();
        scribe_H.SetActive(true);
        scribe_figure.SetActive(true);

        //save the alien name LAURA GRACE lol
        setP1avatar(PlayerAvatar.SCRIBE);
    }

    void peasantClicked()
    {
        resetAll();
        peasant_H.SetActive(true);
        peasant_figure.SetActive(true);

        //save the alien name LAURA GRACE lol
        setP1avatar(PlayerAvatar.PEASANT);
    }

    void workerClicked()
    {
        resetAll();
        worker_H.SetActive(true);
        worker_figure.SetActive(true);

        //save the alien name LAURA GRACE lol
        setP1avatar(PlayerAvatar.WORKER);
    }

    void settingsClicked()
    {
        scroll1.SetActive(true);

        //switch phases to turn off build and place player to create a fake modal pop up box
        StorePhases();
        DisablePhases();
    }

    void delayDisplay()
    {
        StartCoroutine(exitSettingsClicked());
    }

    IEnumerator exitSettingsClicked()
    {
        //time that the scroll waits before it moves on to the next image
        float delay = .001f;

        GameObject[] scrollArray = new GameObject[7];

        //Get the Rectransform of each height in order to get the height 
        scrollArray[1] = scroll1;
        scrollArray[2] = scroll2;
        scrollArray[3] = scroll3;
        scrollArray[4] = scroll4;
        scrollArray[5] = scroll5;
        scrollArray[6] = scroll6;

        //Create a copy of the scrolls in order to reset the heights back to the original
        GameObject[] scrollReset = scrollArray;

        //BEAUTIFUL PIECE OF CODE --- Love Dad
        for (int scrollNum = 1; scrollNum <= 5; scrollNum++)
        {
            scrollArray[scrollNum].SetActive(true);
            float firstScrollRef = scrollArray[scrollNum].GetComponent<RectTransform>().rect.height;
            float tempHeight = firstScrollRef;
            float secondScrollRef = scrollArray[scrollNum + 1].GetComponent<RectTransform>().rect.height;
            for (float i = firstScrollRef; i >= (firstScrollRef - ((firstScrollRef - secondScrollRef))); i -= 8)
            {
                scrollArray[scrollNum].GetComponent<RectTransform>().sizeDelta = new Vector2(scrollArray[scrollNum].GetComponent<RectTransform>().rect.width, i);
                yield return new WaitForSecondsRealtime(delay);
            }
            scrollArray[scrollNum].SetActive(false);

            //add delay before is turns off
            if (scrollNum == 5)
            {
                scrollArray[6].SetActive(true);
                yield return new WaitForSeconds(.1f);
                scrollArray[6].SetActive(false);
            }
            //reset height value
            scrollArray[scrollNum].GetComponent<RectTransform>().sizeDelta = new Vector2(scrollReset[scrollNum].GetComponent<RectTransform>().rect.width, tempHeight);
        }

        RestorePhases();
    }

}
