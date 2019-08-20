using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Mummy")
        {
            Destroy(other.gameObject);
        }
    }
}
