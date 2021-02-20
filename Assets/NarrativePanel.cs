using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativePanel : MonoBehaviour
{
    public Image uiImage;
    public TMPro.TextMeshProUGUI uiText;
    string[] toDisplay;
    int currentIndex;

    public void SetTexts(string[] texts) { toDisplay = texts; ResetStuff(); }
    public void DisplayNext()
    {
        if (currentIndex >= toDisplay.Length)
        {
            uiImage.gameObject.SetActive(false);
            uiText.gameObject.SetActive(false);
        }
        else
        {
            uiText.text = toDisplay[currentIndex];
            currentIndex++;
        }
    }

    void ResetStuff()
    {
        uiImage.gameObject.SetActive(true);
        uiText.gameObject.SetActive(true);
        currentIndex = 0;
    }
}
