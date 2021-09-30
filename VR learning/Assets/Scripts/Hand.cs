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
    private Transform followTarget;
    private Rigidbody body;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mesh.GetComponentInChildren<SkinnedMeshRenderer>();

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
        var distance = Vector3.Distance(followTarget.position, transform.position);
        body.velocity = (followTarget.position - transform.position).normalized * (followSpeed * distance);
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
