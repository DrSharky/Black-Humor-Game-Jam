using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 16.0f;
    private Vector3 forward, right;

    //Camera stuff
    private float smoothing = 5.0f;
    private Vector3 offset;

    //No move before countdown & after game ends.
    private Action freezeMoveListener;
    private bool frozen = true;
    private Action endingListener;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private AudioSource deathSound;
    private bool ending = false;

    void Awake()
    {
        freezeMoveListener = new Action(() => { frozen = !frozen; });
        endingListener = new Action(AnimateEnding);

    }

    void Start()
    {
        EventManager.StartListening("PlayerMove", freezeMoveListener);
        EventManager.StartListening("TimeUp", freezeMoveListener);
        EventManager.StartListening("WinEvent", freezeMoveListener);
        EventManager.StartListening("TimeUp", endingListener);
        EventManager.StartListening("WinEvent", endingListener);

        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        //Camera stuff
        offset = Camera.main.transform.position - transform.position;
    }

	void Update()
    {
        if (!frozen)
        {
            float horizontal = Input.GetAxis("HorizontalKey");
            float vertical = Input.GetAxis("VerticalKey");
            if (horizontal != 0 || vertical != 0)
                Move(horizontal, vertical);

            //Camera stuff
            Vector3 targetCamPos = transform.position + offset;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
        if(ending)
        {
            transform.Translate(transform.up);
        }
    }

    void Move(float horiz, float vert)
    {
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * horiz;
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * vert;
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        transform.forward = heading;
        transform.position += upMovement;
        transform.position += rightMovement;
    }

    void AnimateEnding()
    {
        StartCoroutine(AnimationDelay());
    }

    IEnumerator AnimationDelay()
    {
        anim.SetTrigger("ending");
        yield return new WaitForSeconds(1.0f);
        deathSound.Play();
        ending = true;
    }
}
