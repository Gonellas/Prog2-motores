using System.Collections;
using UnityEngine;

public class Lever : Interact
{ 
    [SerializeField] GameObject _lever;
    [SerializeField] Animator _leverAnim;
    [SerializeField] GameObject _wallPrefab;
    [SerializeField] AudioSource _audioLever;
    [SerializeField] AudioSource _audioWall;

    private void Start()
    {

        _audioWall = _wallPrefab.GetComponent<AudioSource>();
    }

    public override void InteractionAction()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        _leverAnim.SetBool("Open", true);
        _wallPrefab.GetComponent<Animator>().SetBool("Open", true);
        _audioLever.Play();
        _audioWall.Play();

        IMessage interactMessage = _player.GetComponent<IMessage>();
        if (interactMessage != null) interactMessage.DeactivateUI();

        StartCoroutine(DestroyWall());
    }

    private IEnumerator DestroyWall()
    {
        yield return new WaitForSeconds(3f);
        Destroy(_wallPrefab);
    }

}
