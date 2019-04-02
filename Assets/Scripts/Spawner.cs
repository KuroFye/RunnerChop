using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    public GameObject objectRef;
    public Vector3 offsetPosition;
    public Quaternion offsetRotation;
}

public class Spawner : MonoBehaviour
{    

    [SerializeField]
    public List<SpawnableObject> objectsToSpawn = new List<SpawnableObject>();
    public int difficultyLevelNeeded = 1;
    public bool useTimeToSpawn = false;
    public float minTime = 1f, maxTime = 2f;
    float countdown;
    int lastItemIndex;
    bool isWaiting = false;
    // Start is called before the first frame update
    void Start()
    {
        lastItemIndex = objectsToSpawn.Count;
        if (GameManager.instance.difficulty < difficultyLevelNeeded)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!useTimeToSpawn)
            return;

        if (GameManager.instance.gameSpeed == 0)
            return;

        if (!isWaiting)
        {
            float fullValue, restValue;
            restValue = GameManager.instance.totalSpeedMultiplier % 1f;
            fullValue = GameManager.instance.totalSpeedMultiplier - restValue;
            float timeModifier = (1f / fullValue) * (1f - restValue);
            countdown = Random.Range(minTime, maxTime)*timeModifier;
            
            isWaiting = true;
            StartCoroutine("Countdown");
        }
    }

    private IEnumerator Countdown()
    {
        float normalizedTime = 0; 
        while (normalizedTime <= countdown)
        {
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        int itemIndex = Random.Range(0, lastItemIndex);
        Instantiate(objectsToSpawn[itemIndex].objectRef, (gameObject.transform.position + objectsToSpawn[itemIndex].offsetPosition), objectsToSpawn[itemIndex].objectRef.transform.rotation);
        isWaiting = false;
    }

    public GameObject SpawnObject()
    {
        int itemIndex = Random.Range(0, lastItemIndex);
        return Instantiate(objectsToSpawn[itemIndex].objectRef, (gameObject.transform.position + objectsToSpawn[itemIndex].offsetPosition), objectsToSpawn[itemIndex].objectRef.transform.rotation);
    }
}
