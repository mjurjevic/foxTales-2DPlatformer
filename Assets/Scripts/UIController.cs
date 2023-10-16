using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    public Image heart1, heart2, heart3;

    public Sprite heartFull,heartHalf, heartEmpty;

    public Text gemText;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelCompleteText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        UpdateGemCount(); // pozivamo ovu funkciju da bi nam u unity umesto 999 restartovao vrednost i krenuo od 0 da broji
        FadeFromBlack(); // pozivamo funkciju da kada pokrenemo igricu dobijamo taj efekat da iz crnine ulazimo u igricu
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime)); //menjamo mu alfa vrednost tako sto stavljamo Mathf.MoveTowards koji prima tri parametra, trenutna vrednost, ona koju zelimo i brzinom kojom ce se promeniti
            if(fadeScreen.color.a == 1f)
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

    public void UpdateHealthDisplay()
    {
        switch (PlayerHealthController.instance.currentHealth) //dohvatamo iz playerhealthcontrollera funkciju currentHealth i zadajemo mu slucajeve ako pogodi trigger(u nasem slucaju ostrice)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;

            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                break;

            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                break;

            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                break;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

        }


    }


    public void UpdateGemCount()
    {
        gemText.text = LevelManager.instance.gemsCollected.ToString(); //prikazuje nam text u gornjem desnom cosku za gemove koliko smo skupili
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
}
