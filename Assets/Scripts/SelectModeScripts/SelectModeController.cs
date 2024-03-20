using UnityEngine;

namespace SelectModeScripts
{
    public class SelectModeController : MonoBehaviour
    {
        
        [SerializeField] private SelectModeView view;

        private readonly SelectModeModel _model = new();
        
         private void Start()
         {
             view.SetModel(_model);
             _model.Load();
         }
    }
}