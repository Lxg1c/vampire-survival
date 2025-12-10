using UnityEngine;
using System;

namespace UI
{
    public class GameTimer : MonoBehaviour
    {
        [Header("UI Reference")] 
        public TMPro.TextMeshProUGUI timerTextTMP;

        [Header("Timer Settings")] 
        public bool countUp = true;
        public float startTime;
        public float timeLimit = 300f;

        private float _currentTime;
        private bool _isRunning = true;

        public event Action OnTimeUp;
        public event Action<float> OnTimeChanged;

        public float currentTime => _currentTime;
        public float remainingTime => countUp ? _currentTime : Mathf.Max(0, timeLimit - _currentTime);
        public bool isTimeUp => !countUp && _currentTime <= 0;
        public bool isRunning => _isRunning;

        private void Start()
        {
            ResetTimer();
        }

        private void Update()
        {
            if (!_isRunning) return;

            if (countUp)
            {
                _currentTime += Time.deltaTime;
            }
            else
            {
                _currentTime -= Time.deltaTime;

                if (_currentTime <= 0)
                {
                    _currentTime = 0;
                    _isRunning = false;
                    OnTimeUp?.Invoke();
                    Debug.Log("Time's up!");
                }
            }

            OnTimeChanged?.Invoke(_currentTime);
            UpdateTimerDisplay();
        }

        private void UpdateTimerDisplay()
        {
            if (timerTextTMP == null) return;

            if (countUp)
            {
                TimeSpan time = TimeSpan.FromSeconds(_currentTime);
                timerTextTMP.text = $"{time.Minutes:00}:{time.Seconds:00}";
            }
            else
            {
                float remaining = Mathf.Max(0, timeLimit - _currentTime);
                TimeSpan time = TimeSpan.FromSeconds(remaining);
                timerTextTMP.text = $"{time.Minutes:00}:{time.Seconds:00}";
            }
        }

        public void ResetTimer()
        {
            _currentTime = countUp ? startTime : timeLimit;
            _isRunning = true;
            UpdateTimerDisplay();
        }

        public void PauseTimer()
        {
            _isRunning = false;
        }

        public void ResumeTimer()
        {
            _isRunning = true;
        }

        public void StopTimer()
        {
            _isRunning = false;
        }

        private void OnValidate()
        {
            if (timerTextTMP == null)
            {
                Debug.LogWarning("Assign timer text UI element!");
            }
        }
    }
}