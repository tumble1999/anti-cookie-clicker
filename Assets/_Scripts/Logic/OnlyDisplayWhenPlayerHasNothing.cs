using TumbleNet.CookieClicker.Score;
using UnityEngine;

namespace TumbleNet.CookieClicker.Logic
{
    public class OnlyDisplayWhenPlayerHasNothing : MonoBehaviour
    {
        private GameController gameController;
        [SerializeField] private ManageScore manageScore;

        // Use this for initialization
        void Start()
        {
            gameController = GameController.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            gameObject.SetActive((gameController.GetCookieCount() < manageScore.cookies) && (gameController.GetCoinCount() <= manageScore.price));
        } 
    }
}
