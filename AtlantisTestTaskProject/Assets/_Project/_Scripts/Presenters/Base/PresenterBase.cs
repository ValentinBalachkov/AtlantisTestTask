using PanelManager.Scripts.Interfaces;
using Project.Scripts.Models.Interfaces;
using Project.Scripts.Presenters.Interfaces;
using UniRx;

namespace Project.Scripts.Presenters.Base
{
    public class PresenterBase<TModel, TView> : IPresenter<TModel, TView>
    where TModel : IModel
    where TView : IView
    {
        protected CompositeDisposable _disposable;
        protected CompositeDisposable _sessionDisposable;

        protected TModel _model;
        protected TView _view;
        
        protected PresenterBase(TModel model, TView view)
        {
            _disposable = new CompositeDisposable();
            _model = model;
            _view = view;

            _view.ViewOpened.Subscribe(_ => ViewOpened()).AddTo(_disposable);
            _view.ViewClosed.Subscribe(_ => ViewClosed()).AddTo(_disposable);
        }
        
        public void Dispose()
        {
            _disposable.Dispose();
            _disposable = new CompositeDisposable();
        }
        
        protected virtual void ViewOpened()
        {
            _sessionDisposable = new CompositeDisposable();
        }
        
        protected virtual void ViewClosed()
        {
            _sessionDisposable.Dispose();
        }
    }
}