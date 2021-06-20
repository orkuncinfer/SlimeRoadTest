using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingsManager : MonoBehaviour
{
    public GameObject[] ringPrefabs;
    public GameObject currentRing;
    public GameObject platform;

    public int initialRings = 10;

    [HideInInspector]public Stack<GameObject> defaultRings = new Stack<GameObject>();
    [HideInInspector]public Stack<GameObject> movingRings = new Stack<GameObject>();

    public static RingsManager instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        CreateRings(10);

        for (int i = 0; i < initialRings; i++)
        {
            SpawnRings();
        }
    }



    public void CreateRings(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            defaultRings.Push(Instantiate(ringPrefabs[0]));
            movingRings.Push(Instantiate(ringPrefabs[1]));
            movingRings.Peek().SetActive(false);
            defaultRings.Peek().SetActive(false);
        }

    }

    public void SpawnRings()
    {
        if (defaultRings.Count == 0 || movingRings.Count == 0)
        {
            CreateRings(10);
        }

        int randomIndex = Random.Range(0, 2);

        if (randomIndex == 0)
        {
            GameObject tmp = defaultRings.Pop();
            tmp.transform.SetParent(platform.transform);
            tmp.SetActive(true);
            Vector3 _spawnPoint = currentRing.transform.GetChild(0).transform.position;
            tmp.transform.position = new Vector3(_spawnPoint.x, _spawnPoint.y, Random.Range(-1f, 1f));
            currentRing = tmp;
        }
        else
        {
            GameObject tmp = movingRings.Pop();
            tmp.transform.SetParent(platform.transform);
            tmp.SetActive(true);
            Vector3 _spawnPoint = currentRing.transform.GetChild(0).transform.position;
            tmp.transform.position = new Vector3(_spawnPoint.x, _spawnPoint.y, Random.Range(-1f, 1f));
            currentRing = tmp;
        }

    }


}
