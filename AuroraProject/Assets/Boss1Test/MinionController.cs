using UnityEngine;
using System.Collections;

public class MinionController : MonoBehaviour {

    private Transform currentTarget;
    public float exitSpeed;
    public float chaseSpeed;
    private float currentSpeed;
    public float chaseTime;
    public float distanceOffset;

    private bool chasingPlayer = false;


    void Start()
    {
        currentSpeed = exitSpeed;
        StartCoroutine(chaseTimer());
    }

	void Update ()
    {
        moveToCurrentTarget();
	}

    public void setTarget(Transform targetPosition)
    {
        currentTarget = targetPosition;
    }

    private void moveToCurrentTarget()
    {
        if (currentTarget != null)
        {
            if (!chasingPlayer)
            {
                leaveSpawn();
            }
            else if (chasingPlayer && (Vector3.Distance(transform.position, currentTarget.position) >= distanceOffset))
            {
                moveToPlayer();
            }
        }
    }

    private IEnumerator chaseTimer()
    {
        yield return new WaitForSeconds(chaseTime);
        currentSpeed = chaseSpeed;
        chasingPlayer = true;
        setTarget(GameObject.FindWithTag("Player").GetComponent<Transform>());
    }

    private void leaveSpawn()
    {
        transform.position = Vector3.Lerp(transform.position, currentTarget.position, currentSpeed * Time.deltaTime);
    }
    private void moveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, currentSpeed * Time.deltaTime);
    }

}
