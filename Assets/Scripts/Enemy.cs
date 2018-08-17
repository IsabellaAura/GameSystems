using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // Declaration
    public enum State
    {
        Patrol = 0,
        Seek = 1
    }

    public State currentState = State.Patrol;
    public Transform target;
    public float seekRadius = 5f;
    public float range = 1f;

    public Transform waypointParent;
    public float moveSpeed = 0.1f;
    public float stoppingDistance = 1f;

    //Creates a collection of Transforms
    public Transform[] waypoints;
    private int currentIndex = 1;

    // CTRL + M + O (Fold Code) 
    // CTRL + M + P (Unfold Code)

    void Patrol() {
        Transform point = waypoints[currentIndex];
        float distance = Vector3.Distance(transform.position, point.position);
        if (distance < range) 
        {
            if(distance < range) 
            {
                currentIndex++;
            }
            if (currentIndex >= waypoints.Length) 
            {
                currentIndex = 1;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, point.position, moveSpeed);
        float distToTarget = Vector3.Distance(transform.position, point.position);
        if (distToTarget < seekRadius) 
        {
            currentState = State.Seek;
        }
    }


    void Seek() 
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed);
        float distToTarget = Vector3.Distance(transform.position, target.position);
        if (distToTarget > seekRadius) 
        {
            currentState = State.Patrol;
        }
    }

    // Use this for initialization
    void Start() {
        // Getting children of waypointParent
        waypoints = waypointParent.GetComponentsInChildren<Transform>();

    }

    // Update is called once per frame
    void Update() {

        // Switch current state
        switch (currentState) 
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Seek:
                Seek();
                break;

            default:
                break;
        }
    }
}
