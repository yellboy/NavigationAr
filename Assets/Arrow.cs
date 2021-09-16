using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var distance = (this.Goal.transform.position - this.Camera.transform.position).magnitude;
        this.gameObject.GetComponent<Renderer>().material.color = distance > DistanceThreshold ? FarColor : NearColor;

        this.transform.position = this.Camera.transform.position + this.Camera.transform.forward * ZDistanceFromCamera + this.Camera.transform.up * YDistanceFromCamera;
        this.transform.forward = (Goal.transform.position - this.transform.position).normalized;
        this.transform.Rotate(Vector3.right, 90);
    }
}
