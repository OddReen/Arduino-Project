using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] List<Transform> positionsToSpawn;
    public static Spawn Instance;
    [SerializeField] GameObject NPCpref;
    [SerializeField] int quantity;
    [SerializeField] float area;

    [SerializeField] int currentQuantity;


    private void Start()
    {
        Instance = this;
        for (int i = 0; i < quantity; i++)
        {
            Vector3 newVector = Random.insideUnitSphere * area;
            newVector.y *= 0;
            currentQuantity++;
            Instantiate(NPCpref, positionsToSpawn[Random.Range(0, positionsToSpawn.Count)].position + newVector, Quaternion.identity);
        }
    }
    public void Died()
    {
        Vector3 newVector = Random.insideUnitSphere * area;
        newVector.y *= 0;
        Instantiate(NPCpref, positionsToSpawn[Random.Range(0, positionsToSpawn.Count)].position + newVector, Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        if (positionsToSpawn.Count != 0)
        {
            for (int i = 0; i < positionsToSpawn.Count; i++)
            {
                Gizmos.DrawWireSphere(positionsToSpawn[i].position, area);
            }
        }
    }
}
