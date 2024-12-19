using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Quiz_Bezuglyi
{
    public class SymbolsSetup : MonoBehaviour
    {
        [SerializeField]
        private SymbolSet[] _symbolSets;
        [SerializeField]
        private LevelProgression _levels;

        public event Action<string> OnCurrentSymbolSelected;

        private string _currentSymbol;
        private int _rightAnswers;
        private GameObject[] _cellsArray;

        private List<GameObject> _cells = new List<GameObject>();
        private List<SymbolData> _allSymbols = new List<SymbolData>();
        private List<SymbolData> _currentSymbols = new List<SymbolData>();
        private List<SymbolData> _usedSymbols = new List<SymbolData>();

        private CellsGenerator _cellsGenerator;
        private GuessChecker _guessChecker;
        private TweenAnimator _tweenAnimator;
        private UpdaterUI _updaterUI;

        [Inject]
        public void Construct(CellsGenerator cellsGenerator, TweenAnimator tweenAnimator, UpdaterUI updaterUI)
        {
            _cellsGenerator = cellsGenerator;
            _tweenAnimator = tweenAnimator;
            _updaterUI = updaterUI;
        }

        private void Start()
        {
            _allSymbols.AddRange(ChooseRandomSymbolSet().Symbols);
            _cellsArray = _cellsGenerator.GenerateCells(_levels);
            _updaterUI.OnMessageWindowClosed += PlayFirstAppearAnimation;
            GenerateLevel(_rightAnswers);
        }

        public string GenerateLevel(int levelIndex)
        {
            int totalSymbolsNumber = 0;
            for (int i = 0; i <= levelIndex; i++)
            {
                totalSymbolsNumber += _levels.LevelsArray[i].SymbolsNumber;
            }
            _currentSymbols.Clear();
            for (int i = 0; i < _cellsArray.Length; i++)
            {
                GameObject obj = _cellsArray[i];
                if (i < totalSymbolsNumber)
                {
                    SymbolData randomSymbol;
                    do
                    {
                        randomSymbol = _allSymbols[Random.Range(0, _allSymbols.Count)];
                    } while (_currentSymbols.Contains(randomSymbol));

                    ResetSprites(obj);
                    obj.GetComponent<SpriteRenderer>().sprite = randomSymbol.Sprite;
                    obj.GetComponent<Symbol>().SetSymbol(randomSymbol.SymbolName);
                    if (randomSymbol.RotateOnAssign)
                    {
                        obj.transform.Rotate(0, 0, -90);
                    }

                    obj.SetActive(true);
                    if(_rightAnswers == 0)
                    {
                        obj.gameObject.transform.localScale = Vector3.zero;
                    }                    
                    _currentSymbols.Add(randomSymbol);
                }
                else
                {
                    obj.SetActive(false);
                }
            }
            _currentSymbol = ChooseRandomSymbol().SymbolName;
            OnCurrentSymbolSelected?.Invoke(_currentSymbol);
            return _currentSymbol;
        }

        private SymbolData ChooseRandomSymbol()
        {
            SymbolData chosenSymbolData;
            do
            {
                chosenSymbolData = _currentSymbols[Random.Range(0, _currentSymbols.Count)];
            } while (_usedSymbols.Contains(chosenSymbolData));

            _usedSymbols.Add(chosenSymbolData);
            return chosenSymbolData;
        }

        private void ResetSprites(GameObject obj)
        {
            obj.GetComponent<SpriteRenderer>().sprite = null;
            obj.transform.rotation = Quaternion.identity;
        }

        private SymbolSet ChooseRandomSymbolSet()
        {
            return _symbolSets[Random.Range(0, _symbolSets.Length)];
        }

        public void UpdateAnswersCount(int number)
        {
            _rightAnswers = number;
            int levelIndex = Mathf.Min(number, _levels.LevelsArray.Length - 1);
            GenerateLevel(levelIndex);
        }

        public int GetLevelsCount()
        {
            return _levels.LevelsArray.Length;
        }

        public void DisableCells()
        {
            foreach (var cell in _cellsArray)
            {
                cell.SetActive(false);
            }
        }

        private void PlayFirstAppearAnimation()
        {
            foreach (var cell in _cellsArray)
            {
                _tweenAnimator.Bounce(cell, 1f, null);
            }
        }

        private void OnDisable()
        {
            _updaterUI.OnMessageWindowClosed -= PlayFirstAppearAnimation;
        }
    }
}