using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public GameObject[] allMenues;
    public GameObject wantedActiveMenu;

    private void Start()
    {
        if (allMenues.Length > 0)
        {
            foreach (GameObject menu in allMenues)
            {
                menu.SetActive(false);
            }
        }

        if (wantedActiveMenu != null) wantedActiveMenu.SetActive(true);

        SetCursorState(true);
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SetCursorState(bool isVisible)
    {
        Cursor.visible = isVisible;
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}

