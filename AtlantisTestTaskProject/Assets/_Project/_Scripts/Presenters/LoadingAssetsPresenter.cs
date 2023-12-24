using Project.Scripts.Presenters.Base;

public class LoadingAssetsPresenter : PresenterBase<LoadingAssetsModel, LoadingAssetsView>
{
    public LoadingAssetsPresenter(LoadingAssetsModel model, LoadingAssetsView view) : base(model, view)
    {
        
    }
    
    protected override void ViewOpened()
    {
        base.ViewOpened();
    }

    protected override void ViewClosed()
    {
        base.ViewClosed();
    }
}
