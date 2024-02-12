using UnityEngine;
using UnityEngine.Events;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private Transform[] _route;

    private Transform _targetPoint;
    private float _movementSpeed;
    private int _nextPointIndex;

    private event UnityAction MovementUpdate;

    private void Start()
    {
        _movementSpeed = 5f;
        _nextPointIndex = 0;
        _targetPoint = _route[_nextPointIndex];

        MovementUpdate += Move;
        MovementUpdate += Rotate;
    }

    private void LateUpdate()
    {
        MovementUpdate?.Invoke();
    }

    private void Rotate()
    {
        transform.LookAt(_targetPoint.position);
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPoint.position, _movementSpeed * Time.deltaTime);

        if (transform.position == _targetPoint.position)
        {
            _nextPointIndex++;

            if (_nextPointIndex >= _route.Length)
                _nextPointIndex = 0;

            _targetPoint = _route[_nextPointIndex];
        }
    }
}
