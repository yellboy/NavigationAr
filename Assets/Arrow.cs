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
    private NavMeshPath path;
    private Vector3 nextCorner;
    private int nextCornerIndex;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = this.gameObject.GetComponent<Renderer>();

        InvokeRepeating(nameof(UpdatePath), 0, 1.5f);
    }

    private void OnDrawGizmos()
    {
        foreach (var corner in path.corners)
        {
            Gizmos.DrawWireSphere(corner, 0.5f);
        }
    }

    private void UpdatePath()
    {
        path = new NavMeshPath();
        var found = NavMesh.CalculatePath(this.Camera.transform.position, Goal.transform.position, NavMesh.AllAreas, path);
        if (!found)
        {
            print($"No path. Path status: {path.status}");
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
        this.objectRenderer.enabled = distance > 0.3f && path.status != NavMeshPathStatus.PathInvalid;

        objectRenderer.material.color = distance > DistanceThreshold ? FarColor : NearColor;

        this.transform.position = this.Camera.transform.position + this.Camera.transform.forward * ZDistanceFromCamera + this.Camera.transform.up * YDistanceFromCamera;

        var nextCornerDistance = (this.nextCorner - this.Camera.transform.position).magnitude;
        if (nextCornerDistance < 1f)
        {
            nextCornerIndex++;
            nextCorner = path.corners[nextCornerIndex];
        }

        //this.transform.forward = (Goal.transform.position - this.transform.position).normalized;
        this.transform.forward = (nextCorner - this.Camera.transform.position).normalized;
        this.transform.Rotate(Vector3.right, 90);
    }
}
