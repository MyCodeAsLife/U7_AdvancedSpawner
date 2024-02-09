using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private float _lifetime = 20;

    private float _minSpeed = 2;
    private float _maxSpeed = 10;
    private int _minWaypointsCount;
    private int _maxWaypointsCount;

    private void Start()
    {
        _minWaypointsCount = 2;
        _maxWaypointsCount = _waypoints.Length;

        StartCoroutine(EnemySpawn());
    }

    private Transform[] CreateRandomRoute(int lenght)
    {
        Transform[] route = new Transform[lenght];
        int lastIndex = -1;
        int count = 0;

        while (count < lenght)
        {
            int currentIndex = Random.Range(0, _waypoints.Length);

            if (currentIndex != lastIndex)
            {
                lastIndex = currentIndex;
                route[count] = _waypoints[currentIndex];
                count++;
            }
        }

        return route;
    }

    private IEnumerator EnemySpawn()
    {
        var pause = new WaitForSeconds(_timeBetweenSpawn);

        while (true)
        {
            int randomIndex = Random.Range(0, _spawnPoints.Length);
            int randomWaypointsCount = Random.Range(_minWaypointsCount, _maxWaypointsCount);
            float randomSpeed = Random.Range(_minSpeed, _maxSpeed);

            Enemy enemy = Instantiate(_enemy, _spawnPoints[randomIndex].position, Quaternion.identity);

            if (enemy.TryGetComponent(out EnemyMover enemyMover))
            {
                enemyMover.SetSpeed(randomSpeed);
                enemyMover.SetRoute(CreateRandomRoute(randomWaypointsCount));
            }

            Destroy(enemy, _lifetime);

            yield return pause;
        }
    }
}
