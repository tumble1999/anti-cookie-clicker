using System.Collections.Generic;
using TumbleNet.CookieClicker.Items;
using TumbleNet.CookieClicker.Score;
using UnityEngine;

namespace TumbleNet.CookieClicker
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private GameObject shopItemPrefab;
        [SerializeField] private int numberOfShopItems;
        [SerializeField] private Transform shopItemContainer;
        [SerializeField] private int[] baseCookieCount;
        [SerializeField] private int numberOfItemsToSkip;
        private List<GameObject> shopItems = new List<GameObject>();
        public List<GameObject> coveredShopItems = new List<GameObject>();

        [SerializeField] public int coverLength;

        public static ShopController Instance
        {
            get
            {
                ShopController controller;
                controller = GameObject.FindWithTag("GameController").GetComponent<ShopController>();
                return controller;
            }
        }

        public List<GameObject> ShopItems
        {
            get
            {
                return shopItems;
            }

            private set
            {
                shopItems = value;
            }
        }

        // Use this for initialization
        void Start()
        {
            GenerateShopItems();
        }

       
        // Update is called once per frame
        void Update()
        {
        }

        private void GenerateShopItems()
        {
            for (int i = 0; i < numberOfShopItems; i++)
            {
                if (i >= numberOfItemsToSkip)
                {
                    GameObject currentItem = Instantiate(shopItemPrefab, shopItemContainer);
                    float cookieCount = CookieCount(i);
                    int price = CalculatePrice(cookieCount);

                    //Debug.Log("i:" + i + " baseCookieCountID:" + baseCookieCountID + " tenMultiplier:" + tenMultiplyer + " cookieCount:" + cookieCount);

                    currentItem.name = "["+i+"]"+cookieCount + "Cookies";
                    currentItem.GetComponent<ShopItem>().itemName = cookieCount + " Cookies";
                    currentItem.GetComponent<ManageScore>().price = price;
                    currentItem.GetComponent<ManageScore>().cookies = Mathf.RoundToInt(cookieCount);
                    ShopItems.Add(currentItem);

                }
            }
        }

        public float CookieCount(int i)
        {
            int baseCookieCountID = (i) % (baseCookieCount.Length);
            float tenMultiplyer = Mathf.Pow(10, Mathf.Floor(i / baseCookieCount.Length));
            float cookieCount = baseCookieCount[baseCookieCountID] * tenMultiplyer;
            return cookieCount;
        }

        public int CalculatePrice(float cookieCount)
        {
            float w = 1.06f;
            float power = 1 / w;
            float output = Mathf.Pow(cookieCount, power);
            return Mathf.RoundToInt(output);
        }
    } 
}
