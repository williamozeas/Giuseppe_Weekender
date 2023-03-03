using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableGear : MonoBehaviour, IGrabbable
{

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grabbed(GameObject grabber)
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.layer = 7;
        transform.parent = grabber.transform;
        transform.localPosition = new Vector3(0f, 0f, 1f);
        transform.localEulerAngles = Vector3.zero;
    }

    public void Dropped()
    {
        rb.isKinematic = false;
        gameObject.layer = 0;
        rb.velocity = new Vector3(0.2f, 0f, 0f);
        rb.angularVelocity = Vector3.zero;
        transform.parent = null;
    }
}
