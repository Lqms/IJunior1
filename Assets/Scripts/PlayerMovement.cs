using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _sprintSpeedMultiplier = 1;

    [Header("Jump")]
    [SerializeField] private float _gravity = -9f;
    [SerializeField] private float _defaultGravity = -2f;
    [SerializeField] private Transform _legs;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private bool _isGround;
    [SerializeField] private float _jumpPower = 2;

    private CharacterController _controller;
    private Vector3 _velocity;
    private float _defaultSpeedMultiplier = 1;
    private float _maximalSpeedMultiplier = 2;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        _sprintSpeedMultiplier = Input.GetKey(KeyCode.LeftShift) ? _maximalSpeedMultiplier : _defaultSpeedMultiplier;
        _controller.Move(move * _speed * _sprintSpeedMultiplier * Time.deltaTime);
    }

    private void Jump()
    {
        _isGround = Physics.CheckSphere(_legs.position, _groundDistance, _groundMask);

        if (_isGround && _velocity.y < 0)
            _velocity.y = _defaultGravity;

        _velocity.y += _gravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && _isGround)
            _velocity.y = Mathf.Sqrt(_jumpPower * _defaultGravity * _gravity);

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Alarm component))
            component.TurnOn();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Alarm component))
            component.TurnOff();
    }
}
