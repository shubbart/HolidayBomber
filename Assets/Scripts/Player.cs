using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    public GameObject smallBomb;
    public GameObject bomb;
    public GameObject largeBomb;

    public GameObject currentBomb;

    [SyncVar(hook = "HealthCheck")] public float health = 1;
    public float maxHealth;
    [SyncVar]public float money;

    float timer;
    float timerReset = 1.5f;

    void HealthCheck(float h)
    {
        bool alive = h > 0;
        if (alive && h <= 0)
            Death();
    }

    void Death()
    {

    }

    void Start ()
    {
        health = maxHealth;
        timer = timerReset;
        PlayerManager.players.Add(this);
        if (isLocalPlayer)
            PlayerManager.localPlayer = this;

        if (isLocalPlayer)
            CameraSpawn.instance.SpawnOnPlayer();

        if (isServer)
            health = maxHealth;
    }

    void Update()
    {
        if(isLocalPlayer)
            BombSelect();

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            GainMoney();
            timer = timerReset;
        }
    }

    void BombSelect()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            CmdBombSelect(0);
        if (Input.GetKeyUp(KeyCode.Alpha2))
            CmdBombSelect(1);
        if (Input.GetKeyUp(KeyCode.Alpha3))
            CmdBombSelect(2);
    }

    public void SpawnBomb(Vector3 pos, Vector3 dp)
    {
        //if (money - currentBomb.GetComponent<Bomb>().cost >= 0)
        //{
            CmdSpawnBomb(pos, dp);
            money -= currentBomb.GetComponent<Bomb>().cost;
        //}
    }

    void GainMoney()
    {
        money += 5;
    }

    [Command]
    void CmdSpawnBomb(Vector3 pos, Vector3 dp)
    {
        GameObject spawnedBomb = Instantiate(currentBomb, pos + dp, Quaternion.identity);
        NetworkServer.Spawn(spawnedBomb);
    }

    [Command]
    void CmdBombSelect(int bombID)
    {
        switch (bombID)
        {
            case 0:
                currentBomb = smallBomb;
                break;
            case 1:
                currentBomb = bomb;
                break;
            case 2:
                currentBomb = largeBomb;
                break;
        }

        Debug.Log(bomb == null ? "null" : "not null");
    }
}
