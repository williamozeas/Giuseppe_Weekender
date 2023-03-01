using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

[System.Serializable]
class ScriptedForce
{
    public float time;
    public bool isTorque;
    public Vector3 force;
}

public class ScriptedForces : MonoBehaviour
{
    [SerializeField] private List<ScriptedForce> forces;
    private Rigidbody rb;

    private int index;

    // Start is called before the first frame update
    void Awake()
    {
        forces.Sort((a, b) => a.time.CompareTo(b.time));
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //can be made more efficient with iterator probably
        //TODO: make it so you can apply multiple forces in one update
        if (index < forces.Count)
        {
            ScriptedForce nextForce = forces[index];
            if (GameManager.Instance.Time >= nextForce.time)
            {
                ApplyForce(nextForce);
                index++;
            }
        }
        if (index > 0)
        {
            ScriptedForce previousForce = forces[index - 1];
            if (GameManager.Instance.Time < previousForce.time)
            {
                ApplyForce(previousForce);
                index--;
            }
        }

        
    }

    void ApplyForce(ScriptedForce force)
    {
        if (!force.isTorque)
        {
            rb.AddForce(force.force, ForceMode.Impulse);
        }
        else
        {
            rb.AddTorque(force.force, ForceMode.Impulse);
        }

    }
}
