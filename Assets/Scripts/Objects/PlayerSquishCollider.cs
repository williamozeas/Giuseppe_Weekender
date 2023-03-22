using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSquishCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.name);
        //because squishing is caused by objects running into the player, the player should rewind world time
        //hence we use the same code as running out of time
        if(!RewindManager.IsBeingRewinded && !collider.TryGetComponent<DeathCollider>(out var coll))
            GameManager.RunOutOfTime();
    }
}
