using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

    public class CardBase : PoolObject
    {

        [SerializeField] private Button _button;
        
        protected virtual void Awake()
        {
            _button.onClick.AddListener(ButtonClick);
        }

        protected virtual void ButtonClick(){}

        public void SetButtonInteractable(bool interactable)
        {
            _button.onClick.RemoveAllListeners();

            if (interactable)
                _button.onClick.AddListener(ButtonClick);
        }

        public void OnTaken(Transform target)
        {
            transform.DOMove(target.position, 0.75f);
            transform.DOScale(Vector3.zero, 0.75f).OnComplete(OnDeactivate);
        }
        
        public override void OnDeactivate()
        {
            gameObject.SetActive(false);
        }

        public override void OnSpawn()
        {
            gameObject.SetActive(true);
        }

        public override void OnCreated()
        {
            OnDeactivate();
        }
    }
