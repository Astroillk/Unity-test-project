using System;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Material designMaterial;

    private LevelManager _levelManager;
    private bool _death;

    private void Start()
    {
        _death = false;
        _levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        if(_death)
            designMaterial.SetFloat("Alpha", Mathf.Lerp(0f, 1f, 1f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            StartCoroutine(Death());
        }
    }
    
    private IEnumerator Death()
    {
        _death = true;
        yield return new WaitForSeconds(1f);
        
        _levelManager.Respawn(gameObject);
    }
}
