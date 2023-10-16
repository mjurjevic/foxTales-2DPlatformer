using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;
    public Transform farBackground, middleBackground;

    public float minHeight, maxHeight;

    public bool stopFollow;

    //private float lastXpos;
    private Vector2 lastPos;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //lastXpos = transform.position.x; //pozicija kretanja
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        /*transform.position = new Vector3(target.position.x, target.position.y, transform.position.z); // ovde su podesavanja za pracenje kamere karaktera, x,y,z su ose po kojima ih prati, po ovim podesavanjima mi karaktera pratimo sa po x osi jer nema logike da ga pratimo po y sto je na gore/dole i z sto je u sirinu


        float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight); //ovim odredjujemo da clampedY dobija svoju visinu, sto znaci da bude u rangu izmedju min i max koji smo mu zadali u unity
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);*/ // a ovde zapravo dobijamo novu poziciju koja ce zapravo pratiti samo osu y, tj clampedY

        if(!stopFollow) // ovde pravimo da ne bi kamera pratila playera nakon sto prodje sipku gde se zavrsava igra
        {
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y,minHeight,maxHeight), transform.position.z); //samo smo skratili kod iznad stavili sve u jednu liniju, dobili smo odredjenu visinu gde ce kamera pratiti igraca


            //float amountToMoveX = transform.position.x - lastXpos; //ovde oduzimamo pocetnu poziciju sa pozicijom sa trenutnom pozicijom kretanja
            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);
            farBackground.position = farBackground.position + new Vector3(amountToMove.x, amountToMove.y, 0f); // brzina kretanja pozadine
            middleBackground.position += new Vector3(amountToMove.x,amountToMove.y, 0f) * .5f; // takodje brzina kretanja druge pozadine ali malo usporenije radi boljeg "dozivljaja"


            //lastXpos = transform.position.x;
            lastPos = transform.position;

        }
        
    }
}
