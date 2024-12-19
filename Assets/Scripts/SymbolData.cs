using UnityEngine;

namespace Quiz_Bezuglyi
{
    [CreateAssetMenu(fileName = "SymbolData", menuName = "Quiz/SymbolData")]
    public class SymbolData : ScriptableObject
    {
        [SerializeField]
        private string _symbolName;
        [SerializeField]
        private Sprite _sprite;
        [SerializeField]
        private bool _rotateOnAssign;

        public string SymbolName => _symbolName;
        public Sprite Sprite => _sprite;
        public bool RotateOnAssign => _rotateOnAssign;
    }
}