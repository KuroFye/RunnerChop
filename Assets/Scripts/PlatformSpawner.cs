using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    [SerializeField]
    public List<SpawnableObject> objectsToSpawn = new List<SpawnableObject>();

    public float spaceChance = 0.3f, minSpaceDistance = 8.5f, maxSpaceDistance = 19f;
    GameObject lastPlatformRef;
    float distance;
    bool isAfterHole = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator CheckDistance()
    {
        float currentDistance = Vector3.Distance(gameObject.transform.position, lastPlatformRef.transform.position);
        while (currentDistance < distance)
        {
            currentDistance = Vector3.Distance(gameObject.transform.position, lastPlatformRef.transform.position);
            yield return null;
        }
        lastPlatformRef = Instantiate(objectsToSpawn[1].objectRef, (gameObject.transform.position + objectsToSpawn[1].offsetPosition), objectsToSpawn[1].offsetRotation);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Platform") || other.tag.Equals("OneWayPlatform"))
        {
            float chance = Random.Range(0, 1f);
            if(chance < spaceChance)
            {                
                distance = Random.Range(minSpaceDistance, maxSpaceDistance);
                if(lastPlatformRef == null)
                    lastPlatformRef = Instantiate(objectsToSpawn[0].objectRef, (gameObject.transform.position + objectsToSpawn[0].offsetPosition), objectsToSpawn[0].offsetRotation);
                StartCoroutine("CheckDistance");
            }
            else
            {
                lastPlatformRef = Instantiate(objectsToSpawn[0].objectRef, (gameObject.transform.position + objectsToSpawn[0].offsetPosition), objectsToSpawn[0].offsetRotation);
            }            
        }

    }
}
