using System;
using TumbleNet.CookieClicker.AI;
using TumbleNet.CookieClicker.GooglePlay;
using TumbleNet.CookieClicker.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TumbleNet.CookieClicker
{

    [Serializable]
    internal class InitialValues
    {
        public int startingCoins = 0;
        public int startingCookies = 0;
    }

    public class GameController : MonoBehaviour
    {
        #region GameObjects
        [SerializeField] private GameObject floatyText;
        [SerializeField] private GameObject canvas;
        [SerializeField] private GameObject player;
        [SerializeField] private BuyingArea buyingArea;
        [SerializeField] private Camera[] views;
        [SerializeField] private GameObject cookie;
        [SerializeField] private SpeechBubble speechBubble;
        #endregion

        #region Leaderboards
        private Leaderboard leaderboardToltalCookies;
        private Leaderboard leaderboardCoinCoint;
        private Leaderboard leaderboardCookieCoount;
        #endregion

        [SerializeField] private InitialValues initialValues;
        [SerializeField] private Text coinCountText;
        [SerializeField] private Text cookieCountText;

        private bool updatedOnce;

        /// <summary>
        /// Get the the instance of GameController currently in the scene
        /// </summary>
        public static GameController Instance
        {
            get
            {
                GameController controller;
                controller = GameObject.FindWithTag("GameController").GetComponent<GameController>();
                return controller;
            }
        }

        /// <summary>
        /// The number of cookies the player has
        /// </summary>
        private int cookies;
        private int totalCookiesSold;
        private int coins;
        private int currentView;
        [SerializeField] int minCoinsToBeRich;

        public Customer CurrentCustomer
        {
            get
            {
                return buyingArea.customers.Dequeue();
            }
        }

        public Customer[] Customers
        {
            get
            {
                return buyingArea.customers.ToArray();
            }
        }

        #region MonoBehaiviour

        /// <summary>
        /// Use this for initialization
        /// </summary>
        void Start()
        {
            InitGooglePlay();
            UpdateValues();
            UpdateCoinCounter();
            UpdateCookieCounter();
            UpdateGooglePlay();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {

            if (!updatedOnce)
            {
                GooglePlayManager.UnlockAchivement(GPGSIds.achievement_get_the_game);
                updatedOnce = true;
            }

            cookie.SetActive(buyingArea.customers.Count > 0);
            if(buyingArea.customers.Count > 0)
            {
                Customer frontCustomer = buyingArea.customers.ToArray()[0];
                speechBubble.Show(frontCustomer.greeting, frontCustomer.numberOfCookiesWants, frontCustomer.willingToPlay,Mathf.CeilToInt(frontCustomer.timer), frontCustomer.speechBubbleClickEvent);
            }
            else
            {
                speechBubble.Hide();
            }
            player.GetComponent<MeshRenderer>().material.color = coins >= minCoinsToBeRich ? Color.yellow : Color.white;
            if (coins >= minCoinsToBeRich)
            {
                player.GetComponent<MeshRenderer>().material.color = Color.yellow;
                GooglePlayManager.UnlockAchivement(GPGSIds.achievement_money_money_money);
            }
            else
            {
                player.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
        #endregion

        #region Cookie Management
        /// <summary>
        /// Add Cookies to the players cookie count.
        /// </summary>
        /// <param name="number">The number of cookies to add</param>
        public void AddCookies(int number)
        {
            cookies += number;
            UpdateCookieCounter();
            PlayerPrefs.SetInt("cookies", cookies);
        }

        /// <summary>
        /// Get the current number of cookies the player has.
        /// </summary>
        /// <returns></returns>
        public int GetCookieCount()
        {
            return cookies;
        }

        public void AddCookieSoldToTotal(int number)
        {
            totalCookiesSold += number;
            PlayerPrefs.SetInt("totalCookiesSold", totalCookiesSold);
            UpdateGooglePlay();
        }
        #endregion

        #region Coins Management
        /// <summary>
        /// Add Coins to the players cookie count.
        /// </summary>
        /// <param name="number">The number of cookies to add</param>
        public void AddCoins(int number)
        {
            coins += number;
            UpdateCoinCounter();
            PlayerPrefs.SetInt("coins", coins);
        }

        /// <summary>
        /// Get the current number of cookies the player has.
        /// </summary>
        /// <returns>Coins</returns>
        public int GetCoinCount()
        {
            return coins;
        }
        #endregion

        #region Values
        public void ResetValues()
        {
            PlayerPrefs.DeleteAll();
            UpdateValues();
            UpdateViews();
        }

        public void UpdateValues()
        {
            coins = PlayerPrefs.GetInt("coins", initialValues.startingCoins);
            cookies = PlayerPrefs.GetInt("cookies", initialValues.startingCookies);
            totalCookiesSold = PlayerPrefs.GetInt("totalCookiesSold", 0);
            UpdateGooglePlay();
            UpdateCoinCounter();
            UpdateCookieCounter();
        }
        #endregion

        #region TextDisplay
        /// <summary>
        /// Updates the dislay for the number of cookies.
        /// </summary>
        void UpdateCookieCounter()
        {
            cookieCountText.text = cookies + "";
        }

        void UpdateCoinCounter()
        {
            coinCountText.text = coins + "";
        }
        #endregion

        public void NotifyPlayer(string message)
        {
            floatyText.GetComponent<FloatyUpText>().text = message;
            Instantiate(floatyText, canvas.transform);
        }

        #region Views
        public void FixCurrentView()
        {
            currentView = (int)Mathf.PingPong(currentView, views.Length - 1);
        }
        public void UpdateViews()
        {
            FixCurrentView();
            for (int i = 0; i < views.Length; i++)
            {
                views[i].gameObject.SetActive(currentView == i);
            }
        }
        public void SwitchView()
        {
            currentView++;
            GooglePlayManager.UnlockAchivement(GPGSIds.achievement_there_are_no_facts_only_interpretations);
            UpdateViews();
        }
        #endregion

        #region GooglePlay
        void InitGooglePlay()
        {
            GooglePlayManager.Initialize();
            GooglePlayManager.SignIn();
        }
        void UpdateGooglePlay()
        {
            GooglePlayManager.PostToLeaderboard(GPGSIds.leaderboard_number_of_cookies_sold,totalCookiesSold);
            GooglePlayManager.PostToLeaderboard(GPGSIds.leaderboard_coin_count, coins);
            GooglePlayManager.PostToLeaderboard(GPGSIds.leaderboard_cookie_count, cookies);
        }
        #endregion
    }
}
