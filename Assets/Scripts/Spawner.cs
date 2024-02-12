using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _target;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _timeBetweenSpawn;

    private int _enemyMaxCount;
    private int _enemyCount;
    private float _enemySpeed;

    private void Start()
    {
        _enemyMaxCount = 3;
        _enemySpeed = 3;

        StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        var delay = new WaitForSeconds(_timeBetweenSpawn);
        var unitLimit = new WaitUntil(() => _enemyCount < _enemyMaxCount);

        while (true)
        {
            int randomIndex = Random.Range(0, _spawnPoints.Length);
            Enemy enemy = Instantiate(_enemy, _spawnPoints[randomIndex].position, Quaternion.identity);
            _enemyCount++;

            if (enemy.TryGetComponent(out EnemyMover enemyMover))
            {
                enemyMover.SetSpeed(_enemySpeed);
                enemyMover.SetTarget(_target);
            }

            yield return unitLimit;
            yield return delay;
        }
    }
}
