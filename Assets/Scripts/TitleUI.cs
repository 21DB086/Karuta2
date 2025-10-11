using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    public void StartNormal() { GameSettings.Mode = 1; SceneManager.LoadScene("Play"); }
    public void StartHard() { GameSettings.Mode = 0; SceneManager.LoadScene("Play"); }
}