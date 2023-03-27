using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButton : MonoBehaviour
{

    [SerializeField] private SceneNum scene;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Foot"))
        {
            GameManager.Instance.GameState = GameState.GameEnd;
            GameManager.Instance.CurrentScene = scene;
        }
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Foot"))
        {
            GameManager.Instance.GameState = GameState.GameEnd;
            GameManager.Instance.CurrentScene = scene;
        }
    }
}
