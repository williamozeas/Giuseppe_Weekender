using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Attach this class to child to create a collider and rigidbody for interacting with the player
public class InteractionCollider : MonoBehaviour
{
    public enum ColliderType
    {
        BoxCollider,
        CapsuleCollider,
        SphereCollider
    }

    [SerializeField] private ColliderType type = ColliderType.BoxCollider;
    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interaction Colliders");
        //set collider to be equal to parent
        switch (type)
        {
            case ColliderType.BoxCollider:
            {
                BoxCollider parentColl = transform.parent.GetComponent<BoxCollider>();
                BoxCollider coll = gameObject.AddComponent<BoxCollider>();
                coll.center = parentColl.center;
                coll.size = parentColl.size;
                break;
            }
            case ColliderType.CapsuleCollider:
            {
                CapsuleCollider parentColl = transform.parent.GetComponent<CapsuleCollider>();
                CapsuleCollider coll = gameObject.AddComponent<CapsuleCollider>();
                coll.center = parentColl.center;
                coll.radius = parentColl.radius;
                coll.height = parentColl.height;
                coll.direction = parentColl.direction;
                break;
            }
            case ColliderType.SphereCollider:
            {
                SphereCollider parentColl = transform.parent.GetComponent<SphereCollider>();
                SphereCollider coll = gameObject.AddComponent<SphereCollider>();
                coll.center = parentColl.center;
                coll.radius = parentColl.radius;
                break;
            }
        }

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.drag = 0;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void FixedUpdate()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
