using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private bool _isDoorOpen = false;

    private bool _isPlayerInArea = false;
    private Animator _animator;
    private const string Open = "Open";
    private const string Close = "Close";

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
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
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            _isPlayerInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            _isPlayerInArea = false;
        }
    }
}
