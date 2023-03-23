using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button playButton;
    public Button levelSelectButton;
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;
    public Button level5Button;
    public Button controlsButton;
    public Button backButton;

    public Text controlsText;
    
    void Start()
    {
        level1Button.gameObject.SetActive(false);
        level2Button.gameObject.SetActive(false);
        level3Button.gameObject.SetActive(false);
        level4Button.gameObject.SetActive(false);
        level5Button.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);

        playButton.gameObject.SetActive(true);
        levelSelectButton.gameObject.SetActive(true);
        controlsButton.gameObject.SetActive(true);

        controlsText.text = "";
    }

    public void LevelSelect()
    {
        level1Button.gameObject.SetActive(true);
        level2Button.gameObject.SetActive(true);
        level3Button.gameObject.SetActive(true);
        level4Button.gameObject.SetActive(true);
        level5Button.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);

        playButton.gameObject.SetActive(false);
        levelSelectButton.gameObject.SetActive(false);
        controlsButton.gameObject.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        level1Button.gameObject.SetActive(false);
        level2Button.gameObject.SetActive(false);
        level3Button.gameObject.SetActive(false);
        level4Button.gameObject.SetActive(false);
        level5Button.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);

        playButton.gameObject.SetActive(true);
        levelSelectButton.gameObject.SetActive(true);
        controlsButton.gameObject.SetActive(true);

        controlsText.text = "";
    }

    public void ShowControls()
    {
        playButton.gameObject.SetActive(false);
        levelSelectButton.gameObject.SetActive(false);
        controlsButton.gameObject.SetActive(false);

        backButton.gameObject.SetActive(true);

        controlsText.text = "A: Move Paddle Left \n--------------------\n D: Move Paddle Right \n--------------------\n Space: Shoot Ball Straight \n--------------------\n Q + Space: Shoot Ball Left \n--------------------\n E + Space: Shoot Ball Right \n--------------------\n R: Reset Ball Position \n (Only Works When 1 Ball Is Active)";
    }
}
