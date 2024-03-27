using System;
using System.Collections.Generic;
using FactoryPattern;
using FactoryPattern.Menu;
using UnityEngine;

namespace Test
{
    public sealed class UIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform container;
        private readonly Factory _factory = new Factory("UI/Views");

        private AbstractController _current;
        private Stack<AbstractController> _popupsStack = new Stack<AbstractController>();

        private static UIManager _instance;

        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UIManager>();
                    if (_instance == null)
                    {
                        throw new Exception("UIManager game object does not exists on the scene.");
                    }
                }


                return _instance;
            }
        }


        public TController Show<TView, TController, TModel>(bool isPopup = false, TModel model = default)
            where TView : AbstractView<TController, TModel>
            where TController : AbstractController<TModel>, new()
            where TModel : IModel, new()
        {
            if (!isPopup)
            {
                if (_current != null)
                {
                    Close(_current);
                    _current = null;
                }

                var controller = ShowInternal<TView, TController, TModel>(model);
                _current = controller;
                return controller;
            }
            else
            {
                if (_popupsStack.Count != 0)
                {
                    var top = _popupsStack.Peek();
                    top.Hide();
                }

                var controller = ShowInternal<TView, TController, TModel>(model);
                _popupsStack.Push(controller);
                return controller;
            }
        }

        private TController ShowInternal<TView, TController, TModel>(TModel model=default)
            where TView : AbstractView<TController, TModel>
            where TController : AbstractController<TModel>, new()
            where TModel : IModel, new()
        {
            var controller = _factory.Create<TView, TController, TModel>(container,model);
            controller.Show();
            return controller;
        }

        public void Close<TController>(TController controller, bool isPopup = false)
            where TController : AbstractController
        {
            if (!isPopup)
            {
                controller.Hide();
                controller.Dispose();
            }
            else
            {
                if (_popupsStack.Count > 1)
                {
                    var top = _popupsStack.Peek();
                    _popupsStack.Pop();
                    top.Dispose();
                    top = _popupsStack.Peek();
                    top.Hide();
                }
                else
                {
                    var top = _popupsStack.Peek();
                    _popupsStack.Pop();
                    top.Dispose();
                }
            }
        }
    }
}