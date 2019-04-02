using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnPoint
{
    public Spawner spawnRef;
    public float probabilityOfAppearance;

    public static int CompareByChance(SpawnPoint s1, SpawnPoint s2)
    {
        if (s1.probabilityOfAppearance > s2.probabilityOfAppearance)
            return 1;
        if (s1.probabilityOfAppearance < s2.probabilityOfAppearance)
            return -1;
        else
            return 0;
    }

}


public class SpawnerController : MonoBehaviour
{
    [Tooltip("Lower probability objects have priority. Last spawns if none other will")]
    [SerializeField]
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    public float minDistance = 5f, maxDistance = 8f;

    float currentDistanceToSpawn = -1f, spawnedObjectDistance = 0f;
    GameObject lastInstantiatedObject;
    bool initialized = false;
    List<SpawnPoint> activeSpawnPoints = new List<SpawnPoint>();
    // Start is called before the first frame update
    void Start()
    {
        
        //spawnPoints.Add(spawnPoints.Count());
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized)
            return;

        if (spawnedObjectDistance > currentDistanceToSpawn)
        {
            float chance = Random.Range(0f, 1f);
            foreach(SpawnPoint point in activeSpawnPoints)
            {
                Debug.Log("checking percent: " + point.probabilityOfAppearance.ToString());
                if(chance<= point.probabilityOfAppearance)
                {
                    lastInstantiatedObject = point.spawnRef.SpawnObject();
                    currentDistanceToSpawn = Random.Range(minDistance, maxDistance);
                    spawnedObjectDistance = Vector3.Distance(gameObject.transform.position, lastInstantiatedObject.transform.position);
                    Debug.Log("Spawned percent: " + point.probabilityOfAppearance.ToString() + " Spawned distance: "+spawnedObjectDistance.ToString());
                    break;
                }
            }

            if(spawnedObjectDistance > currentDistanceToSpawn)
            {
                lastInstantiatedObject = activeSpawnPoints[activeSpawnPoints.Count - 1].spawnRef.SpawnObject();
                currentDistanceToSpawn = Random.Range(minDistance, maxDistance);
                spawnedObjectDistance = Vector3.Distance(gameObject.transform.position, lastInstantiatedObject.transform.position);
            }
        }
        else
        {
            spawnedObjectDistance = Vector3.Distance(gameObject.transform.position, lastInstantiatedObject.transform.position);
        }

    }

    public void InitializeSpawnerController()
    {
        foreach(SpawnPoint point in spawnPoints)
        {
            if (point.spawnRef.gameObject.activeSelf)
                activeSpawnPoints.Add(point);
        }

        activeSpawnPoints.Sort(SpawnPoint.CompareByChance);

        initialized = true;
    }




}
