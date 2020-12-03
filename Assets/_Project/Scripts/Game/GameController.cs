using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Game")]
    public static Player player;
    public static Beta beta;
    public GameObject enemyContainer;


    [Header("UI")]
    public Text healthText;
    public Text ammoText;
    public Text enemyText;
    public Text infoText;
    public Text shieldText;
    public int HeroNum;

    public int CurrentLVL;

    public bool LVLComplete = false;
    public float resetTimer = 3f;

    private void Awake()
    {
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
    }


    void Start() {
        infoText.gameObject.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }



    // Update is called once per frame
    void Update() {
        

        if (player != null)
        {
            healthText.text = "Health: " + player.Health;
            ammoText.text = "Ammo: " + player.Ammo;
            shieldText.text = "Shield On: " + beta.ShieldStatus + " Shield Time: " + beta.CurrentShieldTime;

            if (player.Killed == true)
            {
                if (CurrentLVL == 1) {
                    infoText.gameObject.SetActive(true);
                    infoText.text = "You Lose";
                    SceneManager.LoadScene("Level1");
                }
                if (CurrentLVL == 2) {
                    infoText.gameObject.SetActive(true);
                    infoText.text = "You Lose";
                    SceneManager.LoadScene("Level2");
                }
            }

        }

        int aliveEnemies = 0;
        foreach (Enemy enemy in enemyContainer.GetComponentsInChildren<Enemy>()) {
            if (enemy.Killed == false) {
                aliveEnemies++;
            }
        }
        enemyText.text = "Enemies: " + aliveEnemies;

        if (aliveEnemies == 0) {
            LVLComplete = true;
            infoText.gameObject.SetActive(true);
            infoText.text = "You Win! Good Job";
        }

        if (LVLComplete == true) {
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0) {
                if (CurrentLVL == 1) {
                    Cursor.visible = true;
                    SceneManager.LoadScene("Level2");

                }
                if (CurrentLVL == 2)
                {
                    SceneManager.LoadScene("Menu");
                    Cursor.visible = true;
                }
            }

        }


    }
}
