using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string _interactMessage;

    public string InteractMessage => _interactMessage;
}
