using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankSeeSaw : MonoBehaviour
{

    public GameObject player;
    KCharacterController characterController;
    SeesawRewind seesawRewind;

    bool turningccw;
    bool turningcw;
    float currentRot;

    bool rewinding;

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        rewinding = false;
        source = GetComponent<AudioSource>();

        transform.rotation = Quaternion.Euler(10f, 90f, 0f);
        characterController = player.GetComponent<KCharacterController>();
        
        turningccw = false;
        turningcw = false;
        currentRot = 10f;


        seesawRewind = gameObject.GetComponent<SeesawRewind>();
        seesawRewind.SerCurrRot(currentRot);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (turningccw) {
            if (currentRot > -10f) {
                currentRot -= 2f;
                transform.rotation = Quaternion.Euler(currentRot, 90f, 0f);
                seesawRewind.SerCurrRot(currentRot);
            } else {
                currentRot = -10f;
                seesawRewind.SerCurrRot(currentRot);
                turningccw = false;
            }
        }
        if (turningcw) {
            if (currentRot < 10f) {
                currentRot += 2f;
                seesawRewind.SerCurrRot(currentRot);
                transform.rotation = Quaternion.Euler(currentRot, 90f, 0f);
            } else {
                currentRot = 10f;
                seesawRewind.SerCurrRot(currentRot);
                turningcw = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("coll");
        if (collision.collider.tag == "Foot" && !rewinding) {
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
                        characterController.AddExpVel(new Vector3(0f, 35f, 0f));
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
                        characterController.AddExpVel(new Vector3(0f, 35f, 0f));
                    }
                    turningcw = true;
                }
            } else {
                //hit mid
            }

            ReversibleSoundEffect sfx = new ReversibleSoundEffect(() => source.Play(), source, Timeline.World);
            sfx.Play();
        }
    }

    public float GetCurrRot()
    {
        return currentRot;
    }

    public void SetCurrRot(float rot)
    {
        currentRot = rot;
    }

    public void SetRewinding(bool rew)
    {
        rewinding = rew;
    }
}
