using _Project._Scripts.Models;
using _Project._Scripts.UI;
using Project.Scripts.Presenters.Base;
using UniRx;
using UnityEngine.XR.ARFoundation;
using Application = UnityEngine.Device.Application;

namespace _Project._Scripts.Presenters
{
    public class InteractionObjectPresenter : PresenterBase<InteractionObjectModel, InteractionObjectView>
    {
        private InteractionObjectView _view;
        private ArManager _arManager;
        private InteractionObjectModel _model;
        public InteractionObjectPresenter(InteractionObjectModel model, InteractionObjectView view, NotificationArPanel notificationArPanel, ArManager arManager) : base(model, view)
        {
            _view = view;

            _model = model;
            
            _arManager = arManager;
            
            notificationArPanel.AcceptArg(PopupEnum.ArNotification);
        }
        
        protected override void ViewOpened()
        {
            base.ViewOpened();
            _view.CloseButtonOnClick.Subscribe(_ => Application.Quit()).AddTo(_sessionDisposable);
            _view.ResetButtonOnClick.Subscribe(_ => _model.ResetModel()).AddTo(_sessionDisposable);
            _view.UserInput += _model.OnDrag;
            _view.UserInput += _model.ZoomObject;
            _arManager.gameObject.SetActive(true);
            _arManager.ARTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }

        protected override void ViewClosed()
        {
            base.ViewClosed();
            _view.UserInput -= _model.OnDrag;
            _view.UserInput -= _model.ZoomObject;
            _arManager.gameObject.SetActive(false);
        }
        
        private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
        {
            _model.Init(_arManager.TrackedObject);
            _arManager.ARTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
    }
}