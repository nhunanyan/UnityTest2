using System;
using System.Linq;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryPattern.SelectModeScripts
{
    public class SelectModeModel : IModel
    {
        public SelectModeInfo SelectModeInfo { get; private set; }
        public event Action OnSelectModeLoaded;

        public void Load()
        {
            //SelectModeInfo = SelectModeDataLoader.Load();

            var modes = Resources.LoadAll<ModeScriptableObject>("Data");
            SelectModeInfo = new SelectModeInfo();
            SelectModeInfo.SelectMode = modes
                .Select(o => new SelectModeEntryInfo
                    { ModeName = o.ModeName, ModeImage = o.ImageUrl })
                .ToArray();

            OnSelectModeLoaded?.Invoke();
        }

        public void Dispose()
        {
        }
    }
}