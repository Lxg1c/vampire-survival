using Player;
using UnityEngine;

namespace Boosts
{
    public class DamagePickup : PickupItem
    {
        public float damageBonus = 2f;

        protected override void OnPickup(PlayerStats player)
        {
            player.AddDamage(damageBonus);
            Debug.Log("Picked up DAMAGE: +" + damageBonus);
        }
    }
}
