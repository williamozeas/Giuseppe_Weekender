using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nail : MonoBehaviour
{

    bool movingDown;

    // Start is called before the first frame update
    void Start()
    {
        movingDown = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movingDown) {
            Debug.Log("meow");
            if (transform.localPosition.y > -15.6f) {
                transform.localPosition -= new Vector3(0f, 1.2f, 0f);
            } else {
                transform.localPosition = new Vector3(transform.localPosition.x, -15.6f, transform.localPosition.z);
                movingDown = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Foot") {
            movingDown = true;
        }
    }
}
