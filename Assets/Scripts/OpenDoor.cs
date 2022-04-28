using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private float _openDistance = 2f;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private bool _isDoorOpen = false;
    private Animator _animator;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, _playerController.transform.position) < _openDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_isDoorOpen == false)
                {
                    _animator.SetTrigger("Open");
                    _isDoorOpen = true;                   
                }
                else
                {
                    _animator.SetTrigger("Close");
                    _isDoorOpen = false;
                }
            }
        }
    }
}
