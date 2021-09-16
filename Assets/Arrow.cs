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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = this.Camera.transform.position + this.Camera.transform.forward * ZDistanceFromCamera + this.Camera.transform.up * YDistanceFromCamera;
        this.transform.forward = (Goal.transform.position - this.transform.position).normalized;
        this.transform.Rotate(Vector3.right, 90);
    }
}
