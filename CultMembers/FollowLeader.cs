using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowLeader : MonoBehaviour
{
    [SerializeField]
    private GameObject leader;
    [SerializeField]
    private float maxDistance;

    private float turnAmt;
    private float forwardAmt;
    private NavMeshAgent agent;
    private Vector3 leaderPos;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        if (leader == null)
            leader = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update ()
    {
        leaderPos = leader.transform.position;
        float distanceFromLeader = Vector3.Distance(transform.position, leaderPos);
        if (distanceFromLeader > maxDistance)
        {
            MoveTowardsLeader();
        }
	}

    void MoveTowardsLeader()
    {
        //Rotate towards the leader first.
        Vector3 leaderDir = leaderPos - transform.position;
        Quaternion lookAtLeader = Quaternion.LookRotation(leaderDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtLeader, 11.0f);

        //If character is rotated close enough to straight towards leader, then move in that direction.
        float followAngle = Quaternion.Angle(transform.rotation, lookAtLeader);
        if(followAngle < 3.0f)
        {
            //Using this so the cult members don't only follow behind the leader.
            Vector3 movePos = RandomNavSphere(leaderPos, maxDistance, -1);
            agent.SetDestination(movePos);
            if (movePos.magnitude > 1f) movePos.Normalize();
            movePos = transform.InverseTransformDirection(movePos);
            movePos = Vector3.ProjectOnPlane(movePos, new Vector3(0, 1, 0));
            turnAmt = Mathf.Atan2(movePos.x, movePos.z);

            forwardAmt = movePos.z;
        }
    }

    //It's only repeated once... I'm not really worried about it.
    Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layerMask);

        return navHit.position;
    }
}
