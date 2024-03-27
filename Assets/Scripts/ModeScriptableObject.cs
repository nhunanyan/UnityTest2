using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Mode", menuName = "Nina/Mode")]
    public class ModeScriptableObject : ScriptableObject
    {
        [SerializeField] private string modeName;
        [SerializeField] private string imageUrl;

        public string ModeName => modeName;

        public string ImageUrl => imageUrl;
    }
}