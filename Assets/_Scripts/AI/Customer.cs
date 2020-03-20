using UnityEngine;

namespace TumbleNet.CookieClicker.AI
{
    public enum SpawnSide
    {
        Left,
        Right
    }
    public class Customer : MonoBehaviour
    {
        public SpawnSide spawnSide;
        [SerializeField] private float speed;
        [SerializeField] [Range(0, 100)] private float percentageRich;
        [SerializeField] [Range(0, 100)] private float percentageInterested;
        public bool interested = false;
        public bool goingHome = false;
        public bool rich = false;
        public bool atStall = false;
        public float timer;
        public int willingToPlay;
        public int numberOfCookiesWants;
        public bool haveBeenServed = false;
        public string greeting = "";
        public UnityEngine.Events.UnityAction speechBubbleClickEvent;

        // Use this for initialization
        void Start()
        {
            willingToPlay = 0;
            numberOfCookiesWants = 0;

            willingToPlay = Random.Range(0, 5);
            numberOfCookiesWants = Random.Range(0, 3);

            if (willingToPlay == 0)
            {
                greeting = "I want free cookies!";
            }
            if (numberOfCookiesWants==0)
            {
                greeting = "Here's a donation.";
            }

            if(numberOfCookiesWants==0 && willingToPlay == 0)
            {
                //greeting = "I dont know why I even qued up, i dont want free cookies or to donate";
                percentageInterested = 0;
            }

            SeeIfRich();
        }

        // Update is called once per frame
        void Update()
        {
            if (goingHome || !interested)
            {
                Vector3 direction = spawnSide == SpawnSide.Left ? Vector3.right : Vector3.left;
                transform.Translate(direction * speed * Time.deltaTime);
            }
            if (interested || !goingHome)
            {
                timer -= Time.deltaTime;
            }
        }

        public void SeeIfRich()
        {
            float richAttempt = Random.Range(0, 100);
            if (richAttempt <= percentageRich)
            {
                rich = true;
                AddColor(Color.yellow);
                willingToPlay *= 10;
                numberOfCookiesWants *= 3;
            }
        }

        public void Decide()
        {
            float interestedAttempt = Random.Range(0, 100);
            if (interestedAttempt <= percentageInterested)
            {
                interested = true;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Interest")
            {
                Decide();
                if (interested)
                {
                    other.GetComponent<BuyingArea>().AddCustomerToQueue(this);
                }
            }
        }

        private void AddColor(Color color)
        {
            GetComponent<MeshRenderer>().material.color = color;
        }
    }
}