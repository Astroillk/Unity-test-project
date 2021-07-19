using System;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEventOn : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnterEvents;

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnterEvents.Invoke();
    }
}
