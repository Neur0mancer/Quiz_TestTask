using UnityEngine;

namespace Quiz_Bezuglyi
{
    [CreateAssetMenu(fileName = "Levels", menuName = "Quiz/Level progression")]
    public class LevelProgression : ScriptableObject
    {
        [SerializeField] private Level[] _levelsArray;

        public Level[] LevelsArray => _levelsArray;
    }
}