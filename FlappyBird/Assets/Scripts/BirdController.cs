// BirdController.cs

using UnityEngine;
using TMPro;

public class BirdController : MonoBehaviour
{
    public float jumpForce = 4f;
    private Rigidbody2D rb;
    private bool isGameStarted = false;
    private int currentScore = 0;
    private int bestScore = 0;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private Canvas winCanvas;
    [SerializeField] private TextMeshProUGUI WinscoreText;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private TextMeshProUGUI GameOverscoreText;
    
    [SerializeField] private AudioSource clamSound;
    [SerializeField] private AudioSource flySound;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource gameOverSound;

    public bool IsGameStarted
    {
        get { return isGameStarted; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // Cargar el mejor puntaje guardado
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateBestScoreText();

        UpdateScoreText();
    }

    private bool hasJumpedOnce = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            isGameStarted = true;
            rb.simulated = true;
            rb.velocity = Vector2.up * jumpForce;
            Debug.Log("Salto activado");

            ObstacleSpawner obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
            if (!hasJumpedOnce && obstacleSpawner != null)
            {
                obstacleSpawner.StartSpawningObstacles();
            }

            hasJumpedOnce = true;
            
            // Reproducir el sonido de "Fly" al dar clic para volar
            flySound.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MorePoint"))
        {
            IncreaseScore();
            other.gameObject.SetActive(false);
            
            // Reproducir el sonido de "Clam" al ganar un punto
            clamSound.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacles") || collision.gameObject.CompareTag("Limits"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            // Guardar el mejor puntaje
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save(); // Asegurar que los datos se guarden inmediatamente
            UpdateBestScoreText();
        }

        if (currentScore >= bestScore)
        {
            Debug.Log("Win");
            // Muestra el canvas de Win y configura el texto del marcador
            winCanvas.gameObject.SetActive(true);
            WinscoreText.text = "Score: " + currentScore;
            
            // Reproducir el sonido de "Win" al ganar
            winSound.Play();
        }
        else
        {
            Debug.Log("Game Over");
            // Muestra el canvas de Game Over y configura el texto del marcador
            gameOverCanvas.gameObject.SetActive(true);
            GameOverscoreText.text = "Score: " + currentScore;
            
            // Reproducir el sonido de "GameOver" al perder
            gameOverSound.Play();
        }

        FindObjectOfType<ObstacleSpawner>().StopSpawningObstacles();
    }

    public void IncreaseScore()
    {
        currentScore++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore;
    }

    private void UpdateBestScoreText()
    {
        bestScoreText.text = "Best Score: " + bestScore;
    }
}
