using TumbleNet.CookieClicker.Interfaces;
using UnityEngine;

namespace TumbleNet.CookieClicker.UI
{
    public class CookieButton : MonoBehaviour, IClickable
    {
        /// <summary>
        /// The referance to the scene's GameController
        /// </summary>
        private GameController gameController;
        
        #region MonoBehaviour
        // Use this for initialization
        void Start()
        {
            gameController = GameController.Instance;
        }
        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        public void OnClick()
        {
            gameController.AddCookies(1);
        }
    }
}
