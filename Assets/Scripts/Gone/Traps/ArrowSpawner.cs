using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;
    public float tiempoEntreSpawns = 2.0f; // Intervalo de tiempo entre spawneo

    private float tiempoUltimoSpawn;

    private void Start()
    {
        tiempoUltimoSpawn = Time.time;
    }

    private void Update()
    {
        // Verifica si es tiempo de spawnear una flecha
        if (Time.time - tiempoUltimoSpawn >= tiempoEntreSpawns)
        {
            SpawnArrow();
            tiempoUltimoSpawn = Time.time;
        }
    }

    private void SpawnArrow()
    {
        // Instancia el prefab
        GameObject instanciaPrefab = Instantiate(arrowPrefab, transform.position, Quaternion.identity);

        // Ajusta la rotaci�n en el eje Y a 90 grados
        instanciaPrefab.transform.rotation = Quaternion.Euler(0, 0, 90);
    }
}