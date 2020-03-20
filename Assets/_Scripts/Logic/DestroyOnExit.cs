using TumbleNet.CookieClicker.AI;
using UnityEngine;

namespace TumbleNet.CookieClicker.Logic
{
    public class DestroyOnExit : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Customer>())
            {
                Destroy(other.gameObject);
            }
        }
    }
}
