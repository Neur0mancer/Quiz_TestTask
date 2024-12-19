using System.Collections.Generic;
using UnityEngine;

namespace Quiz_Bezuglyi
{
    public class CellsGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject _symbolPrefab;
        [SerializeField]
        private Transform _startPosition;

        private float _prefabSize;
        private float _offset = 0.5f;

        public GameObject[] GenerateCells(LevelProgression levels)
        {
            List<GameObject> _cells = new List<GameObject>();
            Vector3 nextPosition = _startPosition.position;
            _prefabSize = _symbolPrefab.GetComponent<Renderer>().bounds.size.x;

            for (int i = 0; i < levels.LevelsArray.Length; i++)
            {                
                for (int j = 0; j < levels.LevelsArray[i].SymbolsNumber; j++)
                {
                    GameObject obj = Instantiate(_symbolPrefab, transform);
                    obj.transform.position = nextPosition;
                    _cells.Add(obj);
                    
                    nextPosition.x += _prefabSize + _offset;
                }
                    nextPosition.x = _startPosition.position.x;
                    nextPosition.y -= _prefabSize + _offset;
            }
            return _cells.ToArray();
        }
    }
}