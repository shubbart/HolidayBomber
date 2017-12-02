using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpawn : MonoBehaviour
{
    private static CameraSpawn _instance;
    public static CameraSpawn instance
    { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
            _instance = this;
        else
            Destroy(this);
    }

    public void SpawnOnPlayer()
    {
        Player player = PlayerManager.localPlayer;
        Vector3 temp = player.transform.position;   
        temp.y += 4.25f;
        if(temp.z < 0)
            temp.z -= 2.5f;
        else
            temp.z += 2.5f;

        transform.position = temp;
        transform.forward = player.gameObject.transform.forward;
        transform.Rotate(25, 0, 0);
    }
}
