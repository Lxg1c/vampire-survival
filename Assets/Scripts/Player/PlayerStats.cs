using UnityEngine;

namespace Player
{
    

    public class PlayerStats : MonoBehaviour
    {
        public float health = 100f;
        public float maxHealth = 100f;

        public float moveSpeed = 5f;
        public float damage = 10f;

        public void Heal(float amount)
        {
            health = Mathf.Min(health + amount, maxHealth);
        }

        public void AddSpeed(float amount)
        {
            moveSpeed += amount;
        }

        public void AddDamage(float amount)
        {
            damage += amount;
        }
    }

}