
using UnityEngine;

namespace TumbleNet.CookieClicker.UI
{
    public class ChangeView : MonoBehaviour
    {
        GameController gameController;
        // Use this for initialization
        void Start()
        {
            gameController = GameController.Instance;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitiateViewChange()
        {
            gameController.SwitchView();
        }
    }
}