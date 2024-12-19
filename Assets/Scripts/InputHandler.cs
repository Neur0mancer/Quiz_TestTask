using UnityEngine;

namespace Quiz_Bezuglyi
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField]
        private Camera _mainCamera;        
        private GuessChecker _guessChecker;
        private TweenAnimator _tweenAnimator;
        private EffectsController _effectsController;
        private void Start()
        {
            _tweenAnimator = GetComponent<TweenAnimator>();
            _guessChecker = GetComponent<GuessChecker>();
            _effectsController = GetComponent<EffectsController>();
        }
        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                HandleInput();
            }
        }
        private void HandleInput()
        {
            Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                Symbol symbol = hit.collider.GetComponent<Symbol>();
                if (symbol != null)
                {
                    bool isCorrect = _guessChecker.Check(symbol.SymbolName);
                    if (isCorrect)
                    {
                        _effectsController.PlayEffectOnTransform(hit.transform);
                        _tweenAnimator.Bounce(symbol.gameObject, 1f, () =>
                        {
                            _guessChecker.UpdateBoardState();
                        });
                    }
                    else
                    {
                        _tweenAnimator.Shake(symbol.gameObject, 1f);
                    }
                }
            }
        }
    }
 }