using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private GameObject Camera;

    [SerializeField]
    private GameObject Goal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.forward = (Goal.transform.position - this.transform.position).normalized;
        this.transform.Rotate(Vector3.right, 90);// *= Quaternion.AngleAxis(90, Vector3.right);
    }
}
