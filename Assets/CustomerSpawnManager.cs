using System.Collections;
using UnityEngine;

public class CustomerSpawnManager : MonoBehaviour
{

    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject customerPrefab;
    [SerializeField] private float customerSpawnInterval = 5f;
    public static CustomerSpawnManager Instance { get; private set; }



    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(SpawnCustomers());
    }


    IEnumerator SpawnCustomers()
    {
        while (true)
        {
            SelectSpawnPoint();
            yield return new WaitForSeconds(customerSpawnInterval);
        }
    }

    void SelectSpawnPoint()
    {
        int randIndex = Random.Range(0, spawnPoints.Length);

        if (!spawnPoints[randIndex].GetComponent<SpawnPoint>().isEmpty)
        {
            Instantiate(customerPrefab, spawnPoints[randIndex].position, Quaternion.identity);
            spawnPoints[randIndex].GetComponent<SpawnPoint>().isEmpty = true;
        }
    }
}
