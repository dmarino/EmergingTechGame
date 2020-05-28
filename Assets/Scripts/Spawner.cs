using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Vector3 _minPos;

    [SerializeField]
    private Vector3 _maxPos;

    [SerializeField]
    private GameObject[] _prefab;

    public int _amountOfObjectsToSpawn;


    private void Start()
    {
        while(_amountOfObjectsToSpawn < 50)
        {
            Vector3 randomPos = new Vector3(Random.Range(_minPos.x, _maxPos.x), _minPos.y, Random.Range(_minPos.z, _maxPos.z));

            RaycastHit hit;

            if(Physics.Raycast(randomPos, -Vector3.up , out hit,Mathf.Infinity))
            {

                GameObject copy = Instantiate(_prefab[Random.Range(0, 2)], hit.point, Quaternion.identity);
                _amountOfObjectsToSpawn++;

            }

        }
    }
}
