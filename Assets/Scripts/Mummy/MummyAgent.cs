using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MummyAgent : MonoBehaviour
{

    MummyState curState = MummyState.Spawn;

    NavMeshAgent agent;

    public Vector3 target;


    float timeBeforeDisappear = 15f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    private void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Ground"){
            if(curState==MummyState.Spawn)
                PursuitState();
        }
        else if(collision.gameObject.tag=="Trap"){
            if(curState!=MummyState.Ragdoll)
                RagdollState();
            Vector3 contactPos = collision.contacts[0].point;
            Vector3 trapPos = collision.transform.position;
            Vector3 forceVector = new Vector3(contactPos.x - trapPos.x, contactPos.y - trapPos.y, contactPos.z - trapPos.z).normalized;
            GetComponent<RagdollEnabler>().AddForceToRagdoll(forceVector * (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude * 3F));
        }
    }

    public void PursuitState()
    {
        curState = MummyState.Pursuit;
        agent.enabled = true;
        agent.SetDestination(target);
    }

    public void RagdollState()
    {
        curState = MummyState.Ragdoll;
        //agent.SetDestination(this.transform.position);
        agent.enabled = false;
        GetComponent<RagdollEnabler>().DoRagdoll(true);
        Destroy(this.gameObject, timeBeforeDisappear);
    }

    public enum MummyState 
    {
        Pursuit,
        Ragdoll,
        Spawn 
    }
}
