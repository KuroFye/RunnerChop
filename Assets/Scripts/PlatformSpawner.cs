using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    [SerializeField]
    public List<SpawnableObject> objectsToSpawn = new List<SpawnableObject>();

    public float spaceChance = 0.3f, minSpaceDistance = 1.2f, maxSpaceDistance = 2.5f;
    float countdown;
    bool isAfterHole = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Countdown()
    {
        float normalizedTime = 0;
        
        while (normalizedTime <= countdown)
        {
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        Instantiate(objectsToSpawn[1].objectRef, (gameObject.transform.position + objectsToSpawn[1].offsetPosition), objectsToSpawn[1].offsetRotation);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Platform") || other.tag.Equals("OneWayPlatform"))
        {
            float chance = Random.Range(0, 1f);
            if(chance < spaceChance)
            {                
                float fullValue, restValue;
                restValue = GameManager.instance.totalSpeedMultiplier % 1f;
                fullValue = GameManager.instance.totalSpeedMultiplier - restValue;
                float timeModifier = (1f / fullValue) * (1f - restValue);
                countdown = Random.Range(minSpaceDistance, maxSpaceDistance) * timeModifier;
                StartCoroutine("Countdown");
            }
            else
            {
                Instantiate(objectsToSpawn[0].objectRef, (gameObject.transform.position + objectsToSpawn[0].offsetPosition), objectsToSpawn[0].offsetRotation);
            }            
        }

    }
}
