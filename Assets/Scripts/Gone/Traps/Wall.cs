using System.Collections;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Transform objetoAMover;
    public float distanciaMaximaZ = 5.0f;
    public float distanciaMinimaZ = 0.0f;
    public float velocidad = 1.0f;

    private Vector3 posicionInicial;
    private bool seMovio = false;

    private void Start()
    {
        if (objetoAMover == null)
        {
            Debug.LogError("Por favor, asigna el objeto a mover en el Inspector.");
            enabled = false;
            return;
        }

        posicionInicial = objetoAMover.position;
        StartCoroutine(MoverObjeto());
    }

    private IEnumerator MoverObjeto()
    {
        while (!seMovio)
        {
            float tiempo = Mathf.PingPong(Time.time * velocidad, 1);
            objetoAMover.position = Vector3.Lerp(posicionInicial + Vector3.forward * distanciaMinimaZ, posicionInicial + Vector3.forward * distanciaMaximaZ, tiempo);

            if (tiempo >= 0.99f) // Cambiamos la condición de salida para ser menos restrictiva.
            {
                seMovio = true;
            }

            yield return null;
        }
    }
}