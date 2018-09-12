using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 4.0f;

    Vector3 forward, right;

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

	void Update()
    {
        float horizontal = Input.GetAxis("HorizontalKey");
        float vertical = Input.GetAxis("VerticalKey");
        if (horizontal != 0 || vertical != 0)
            Move(horizontal, vertical);
	}

    void Move(float horiz, float vert)
    {
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * horiz;
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * vert;
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        transform.forward = heading;
        transform.position += Vector3.ClampMagnitude(rightMovement + upMovement, 0.2f);
    }
}
