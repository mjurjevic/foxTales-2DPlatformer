using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public float waitToRespawn;

    public int gemsCollected; //varijabla koja ce nam biti brojac koliko smo pokupili gemova

    public string levelToLoad;

    public float timeInLevel;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
         timeInLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeInLevel += Time.deltaTime; // meri vreme provedeno na nivou, a kad je pauzirana igrica NE MERI vreme
    }


    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo()); // moramo da pokrenemo CoRoutine, tj RespawnCo da bi se spawnovao player
    }

    private IEnumerator RespawnCo()
    {

        PlayerController.instance.gameObject.SetActive(false);
        AudioManager.instance.PlaySFX(8);

        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed) ); // yield return ceka vrednost da bude true, stavljamo vrednost da cekamo malo pre no sto se respawnuje player
        
        UIController.instance.FadeToBlack(); //pozivamo funkciju da se pokrene

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .2f); //stavljamo da bude zadrzano vreme za koje ce se zacrneti ekran kad umremo dodajemo 2 sekunde 

        UIController.instance.FadeFromBlack(); // ovde pozivamo iz UIControllera funkciju FadeFromBlack() da nam vrati na normalno

        PlayerController.instance.gameObject.SetActive(true);


        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;

        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UIController.instance.UpdateHealthDisplay();
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo()); // pozivamo EndLevelCo funkciju
    }

    public IEnumerator EndLevelCo()
    {
        AudioManager.instance.PlayLevelVictory();

        PlayerController.instance.stopInput = true; //prestaju komande i nas player po defaultu ide desno

        CameraController.instance.stopFollow = true; //kamera prestaje da prati playera u trenutku kada se prodje kraj nivoa
        UIController.instance.levelCompleteText.SetActive(true); //prikazuje se tekst Level Complete

        yield return new WaitForSeconds(1.5f); //stavljamo wait period na 1.5 sekundu

        UIController.instance.FadeToBlack(); //pozivamo funkciju iz UI controllera FadeToBlack da nam pocrni ekran



        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + 1f); //nakon 3 sek da se ucita sledeca scena(nivo) komandom ispod

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1); //zadajemo mu int vrednost koja scena(nivo) je otkljucana spajamo 2 stringa
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name); // nakon zavrsenog nivoa zelimo da nam igrac bude na tom nivou u level menadzeru

        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems"))
        {

            if(gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems")) // sa ovim zelimo da proverimo ako nam je onoliko koliko smo skupili gemova vece nego sto smo mi kad smo igrali tren skupili onda update vrednsot, u sustini ovim ne zelimo da prikazemo ako u nivou skupimo manje nego prethodni put
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
            }

        }else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
        }


        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {

            if(timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time")) // isto vazi i za vreme
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
            }

        }else
        {

            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        }

        SceneManager.LoadScene(levelToLoad);
    }


    //tilemap, desni klik 2d pa izaberemo tilemap, unutar tilemapa stavimo layer na ground,sorting layer na world, dodajemo tilemap collider, composite collider, rigidbody2d promeniti na kinematic i u tilemap collider oznaciti used by composite
}
