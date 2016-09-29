using UnityEngine;
using System.Collections;

public class Boss1Controller : MonoBehaviour {

    public Transform[] waypointArray;
    public float speed;
    private int currentWaypointArrayIndex = 0;
    public Transform spawner1;
    public Transform spawner2;
    public Transform loc1;
    public Transform loc2;
    public GameObject minionPrefab1;
    private Transform currentSpawner;
    private Transform currentLoc;

    void Start()
    {
        currentSpawner = spawner1;
        currentLoc = loc1;
    }

	void Update ()
    {
        moveToCurrentWaypoint();
        rotateToCurrentWaypoint();
        if (Input.GetButtonDown("Fire1"))
        {
            currentWaypointArrayIndex = 1;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            releaseMinions();
        }
    }

    private void releaseMinions()
    {
        GameObject minion = (GameObject)Instantiate(minionPrefab1, currentSpawner.position, transform.rotation);
        MinionController minionController = minion.GetComponent<MinionController>();
        minionController.setTarget(currentLoc);
        changeSpawn();
    }

    private void changeSpawn()
    {
        if (currentSpawner == spawner1)
        {
            currentSpawner = spawner2;
            currentLoc = loc2;
        }
        else if (currentSpawner == spawner2)
        {
            currentSpawner = spawner1;
            currentLoc = loc1;
        }

    }

    private void moveToCurrentWaypoint()
    {
        transform.position = Vector3.Lerp(transform.position, waypointArray[currentWaypointArrayIndex].position, speed * Time.deltaTime);
    }

    private void rotateToCurrentWaypoint()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, waypointArray[currentWaypointArrayIndex].rotation, speed * Time.deltaTime);
    }

}
