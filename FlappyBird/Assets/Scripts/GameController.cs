// GameController.cs

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Button playAgainButtonGameOver;
    public Button playAgainButtonWin;

    void Start()
    {
        // Agregar listeners a los botones
        playAgainButtonGameOver.onClick.AddListener(RestartGame);
        playAgainButtonWin.onClick.AddListener(RestartGame);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
