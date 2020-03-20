using UnityEngine;

namespace TumbleNet.CookieClicker.Logic
{

    public class ControlWhenVisible : MonoBehaviour
    {
        [SerializeField]
        private bool visibleWhileDeveloping=false, visibleWhileInBuild=false;

        void Awake()
        {
#if UNITY_EDITOR
            gameObject.SetActive(visibleWhileDeveloping);
#else
            gameObject.SetActive(visibleWhileInBuild);
#endif
        }
    } 
}
