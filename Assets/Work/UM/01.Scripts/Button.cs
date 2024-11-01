using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{


    public void SceneChange(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void Continue(GameObject ui)
    {
          ui.SetActive(false);
    }

    public void End()
    {
        Application.Quit();
    }
}
