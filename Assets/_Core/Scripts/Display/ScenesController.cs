using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Display
{
    public class ScenesController : MonoBehaviour
    {
        public static void LoadScene(SceneAsset scene)
        {
            SceneManager.LoadScene(scene.name);
        }
        
        public static void QuitGame()
        {
            Application.Quit();
        }
    }
}