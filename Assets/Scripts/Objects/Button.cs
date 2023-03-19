using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public GameObject pistonTop;
    PistonTop pistonTopScript;

    Transform buttonChild;

    bool startPistonState;
    public bool currentPistonState;

    // Start is called before the first frame update
    void Start()
    {
        pistonTopScript = pistonTop.GetComponent<PistonTop>();
        buttonChild = transform.GetChild(0);
        startPistonState = pistonTopScript.state;
        currentPistonState = startPistonState;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ButtonPusher"){
            buttonChild.position -= new Vector3(0f, 0.18f, 0f);
            pistonTopScript.StateChange(!startPistonState);
            currentPistonState = !startPistonState;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "ButtonPusher"){
            buttonChild.position += new Vector3(0f, 0.18f, 0f);
            pistonTopScript.StateChange(startPistonState);
            currentPistonState = startPistonState;
        }
    }
}
