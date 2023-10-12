using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private bool isPlayerOnPlatform = false;
    private bool isFalling = false;
    private float fallTimer = 3.0f; 
    public GameObject objectToDeactivate;
    SceneManagerController sceneManagerController;

    private void Start()
    {
        sceneManagerController = FindObjectOfType<SceneManagerController>();
    }

    private void Update()
    {
        if (isPlayerOnPlatform && !isFalling)
        {
            fallTimer -= Time.deltaTime;
            if (fallTimer <= 0)
            {
                Fall();
                Debug.Log("Cayendo");
            }
        } 


    }

    private void Fall()
    {
        isFalling = true;

        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }

        gameObject.SetActive(false);

        if (isPlayerOnPlatform && isFalling)
        {
            Debug.Log("Mori");
            Invoke("RestartLevel", 3.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Colision");
            isPlayerOnPlatform = true;            
          
        }       
    }

    private void RestartLevel()
    {
        sceneManagerController.RestartLevel();
    }
}