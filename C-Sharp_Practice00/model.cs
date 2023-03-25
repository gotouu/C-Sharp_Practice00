using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelInterface
{
    public interface IModel
    {
        public void Initialize();
        public void Update();
        public void Draw();
    }
    public class model : IModel
    {
        string m_viewModel = "〇";
        int m_x = 0, m_y = 0;
        public model() { }
        ~model() { }

        public void Initialize()
        {
            ControllerClass.controller controller = ControllerClass.controller.GetInstance();

            Func<int, int> f = n => { --m_x; return 10; };
            controller.RegistInputAction("LeftArrow", "test", f);
            f = n => { ++m_x; return 10; };
            controller.RegistInputAction("RightArrow", "test", f);
            f = n => { --m_y; return 10; };
            controller.RegistInputAction("UpArrow", "test", f);
            f = n => { ++m_y; return 10; };
            controller.RegistInputAction("DownArrow", "test", f);
        }
        public void Update() { 
        }
        public void Draw()
        {
            ViewClass.C_View view = ViewClass.C_View.GetInstance();
            view.WriteViewPortDot(m_x, m_y, m_viewModel);
        }
    }
}
