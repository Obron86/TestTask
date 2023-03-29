using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private CanvasRenderer gameOverPanel;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button restartButton;
    [SerializeField] private TouchJoystick touchJoystick;
    
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private GameObject gameControllerPrefab;
    [SerializeField] private GameObject playerControllerPrefab;
    [SerializeField] private GameObject enemySpawnerPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject mineSpawnerPrefab;
    [SerializeField] private GameObject vFXControllerPrefab;
    
    private GameController _gameController;
    private PlayerController _playerController;
    private EnemySpawner _enemySpawner;
    private GameObject _floor;
    private MineSpawner _mineSpawner;
    private VFXController _vfxController;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _floor = Instantiate(floorPrefab);
        _gameController = Instantiate(gameControllerPrefab).GetComponent<GameController>();
        _playerController = Instantiate(playerControllerPrefab).GetComponent<PlayerController>();
        _enemySpawner = Instantiate(enemySpawnerPrefab).GetComponent<EnemySpawner>();
        _mineSpawner = Instantiate(mineSpawnerPrefab).GetComponent<MineSpawner>();
        _vfxController = Instantiate(vFXControllerPrefab).GetComponent<VFXController>();

        _gameController.Initialize(_playerController, _vfxController, gameOverPanel, timerText, restartButton);
        _playerController.Initialize(_floor, touchJoystick);
        _mineSpawner.Initialize(_floor, minePrefab, _gameController.GameModel.mineCount);
        _enemySpawner.Initialize(_gameController, _floor, enemyPrefab, _playerController);
        _enemySpawner.SetEnemyPrefab(enemyPrefab);
    }

}
