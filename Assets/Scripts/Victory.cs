using UnityEngine;

public class Victory : MonoBehaviour
{
    public SceneManagerController sceneManagerController; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sceneManagerController != null)
            {
                sceneManagerController.ChangeScene("Victory");
                sceneManagerController.SetCursorState(true);
            }
        }
    }
}
