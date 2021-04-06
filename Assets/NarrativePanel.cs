using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct NeedTopBeKilled
{
    public GameObject[] objects;
}

public class NarrativePanel : MonoBehaviour
{
    public NeedTopBeKilled[] killingSteps;
    public Image uiImage;
    public TMPro.TextMeshProUGUI uiText;
    public float time;
    int currentIndex;
    float currentTime;
    bool closed;
    string[] toDisplay = {
        "12 November 2033\nExperiment 42 : begining.",
        "Okay let's do the usual, try to do that slime eat some of the other slimes and throw them to the guard in the corner.",
        "Nice job there!\n Wait... it looks like it did too much damage, the guard got destroyed and the gate is now open !\n This is the end of the experiment, put it back into its box.",
        "What are you...\nThe slime is acting on his own ??\nFor god sake, bring more guards !",
        "That doesn't look good, let's hope it's too stupid to climb some plateforms.",
        "MAXIMUN SECURITY NOW !!!\nCLOSE THE PORTAL !!!",
        "Everyone, I'm happy to announce that everyone's holidays has been approved, for an indetermined amount of time..."
    };

    void Start()
    {
        ResetStuff();
        DisplayNext();
    }

    void FixedUpdate()
    {
        CheckCurrentStep();
        if (closed)
            return;

        currentTime -= Time.fixedDeltaTime;
        if (currentTime <= 0)
            if (currentIndex == 1)
                DisplayNext();
            else
            {
                Close();
                closed = true;
            }
    }

    void CheckCurrentStep()
    {
        if (currentIndex >= killingSteps.Length
            || killingSteps[currentIndex].objects.Length == 0) // Skip
            return;
        for (int i = 0; i < killingSteps[currentIndex].objects.Length; ++i)
            if (killingSteps[currentIndex].objects[i] != null)
                return ;
        DisplayNext();
    }
    public void DisplayNext()
    {
        if (currentIndex >= toDisplay.Length)
            return;

        uiImage.gameObject.SetActive(true);
        uiText.gameObject.SetActive(true);
        uiText.text = toDisplay[currentIndex];
        currentIndex++;
        currentTime = currentIndex == 1 ? 3 : time;
        closed = false;
    }

    void ResetStuff()
    {
        uiImage.gameObject.SetActive(true);
        uiText.gameObject.SetActive(true);
        currentIndex = 0;
    }

    public void Close()
    {
        uiImage.gameObject.SetActive(false);
        uiText.gameObject.SetActive(false);
    }
}
