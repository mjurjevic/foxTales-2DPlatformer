using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{

    public LSPlayer thePlayer;


    private MapPoint[] allPoints;
    // Start is called before the first frame update
    void Start()
    {
        allPoints = FindObjectsOfType<MapPoint>(); //trazimo sve objekte mappoint
        if(PlayerPrefs.HasKey("CurrentLevel")) //proveravamo jel ima kljuc
        {
            foreach(MapPoint point in allPoints)
            {
                if(point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    thePlayer.transform.position = point.transform.position;
                    thePlayer.currentPoint = point;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo()); //moramo da pozovemo tu funkciju
    }

    public IEnumerator LoadLevelCo()
    {

        AudioManager.instance.PlaySFX(4);
        LSUIController.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / LSUIController.instance.fadeSpeed) + .25f);

        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
    }
}
