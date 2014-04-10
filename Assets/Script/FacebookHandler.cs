using UnityEngine;
using System.Collections.Generic;

namespace SocialModule
{
	public class FacebookHandler
    {
        private bool isInit = false;
		private bool lastResponse = false;
        private string lastResponseString = string.Empty;
        private Texture2D lastResponseTexture;
        public string FeedToId = "";
        public string FeedLink = "";
        public string FeedLinkName = "";
        public string FeedLinkCaption = "";
        public string FeedLinkDescription = "";
        public string FeedPicture = "";
        public string FeedMediaSource = "";
        public string FeedActionName = "";
        public string FeedActionLink = "";
        public string FeedReference = "";
        public bool IncludeFeedProperties = false;
        private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();
        public string ApiQuery = "/me/friends?fields=id,name";

        #region FB.Init()

        public void CallFBInit()
		{
			lastResponseString = "Calling FB.Init";
            FB.Init(OnInitComplete, OnHideUnity);
		}

        private void OnInitComplete()
        {
            Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
			lastResponseString = "FB.Init completed";
            isInit = true;
        }

        private void OnHideUnity(bool isGameShown)
        {
            Debug.Log("Is game shown? " + isGameShown);
        }

        #endregion

        #region FB.Login()

        public void CallFBLogin()
		{
			FB.Login("email,publish_actions,read_friendlists", LoginCallback);
		}

        void LoginCallback(FBResult result)
        {
            if (result.Error != null)
			{
				lastResponse = false;
                lastResponseString = result.Error;
			}
            else if (!FB.IsLoggedIn)
			{
				lastResponse = true;
                lastResponseString = "Login cancelled by Player";
			}
            else
			{
				lastResponse = true;
                lastResponseString = "Login was successful!";
			}
        }

		public void CallFBLogout()
		{
            FB.Logout();
			lastResponseString = "Log out was succesful!";
		}

        #endregion

        #region FB.PublishInstall()

        public void CallFBPublishInstall()
        {
            FB.PublishInstall(PublishComplete);
        }

        private void PublishComplete(FBResult result)
        {
            Debug.Log("Publish response: " + result.Text);
        }

        #endregion

        #region FB.API()

        public void CallFBAPI()
		{
            FB.API(ApiQuery, Facebook.HttpMethod.GET, Callback);
		}

        #endregion

        #region FB.CallFBFeed()

        public void CallFBFeed()
		{
            Dictionary<string, string[]> feedProperties = null;
            if(IncludeFeedProperties)
                feedProperties = FeedProperties;
            FB.Feed(
                toId: FeedToId,
                link: FeedLink,
                linkName: FeedLinkName,
                linkCaption: FeedLinkCaption,
                linkDescription: FeedLinkDescription,
                picture: FeedPicture,
                mediaSource: FeedMediaSource,
                actionName: FeedActionName,
                actionLink: FeedActionLink,
                reference: FeedReference,
                properties: feedProperties,
                callback: Callback
                );
		}

        private void Callback(FBResult result)
        {
            lastResponseTexture = null;
            // Some platforms return the empty string instead of null.
            if (!System.String.IsNullOrEmpty(result.Error))
			{
				lastResponse = false;
                lastResponseString = result.Error;
			}
            else if (!ApiQuery.Contains("/picture"))
			{
				lastResponse = true;
                lastResponseString = result.Text;
			}
            else
            {
				lastResponse = true;
                lastResponseTexture = result.Texture;
			}
        }

        #endregion

        public bool IsInit { get { return isInit; } }

		public bool LastResponse { get { return lastResponse; } }

        public string LastResponseString { get { return lastResponseString; } }

        public Texture2D LastResponseTexture { get { return lastResponseTexture; } }

		public string AccessToken { get { return FB.AccessToken; } }
    }

	public class FBFriends
	{
		public List<FBFriendsItem> data { get; set; }
	}

	public class FBFriendsItem
	{
		public string id {get;set;}
		public string name {get;set;}
	}
}