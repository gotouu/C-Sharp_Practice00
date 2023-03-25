using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewClass
{
    public class C_View
    {
        // ViewPortのドット配列
        Dictionary<Tuple<int, int>, string> m_viewport = new Dictionary<Tuple<int, int>, string>();
        // ViewPortの縦横ドット数
        int m_width = 0, m_height = 0, m_dots = 0;
        // 何更新に一回画面を更新するか(一行描画の基準　＝　1000000
        int m_defaultViewUpdateSpeed = 1000000;
        // デフォルトのドット表示
        string m_clearDot = "・";

        private static C_View m_view = new C_View();

        private C_View() { }
        ~C_View() { }

        
        public static C_View GetInstance() { return m_view; }// メンバ取得関数

        public void Initialize(int x, int y)
        {
            m_width = x;
            m_height = y;

           m_dots = m_width * m_height;

            for (int i = 0; i < y - 1; i++)
            {
                Tuple<int, int> key;
                for (int l = 0; l < x - 1; l++)
                {
                    key = new Tuple<int, int>(l, i);
                    m_viewport.Add(key, m_clearDot);
                }
                key = new Tuple<int, int>(i, m_dots);
                m_viewport.Add(key, "\n");
            }

        }
        public void VeiwLoop()
        {
            int fps = 0;
            ControllerClass.controller controller = ControllerClass.controller.GetInstance();

            controller.RegistModel(new modelInterface.model());
            do
            {
                ++fps;
                if(fps == m_defaultViewUpdateSpeed * m_height)
                {
                    Console.Clear();
                    ViewPortRefresh();

                    controller.ProcessInput();
                    controller.Update();
                    controller.Draw();
                    foreach (var item in m_viewport)
                    {
                        Console.Write(item.Value.ToString());
                    }
                    fps = 0;

                }
            } while (true);
        }
        private void ViewPortRefresh()
        {
            m_viewport.Clear();
            for (int i = 0; i < m_height - 1; i++)
            {
                Tuple<int, int> key;
                for (int l = 0; l < m_width - 1; l++)
                {
                    key = new Tuple<int, int>(l, i);
                    m_viewport.Add(key, m_clearDot);
                }
                key = new Tuple<int, int>(i, m_dots);
                m_viewport.Add(key, "\n");
            }
        }

        public void WriteViewPortDot(int x, int y, string write)
        {
            Tuple<int, int> key = new Tuple<int, int>(x, y);

            if (m_viewport.ContainsKey(key))
            {
                m_viewport.Remove(key);
                m_viewport.Add(key, write);
            }
        }
        public string GetViewPortDot(int x, int y, string write)
        {
            Tuple<int, int> key = new Tuple<int, int>(x, y);
            string res = "none";

            if (m_viewport.ContainsKey(key))
                m_viewport.TryGetValue(key, out res);

            return res;
        }

    }
}
