using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LSUIController : MonoBehaviour
{

    public static LSUIController instance;
    public string mainMenu;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelInfoPanel;

    public Text levelName, gemsFound, gemsTarget, bestTime, timeTarget;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime)); //menjamo mu alfa vrednost tako sto stavljamo Mathf.MoveTowards koji prima tri parametra, trenutna vrednost, ona koju zelimo i brzinom kojom ce se promeniti
            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }
        else
        {
            if (shouldFadeFromBlack)
            {
                fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime)); //menjamo mu alfa vrednost tako sto stavljamo Mathf.MoveTowards koji prima tri parametra, trenutna vrednost, ona koju zelimo i brzinom kojom ce se promeniti
                if (fadeScreen.color.a == 0f)
                {
                    shouldFadeFromBlack = false;
                }
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }


    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }

    public void ShowInfo(MapPoint levelInfo)
    {
        levelName.text = levelInfo.levelName;

        gemsFound.text = "FOUND: " + levelInfo.gemsCollected;
        gemsTarget.text = "IN LEVEL: " + levelInfo.totalGems;

        timeTarget.text = "TARGET: " + levelInfo.targetTime + "s";

        if (levelInfo.bestTime == 0)
        {
            bestTime.text = "BEST: ----";
        }else
        {
            bestTime.text = "BEST: " + levelInfo.bestTime.ToString("F1") + "s";
        }

        levelInfoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        levelInfoPanel.SetActive(false); 
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
