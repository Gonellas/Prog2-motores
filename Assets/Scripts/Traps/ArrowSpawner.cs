using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;
    public float tiempoEntreSpawns = 2.0f; 

    private float tiempoUltimoSpawn;

    private void Start()
    {
        
        tiempoUltimoSpawn = Time.time;
    }

    private void Update()
    {
        if (Time.time - tiempoUltimoSpawn >= tiempoEntreSpawns)
        {
            SpawnArrow();
            tiempoUltimoSpawn = Time.time;
        }
    }

    private void SpawnArrow()
    {
        GameObject instanciaPrefab = Instantiate(arrowPrefab, transform.position, Quaternion.identity);

        instanciaPrefab.transform.rotation = Quaternion.Euler(0, 90, 90);
    }
}