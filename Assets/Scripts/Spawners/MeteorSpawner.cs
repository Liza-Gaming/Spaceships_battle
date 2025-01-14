using System.Collections;
using UnityEngine;

/**
 * This class is responsible for spawning meteors in a rain-like manner 
 * at specified intervals and positions.
 * I used Chat GPT to impliment this idea
 */

public class MeteorSpawner : MonoBehaviour
{
    [Tooltip("The meteor prefab to be instantiated during the meteor rain.")]
    [SerializeField] private GameObject[] meteorPrefabs;

    [Tooltip("Time between each meteor rain event in seconds.")]
    [SerializeField] private float spawnInterval = 5f;

    [Tooltip("Number of meteors to spawn during each rain event.")]
    [SerializeField] private int meteorsPerInterval = 5;

    [Tooltip("Vertical range for spawning meteors. This defines how high/low the meteors can appear.")]
    [SerializeField] private float spawnRangeY = 10f;

    [Tooltip("X coordinate from which the meteors will spawn (left side of the screen).")]
    [SerializeField] private float spawnXPosition = -10f;

    [Tooltip("Delay between the spawning of each meteor within the same interval.")]
    [SerializeField] private float spawnDelay = 0.2f;

    private float timer; // Timer to track when to spawn the next meteor rain

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            StartCoroutine(SpawnMeteorRain()); // Start spawning meteors when timer reaches 0
            timer = spawnInterval; // Reset the timer for the next rain event
        }
    }

    // Coroutine that handles the actual spawning of meteors.
    private IEnumerator SpawnMeteorRain()
    {
        for (int i = 0; i < meteorsPerInterval; i++)
        {
            int rand = Random.Range(0, meteorPrefabs.Length);
            // Determine a random Y position within the defined spawn range
            float yPosition = Random.Range(-spawnRangeY, spawnRangeY);
            Vector3 spawnPosition = new Vector3(spawnXPosition, yPosition, 0);
            // Create an instance of the meteor prefab at the specified position
            GameObject meteor = Instantiate(meteorPrefabs[rand], spawnPosition, Quaternion.identity);
            // Wait for the specified delay before spawning the next meteor
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}