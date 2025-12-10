using Enemy;
using UnityEngine;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public GameObject[] itemDrops;
        public float itemDropChance = 0.2f;

        public float spawnDistance = 10f;

        public Camera cam;


        public void SpawnWave(int enemyCount, float hpMult, float dmgMult)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                SpawnEnemy(hpMult, dmgMult);
            }

            TrySpawnItem();
        }

        private void SpawnEnemy(float hpMult, float dmgMult)
        {
            Debug.Log("Spawner: Start SpawnEnemy");

            if (cam == null)
            {
                Debug.LogError("Spawner ERROR: Camera.main == NULL");
                return;
            }

            if (enemyPrefab == null)
            {
                Debug.LogError("Spawner ERROR: enemyPrefab == NULL");
                return;
            }

            float angle = Random.Range(0f, 360f);
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            Vector3 spawnPos = cam.transform.position + direction * spawnDistance;

            spawnPos.z = 0;

            Debug.Log("Spawner: Spawn position = " + spawnPos);

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            if (enemy == null)
            {
                Debug.LogError("Spawner ERROR: Instantiate returned NULL");
                return;
            }

            Debug.Log("Spawner: enemy instantiated: " + enemy.name);

            EnemyHealth hp = enemy.GetComponent<EnemyHealth>();
            Debug.Log("EnemyHealth component: " + (hp != null));

            if (hp != null)
                hp.health *= hpMult;

            EnemyAttack dmg = enemy.GetComponent<EnemyAttack>();
            Debug.Log("EnemyAttack component: " + (dmg != null));

            if (dmg != null)
                dmg.damage *= dmgMult;

            Debug.Log("Spawner: SpawnEnemy finished");
        }


        private void TrySpawnItem()
        {
            if (itemDrops.Length == 0) return;

            if (Random.value < itemDropChance)
            {
                int i = Random.Range(0, itemDrops.Length);
                Vector3 pos = cam.transform.position + new Vector3(
                    Random.Range(-3f, 3f),
                    Random.Range(-3f, 3f),
                    0
                );

                Instantiate(itemDrops[i], pos, Quaternion.identity);
            }
        }
    }
}
