using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummySpawner : MonoBehaviour
{
    public GameObject mummyPrefab;

    public Transform target;
    Vector3 targetPos;

    private void Awake()
    {
        targetPos = target.position;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            SpawnMummy();
        }
    }

    public void SpawnMummy()
    {
        Vector3 randPos = new Vector3(transform.position.x + Random.Range(-18f, 18f), transform.position.y, transform.position.z + Random.Range(-10f, 0f));
        GameObject spawned = Instantiate(mummyPrefab, randPos, Quaternion.identity)as GameObject;
        spawned.GetComponent<MummyAgent>().target = new Vector3(targetPos.x + Random.Range(-18f, 18f), targetPos.y, targetPos.z);
    }
}
