using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBomb : MonoBehaviour
{
    float force;
    public float dir;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bomb")
        {
            if (!other.gameObject.GetComponent<Bomb>().moving)
            {
                other.transform.rotation = transform.rotation;
                force = other.gameObject.GetComponent<Bomb>().speed;
                other.GetComponent<Rigidbody>().AddForce(Vector3.forward * force * dir);
                other.gameObject.GetComponent<Bomb>().moving = true;
            }
        }
    }
}
