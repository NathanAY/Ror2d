﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    public GameObject stoneGolem;
    private Player _player;
    
    void Start()
    {
        _player = Player.Instance;
        Invoke("SpawnEnemy", 2);
    }
    
    private void SpawnEnemy()
    {
        int range = Random.Range(10, 150);
        Instantiate(stoneGolem, _player.transform.position + new Vector3(range, range, 0), Quaternion.identity);
        Invoke("SpawnEnemy", Random.Range(1, 5));
    } 
}