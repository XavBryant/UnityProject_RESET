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

    public int LVLNUM = 0;

    public object LVL1;

    public bool LVLComplete = false;
    public float resetTimer = 3f;




    void Start() {
        infoText.gameObject.SetActive(false);
        DontDestroyOnLoad(this);
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
                LVLComplete = true;
                infoText.gameObject.SetActive(true);
                infoText.text = "You Lose";
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
                if (LVLNUM == 0) {
                    LVL1.SetActive(false);
                    LVLNUM = 1;
                }



                SceneManager.LoadScene("Menu");
                Cursor.visible = true;
            
            
            
            }

        }


    }
}
