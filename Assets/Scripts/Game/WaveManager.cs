using UnityEngine;
using System.Collections;
using UI;

namespace Game
{
    public class WaveManager : MonoBehaviour
    {
        [Header("Wave Settings")]
        public float timeBetweenWaves = 25f;
        public int maxWaves = 10;
        public bool winByTime ;
        
        [Header("Difficulty Scaling")]
        public int baseEnemyCount = 5;
        public float hpMultiplierPerWave = 1.2f;
        public float damageMultiplierPerWave = 1.15f;

        [Header("References")]
        public EnemySpawner spawner;
        public GameTimer gameTimer;

        [Header("UI")]
        public TMPro.TextMeshProUGUI waveText;
        public GameObject winCanvas;

        private int _currentWave;
        private bool _gameRunning = true;
        private Coroutine _waveRoutine;

        public int CurrentWave => _currentWave;
        public bool IsGameRunning => _gameRunning;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (gameTimer == null)
            {
                gameTimer = FindFirstObjectByType<GameTimer>();
            }

            if (winByTime && gameTimer != null)
            {
                gameTimer.OnTimeUp += WinGame;
            }

            _waveRoutine = StartCoroutine(WaveRoutine());
        }

        IEnumerator WaveRoutine()
        {
            while (_gameRunning)
            {
                if (CheckWinConditions())
                {
                    yield break;
                }

                yield return StartCoroutine(StartWave());
                
                if (CheckWinConditions())
                {
                    yield break;
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        private bool CheckWinConditions()
        {
            if (!_gameRunning) return true;

            if (winByTime && gameTimer != null && gameTimer.isTimeUp)
            {
                WinGame();
                return true;
            }

            if (!winByTime && _currentWave >= maxWaves)
            {
                WinGame();
                return true;
            }

            return false;
        }

        IEnumerator StartWave()
        {
            if (!_gameRunning) yield break;

            _currentWave++;
            
            if (waveText != null)
            {
                waveText.text = $"Волна: {_currentWave}/{maxWaves}";
            }
            
            var enemyCount = Mathf.RoundToInt(baseEnemyCount * Mathf.Pow(1.2f, _currentWave));
            var enemyHpMult = Mathf.Pow(hpMultiplierPerWave, _currentWave);
            var enemyDmgMult = Mathf.Pow(damageMultiplierPerWave, _currentWave);

            if (spawner != null)
            {
                spawner.SpawnWave(enemyCount, enemyHpMult, enemyDmgMult);
            }
            else
            {
                Debug.LogWarning("EnemySpawner не назначен в WaveManager!");
            }

            yield return null;
        }

        private void WinGame()
        {
            if (!_gameRunning) return;

            _gameRunning = false;
            
            if (_waveRoutine != null)
            {
                StopCoroutine(_waveRoutine);
            }

            Debug.Log("ПОБЕДА!");
        }

        private void OnDestroy()
        {
            if (gameTimer != null)
            {
                gameTimer.OnTimeUp -= WinGame;
            }
        }

        private void OnValidate()
        {
            if (spawner == null)
            {
                Debug.LogWarning("Назначьте EnemySpawner в инспекторе!");
            }
        }
    }
}