using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showEnding : MonoBehaviour
{
    public GameObject EndingStory;
    // Start is called before the first frame update
    void Start()
    {
        EndingStory.gameObject.SetActive(false); // default
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameState == GameState.GameEnd) {
            EndingStory.gameObject.SetActive(true);
        }
        
    }
}
