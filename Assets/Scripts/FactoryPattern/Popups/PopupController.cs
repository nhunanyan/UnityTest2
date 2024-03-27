using System;

namespace FactoryPattern.Popups
{
    public enum Result
    {
        Yes,
        No
    }
    public class PopupController : AbstractController<PopupModel>
    {
        public event Action<Result> OnResult; 
        public override void Show()
        {
        }

        public override void Hide()
        {
        }

        public void OnYes()
        {
            OnResult?.Invoke(Result.Yes);
        }

        public void OnNo()
        {
            OnResult?.Invoke(Result.No);
        }
    }
}