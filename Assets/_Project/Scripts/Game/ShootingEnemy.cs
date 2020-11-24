using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : Enemy 
{

    public AudioSource deathSound;

    public float shootingInterval = 2f;
    public float shootingDistance = 20f;
    public float chasingInterval = .5f;
    public float chasingDistance = 16f;

    public int heat = 5;
    public int EnemyDamage = 5;

    private Player player;
    private float shootingTimer;
    private float chasingTimer;

    public GameObject ExplosionEffect;

    private NavMeshAgent agent;



    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindObjectOfType<Player>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(player.transform.position);
    }

    // Update is called once per frame
    void Update() {

        if (this.enabled)
        {
            if (player.Killed == true)
            {
                agent.enabled = false;
                this.enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
            }

            /// shooting logic
            shootingTimer -= Time.deltaTime;
            if (shootingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= shootingDistance)
            {
                shootingTimer = shootingInterval;

                GameObject bullet = ObjectPoolingManager.Instance.GetBullet(false);
                bullet.transform.position = transform.position;
                bullet.transform.forward = (player.transform.position - transform.position).normalized;
            }
            ///chasing logic
            chasingTimer -= Time.deltaTime;
            if (chasingTimer <= 0 && Vector3.Distance(transform.position, (player.transform.position)) <= chasingDistance)
            {
                chasingTimer = chasingInterval;
                agent.SetDestination(player.transform.position);
            }

            if (health <= 0) {
                OnKill();
            }

        }
    }


    void OnTriggerEnter(Collider otherCollidor)
    {
        UnityEngine.Debug.Log("Detected Collision");

        if (otherCollidor.gameObject.GetComponent<Bullet>() != null)
        {
            UnityEngine.Debug.Log("Enemy Got hit by bullet");
            Bullet bullet = otherCollidor.GetComponent<Bullet>();
            if (bullet.ShotByPlayer == true)
            {
                UnityEngine.Debug.Log("Apply Damage To Enemy");
                health -= bullet.damage;
                bullet.gameObject.SetActive(false);
            }
        }
    }



    protected override void OnKill() {
        deathSound.Play();
        Instantiate(ExplosionEffect, transform.position, transform.rotation);
        agent.enabled = false;
        this.enabled = false;
        transform.localEulerAngles = new Vector3(10, transform.localEulerAngles.y, transform.localEulerAngles.z);
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay() {
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }

}
