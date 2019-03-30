using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator animator;
    public Animator whaleTutorialAnim;
    public Animator whaleMainGameAnim;
    public Animator camAnim;
    private int levelToLoad;


    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void WhaleTriggerMainMenuTutorial()
    {
        whaleTutorialAnim.SetTrigger("PlayWhaleSplash");
    }

    public void WhaleTriggerMainMenuGame()
    {
        whaleMainGameAnim.SetTrigger("PlayWhaleSplash");
    }


    public void SelectTutorial()
    {
        WhaleTriggerMainMenuTutorial();
        //triggers whale animation, whale animation has trigger which fades to white and loads next level
    }

    public void SelectGame()
    {
        WhaleTriggerMainMenuGame();
        //triggers whale animation, whale animation has trigger which fades to white and loads next level
    }

    public void LoadTutorialLevel()
    {
        FadeToLevel(2);
    }

    public void LoadMainGameLevel()
    {
        FadeToLevel(4);
    }

    public void CharacterSelectionCompleteTutorial()
    {
        //pull camera back to make room for whale
        camAnim.SetTrigger("CameraPullBack");

        //add event to camera pull back animation to start whale
        //add event to whale animation to load next level
    }

    public void CharacterSelectionCompleteGame()
    {
        //pull camera back to make room for whale
        camAnim.SetTrigger("CameraPullBack");

        //add event to camera pull back animation to start whale
        //add event to whale animation to load next level
    }
}
