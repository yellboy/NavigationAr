using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Follower : MonoBehaviour
{
    public Transform target;

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        this.navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.InvokeRepeating(nameof(UpdatePath), 3f, 3f);
    }

    void UpdatePath()
    {
        if (null == this.target)
            return;

        this.navMeshAgent.destination = this.target.position;
    }
}
