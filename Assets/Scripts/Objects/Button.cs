using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public GameObject[] pistonTops;
    PistonTop[] pistonTopScripts;
    ButtonRewind buttonRewindScript;

    Transform buttonChild;

    bool buttonPressed;

    bool inContact;


    // Start is called before the first frame update
    void Start()
    {
        pistonTopScripts = new PistonTop[pistonTops.Length];
        for (int i = 0; i < pistonTops.Length; i++) {
            pistonTopScripts[i] = pistonTops[i].GetComponent<PistonTop>();
        }
        buttonRewindScript = gameObject.GetComponent<ButtonRewind>();
        buttonChild = transform.GetChild(1);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ButtonPusher" || other.tag == "Player"){
            StateChange(true);
            buttonPressed = true;
            inContact = true;
        }
    }

    void OnTriggerStay (Collider other)
    {
        if (other.tag == "ButtonPusher" || other.tag == "Player"){
            if (!buttonPressed) {
                StateChange(true);
                buttonPressed = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "ButtonPusher" || other.tag == "Player"){
            StateChange(false);
            buttonPressed = false;
            inContact = false;
        }
    }

    public void StateChange(bool buttonNowPressed)
    {
        if (buttonNowPressed == buttonPressed) {
            return;
        }
        buttonPressed = buttonNowPressed;
        if (buttonNowPressed) {
            buttonChild.position -= new Vector3(0f, 0.18f, 0f);

            StartCoroutine(Unpress());

        } else {
            buttonChild.position += new Vector3(0f, 0.18f, 0f);
        }
        for (int i = 0; i < pistonTops.Length; i++) {
            pistonTopScripts[i].StateChange(buttonNowPressed);
        }
        buttonRewindScript.StateChange(buttonNowPressed);

    }

    IEnumerator Unpress()
    {
        if (inContact || !buttonPressed) {
            yield break;
        }
        yield return new WaitForSeconds(5f);
        if (inContact || !buttonPressed) {
            yield break;
        }
        Debug.Log("goup");
        StateChange(false);
    }
}
