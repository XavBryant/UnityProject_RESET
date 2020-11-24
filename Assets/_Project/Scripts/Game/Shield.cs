using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider otherCollidor)
    {
        UnityEngine.Debug.Log("Shield Collision"+ otherCollidor);
         if (otherCollidor.gameObject.GetComponent<Bullet>() != null)
        {
            UnityEngine.Debug.Log("Bullet Hit Shield" + otherCollidor);
            Bullet bullet = otherCollidor.GetComponent<Bullet>();
            if (bullet.ShotByPlayer == false)
            {
                bullet.gameObject.SetActive(false);
                UnityEngine.Debug.Log("BulletTurnedOff" + otherCollidor);
            }
        }
    }



}
