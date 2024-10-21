using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Display
{
    public class ScenesController : MonoBehaviour
    {
        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        
        public static void QuitGame()
        {
            Application.Quit();
        }
    }
}