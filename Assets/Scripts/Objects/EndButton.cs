using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButton : MonoBehaviour
{

    [SerializeField] private SceneNum scene;
    public GameObject bgDark;
    public GameObject bgLight;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Foot"))
        {
            bgDark.SetActive(false);
            bgLight.SetActive(true);
            GameManager.Instance.GameState = GameState.GameEnd;
            GameManager.Instance.CurrentScene = scene;
        }
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Foot"))
        {
            bgDark.SetActive(false);
            bgLight.SetActive(true);
            GameManager.Instance.GameState = GameState.GameEnd;
            GameManager.Instance.CurrentScene = scene;
        }
    }
}
