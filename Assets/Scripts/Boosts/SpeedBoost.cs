using UnityEngine;

using Player;

namespace Boosts
{
    public class SpeedPickup : PickupItem
    {
        public float speedBonus = 100f;

        protected override void OnPickup(PlayerStats stats)
        {
            stats.AddSpeed(speedBonus);
            Debug.Log("Picked up SPEED: +" + speedBonus);
        }
    }
}