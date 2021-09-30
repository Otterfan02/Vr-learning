using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
//Animation variables
    private Animator animator;
    public float animationSpeed;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";
    private SkinnedMeshRenderer mesh;
    
    
    //Physics Movement
    [SerializeField] private GameObject followObject;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;

    private Transform followTarget;
    private Rigidbody body;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mesh = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();

        //Physics movement
        followTarget = followObject.transform;
        body = GetComponent<Rigidbody>();
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.mass = 20f;
        
        //Teleport hands
        body.position = followTarget.position;
        body.rotation = followTarget.rotation;
        


    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();

        PhysicsMove();
    }

    private void PhysicsMove()
    {
        //position
        var positionWithOffsett = followTarget.position + positionOffset;
        var distance = Vector3.Distance(positionWithOffsett, transform.position);
        body.velocity = (positionWithOffsett - transform.position).normalized * (followSpeed * distance);
        
        //Rotation
        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        body.angularVelocity = angle * (axis * Mathf.Deg2Rad * rotateSpeed);
        

    }

    public void SetGrip(float readValue)
    {
        gripTarget = readValue;
    }

    public void SetTrigger(float readValue)
    {
        triggerTarget = readValue;
    }

    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
    }

    public void ToggleVisibility()
    {
        mesh.enabled = !mesh.enabled;
    }
}
