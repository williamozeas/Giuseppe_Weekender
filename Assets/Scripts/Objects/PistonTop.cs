using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonTop : MonoBehaviour
{

    public bool state;

    bool movingOut;
    bool movingIn;

    public float speed;

    public float force;

    public GameObject player;

    void Start()
    {
        movingOut = false;
        movingIn = false;
    }

    void FixedUpdate()
    {
        if (movingOut) {
            if  (transform.localPosition.y < 1f) {
                transform.localPosition += Vector3.up * 3 * speed;
            } else {
                transform.localPosition = new Vector3(0f, 1f, 0f);
                movingOut = false;
            }
        } else if (movingIn) {
            if  (transform.localPosition.y > 0f) {
                transform.localPosition -= Vector3.up * speed;
            } else {
                transform.localPosition = new Vector3(0f, 0f, 0f);
                movingIn = false;
            }
        }
    }

    public void StateChange(bool newState)
    {
        Debug.Log(newState);
        state = newState;
        if (newState) {
            movingIn = false;
            movingOut = true;
            GiveImpulse();
        } else {
            movingIn = true;
            movingOut = false;
        }
    }

    void GiveImpulse()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f);
        foreach (Collider hit in hitColliders)
        {
            Debug.Log(hit.name);
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && hit.tag != "Unmovable"){
                Debug.Log("hey");
                rb.transform.position += Vector3.up * 0.5f;
                rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            }
        }
    }
    
}
