using _Project._Scripts.UI;
using PanelManager.Scripts.Interfaces;
using PanelManager.Scripts.Panels;
using TMPro;
using UnityEngine;

public class NotificationArPanel : ViewBase, IAcceptArg<PopupEnum>
{
    [SerializeField] private PopupTextConfig _popupTextConfig;
    [SerializeField] private TMP_Text _notificationText;
    
    public override PanelType PanelType => PanelType.Overlay;
    public override bool RememberInHistory => false;
    public void AcceptArg(PopupEnum arg)
    {
        _notificationText.text = _popupTextConfig.GetText(arg);
    }
}
