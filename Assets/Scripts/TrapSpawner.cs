using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    public List<GameObject> traps;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SpawnTrap();
        }
    }

    public void SpawnTrap()
    {
        float randSize = Random.Range(3f, 6f);
        Vector3 randPos = new Vector3(transform.position.x + Random.Range(-10f, 10f), transform.position.y, transform.position.z + Random.Range(-10f, 0f));
        GameObject spawned = Instantiate(traps[0], randPos, Quaternion.identity) as GameObject;
        spawned.transform.localScale = new Vector3(randSize, randSize, randSize);
        spawned.GetComponent<SimpleForce>().AddForce();
    }
}
