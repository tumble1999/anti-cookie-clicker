using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using UnityEngine;

namespace TumbleNet.CookieClicker.GooglePlay
{
    public abstract class GameService
    {
        public string id;

        public GameService(string id)
        {
            this.id = id;
        }
    }

    [Serializable]
    public class Leaderboard : GameService
    {
        public Leaderboard(string id) : base(id)
        {
        }

        public void PostScore(long score)
        {
            GooglePlayManager.PostToLeaderboard(id, score);
        }

        public void Open()
        {
            GooglePlayManager.OpenLeaderboard(id);
        }
    }

    [Serializable]
    public class Achivement : GameService
    {
        public Achivement(string id) : base(id)
        {
        }

        public void Unlock()
        {
            GooglePlayManager.UnlockAchivement(id);
        }
        
        public void Increment(int amount)
        {
            GooglePlayManager.IncrementAchievement(id, amount);
        }
    }

    public static class GooglePlayManager
    {
        public static PlayGamesClientConfiguration config;

        public static void Initialize()
        {
            Debug.Log("Initialize");
            config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        //.EnableSavedGames()
        // registers a callback to handle game invitations received while the game is not running.
        //.WithInvitationDelegate(< callback method >)
        // registers a callback for turn based match notifications received while the
        // game is not running.
        //.WithMatchDelegate(< callback method >)
        // requests the email address of the player be available.
        // Will bring up a prompt for consent.
        //.RequestEmail()
        // requests a server auth code be generated so it can be passed to an
        //  associated back end server application and exchanged for an OAuth token.
        //.RequestServerAuthCode(false)
        // requests an ID token be generated.  This OAuth token can be used to
        //  identify the player to other services such as Firebase.
        //.RequestIdToken()
        .Build();

            PlayGamesPlatform.InitializeInstance(config);
            // recommended for debugging:
            PlayGamesPlatform.DebugLogEnabled = true;
            // Activate the Google Play Games platform
            PlayGamesPlatform.Activate();
        }

        public static void SignIn()
        {
            Debug.Log("SignIn");
            Social.localUser.Authenticate((bool success) => {
                // handle success or failure
            });
        }

        public static void SetAchivementProgress(string id, double progress)
        {
            Debug.Log("SetAchivementProgress");
            // unlock achievement (achievement ID "Cfjewijawiu_QA")
            Social.ReportProgress(id, progress, (bool success) => {
                // handle success or failure
            });
        }

        public static void UnlockAchivement(string id)
        {
            Debug.Log("UnlockAcivement");
            SetAchivementProgress(id, 100.0);
        }

        public static void IncrementAchievement(string id, int amount)
        {
            Debug.Log("IncrementAcivement");
            PlayGamesPlatform.Instance.IncrementAchievement(id, amount, (bool success) =>
            {
                // handle success or failure
            });
        }

        
        public static void PostToLeaderboard(string id, long score)
        {
            Debug.Log("PostToLeaderBoard");
            // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
            Social.ReportScore(score, id, (bool success) => {
                // handle success or failure
            });
        }

        public static void OpenAchivements()
        {
            Debug.Log("OpenAchivements");
            Social.ShowAchievementsUI();
        }

        public static void OpenLeaderboards()
        {
            Debug.Log("OpenLeaderboards");
            Social.ShowLeaderboardUI();
        }

        public static void OpenLeaderboard(string id)
        {
            Debug.Log("OpenLeaderboard");
            PlayGamesPlatform.Instance.ShowLeaderboardUI(id);
        }
    }
}
