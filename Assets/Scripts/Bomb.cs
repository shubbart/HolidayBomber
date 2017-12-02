using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bomb : NetworkBehaviour
{
    public float speed;
    public float damage;
    public float cost;
    public bool moving;
    Rigidbody rbody;

    [SyncVar(hook = "HealthCheck")] public float health = 1;
    public float maxHealth;

    void HealthCheck(float h)
    {
        bool alive = health > 0;
        if (alive && h <= 0)
            Death();
    }

    void Death()
    {
        Destroy(gameObject);
    }


	void Start ()
    {
        health = maxHealth;
        rbody = GetComponent<Rigidbody>();
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bomb")
        {
            other.gameObject.GetComponent<Bomb>().health -= damage;
            health -= other.gameObject.GetComponent<Bomb>().damage;
        }

        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().health -= damage;
            other.gameObject.GetComponent<Player>().money += 3;
            Destroy(gameObject);
        }
    }
}
