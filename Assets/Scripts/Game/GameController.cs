using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    private VFXController _vfxController;
    
    private TextMeshProUGUI _timerText;
    private CanvasRenderer _gameOverPanel;
    private Button _restartButton;
    private float _startTime;
    private bool _gameIsOver;

    private void Start()
    {
        _startTime = Time.time;
        _gameOverPanel.gameObject.SetActive(false);
    }

    private void GameOver()
    {
        _gameIsOver = true;
        _gameOverPanel.gameObject.SetActive(true);
        var timeElapsed = Time.time - _startTime;
        _timerText.text = $"Time: {timeElapsed.ToString("F2")}s";
    }

    private void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public GameModel GameModel { get; private set; }

    private PlayerController PlayerController { get; set; }

    public void Initialize(PlayerController playerController, VFXController vfxController, CanvasRenderer gameOverPanel, TextMeshProUGUI timerText, Button restartButton)
    {
        PlayerController = playerController;
        GameModel = new GameModel();
        PlayerController.OnPlayerCollision += HandlePlayerCollision;
        _vfxController = vfxController;
        _gameOverPanel = gameOverPanel;
        _timerText = timerText;
        _restartButton = restartButton;
        _restartButton.onClick.AddListener(RestartGame);
    }
    
    private void HandlePlayerCollision()
    {
        StartCoroutine(PlayerCollisionSequence());
    }

    private IEnumerator PlayerCollisionSequence()
    {
        if (_gameIsOver) yield break;
        _vfxController.SpawnExplosion(PlayerController.transform.position);
        _gameIsOver = true;
        GlobalGameEvents.GameOver.Invoke();

        yield return new WaitForSeconds(2f);

        GameOver();
    }

    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(RestartGame);
    }
}