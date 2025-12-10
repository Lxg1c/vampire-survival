using TMPro;
using Player;
using UnityEngine;

namespace UI
{
    public class Stats : MonoBehaviour
    {
        public PlayerStats stats;

        public TextMeshProUGUI healthText;
        public TextMeshProUGUI speedText;
        public TextMeshProUGUI damageText;

        private void Start()
        {
            if (stats == null)
                stats = FindFirstObjectByType<PlayerStats>();

            UpdateUI();
        }

        private void Update()
        {
            UpdateUI();
        }

        public void UpdateUI()
        {
            healthText.text = "Здоровье: " + Mathf.RoundToInt(stats.health) + "/" + Mathf.RoundToInt(stats.maxHealth);
            speedText.text = "Скорость: " + stats.moveSpeed.ToString("0.0");
            damageText.text = "Сила: " + stats.damage.ToString("0.0");
        }
    }
}
