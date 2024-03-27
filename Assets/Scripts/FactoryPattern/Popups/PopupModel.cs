using System;

namespace FactoryPattern.Popups
{
    public class PopupModel : IModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        
        public void Dispose()
        {
        }
    }
}