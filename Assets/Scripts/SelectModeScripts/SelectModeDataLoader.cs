using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace SelectModeScripts
{
    public class SelectModeDataLoader
    {
        public static SelectModeInfo Load()
        {
            var json = Resources.Load<TextAsset>("Data/selectmodeboard").text;
            var selectModeBoard = JsonConvert.DeserializeObject<SelectModeInfo>(json);
            int count = selectModeBoard.SelectMode.GetLength(0);
            selectModeBoard.SelectMode = selectModeBoard.SelectMode.Take(count).ToArray();

            return selectModeBoard;
        }
    }
}