using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //koristimo ovo da bi mogli da ucitamo odredjenu scenu

public class MainMenu : MonoBehaviour
{


    public string startScene, continueScene;

    public GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey(startScene + "_unlocked"))
        {
            continueButton.SetActive(true);
        } else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartGame()
    {
        SceneManager.LoadScene(startScene); // ovde pozivamo varijablu startScene da bi zapoceli tu odredjenu scenu, a u unity smo mu zadali ime scene koje ce ucitati

        PlayerPrefs.DeleteAll();
    }


    public void ContinueGame()
    {
        SceneManager.LoadScene(continueScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }

}
