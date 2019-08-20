using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEnabler : MonoBehaviour
{
    Animator anim;

    Collider mainCollider;
    public Collider[] ragdollColliders;

    public Collider[] freeRagdollColliders; 

    Rigidbody mainRigidbody;
    public Rigidbody[] ragdollRigidbodies;

    public Transform originTransformRig;//pour remettre le perso droit quand on quitte le ragdoll

    void Awake()
    {
        mainCollider = GetComponent<Collider>();
        mainRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        foreach (var col in freeRagdollColliders)
            Physics.IgnoreCollision(mainCollider, col);
    }

    void Start() 
    { 
        foreach (var col in ragdollColliders)
            col.enabled = false;
        DoRagdoll(false);
    }

    public void DoRagdoll(bool isRagdoll)
    {
        foreach (var col in ragdollColliders)
            col.enabled = isRagdoll;
        foreach (var body in ragdollRigidbodies)
        {
            if(body!=mainRigidbody)
            {
                body.isKinematic = !isRagdoll;
                body.useGravity = isRagdoll;
            }
        }
        if(!isRagdoll)
        {
            originTransformRig.rotation = Quaternion.Euler(new Vector3(-90f, originTransformRig.rotation.eulerAngles.y, originTransformRig.rotation.eulerAngles.z));
        }
        mainCollider.enabled = !isRagdoll;
        mainRigidbody.useGravity = !isRagdoll;
        anim.enabled = !isRagdoll;
    }

    public void AddForceToRagdoll(Vector3 force)
    {
        foreach(var dollBody in ragdollRigidbodies)
        {
            dollBody.AddForce(force, ForceMode.Impulse);
        }
    }
}
