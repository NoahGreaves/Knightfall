using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;            // prefab to spawn
    [SerializeField] private int _maxTime = 5;              // max spawn time
    [SerializeField] private int _minTime = 2;              // min spawn time 
    private int _spawnTime;                // the time to spawn an object

    private float _lastSpawnTime;

    private void SetRandomTime()
    {
        // sets a random time between the min and max time to spawn a platform
        _spawnTime = Random.Range(_minTime, _maxTime);
    }

    // spawns the platform and resets the current time 
    private void SpawnPlatform()
    {
        Instantiate(GetRandomPrefab(), transform.position, Quaternion.identity);
    }

    // spawns the prefabs at random intervals and spawns random ground assets fro man array
    private GameObject GetRandomPrefab()
    {
        int randomIndex = Random.Range(0, _prefabs.Length);
        return _prefabs[randomIndex];
    }

    private void Update()
    {
        // the random spawn time and the random platform to spawn
        if(_lastSpawnTime + _spawnTime <= Time.timeSinceLevelLoad)
        {
            _lastSpawnTime = Time.timeSinceLevelLoad;
            SetRandomTime();
            SpawnPlatform();
        }
    }
}
