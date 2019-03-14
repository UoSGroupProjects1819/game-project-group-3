using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator animator;
    public Animator whaleAnim;
    public Animator camAnim;
    private int levelToLoad;

    public bool nextIsTutorial = false;
    public bool nextIsGame = false;



    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void WhaleTriggerMainMenu()
    {
        whaleAnim.SetTrigger("PlayWhaleSplash");
    }

    public void SelectTutorial()
    {
        nextIsGame = false;
        nextIsTutorial = true;
        FadeToLevel(1);
    }

    public void SelectGame()
    {
        nextIsTutorial = false;
        nextIsGame = true;
        FadeToLevel(1);
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
