using UnityEngine;

namespace Quiz_Bezuglyi
{
    public class Symbol : MonoBehaviour
    {
        private string _symbolName;

        public string SymbolName => _symbolName;

        public void SetSymbol(string symbolName)
        {
            _symbolName = symbolName;
        }
    }
}