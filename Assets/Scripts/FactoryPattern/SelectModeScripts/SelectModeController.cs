using System.Net.Sockets;
using UnityEngine;

namespace FactoryPattern.SelectModeScripts
{
    public class SelectModeController : AbstractController<SelectModeModel>
    {
        public override void Show()
        {
            Load();
        }
        
        public override void Hide()
        {
            
        }
        
        private void Load()
        {
            Model.Load();
        }
        
         
    }
}