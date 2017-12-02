using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BombSpawner : NetworkBehaviour
{
    public float dropHeight;
    Vector3 dropPoint;
    public Player player;

    void Start ()
    {
        dropPoint = new Vector3(0, dropHeight, 0);
	}

    void OnMouseUp()
    {
        Debug.Log("Up");
        SummonBomb();
    }

    void SummonBomb()
    {
        player = PlayerManager.localPlayer;     
        //if(player.GetComponent<Player>().money - player.GetComponent<Player>().currentBomb.GetComponent<Bomb>().cost >= 0)
            player.SpawnBomb(transform.position, dropPoint);
    }
}
