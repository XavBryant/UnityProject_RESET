using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSwitchingManager : MonoBehaviour {
    public GameObject theHero;
    public GameObject theLevel;
    public GameObject GameActive;
    public GameObject StartupCanvas;
    public GameObject UICamera;
    public int HeroNum;

    public Player player;


    void Start() {
        GameObject LVL = (theLevel);
        LVL.SetActive(false);

    }

    public void OnPlay(int HeroNum)
    {

        theHero = transform.GetChild(HeroNum).gameObject;
        theHero.SetActive(true);
        GameController.player = theHero.GetComponent<Player>();
        GameController.beta = theHero.GetComponent<Beta>();
        LVLstart();
    }


    public void LVLstart() {
        GameObject LVL = (theLevel);
        LVL.SetActive(true);

        GameActive.SetActive(true);

        StartupCanvas.SetActive(false);

        UICamera.SetActive(false);
    }


    //void SelectCharacterAlpha()
    //{
    //    GameObject prefabInstance = Instantiate(AlphaHero);

    //}

}
