using UnityEngine;

namespace TumbleNet.CookieClicker.AI
{
    public class CustomerController : MonoBehaviour
    {
        [SerializeField] private GameObject[] customerSpawners;
        [SerializeField][Range(0,100)] private float percentageChance;
        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private float zRange;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            SpawnCustomer();
        }

        private void SpawnCustomer()
        {
            int customerSpawnAttempt = Random.Range(0, 100);
            if (customerSpawnAttempt <= percentageChance)
            {
                int spawnSide = Random.Range(0, 2);
                customerPrefab.GetComponent<Customer>().spawnSide = (SpawnSide) spawnSide;
                customerPrefab.transform.position = new Vector3(customerPrefab.transform.position.x , customerPrefab.transform.position.y, Random.Range(-zRange, zRange));
                //customerPrefab.GetComponent<Customer>().SeeIfRich();
                Instantiate(customerPrefab, customerSpawners[spawnSide].transform);
            }
        }
    }
}
