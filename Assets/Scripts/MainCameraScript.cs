using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class MainCameraScript : MonoBehaviour
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
        CheckForInteractableObject();

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

    private void CheckForInteractableObject()
    {
        RaycastHit hit;
        Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.GetComponent<InteractableObject>() && hit.distance < _interactDistance)
            {
                InteractableObject interactableObject = hit.transform.GetComponent<InteractableObject>();
                UIManager.Instance.ShowTextHint(interactableObject.InteractMessage);
            }
            else
            {
                UIManager.Instance.ShowTextHint("");
            }
        }
    }
}
