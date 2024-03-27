using System;
using Object = UnityEngine.Object;
namespace FactoryPattern
{
    public abstract class AbstractController : IDisposable
    {
        public virtual void Show()
        {
        }

        public virtual void Hide()
        {
        }

        public abstract void Dispose();
    }

    public abstract class AbstractController<TModel> : AbstractController where TModel : IModel
    {
        internal TModel Model { get; private set; }
        internal AbstractView View;

        public static TController Create<TController>(TModel model, AbstractView view)
            where TController : AbstractController<TModel>, new()
        {
            return new TController
            {
                Model = model,
                View = view
            };
        }

        public override void Dispose()
        {
            Object.Destroy(View.gameObject);
        }
    }
}