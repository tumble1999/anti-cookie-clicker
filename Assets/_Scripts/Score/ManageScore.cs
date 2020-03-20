using System;
using TumbleNet.CookieClicker.AI;
using TumbleNet.CookieClicker.GooglePlay;
using UnityEngine;

namespace TumbleNet.CookieClicker.Score
{
    public class ManageScore : MonoBehaviour
    {
        [SerializeField] public int cookies;
        [SerializeField] public int price;

        /// <summary>
        /// The referance to the scene's GameController
        /// </summary>
        private GameController gameController;

        public int Cookies
        {
            get
            {
                return cookies;
            }
        }

        public int Price
        {
            get
            {
                return price;
            }
        }

        // Use this for initialization
        void Start()
        {
            gameController = GameController.Instance;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddCoins(int coinsToAdd)
        {
            gameController.AddCoins(coinsToAdd);
        }

        public void AddCookies(int cookiesToAdd)
        {
            gameController.AddCookies(cookiesToAdd);
        }

        public void ResetValues()
        {
            gameController.ResetValues();
        }

        public void SellCookies(Action<bool> callback)
        {            
            if (gameController.GetCookieCount() >= cookies)
            {
                AddCookies(-cookies);
                gameController.AddCookieSoldToTotal(cookies);
                AddCoins(price);
            }
            else
            {
                gameController.NotifyPlayer("Need more Cookies");
            }
            callback(gameController.GetCookieCount() >= cookies);
        }
        public void SellCookies()
        {
            SellCookies((bool worked) => { });
        }

        public void BuyCookies(Action<bool> callback)
        {
            if (gameController.GetCoinCount()>=price)
            {
                AddCookies(cookies);
                AddCoins(-price);
                GooglePlayManager.UnlockAchivement(GPGSIds.achievement_whos_buying_cookies_now);
            }
            else
            {
                gameController.NotifyPlayer("Not Enough money");
                GooglePlayManager.UnlockAchivement(GPGSIds.achievement_you_cant_afford_that);
            }
        }

        public void BuyCookies()
        {
            BuyCookies((bool worked) => { });
        }

        public void ServeCustomer()
        {
            Customer currentCustomer = gameController.Customers[0];

            cookies = currentCustomer.numberOfCookiesWants;
            price = currentCustomer.willingToPlay;
            if (gameController.GetCookieCount() >= cookies)
            {
                SellCookies((bool worked) =>
                {
                    if (worked)
                    {
                        Customer currentCustomerDone = gameController.CurrentCustomer;
                        GooglePlayManager.UnlockAchivement(GPGSIds.achievement_you_have_to_start_somewhere);
                        currentCustomerDone.goingHome = true;
                        currentCustomerDone.GetComponent<MeshRenderer>().material.color = Color.green;
                    }
                });
                
            }
            else
            {
                gameController.NotifyPlayer("Out of Cookies");
                GooglePlayManager.UnlockAchivement(GPGSIds.achievement_sorry_were_out_of_stock);
            }
        }
    }

}