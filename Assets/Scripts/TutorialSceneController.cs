using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Rewired;
using Fungus;

public class TutorialSceneController : MonoBehaviour
{
    // Main Controller for all the tutorial events (think of the main function)
    private TutorialCutscene tutorialCutscene;
    public PlayableDirector firstCutscene;
    public Flowchart flowchart;
    public TutorialHintStatusChecker hintStatusChecker;
    public GameObject firstGraphic;
    public GameObject secondGraphic;
    public GameObject thirdGraphic;
    public GameObject SkipButton;
    public GameObject ObjectiveText;
    public GameObject HintTutorialText;
    public GameObject AccuseTutorialText;
    public GameObject EscapeTutorialText;
    public Animator SkipButtonFade;
    private bool showingGraphic = false;
    private bool showingFirstGraphic = false;
    private bool showingSecondGraphic = false;
    private bool showingThirdGraphic = false;
    private bool showingCutscene = true;

    void Start() {
        tutorialCutscene = this.GetComponent<TutorialCutscene>();
    }
    void Update()
    {
        if(showingCutscene)
        {
            cutsceneMode();
        }
        if(showingGraphic)
        {
            for (int i = 0; i < ReInput.players.playerCount; i++)
            {
                Player player = ReInput.players.Players[i];
                // Check if player pressed the skip button
                if(player.GetButtonDown("Skip")){
                    SkipButtonFade.SetBool("Fade", true);
                    if(showingFirstGraphic)
                    {
                        firstGraphic.SetActive(false);
                        showingFirstGraphic = false;
                        ObjectiveText.SetActive(true);
                        HintTutorialText.SetActive(true);
                    }
                    else if(showingSecondGraphic)
                    {
                        secondGraphic.SetActive(false);
                        showingSecondGraphic = false;
                        ObjectiveText.SetActive(true);
                        AccuseTutorialText.SetActive(true);
                    }
                    else if(showingThirdGraphic)
                    {
                        thirdGraphic.SetActive(false);
                        showingThirdGraphic = false;
                        ObjectiveText.SetActive(true);
                        EscapeTutorialText.SetActive(true);
                    }
                    showingGraphic = false;
                    showingCutscene = false;
                    playMode();
                }
            }
        }


    }

    public void playMode()
    {
        Debug.Log("Enter Play Mode");
        for (int j = 0; j < ReInput.players.playerCount; j++)
        {
            Player player_again = ReInput.players.Players[j];
            player_again.controllers.maps.LoadMap(ControllerType.Joystick, player_again.id, "default", "default", true);
            player_again.controllers.maps.LoadMap(ControllerType.Joystick, player_again.id, "Cutscene", "default", false);
        }
    }

    public void cutsceneMode()
    {
        Debug.Log("Enter Cutscene Mode");
        for (int i = 0; i < ReInput.players.playerCount; i++)
        {
            Player player = ReInput.players.Players[i];
            player.controllers.maps.LoadMap(ControllerType.Joystick, player.id, "default", "default", false);
            player.controllers.maps.LoadMap(ControllerType.Joystick, player.id, "Cutscene", "default", true);
        }
    }
    public void playTimeline()
    {
        firstCutscene.Play();
    }
    public void showFirstGraphic()
    {
        cutsceneMode();
        firstGraphic.SetActive(true);
        SkipButton.SetActive(true);
        showingGraphic = true;
        showingFirstGraphic = true;
        showingCutscene = true;

    }

    public void showSecondGraphic()
    {
        cutsceneMode();
        secondGraphic.SetActive(true);
        SkipButton.SetActive(true);
        showingGraphic = true;
        showingSecondGraphic = true;
        showingCutscene = true;
    }

    public void showThirdGraphic()
    {
        cutsceneMode();
        thirdGraphic.SetActive(true);
        SkipButton.SetActive(true);
        showingGraphic = true;
        showingThirdGraphic = true;
        showingCutscene = true;
    }

    public void secondDialogueTrigger()
    {
        cutsceneMode();
        ObjectiveText.SetActive(false);
        HintTutorialText.SetActive(false);
        AccuseTutorialText.SetActive(false);
        EscapeTutorialText.SetActive(false);
        showingCutscene = true;
        flowchart.ExecuteBlock("Notebook");
    }

    public void thirdDialogueTrigger()
    {
        cutsceneMode();
        ObjectiveText.SetActive(false);
        HintTutorialText.SetActive(false);
        AccuseTutorialText.SetActive(false);
        EscapeTutorialText.SetActive(false);
        showingCutscene = true;
        flowchart.ExecuteBlock("Escape");
    }

    public void finalDialogueTrigger()
    {
        cutsceneMode();
        ObjectiveText.SetActive(false);
        HintTutorialText.SetActive(false);
        AccuseTutorialText.SetActive(false);
        EscapeTutorialText.SetActive(false);
        showingCutscene = true;
        flowchart.ExecuteBlock("End");
    }

    public void wrongTargetDialogueTrigger()
    {
        flowchart.ExecuteBlock("Wrong target");
    }

    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
