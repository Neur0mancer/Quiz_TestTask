using UnityEngine;
using VContainer;

namespace Quiz_Bezuglyi
{
    public class GuessChecker : MonoBehaviour
    {
        private string _currentSymbol;
        private int _rightAnswers;

        private SymbolsSetup _symbolsSetup;
        private UpdaterUI _updaterUI;

        [Inject]
        public void Construct(SymbolsSetup symbolsSetup)
        {
            _symbolsSetup = symbolsSetup;
            symbolsSetup.OnCurrentSymbolSelected += SymbolsSetup_OnCurrentSymbolSelected;
        }

        private void Start()
        {
            _updaterUI = GetComponent<UpdaterUI>();
            ResetAnswers();
        }

        private void SymbolsSetup_OnCurrentSymbolSelected(string obj)
        {
            _currentSymbol = obj;
            _updaterUI.UpdateText(_currentSymbol);
        }

        public bool Check(string symbol)
        {           
            if (symbol == _currentSymbol)
            {
                _rightAnswers++;                
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ResetAnswers()
        {
            _rightAnswers = 0;
        }

        public void UpdateBoardState()
        {
            _symbolsSetup.UpdateAnswersCount(_rightAnswers);
            if (_rightAnswers >= _symbolsSetup.GetLevelsCount())
            {
                _symbolsSetup.DisableCells();
                _updaterUI.DisableText();
                _updaterUI.ShowReplay();
            }
        }

        private void OnDisable()
        {
            _symbolsSetup.OnCurrentSymbolSelected -= SymbolsSetup_OnCurrentSymbolSelected;
        }
    }
}