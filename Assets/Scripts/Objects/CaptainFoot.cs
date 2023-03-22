using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class CaptainFoot : MonoBehaviour
{
    enum StompPhase
    {
        Tracking,
        Windup,
        Stomp,
        Rest,
        Recovery
    }

    private StompPhase phase = StompPhase.Tracking;
    
    public float trackingTime = 4.5f;
    public float windupTime = 0.5f;
    public float restTime = 1f;
    public float recoveryTime = 1f;
    public float stompTime = 0.2f;
    public LayerMask collidableLayers;
    public List<Transform> raycastOrigins = new List<Transform>();
    
    private float timer = 0;
    private float xTarget;
    private float velocity;
    private float acceleration;
    private float distanceToStomp;
    private float originalHeight;
    private float stompStartHeight;
    private float recoveryStartPos;
    private Player player;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.Player;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (phase)
        {
            case StompPhase.Tracking:
            {
                xTarget = player.transform.position.x;

                acceleration = Mathf.Pow(timer / trackingTime, 2);

                velocity = (xTarget - transform.position.x) * acceleration * 20f;
                float newPosX = transform.position.x + velocity * Time.fixedDeltaTime;
                rb.MovePosition(new Vector3(newPosX, transform.position.y, transform.position.z));
                timer += Time.fixedDeltaTime;
                if (timer > trackingTime)
                {
                    StartWindup();
                }
                break;
            }
            case StompPhase.Windup:
            {
                timer += Time.fixedDeltaTime;
                float newPosY = transform.position.y + Time.fixedDeltaTime;
                rb.MovePosition(new Vector3(transform.position.x, newPosY, transform.position.z));
                if (timer > windupTime)
                {
                    StartStomp();
                }
                break;
            }
            case StompPhase.Stomp:
            {
                timer += Time.fixedDeltaTime;
                if (timer > stompTime)
                {
                    rb.MovePosition(new Vector3(transform.position.x, originalHeight - distanceToStomp, transform.position.z));
                    StartRest();
                    break;
                }

                float newPosY = EasingFunction.EaseInQuad(stompStartHeight, originalHeight - distanceToStomp, timer/stompTime);
                Debug.Log(newPosY);
                rb.MovePosition(new Vector3(transform.position.x, newPosY, transform.position.z));
                break;
            }
            case StompPhase.Rest:
            {
                timer += Time.fixedDeltaTime;
                if (timer > restTime)
                {
                    StartRecover();
                }
                break;
            }
            case StompPhase.Recovery:
            {
                timer += Time.fixedDeltaTime;
                float newPosY = EasingFunction.EaseInOutCubic(recoveryStartPos, originalHeight, timer/recoveryTime);
                rb.MovePosition(new Vector3(transform.position.x, newPosY, transform.position.z));
                if (timer > recoveryTime)
                {
                    StartTrack();
                }
                break;
            }
        }
    }

    void StartWindup()
    {
        timer = 0;
        phase = StompPhase.Windup;
        originalHeight = transform.position.y;
    }

    private void StartStomp()
    {
        timer = 0;
        phase = StompPhase.Stomp;
        RaycastHit[] hits = new RaycastHit[3];
        for (int i = 0; i < 3; i++)
        {
            RaycastHit hit;
            Ray ray = new Ray(raycastOrigins[i].position, Vector3.down);
            Physics.Raycast(ray, out hit, 100, collidableLayers);
            hits[i] = hit;
            Debug.Log(distanceToStomp);
        }

        RaycastHit shortest = hits.Aggregate(((a, b) =>  a.distance < b.distance ? a : b ));
        distanceToStomp = shortest.distance;
        stompStartHeight = transform.position.y;
    }

    private void StartRest()
    {
        timer = 0;
        phase = StompPhase.Rest;
    }

    private void StartRecover()
    {
        timer = 0;
        phase = StompPhase.Recovery;
        recoveryStartPos = transform.position.y;
    }

    void StartTrack()
    {
        timer = 0;
        phase = StompPhase.Tracking;
    }
}
