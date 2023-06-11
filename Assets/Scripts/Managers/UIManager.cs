using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] Panels pnl;

    private CanvasGroup activePanel = null;

    public void Start()
    {
        FadeInAndOutPanels(pnl.mainMenu);
    }

    public void StartGame()
    {
        GameManager.OnStartGame();
    }

    public void OnGameStarted()
    {
        FadeInAndOutPanels(pnl.gameIn);
    }

    public void OnFail()
    {
        FadeInAndOutPanels(pnl.fail);
    }

    public void OnSuccess()
    {
        FadeInAndOutPanels(pnl.success);
    }
    
    
    public void ReloadScene()
    {
        GameManager.ReloadScene();
    }

    private Tween activeOutTween = null, activeInTween = null;

    void FadeInAndOutPanels(CanvasGroup _in)
    {
        CanvasGroup _out = activePanel;
        activePanel = _in;

        if(_out != null)
        {
            if(activeOutTween != null)
                activeOutTween.Kill(false);
            _out.interactable = false;
            _out.blocksRaycasts = false;

            activeOutTween = _out.DOFade(0f, Configs.UI.FadeOutTime).OnComplete(() =>
            {
                activeOutTween =_in.DOFade(1f, Configs.UI.FadeOutTime).OnComplete(() =>
                {
                    _in.interactable = true;
                    _in.blocksRaycasts = true;
                    activeOutTween = null;
                });
            });
        }
        else
        {
            if(activeInTween != null)
                activeInTween.Kill(false);
            
            activeInTween = _in.DOFade(1f, Configs.UI.FadeOutTime).OnComplete(() =>
            {
                activeInTween = null;
                _in.interactable = true;
                _in.blocksRaycasts = true;
            });
        }
    }
    
    
    [System.Serializable]
    public class Panels
    {
        public CanvasGroup mainMenu, gameIn, success, fail;
    }

    [System.Serializable]
    public class Buttons
    {
        public Button play;
    }
}
