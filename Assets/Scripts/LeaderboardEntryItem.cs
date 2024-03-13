using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class LeaderboardEntryItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI positionText;
        [SerializeField] private RawImage avatarImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI scoreText;

        public void Setup(LeaderboardEntryInfo entryInfo)
        {
            positionText.text = $"{transform.GetSiblingIndex() + 1}";
            nameText.text = entryInfo.UserName;
            scoreText.text = entryInfo.Score.ToString();
            StartCoroutine(RetrieveAvatarRoutine(entryInfo));
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
    }
}