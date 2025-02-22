using UnityEngine;
using UnityEngine.SceneManagement;

/*
    Script to load scenes given the needed scene name in the inspector
*/

public class Scene : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
