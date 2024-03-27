using UnityEditor.SceneManagement;
using UnityEngine;

namespace FactoryPattern
{
    public abstract class AbstractView : MonoBehaviour
    {
        internal abstract void Initialize();
        protected virtual void OnDestroy()
        {
            OnDestroying();
        }
        
        
        protected virtual void OnDestroying()
        {
        }
    }
    public abstract class AbstractView<TController, TModel> : AbstractView
        where TController : AbstractController<TModel>
        where TModel : IModel
    {
        protected TController Controller { get; private set; }
        protected TModel Model => Controller.Model;

        internal void SetController(TController controller)
        {
            Controller = controller;
        }


        protected sealed override void OnDestroy()
        {
            base.OnDestroy();
            Model?.Dispose();
        }
    }
}