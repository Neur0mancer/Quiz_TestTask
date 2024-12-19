using UnityEngine;

namespace Quiz_Bezuglyi
{
    [CreateAssetMenu(fileName = "Level", menuName = "Quiz/Levels")]
    public class Level : ScriptableObject
    {
        [SerializeField] 
        private int _symbolsNumber;

        public int SymbolsNumber => _symbolsNumber;
    }
}