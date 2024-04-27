using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    public float trajectoryVariance = 15.0f;
    public float spawnRate = 2.0f;
    public float spawnDistance = 15.0f;
    public int spawnAmount = 1;
    private void Start()
    {
        InvokeRepeating(nameof(Spawn),this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for(int i = 0; i < this.spawnAmount; i++) {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float varience = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion spawnRotation = Quaternion.AngleAxis(varience,Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, spawnRotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(spawnRotation * -spawnDirection);
        }
    }
}
