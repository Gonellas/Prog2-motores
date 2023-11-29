using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _lever;
    [SerializeField] Animator _leverAnim;
    [SerializeField] GameObject _wallPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenDor();
        }
    }

    public void OpenDor()
    {
        _leverAnim.SetBool("Open", true);
        _wallPrefab.GetComponent<Animator>().SetBool("Open", true);

        StartCoroutine(DestroyWall());
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("abrete sesamo");
        if (other.gameObject.CompareTag("Player"))
        {
            OpenDor();
        }
    }

    private IEnumerator DestroyWall()
    {
        yield return new WaitForSeconds(3f);
        Destroy(_wallPrefab);
    }

}
