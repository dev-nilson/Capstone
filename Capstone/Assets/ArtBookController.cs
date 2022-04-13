using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtBookController : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private GameObject allAliensButton;
    [SerializeField]
    private GameObject pharoahButton;
    [SerializeField]
    private GameObject scribeButton;
    [SerializeField]
    private GameObject workerButton;
    [SerializeField]
    private GameObject peasantButton;
    [SerializeField]
    private GameObject uiButton;

    //All Aliens Panel
    [SerializeField]
    private GameObject allAliensPanel;
    [SerializeField]
    private GameObject allAliensRightButton;
    [SerializeField]
    private GameObject allAliensLeftButton;
    [SerializeField]
    private GameObject allAliensBackButton;
    [SerializeField]
    private GameObject allConcepts;
    [SerializeField]
    private GameObject allEarlyDesigns;
    [SerializeField]
    private GameObject allFinalDesigns;
    [SerializeField]
    private Text allAliensCurrentImageDescription;

    //Pharoah Panel
    [SerializeField]
    private GameObject pharoahPanel;
    [SerializeField]
    private GameObject pharoahRightButton;
    [SerializeField]
    private GameObject pharoahLeftButton;
    [SerializeField]
    private GameObject pharoahBackButton;
    [SerializeField]
    private GameObject pharoahConcepts;
    [SerializeField]
    private GameObject pharoahDynamicPoses;
    [SerializeField]
    private Text pharoahCurrentImageDescription;

    //Scribe Panel
    [SerializeField]
    private GameObject scribePanel;
    [SerializeField]
    private GameObject scribeRightButton;
    [SerializeField]
    private GameObject scribeLeftButton;
    [SerializeField]
    private GameObject scribeBackButton;
    [SerializeField]
    private GameObject scribeConcepts;
    [SerializeField]
    private GameObject scribeDynamicPoses;
    [SerializeField]
    private Text scribeCurrentImageDescription;

    //Worker Panel 
    [SerializeField]
    private GameObject workerPanel;
    [SerializeField]
    private GameObject workerRightButton;
    [SerializeField]
    private GameObject workerLeftButton;
    [SerializeField]
    private GameObject workerBackButton;
    [SerializeField]
    private GameObject workerConcepts;
    [SerializeField]
    private GameObject workerDynamicPoses;
    [SerializeField]
    private Text workerCurrentImageDescription;

    //Peasant Panel 
    [SerializeField]
    private GameObject peasantPanel;
    [SerializeField]
    private GameObject peasantRightButton;
    [SerializeField]
    private GameObject peasantLeftButton;
    [SerializeField]
    private GameObject peasantBackButton;
    [SerializeField]
    private GameObject peasantConcepts;
    [SerializeField]
    private GameObject peasantDynamicPoses;
    [SerializeField]
    private Text peasantCurrentImageDescription;

    //Ui Panel
    [SerializeField]
    private GameObject UiPanel;
    [SerializeField]
    private GameObject UiRightButton;
    [SerializeField]
    private GameObject UiLeftButton;
    [SerializeField]
    private GameObject UiBackButton;
    [SerializeField]
    private GameObject MenuSketch;
    [SerializeField]
    private GameObject QuickSketch;
    [SerializeField]
    private GameObject MultiSketch;
    [SerializeField]
    private GameObject GameSketch;
    [SerializeField]
    private GameObject earlyMenuDesigns;
    [SerializeField]
    private GameObject earlyMenuDesigns2;
    [SerializeField]
    private GameObject earlyMenuDesigns3;
    [SerializeField]
    private GameObject earlyMenuDesigns4;
    [SerializeField]
    private GameObject quickGameDesign;
    [SerializeField]
    private GameObject gameScreenDesign;
    [SerializeField]
    private GameObject logoDesign;
    [SerializeField]
    private Text UiCurrentImageDescription;
    #endregion

    #region AllAliensPanel
    public void allAliensRightButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        if (allConcepts.activeSelf)
        {
            allConcepts.SetActive(false);
            allEarlyDesigns.SetActive(true);
            allAliensCurrentImageDescription.text = "Early Alien Designs B/W";
        }
        else if (allEarlyDesigns.activeSelf)
        {
            allEarlyDesigns.SetActive(false);
            allFinalDesigns.SetActive(true);
            allAliensCurrentImageDescription.text = "Final Alien Designs Color";
        }
        else if (allFinalDesigns.activeSelf)
        {
            allFinalDesigns.SetActive(false);
            allConcepts.SetActive(true);
            allAliensCurrentImageDescription.text = "All Alien Silhouettes B/W";
        }
    }

    public void allAliensLeftButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        if (allConcepts.activeSelf)
        {
            allConcepts.SetActive(false);
            allFinalDesigns.SetActive(true);
            allAliensCurrentImageDescription.text = "Final Alien Designs Color";
        }
        else if (allEarlyDesigns.activeSelf)
        {
            allEarlyDesigns.SetActive(false);
            allConcepts.SetActive(true);
            allAliensCurrentImageDescription.text = "All Alien Silhouettes B/W";
        }
        else if (allFinalDesigns.activeSelf)
        {
            allFinalDesigns.SetActive(false);
            allEarlyDesigns.SetActive(true);
            allAliensCurrentImageDescription.text = "Early Alien Designs B/W";
        }
    }

    public void allAliensBackButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        allAliensPanel.SetActive(false);
    }

    public void allAliensButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        allAliensPanel.SetActive(true);
    }
    #endregion

    #region PharoahPanel
    public void pharoahRightButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        if (pharoahConcepts.activeSelf)
        {
            pharoahConcepts.SetActive(false);
            pharoahDynamicPoses.SetActive(true);

            pharoahRightButton.SetActive(false);
            pharoahLeftButton.SetActive(true);
            pharoahCurrentImageDescription.text = "Pharoah Dynamic Poses";
        }
    }

    public void pharoahLeftButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        if (pharoahDynamicPoses.activeSelf)
        {
            pharoahDynamicPoses.SetActive(false);
            pharoahConcepts.SetActive(true);

            pharoahRightButton.SetActive(true);
            pharoahLeftButton.SetActive(false);
            pharoahCurrentImageDescription.text = "'Pharoah' Alien Species";
        }
    }

    public void pharoahBackButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        pharoahPanel.SetActive(false);
    }

    public void pharoahButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        pharoahPanel.SetActive(true);
    }
    #endregion

    #region ScribePanel
    public void scribeRightButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        if (scribeConcepts.activeSelf)
        {
            scribeConcepts.SetActive(false);
            scribeDynamicPoses.SetActive(true);

            scribeRightButton.SetActive(false);
            scribeLeftButton.SetActive(true);
            scribeCurrentImageDescription.text = "Scribe Dynamic Poses";
        }
    }

    public void scribeLeftButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        if (scribeDynamicPoses.activeSelf)
        {
            scribeDynamicPoses.SetActive(false);
            scribeConcepts.SetActive(true);

            scribeRightButton.SetActive(true);
            scribeLeftButton.SetActive(false);
            scribeCurrentImageDescription.text = "'Scribe' Alien Species";
        }
    }

    public void scribeBackButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        scribePanel.SetActive(false);
    }

    public void scribeButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        scribePanel.SetActive(true);
    }
    #endregion

    #region WorkerPanel
    public void workerRightButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        if (workerConcepts.activeSelf)
        {
            workerConcepts.SetActive(false);
            workerDynamicPoses.SetActive(true);

            workerRightButton.SetActive(false);
            workerLeftButton.SetActive(true);
            workerCurrentImageDescription.text = "Worker Dynamic Poses";
        }
    }

    public void workerLeftButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        if (workerDynamicPoses.activeSelf)
        {
            workerDynamicPoses.SetActive(false);
            workerConcepts.SetActive(true);

            workerRightButton.SetActive(true);
            workerLeftButton.SetActive(false);
            workerCurrentImageDescription.text = "'Worker' Alien Species";
        }
    }

    public void workerBackButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        workerPanel.SetActive(false);
    }

    public void workerButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        workerPanel.SetActive(true);
    }
    #endregion

    #region PeasantPanel
    public void peasantRightButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        if (peasantConcepts.activeSelf)
        {
            peasantConcepts.SetActive(false);
            peasantDynamicPoses.SetActive(true);

            peasantRightButton.SetActive(false);
            peasantLeftButton.SetActive(true);
            peasantCurrentImageDescription.text = "Peasant Dynamic Poses";
        }
    }

    public void peasantLeftButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        if (peasantDynamicPoses.activeSelf)
        {
            peasantDynamicPoses.SetActive(false);
            peasantConcepts.SetActive(true);

            peasantRightButton.SetActive(true);
            peasantLeftButton.SetActive(false);
            peasantCurrentImageDescription.text = "'Peasant' Alien Species";
        }
    }

    public void peasantBackButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        peasantPanel.SetActive(false);
    }

    public void peasantButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        peasantPanel.SetActive(true);
    }
    #endregion

    #region UiPanel
    public void UiRightButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        if (MenuSketch.activeSelf)
        {
            MenuSketch.SetActive(false);
            QuickSketch.SetActive(true);
            UiCurrentImageDescription.text = "Quick Game Menu Sketches";
        }
        else if (QuickSketch.activeSelf)
        {
            QuickSketch.SetActive(false);
            MultiSketch.SetActive(true);
            UiCurrentImageDescription.text = "Multiplayer Menu Sketches";
        }
        else if (MultiSketch.activeSelf)
        {
            MultiSketch.SetActive(false);
            GameSketch.SetActive(true);
            UiCurrentImageDescription.text = "Game Screen Sketches";
        }
        else if (GameSketch.activeSelf)
        {
            GameSketch.SetActive(false);
            earlyMenuDesigns.SetActive(true);
            UiCurrentImageDescription.text = "B/W Menu Backgrounds";
        }
        else if (earlyMenuDesigns.activeSelf)
        {
            earlyMenuDesigns.SetActive(false);
            earlyMenuDesigns2.SetActive(true);
            UiCurrentImageDescription.text = "Menu Background version 1";
        }
        else if (earlyMenuDesigns2.activeSelf)
        {
            earlyMenuDesigns2.SetActive(false);
            earlyMenuDesigns3.SetActive(true);
            UiCurrentImageDescription.text = "Menu Background version 2";
        }
        else if (earlyMenuDesigns3.activeSelf)
        {
            earlyMenuDesigns3.SetActive(false);
            earlyMenuDesigns4.SetActive(true);
            UiCurrentImageDescription.text = "Menu Background final version";
        }
        else if (earlyMenuDesigns4.activeSelf)
        {
            earlyMenuDesigns4.SetActive(false);
            quickGameDesign.SetActive(true);
            UiCurrentImageDescription.text = "Early Quick Game Screen Design";
        }
        else if (quickGameDesign.activeSelf)
        {
            quickGameDesign.SetActive(false);
            gameScreenDesign.SetActive(true);
            UiCurrentImageDescription.text = "Early Game Screen Design";
        }
        else if (gameScreenDesign.activeSelf)
        {
            gameScreenDesign.SetActive(false);
            logoDesign.SetActive(true);
            UiCurrentImageDescription.text = "Pyramid Paradox Logo";
        }
        else if (logoDesign.activeSelf)
        {
            logoDesign.SetActive(false);
            MenuSketch.SetActive(true);
            UiCurrentImageDescription.text = "Main Menu Sketches";
        }
    }

    public void UiLeftButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        if (MenuSketch.activeSelf)
        {
            MenuSketch.SetActive(false);
            logoDesign.SetActive(true);
            UiCurrentImageDescription.text = "Pyramid Paradox Logo";
        }
        else if (QuickSketch.activeSelf)
        {
            QuickSketch.SetActive(false);
            MenuSketch.SetActive(true);
            UiCurrentImageDescription.text = "Main Menu Sketches";
        }
        else if (MultiSketch.activeSelf)
        {
            MultiSketch.SetActive(false);
            QuickSketch.SetActive(true);
            UiCurrentImageDescription.text = "Quick Game Menu Sketches";
        }
        else if (GameSketch.activeSelf)
        {
            GameSketch.SetActive(false);
            MultiSketch.SetActive(true);
            UiCurrentImageDescription.text = "Multiplayer Menu Sketches";
        }
        else if (earlyMenuDesigns.activeSelf)
        {
            earlyMenuDesigns.SetActive(false);
            GameSketch.SetActive(true);
            UiCurrentImageDescription.text = "Game Screen Sketches";
        }
        else if (earlyMenuDesigns2.activeSelf)
        {
            earlyMenuDesigns2.SetActive(false);
            earlyMenuDesigns.SetActive(true);
            UiCurrentImageDescription.text = "B/W Menu Backgrounds";
        }
        else if (earlyMenuDesigns3.activeSelf)
        {
            earlyMenuDesigns3.SetActive(false);
            earlyMenuDesigns2.SetActive(true);
            UiCurrentImageDescription.text = "Menu Background version 1";
        }
        else if (earlyMenuDesigns4.activeSelf)
        {
            earlyMenuDesigns4.SetActive(false);
            earlyMenuDesigns3.SetActive(true);
            UiCurrentImageDescription.text = "Menu Background version 2";
        }
        else if (quickGameDesign.activeSelf)
        {
            quickGameDesign.SetActive(false);
            earlyMenuDesigns4.SetActive(true);
            UiCurrentImageDescription.text = "Menu Background final version";
        }
        else if (gameScreenDesign.activeSelf)
        {
            gameScreenDesign.SetActive(false);
            quickGameDesign.SetActive(true);
            UiCurrentImageDescription.text = "Early Quick Game Screen Design";
        }
        else if (logoDesign.activeSelf)
        {
            logoDesign.SetActive(false);
            gameScreenDesign.SetActive(true);
            UiCurrentImageDescription.text = "Early Game Screen Design";
        }
    }

    public void UiBackButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        UiPanel.SetActive(false);
    }

    public void UiButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        UiPanel.SetActive(true);
    }
    #endregion

}
