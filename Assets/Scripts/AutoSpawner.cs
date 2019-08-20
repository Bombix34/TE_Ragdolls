using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpawner : MonoBehaviour
{
    MummySpawner mummySpawner;
    TrapSpawner trapSpawner;

    public float mummyFrequency;
    public float trapFrequency;

    float curChronoMummy;
    float curChronoTrap;

    private void Awake()
    {
        mummySpawner = GetComponentInChildren<MummySpawner>();
        trapSpawner = GetComponentInChildren<TrapSpawner>();
    }

    void Start()
    {
        curChronoMummy = Random.Range(mummyFrequency, mummyFrequency * 1.5f);
        curChronoTrap = Random.Range(trapFrequency, trapFrequency * 1.5f);
    }

    void Update()
    {
        curChronoMummy -= Time.deltaTime;
        curChronoTrap -= Time.deltaTime;
        if(curChronoMummy<=0)
        {
            mummySpawner.SpawnMummy();
            curChronoMummy = Random.Range(mummyFrequency, mummyFrequency * 1.5f);
        }
        if(curChronoTrap<=0)
        {
            trapSpawner.SpawnTrap();
            curChronoTrap = Random.Range(trapFrequency, trapFrequency * 1.5f);
        }
    }
}
