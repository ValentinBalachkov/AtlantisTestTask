using System;
using PanelManager.Scripts.Panels;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project._Scripts.UI
{
    public class InteractionObjectView : ViewBase, IDragHandler
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _resetButton;
        
        
        public Action<PointerEventData> UserInput;
        public IObservable<Unit> CloseButtonOnClick => _closeButton.onClick.AsObservable();
        public IObservable<Unit> ResetButtonOnClick => _resetButton.onClick.AsObservable();
        
        public override PanelType PanelType => PanelType.Screen;
        public override bool RememberInHistory => false;

        private Transform _model;
        

        protected override void OnOpened()
        {
            base.OnOpened();
        }

        protected override void OnClosed()
        {
            base.OnClosed();
        }

        public void OnDrag(PointerEventData eventData)
        {
            UserInput?.Invoke(eventData);
        }
    }
}