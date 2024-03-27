using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace FactoryPattern.SelectModeScripts
{
    public class SelectModeEntryItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _modeName;
        [SerializeField] private RawImage _modeImage;

        public void Setup(SelectModeEntryInfo entryInfo)
        {
            _modeName.text = entryInfo.ModeName;
            StartCoroutine(RetrieveAvatarRoutine(entryInfo));
        }
        
        private IEnumerator RetrieveAvatarRoutine(SelectModeEntryInfo info)
        {
            string url = string.Format(info.ModeImage, info.ModeName);
            Uri uri = new Uri(url);
            UnityWebRequest request = UnityWebRequest.Get(uri);
            request.downloadHandler = new DownloadHandlerTexture(true);
            yield return request.SendWebRequest();
            _modeImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}