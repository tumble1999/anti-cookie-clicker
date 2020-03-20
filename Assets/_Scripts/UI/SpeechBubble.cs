using UnityEngine;
using UnityEngine.UI;

namespace TumbleNet.CookieClicker.UI
{
    public class SpeechBubble : MonoBehaviour
    {
        [SerializeField] private Text chatText;
        [SerializeField] private GameObject cookiesContainer;
        [SerializeField] private Text cookiesText;
        [SerializeField] private GameObject coinsContainer;
        [SerializeField] private Text coinsText;
        [SerializeField] private GameObject countdownContainer;
        [SerializeField] private Text countdownText;
        private Button button;

        // Use this for initialization
        void Start()
        {
            button = GetComponent<Button>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void SetValues(bool active, string chat, int cookies, int coins, int countdown, UnityEngine.Events.UnityAction pressAction)
        {
            gameObject.SetActive(active);

            chatText.gameObject.SetActive(chat != "");
            chatText.text = chat;

            //coinsContainer.SetActive(coins > 0);
            coinsText.text = coins.ToString();

            cookiesContainer.SetActive(cookies > 0);
            cookiesText.text = cookies.ToString();

            countdownContainer.SetActive(countdown > -1);
            countdownText.text = countdown.ToString();

            button.onClick.AddListener(pressAction);
        }
        public void Show(string chat, int cookies, int coins, int countdown, UnityEngine.Events.UnityAction pressAction)
        {
            SetValues(true, chat, cookies, coins, countdown, pressAction);
        }

        public void Hide()
        {
            SetValues(false, "", 0, 0,-1, () => { });
        }
    }

}