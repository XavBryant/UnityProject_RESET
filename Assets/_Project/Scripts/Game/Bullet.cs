using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 30f;
    public float lifeDuration = 2f;
    public int damage = 1;

    private float lifeTimer;
    private bool shotByPlayer;
    public bool ShotByPlayer { get { return shotByPlayer; } set { shotByPlayer = value; } }


    // Start is called before the first frame update
    void OnEnable()
    {
        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        // move
        transform.position += transform.forward * speed * Time.deltaTime;

        //check if bullet should be destroyed
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f) {
            gameObject.SetActive(false);
        }
    }
}
