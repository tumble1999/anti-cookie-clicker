using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TumbleNet.CookieClicker.UI
{
    public class MenuOpener : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OpenMenu(Menu m)
        {
            m.OpenMenu();
        }
        public void CloseMenu(Menu m)
        {
            m.CloseMenu();
        }
    }
}
