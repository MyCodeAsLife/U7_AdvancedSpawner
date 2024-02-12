using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyMover : MonoBehaviour
{
    private const string Speed = "Speed";

    private Transform _target;
    private Animator _animator;
    private float _moveSpeed;
    private float _followingDistance;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _followingDistance = 1.9f;

        StartCoroutine(Movement());
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void SetSpeed(float speed)
    {
        _moveSpeed = speed;
    }

    private void Rotate()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        direction.y = 0;
        transform.forward = direction;
    }

    private void Move()
    {
        _animator.SetFloat(Speed, _moveSpeed);
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }

    private IEnumerator Movement()
    {
        const float Second = 0.2f;

        var wait = new WaitForSeconds(Second);

        while (true)
        {
            Move();
            Rotate();

            float distance = Vector3.Distance(transform.position, _target.position);

            if (distance <= _followingDistance)
            {
                _animator.SetFloat(Speed, 0);

                yield return wait;

                _animator.SetFloat(Speed, _moveSpeed);
            }

            yield return null;
        }
    }
}
