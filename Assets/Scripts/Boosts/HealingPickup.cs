using Player;
using UnityEngine;

namespace Boosts
{
    public class HealingPickup : PickupItem
    {
        public float healAmount = 20f;

        protected override void OnPickup(PlayerStats stats)
        {
            PlayerHealth health = stats.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.Heal(healAmount);
                Debug.Log("Picked up HEAL: +" + healAmount);
            }
        }
    }
}