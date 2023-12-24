using System;
using DG.Tweening;
using PanelManager.Scripts.Interfaces;
using PanelManager.Scripts.Panels;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project._Scripts.UI
{
    public class CameraPermissionView : ViewBase, IAcceptArg<PopupEnum>
    {
        [SerializeField] private PopupTextConfig _popupTextConfig;
    
        [SerializeField] private TMP_Text _notificationText;
        [SerializeField] private Image _backgroundText;
        [SerializeField] private Button _buttonSkip;

        public IObservable<Unit> SkipButtonClick => _buttonSkip.OnClickAsObservable(); 
        public override PanelType PanelType => PanelType.Overlay;
        public override bool RememberInHistory => false;
        
        private Sequence _sequence;
        private Color _defaultNotificationText;
        private Color _defaultColorBackgroundText;
    
        
        protected override void OnOpened()
        {
            base.OnOpened();

            StartAnimation();
        }

        protected override void OnClosed()
        {
            base.OnClosed();
        
            AnimationComplete();
        }

        private void StartAnimation()
        {
            _sequence = DOTween.Sequence();
            
            _defaultColorBackgroundText = _backgroundText.color;
            
            _defaultNotificationText = _notificationText.color;
        }

        private void AnimationComplete()
        {
            _sequence?.Kill();
            
            _backgroundText.color = _defaultColorBackgroundText;
            
            _notificationText.color = _defaultNotificationText;
        }

        public void AcceptArg(PopupEnum arg)
        {
            _notificationText.text = _popupTextConfig.GetText(arg);
        }
    }
}
