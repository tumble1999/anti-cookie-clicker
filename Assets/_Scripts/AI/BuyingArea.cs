using System.Collections.Generic;
using UnityEngine;

namespace TumbleNet.CookieClicker.AI
{
    public class BuyingArea : MonoBehaviour
    {
        public Queue<Customer> customers = new Queue<Customer>();
        public Customer[] customersArray;
        [SerializeField] private float startingPosition;
        [SerializeField] private float spaceBetweenCustomers;
        [SerializeField] private int maxCustomers;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            customersArray = customers.ToArray();
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers.ToArray()[i].timer <= 0)
                {
                    customers.ToArray()[i].goingHome = true;
                    customers.ToArray()[i].interested = false;
                    customers.ToArray()[i].GetComponent<MeshRenderer>().material.color = Color.red;
                    RemoveFromQueue(customers.ToArray()[i]);
                }
                customers.ToArray()[i].transform.position = new Vector3(0, customers.ToArray()[i].transform.position.y, startingPosition + ((i - 1) * spaceBetweenCustomers));
            }
        }
        

        public void AddCustomerToQueue(Customer c)
        {
            if (customers.Count < maxCustomers)
            {
                if (!customers.Contains(c))
                {
                    c.atStall = true;
                    customers.Enqueue(c);
                }
            }
            else
            {
                c.goingHome = true;
            }
        }

        void RemoveFromQueue(Customer c)
        {
            for (int i = 0; i < customers.Count; i++)
            {
                Customer currentCustomer = customers.Dequeue();
                if(currentCustomer != c)
                {
                    customers.Enqueue(currentCustomer);
                }
            }
        }
    }
}