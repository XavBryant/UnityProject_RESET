using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Beta : MonoBehaviour
{
    public GameObject ShieldObject;

    [Header("Shield Logic")]
    private float ShieldTimer = 0f;
    private bool ShieldBool = false;
    private float MaxShieldTime = 12f;
    public float CooldownVal = 5f;
    public bool BoolCooldown = false;

    public Player player;

    public float CurrentShieldTime { get { return ShieldTimer; } }
    public bool ShieldStatus { get { return ShieldBool; } }


    // Start is called before the first frame update
    void Start()
    {
       player = this.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // send shield on
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (ShieldBool == false && BoolCooldown == false)
            {
                UnityEngine.Debug.Log("Shield Turned On");
                ShieldBool = true;
            }
        // send shield off
        else
        {
            UnityEngine.Debug.Log("Shield Turned Off");
            ShieldObject.SetActive(false);
            ShieldBool = false;
        }

    }

        // turn shield on
        if (ShieldBool == true) {
            if (ShieldTimer >= 0)
            {
                ShieldObject.SetActive(true);
                ShieldTimer -= Time.deltaTime;
                if (ShieldTimer <= 0)
                {
                    UnityEngine.Debug.Log("Out Of Time");
                    ShieldObject.SetActive(false);
                    ShieldBool = false;
                    StartCoroutine(ShieldDelay());
                }
            }
        }

        //recharge shield
        if (ShieldBool == false && BoolCooldown == false && ShieldTimer != MaxShieldTime && ShieldTimer <= MaxShieldTime)
        {
            ShieldTimer += Time.deltaTime;
        }

    }

    public IEnumerator ShieldDelay() {
        BoolCooldown = true;
        yield return new WaitForSeconds(CooldownVal);
        BoolCooldown = false;
    }

}


