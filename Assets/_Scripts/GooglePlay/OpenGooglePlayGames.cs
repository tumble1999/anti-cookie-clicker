using UnityEngine;
using UnityEngine.UI;

namespace TumbleNet.CookieClicker.GooglePlay
{
    public class OpenGooglePlayGames : MonoBehaviour
    {
        [SerializeField] private OpenWhat openWhat;

        public void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() => OpenGooglePlay());
        }

        public enum OpenWhat
        {
            Leaderboards,
            Achievements
        }

        void OpenGooglePlay()
        {
            switch (openWhat)
            {
                case OpenWhat.Leaderboards:
                    GooglePlayManager.OpenLeaderboards();
                    break;
                case OpenWhat.Achievements:
                    GooglePlayManager.OpenAchivements();
                    break;
                default:
                    break;
            }
        }
    }
}
