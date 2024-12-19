using UnityEngine;

namespace Quiz_Bezuglyi
{
    [CreateAssetMenu(fileName = "SymbolSet", menuName = "Quiz/SymbolSet")]
    public class SymbolSet : ScriptableObject
    {
        [SerializeField]
        private SymbolData[] symbols;

        public SymbolData[] Symbols => symbols;
    }
}