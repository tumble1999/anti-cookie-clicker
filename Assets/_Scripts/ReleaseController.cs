using System;
using UnityEngine;
using UnityEngine.UI;

namespace TumbleNet.CookieClicker
{
    [Serializable]
    internal enum Release
    {
        Editor,
        Alpha,
        Beta,
        Production
    }
    public class ReleaseController : MonoBehaviour
    {
        [SerializeField] private Release release;
        [SerializeField] private Text releaseInfoText;
        [SerializeField] private int buildNumber;

        // Use this for initialization
        void Start()
        {
#if UNITY_EDITOR
            release = 0;
#endif
            if (release != Release.Production)
            {
                string device = SystemInfo.deviceModel; 
                string version = " v" + Application.version;
                string buildTrack = release.ToString() + " build " + buildNumber;
                releaseInfoText.text = version + " " + buildTrack + " / " + device;
            }
            else
            {
                releaseInfoText.text = "";
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
