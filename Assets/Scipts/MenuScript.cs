using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Asteroids");
    }
}