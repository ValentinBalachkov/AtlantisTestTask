using PanelManager.Scripts.Panels;

public class LoadingAssetsView : ViewBase
{
    public override PanelType PanelType => PanelType.Screen;
    public override bool RememberInHistory => false;
    
    protected override void OnOpened()
    {
        base.OnOpened();
        
    }

    protected override void OnClosed()
    {
        base.OnClosed();
        
    }
}
