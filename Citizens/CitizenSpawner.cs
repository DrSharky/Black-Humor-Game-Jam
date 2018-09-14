using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class CitizenSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject citizen;
    [SerializeField]
    GameObject wanderCitizen;

    Terrain totalArea;

    float xMinVal = 0.0f;
    float xMaxVal = 107.7f;
    float zMinVal = 42.55f;
    float zMaxVal = 165.25f;

    List<Vector3> crowdSpawnPoints;

    void Start ()
    {
        crowdSpawnPoints = new List<Vector3>();
        for(int i = 0; i < 4; i++)
        {
            crowdSpawnPoints.Add(GenerateCrowdPoint(xMinVal, xMaxVal, zMinVal, zMaxVal));
            for (int j = 0; j < 5; j++)
            {
                Vector3 spawnPosition = GeneratePositionInCrowdRadius(crowdSpawnPoints[i]);
                GameObject newCit = Instantiate(citizen, spawnPosition, GenerateLookRotation(crowdSpawnPoints[i], spawnPosition));
                newCit.GetComponentInChildren<BaseCitizen>().player = player;
            }
        }

        for(int k = 0; k < 35; k++)
        {
            Vector3 wanderSpawn = GenerateCrowdPoint(20.0f, 175.0f, 20.0f, 175.0f);
            GameObject newWanderCit = Instantiate(wanderCitizen, wanderSpawn, transform.rotation);
            newWanderCit.GetComponentInChildren<BaseCitizen>().player = player;
            newWanderCit.GetComponentInChildren<WanderingAI>().player = player;
        }
	}

    Vector3 GenerateCrowdPoint(float xMinVal, float xMaxVal, float zMinVal, float zMaxVal)
    {
        float xGenVal = UnityEngine.Random.Range(xMinVal, xMaxVal);
        float zGenVal = UnityEngine.Random.Range(zMinVal, zMaxVal);

        return new Vector3(xGenVal, 1.0f, zGenVal);
    }

    Vector3 GeneratePositionInCrowdRadius(Vector3 origin)
    {
        Vector3 spawnPosition = UnityEngine.Random.insideUnitSphere * 10.0f;
        spawnPosition += origin;
        return spawnPosition;
    }

    Quaternion GenerateLookRotation(Vector3 origin, Vector3 spawnPos)
    {
        Vector3 newDir = origin - spawnPos;
        return Quaternion.LookRotation(newDir);
    }

    Vector3 GenerateWanderSpawnPoint()
    {
        return new Vector3();
    }
}
