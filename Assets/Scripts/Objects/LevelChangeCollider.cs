using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeCollider : MonoBehaviour
{
    [SerializeField] private SceneNum scene;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.CurrentScene = scene;
        }
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.CurrentScene = scene;
        }
    }
}
