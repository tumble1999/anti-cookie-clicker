using TumbleNet.CookieClicker.Score;
using UnityEngine;
using UnityEngine.UI;

namespace TumbleNet.CookieClicker.Items
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(ManageScore))]
    public class ShopItem : MonoBehaviour
    {
        private GameController gameController;
        private ShopController shopController;
        private GameObject cover;
        private Button button;
        private ManageScore manageScore;

        [SerializeField] private Text itemNameText;
        [SerializeField] private Text cookieAmountText;
        [SerializeField] private Text priceText;

        [SerializeField] public string itemName;
        public int price;
        public int cookies;
        public int timesBought;

        // Use this for initialization
        void Start()
        {
            gameController = GameController.Instance;
            shopController = ShopController.Instance;
            button = GetComponent<Button>();
            manageScore = GetComponent<ManageScore>();
            cover = GetComponentInChildren<ShopItemCover>().gameObject;

            price = manageScore.Price;
            cookies = manageScore.Cookies;

            itemNameText.text = itemName;
            cookieAmountText.text = cookies.ToString();
            priceText.text = price.ToString();
            button.onClick.AddListener(() => BuyItem());
        }

        // Update is called once per frame
        void Update()
        {
            bool withinVisibleCover = shopController.coveredShopItems.IndexOf(gameObject) <= shopController.coverLength;
            bool ableToAfford = gameController.GetCoinCount() >= price;
            bool onCoverList = shopController.coveredShopItems.Contains(gameObject);
            bool visibleCovered = !ableToAfford & withinVisibleCover;
            bool shouldBeVisible = ableToAfford | visibleCovered;

            cover.SetActive(!ableToAfford);
            gameObject.SetActive(shouldBeVisible);

            if (ableToAfford & onCoverList)
            {
                shopController.coveredShopItems.Remove(gameObject);
            }
            if(!ableToAfford & !onCoverList)
            {
                shopController.coveredShopItems.Add(gameObject);
            }
        }

        public void BuyItem()
        {
            manageScore.BuyCookies((bool worked) => {
                timesBought++;
            });
        }
        void UpdateValues()
        {
            price = manageScore.Price;
            cookies = manageScore.Cookies;

            itemNameText.text = itemName;
            cookieAmountText.text = cookies.ToString();
            priceText.text = price.ToString();
            button.onClick.AddListener(() => BuyItem());
        }
    }
}