using FactoryPattern.Menu;
using Test;
using UnityEngine;

namespace FactoryPattern
{
    public class ApplicationRunner : MonoBehaviour
    {
        private void Start()
        {
            Run();
        }

        private void Run()
        {
            UIManager.Instance.Show<MenuView, MenuController, MenuModel>();
        }
    }
}