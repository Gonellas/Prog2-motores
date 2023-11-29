using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private bool _level;
    [SerializeField] private bool _prototype;
    [SerializeField] private float _timeBetweenSpawns = 2.0f; 
    [SerializeField] private float _lastSpawnTime;

    private void Start()
    {
        _lastSpawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - _lastSpawnTime >= _timeBetweenSpawns)
        {
            SpawnArrow();
            _lastSpawnTime = Time.time;
        }
    }

    private void SpawnArrow()
    {
        GameObject instanciaPrefab = Instantiate(arrowPrefab, transform.position, Quaternion.identity);

        if(_level)
        instanciaPrefab.transform.rotation = Quaternion.Euler(0, 90, 90);

        if(_prototype)
        instanciaPrefab.transform.rotation = Quaternion.Euler(0, 0, 90);
    }
}