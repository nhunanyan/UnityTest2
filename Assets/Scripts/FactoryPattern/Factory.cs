using UnityEngine;

namespace FactoryPattern
{
    public class Factory
    {
        private readonly string _baseViewPath; 

        public Factory(string baseViewPath)
        {
            _baseViewPath = baseViewPath;
        }

        public TController Create<TView, TController, TModel>( RectTransform container,TModel model=default)
            where TView : AbstractView<TController, TModel>
            where TController : AbstractController<TModel>, new()
            where TModel : IModel, new()
        {
            var viewName = typeof(TView).Name;
            var viewPath = $"{_baseViewPath}/{viewName}";
            var viewTemplate = Resources.Load<TView>(viewPath);

            var view = Object.Instantiate(viewTemplate, container);

            model ??= new TModel();
            var controller = AbstractController<TModel>.Create<TController>(model, view);
            view.SetController(controller);
            view.Initialize();
            return controller;
        }
    }
}