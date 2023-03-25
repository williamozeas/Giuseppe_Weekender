using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankSeeSaw : MonoBehaviour
{

    public GameObject player;
    public KCharacterController characterController;

    public bool turningccw;
    public bool turningcw;
    public float currentRot;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(10f, 90f, 0f);
        characterController = player.GetComponent<KCharacterController>();
        
        turningccw = false;
        turningcw = false;
        currentRot = 10f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (turningccw) {
            if (currentRot > -10f) {
                currentRot -= 2f;
                transform.rotation = Quaternion.Euler(currentRot, 90f, 0f);
            } else {
                currentRot = -10f;
                turningccw = false;
            }
        }
        if (turningcw) {
            if (currentRot < 10f) {
                currentRot += 2f;
                transform.rotation = Quaternion.Euler(currentRot, 90f, 0f);
            } else {
                currentRot = 10f;
                turningcw = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("coll");
        if (collision.collider.tag == "Foot") {
            //Captain hit plank
            if (collision.collider.transform.position.x < transform.position.x - .5f) {
                //hit left
                if (currentRot != -10f){
                    //not fully left
                    if (player.transform.position.x > transform.position.x + .5f
                        && player.transform.position.x < transform.position.x + 9.6f
                        && player.transform.position.y > transform.position.y + .5f
                        && player.transform.position.y < transform.position.y + 4.5f) {
                            //player in pos
                        characterController.AddExpVel(new Vector3(0f, 30f, 0f));
                    }
                    turningccw = true;
                }
            } else if (collision.collider.transform.position.y > transform.position.y + .8f) {
                //hit right
                if (currentRot != 10f){
                    //not fully right
                    if (player.transform.position.x < transform.position.x - .5f
                        && player.transform.position.x > transform.position.x - 9.6f
                        && player.transform.position.y > transform.position.y + .5f
                        && player.transform.position.y < transform.position.y + 4.5f) {
                            //player in pos
                        characterController.AddExpVel(new Vector3(0f, 30f, 0f));
                    }
                    turningcw = true;
                }
            } else {
                //hit mid
            }
        }
    }
}
