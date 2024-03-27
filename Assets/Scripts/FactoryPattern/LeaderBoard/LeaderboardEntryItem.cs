using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace FactoryPattern.LeaderBoard
{
    public class LeaderboardEntryItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI positionText;
        [SerializeField] private RawImage avatarImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private RectTransform highlightBackground;

        private LeaderboardEntryInfo _entryInfo;
        private Coroutine _imageDownloaderRoutine;

        public void Setup(int index, LeaderboardEntryInfo entryInfo)
        {
            _entryInfo = entryInfo;
            positionText.text = $"{index + 1}";
            nameText.text = entryInfo.UserName;
            scoreText.text = entryInfo.Score.ToString();
            if (_imageDownloaderRoutine != null)
            {
                StopCoroutine(_imageDownloaderRoutine);
            }

            _imageDownloaderRoutine = StartCoroutine(RetrieveAvatarRoutine(entryInfo));
        }

        private IEnumerator RetrieveAvatarRoutine(LeaderboardEntryInfo info)
        {
            string url = string.Format(info.AvatarUrl, info.Gender, info.UserName);
            Uri uri = new Uri(url);
            UnityWebRequest request = UnityWebRequest.Get(uri);
            request.downloadHandler = new DownloadHandlerTexture(true);
            yield return request.SendWebRequest();
            avatarImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }

        public void Highlight(string userName)
        {
            highlightBackground.gameObject.SetActive(userName == _entryInfo.UserName);
        }
    }
}