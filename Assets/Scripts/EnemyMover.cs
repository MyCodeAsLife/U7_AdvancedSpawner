using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class EnemyMover : MonoBehaviour
{
    private const string Speed = "Speed";

    private Transform[] _route;
    private Transform _targetPoint;
    private Animator _animator;
    private float _movementSpeed;
    private int _nextPointIndex;

    private event UnityAction MovementUpdate;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _nextPointIndex = 0;
        _targetPoint = _route[_nextPointIndex];

        MovementUpdate += Move;
        MovementUpdate += Rotate;
    }

    private void LateUpdate()
    {
        MovementUpdate?.Invoke();
    }

    public void SetSpeed(float speed)
    {
        _movementSpeed = speed;
    }

    public void SetRoute(Transform[] route)
    {
        _route = route;
    }

    private void Rotate()
    {
        transform.LookAt(_targetPoint.position);
    }

    private void Stop()
    {
        MovementUpdate -= Move;
        MovementUpdate -= Rotate;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPoint.position, _movementSpeed * Time.deltaTime);

        if (transform.position == _targetPoint.position)
        {
            _nextPointIndex++;

            if (_nextPointIndex < _route.Length)
            {
                _targetPoint = _route[_nextPointIndex];
            }
            else
            {
                _movementSpeed = 0;
                Stop();
            }
        }

        _animator.SetFloat(Speed, _movementSpeed);
    }
}
