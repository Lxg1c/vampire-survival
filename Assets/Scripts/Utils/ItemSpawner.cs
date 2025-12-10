using UnityEngine;

namespace Utils
{
    using UnityEngine;

    public class ItemSpawner : MonoBehaviour
    {
        public GameObject[] itemPrefabs;
        public float spawnInterval = 30f;
        public float spawnDistance = 20;

        private Camera cam;

        private void Start()
        {
            cam = Camera.main;
            InvokeRepeating(nameof(SpawnItem), 1f, spawnInterval);
        }

        private void SpawnItem()
        {
            if (itemPrefabs.Length == 0) return;

            float angle = Random.Range(0f, 360f);
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

            Vector3 spawnPos = cam.transform.position + direction * spawnDistance;
            spawnPos.z = 0;

            int randomItem = Random.Range(0, itemPrefabs.Length);
            Instantiate(itemPrefabs[randomItem], spawnPos, Quaternion.identity);
        }
    }
}
