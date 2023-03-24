using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonTop : MonoBehaviour
{

    public bool startingState;
    
    public bool reversed;

    bool state;

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
        MovePiston();
    }

    public void StateChange(bool buttonPressed)
    {
        bool newState = buttonPressed ^ startingState;

        if (!state & newState) {
            GiveImpulse();
        }
        state = newState;
        if (newState) {
            movingIn = false;
            movingOut = true;
        } else {
            movingIn = true;
            movingOut = false;
        }
    }

    void GiveImpulse()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.TransformPoint(transform.localPosition + new Vector3(0f, 18, 0f)), 1.5f);
        foreach (Collider hit in hitColliders)
        {
            Debug.Log(hit.name);
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && hit.tag != "Unmovable"){
                rb.transform.position += transform.up * 0.5f;
                rb.AddForce(transform.up * force, ForceMode.Impulse);
            }
        }
    }

    void MovePiston()
    {
        if (reversed) {
            if (startingState) {
                if (movingIn) {
                    if  (transform.localPosition.y < 20f) {
                        transform.localPosition += Vector3.up * 3 * speed;
                    } else {
                        transform.localPosition = new Vector3(0f, 20f, 0f);
                        movingOut = false;
                    }
                } else if (movingOut) {
                    if  (transform.localPosition.y > 0f) {
                        transform.localPosition -= Vector3.up * speed;
                    } else {
                        transform.localPosition = new Vector3(0f, 0f, 0f);
                        movingIn = false;
                    }
                }
            } else {
                if (movingIn) {
                    if  (transform.localPosition.y < 0f) {
                        transform.localPosition += Vector3.up * 3 * speed;
                    } else {
                        transform.localPosition = new Vector3(0f, 00f, 0f);
                        movingOut = false;
                    }
                } else if (movingOut) {
                    if  (transform.localPosition.y > -20f) {
                        transform.localPosition -= Vector3.up * speed;
                    } else {
                        transform.localPosition = new Vector3(0f, -20f, 0f);
                        movingIn = false;
                    }
                }
            }
        } else {
            if (startingState) {
                if (movingOut) {
                    if  (transform.localPosition.y < 0f) {
                        transform.localPosition += Vector3.up * 3 * speed;
                    } else {
                        transform.localPosition = new Vector3(0f, 0f, 0f);
                        movingOut = false;
                    }
                } else if (movingIn) {
                    if  (transform.localPosition.y > -20f) {
                        transform.localPosition -= Vector3.up * speed;
                    } else {
                        transform.localPosition = new Vector3(0f, -20f, 0f);
                        movingIn = false;
                    }
                }
            } else {
                if (movingOut) {
                    if  (transform.localPosition.y < 20f) {
                        transform.localPosition += Vector3.up * 3 * speed;
                    } else {
                        transform.localPosition = new Vector3(0f, 20f, 0f);
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
        }
    }
    
}
