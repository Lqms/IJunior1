using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class FirstPersonCameraController : MonoBehaviour
{
    [SerializeField] private float _mouseSpeed = 250f;
    [SerializeField] private float _interactDistance = 2f;
    [SerializeField] private Transform _playerBody;

    private float _rotationY = 0f;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        InteractWithScriptedObjects();

        if (Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") > 0)
            CameraMove();
    }

    private void CameraMove()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSpeed * Time.deltaTime;

        float minimalRange = -45f;
        float maximalRange = 45f;
        _rotationY -= mouseY;
        _rotationY = Mathf.Clamp(_rotationY, minimalRange, maximalRange);

        _playerBody.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(_rotationY, 0f, 0f);
    }

    private void InteractWithScriptedObjects()
    {
        RaycastHit hit;
        Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.TryGetComponent<InteractableObject>(out InteractableObject interactableObject) && hit.distance < _interactDistance)
                UIManager.Instance.ShowTextHint(interactableObject.InteractMessage);
            else
                UIManager.Instance.ShowTextHint("");
        }
    }
}
