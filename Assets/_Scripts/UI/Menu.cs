using UnityEngine;

namespace TumbleNet.CookieClicker.UI
{
    public class Menu : MonoBehaviour
    {
        //private RectTransform rectTransform;

        // Use this for initialization
        void Start()
        {
            //rectTransform = GetComponent<RectTransform>();
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OpenMenu()
        {
            gameObject.SetActive(true);
        }

        public void CloseMenu()
        {
            gameObject.SetActive(false);
        }
    }
}