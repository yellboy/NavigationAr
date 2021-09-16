using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private GameObject Camera;

    [SerializeField]
    private GameObject Goal;

    [SerializeField] 
    private float ZDistanceFromCamera = 0.5f;

    [SerializeField] 
    private float YDistanceFromCamera = 0f;

    [SerializeField] 
    private Color FarColor = Color.red;

    [SerializeField]
    private Color NearColor = Color.green;

    [SerializeField] 
    private float DistanceThreshold = 10f;

    private Renderer objectRenderer;
    private NavMeshAgent navMeshAgent;
    private NavMeshPath path;
    private Vector3 nextCorner;
    private int nextCornerIndex;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = this.gameObject.GetComponent<Renderer>();

        this.navMeshAgent = this.Camera.GetComponent<NavMeshAgent>();

        InvokeRepeating(nameof(UpdatePath), 0, 3);
    }

    private void UpdatePath()
    {
        path = new NavMeshPath();
        var found = this.navMeshAgent.CalculatePath(Goal.transform.position, path);

        if (!found)
        {
            print("No path");
            return;
        }

        print("Path found");
        nextCorner = path.corners.First();
        nextCornerIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var distance = (this.Goal.transform.position - this.Camera.transform.position).magnitude;
        objectRenderer.material.color = distance > DistanceThreshold ? FarColor : NearColor;

        this.transform.position = this.Camera.transform.position + this.Camera.transform.forward * ZDistanceFromCamera + this.Camera.transform.up * YDistanceFromCamera;

        var nextCornerDistance = (this.nextCorner - this.Camera.transform.position).magnitude;
        if (nextCornerDistance < 0.1f)
        {
            nextCornerIndex++;
            nextCorner = path.corners[nextCornerIndex];
        }

        //this.transform.forward = (Goal.transform.position - this.transform.position).normalized;
        this.transform.forward = (nextCorner - this.Camera.transform.position).normalized;
        this.transform.Rotate(Vector3.right, 90);
    }
}
