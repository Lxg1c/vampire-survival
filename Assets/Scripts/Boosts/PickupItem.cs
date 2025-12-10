using Player;
using UnityEngine;

namespace Boosts
{
    public abstract class PickupItem : MonoBehaviour
    {
        protected abstract void OnPickup(PlayerStats player);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerStats stats = collision.GetComponent<PlayerStats>();
                if (stats != null)
                {
                    OnPickup(stats);
                    Destroy(gameObject);
                }
            }
        }
    }
}
