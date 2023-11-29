using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _lever;
    [SerializeField] Animator _leverAnim;
    [SerializeField] GameObject _wallPrefab;
    private bool playerInRange = false;

    [SerializeField] Transform playerTransform;
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance < 2.0f && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        _leverAnim.SetBool("Open", true);
        _wallPrefab.GetComponent<Animator>().SetBool("Open", true);

        StartCoroutine(DestroyWall());
    }

   

    private IEnumerator DestroyWall()
    {
        yield return new WaitForSeconds(3f);
        Destroy(_wallPrefab);
    }

}
