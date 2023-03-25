using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewClass;

namespace ControllerClass
{
    using FunctionDictionary = Dictionary<string, Func<int, int>>;
    public class controller
    {
        ConsoleKeyInfo m_consoleKeyInfo;
        // 登録されている関数群
        Dictionary<string, FunctionDictionary> m_inputActionFunc = new Dictionary<string, FunctionDictionary>();
        Dictionary<string, modelInterface.IModel> m_modelList = new Dictionary<string, modelInterface.IModel>();
        private static controller m_controller = new controller();
        private controller()
        {
            
        }
        ~controller() { }
        public static controller GetInstance() { return m_controller; }// メンバ取得関数

        public void Update()
        {
            foreach (var item in m_modelList)
            {
                item.Value.Update();
            }
        }
        public void Draw()
        {
            foreach (var item in m_modelList)
            {
                item.Value.Draw();
            }
        }

        public void ProcessInput()
        {
            // 待ち処理除外
            if (!Console.KeyAvailable) return;
            // 登録済みキーであれば関数群を実行する
            m_consoleKeyInfo = Console.ReadKey();
            FunctionDictionary lis = new FunctionDictionary();
            if (m_inputActionFunc.TryGetValue(m_consoleKeyInfo.Key.ToString(), out lis))
                foreach (var item in lis)
                    item.Value(1);
        }

        public bool RegistInputAction(string keyName, string actionName, Func<int, int> f) {

            FunctionDictionary actionMap;
            // 登録済み入力キー種別か検索し、あれば関数を追加できるか試す（同名登録済みの場合False
            if (m_inputActionFunc.TryGetValue(keyName, out actionMap))
                return actionMap.TryAdd(actionName, f);
            // 未登録の場合登録
            actionMap = new FunctionDictionary{{ actionName, f }};
            m_inputActionFunc.Add(keyName, actionMap);
            return true;
        }

        public void RegistModel(modelInterface.IModel model)
        {
            if (m_modelList.TryAdd("A", model))
                model.Initialize();
        }
    }
}
