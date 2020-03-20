using UnityEngine;
using UnityEngine.UI;
using TumbleNet.CookieClicker.UI;

namespace TumbleNet.CookieClicker.Items
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(MenuOpener))]
    public class MenuItem : MonoBehaviour
    {
        [SerializeField] private Menu menuToOpen;
        private MenuOpener menuOpener;
        private Button button;

        // Use this for initialization
        void Start()
        {
            button = GetComponent<Button>();
            menuOpener = GetComponent<MenuOpener>();
            button.onClick.AddListener(() => ActivateMenuItem());
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ActivateMenuItem()
        {
            menuOpener.OpenMenu(menuToOpen);
        }
    }
}
