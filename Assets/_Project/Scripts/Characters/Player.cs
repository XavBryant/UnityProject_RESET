using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Visuals")]
    public Camera playerCamera;

    [Header("Gameplay")]
    public int initialHealth = 100;
    public int initialAmmo = 20;
    public float hurtDuration = 0.5f;

    public int damage = 20;

    private int health;
    public int Health { get { return health; } }

    private int ammo;
    public int Ammo { get { return ammo; } }

    private bool isHurt = false;
    private int knockbackForce = 10;

    private bool killed = false;
    public bool Killed { get { return killed; } }

    GameObject hazard;

    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
        ammo = initialAmmo;
    }




    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (ammo > 0 && Killed == false)
            {
                ammo--;

                GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet(true);
                bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
                bulletObject.transform.forward = playerCamera.transform.forward;
            }
        }
    }


    ///Crosshair
    void OnGUI(){GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 12, 12), "");}


    //Check For Collisions
    void OnTriggerEnter(Collider otherCollidor)
    {

        if (isHurt == false)
        {
            hazard = null;
        }

        if (otherCollidor.gameObject.GetComponent<AmmoCrate>() != null) {
            //collect ammo crates
            AmmoCrate ammoCrate = otherCollidor.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;
            Destroy(ammoCrate.gameObject);}


         else if (otherCollidor.gameObject.GetComponent<HealthCrate>() != null) {
            //collect health crates
            HealthCrate healthCrate = otherCollidor.GetComponent<HealthCrate>();
            health += healthCrate.health;
            Destroy(healthCrate.gameObject);
        }


        if (otherCollidor.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = otherCollidor.GetComponent<Enemy>();
            if (enemy.Killed == false)
            {

                hazard = enemy.gameObject;
                health -= enemy.damage;
            }
        }


        if (otherCollidor.gameObject.GetComponent<Spike>() != null) {
            health -= health;
        
        }


        else if (otherCollidor.gameObject.GetComponent<Bullet>() != null)
        {
            UnityEngine.Debug.Log("HitByBullet1");
            Bullet bullet = otherCollidor.GetComponent<Bullet>();
            if (bullet.ShotByPlayer == false)
            {
                UnityEngine.Debug.Log("ApplyDamage");
                hazard = bullet.gameObject;
                health -= bullet.damage;
                bullet.gameObject.SetActive(false);
            }
        }

        if (hazard != null)
        {
            isHurt = true;
            //perform knockback effect
            Vector3 hurtDirection = (transform.position - hazard.transform.position).normalized;
            Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
            GetComponent<ForceReceiver>().AddForce(knockbackDirection, knockbackForce);

            StartCoroutine(HurtRoutine());
        }
        if (health <= 0) {
            if (killed == false) {
                killed = true;
                this.GetComponent<UnityStandardAssets.Characters.FirstPerson.FPSController>().enabled = false;
                OnKill();
            }
        }    
    
    }
    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(hurtDuration);
        isHurt = false;
    }
    private void OnKill() {
        this.enabled = false;
    }
}
