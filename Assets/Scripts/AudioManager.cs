using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance; // pravimo da bi mogli i na drugim skriptama da koristimo ovu skriptu


    public AudioSource[] soundEffects;

    public AudioSource bgm, levelEndMusic, bossMusic;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int soundToPlay)
    {
        soundEffects[soundToPlay].Stop(); //ovo stavljamo u slucaju da imamo vise neprijatelja tj ubijemo vise neprijatelja odjednom

        soundEffects[soundToPlay].pitch = Random.Range(.9f, 1.1f); //da ne bude jedno te isti zvuk kada se nesto ponavlja vec da budu razliciti tj random pitch da imaju

        soundEffects[soundToPlay].Play(); //ovde nam soundToPlay zapravo sluzi kao neka varijabla za broj numere koju cemo da pustamo, primera radi ako igrac skace..
    }

    public void PlayLevelVictory()
    {
        bgm.Stop();
        levelEndMusic.Play();
    }

    public void PlayBossMusic()
    {
        bgm.Stop();
        bossMusic.Play();
    }

    public void StopBossMusic() 
    {
        bossMusic.Stop();
        bgm.Play();
    }
}
