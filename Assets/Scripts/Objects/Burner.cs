using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burner : MonoBehaviour
{

    private bool isBurning;
    private bool wasBurning;

    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        isBurning = false;
        wasBurning = false;

        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBurning();
        Burn();
    }

    void CheckBurning()
    {

        if (GameManager.Instance.Time > 5.0f) {
            isBurning = true;
        } else {
            isBurning = false;
        }
    }
    
    void Burn()
    {
        if (isBurning && !wasBurning) {
            ps.Play();
            wasBurning = true;
        } else if (!isBurning && wasBurning) {
            ps.Stop();
            wasBurning = false;
        }
    }
}
