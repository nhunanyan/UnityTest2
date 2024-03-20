using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace SelectModeScripts
{
    public class SelectModeModel
    {
        public SelectModeInfo SelectModeInfo { get; private set; }
        public event Action OnLeaderboardLoaded;

        public void Load()
        {
            SelectModeInfo = SelectModeDataLoader.Load();
            
            OnLeaderboardLoaded?.Invoke();
        }
    }
}