using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableGear : MonoBehaviour, IGrabbable
{

    Rigidbody rb;

    GrabbableRewind rewindScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rewindScript = GetComponent<GrabbableRewind>();
    }

    public void Grabbed(GameObject grabber)
    {
        rewindScript.Grabbed(transform.position);
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
        transform.localPosition = new Vector3(0f, -0.5f, 1f);
        transform.localEulerAngles = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.parent = null;
        rewindScript.Dropped(transform.position);
    }
}
