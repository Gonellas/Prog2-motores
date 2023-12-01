using System.Collections;
using UnityEngine;

public class Lever : Interact
{ 
    [SerializeField] GameObject _lever;
    [SerializeField] Animator _leverAnim;
    [SerializeField] GameObject _wallPrefab;
    [SerializeField] AudioSource _audioLever;
    [SerializeField] AudioSource _audioWall;

    new private void Start()
    {
        base.Start();


        _audioWall = _wallPrefab.GetComponent<AudioSource>();
    }

    protected override void InteractionAction()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);

        if (distance < 2.0f && Input.GetKey(KeyCode.E))
        {
            _leverAnim.SetBool("Open", true);
            _wallPrefab.GetComponent<Animator>().SetBool("Open", true);
            _audioLever.Play();
            _audioWall.Play();

            StartCoroutine(DestroyWall());
        }  
    }

    private IEnumerator DestroyWall()
    {
        yield return new WaitForSeconds(3f);
        Destroy(_wallPrefab);
    }

}
