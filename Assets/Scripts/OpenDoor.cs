using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OpenDoor : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerController;
    [SerializeField] private bool _isDoorOpen = false;

    private Animator _animator;
    private bool _isPlayerInArea = false;

    private const string Open = "Open";
    private const string Close = "Close";

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isPlayerInArea && Input.GetKeyDown(KeyCode.E))
        {
            if (_isDoorOpen == false)
            {
                _animator.SetTrigger(Open);
                _isDoorOpen = true;
            }
            else
            {
                _animator.SetTrigger(Close);
                _isDoorOpen = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerController))
            _isPlayerInArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerController))
            _isPlayerInArea = false;
    }
}
