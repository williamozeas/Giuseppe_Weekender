using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject arm;
    [SerializeField] private GameObject leg;
    
    // Start is called before the first frame update
    void Start()
    {
        leg.SetActive(true);
        arm.SetActive(false);
    }

    public void SwitchToArm()
    {
        arm.SetActive(true);
        //move leg away
        StartCoroutine(GoAwayLeg());
    }

    IEnumerator GoAwayLeg()
    {
        leg.GetComponentInChildren<Collider>().enabled = false;
        leg.GetComponentInChildren<CaptainFoot>().enabled = false;
        Rigidbody rb = leg.GetComponentInChildren<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = new Vector3(-5, 0, 0);
        yield return new WaitForSeconds(5f);
        leg.SetActive(false);
    }
}
