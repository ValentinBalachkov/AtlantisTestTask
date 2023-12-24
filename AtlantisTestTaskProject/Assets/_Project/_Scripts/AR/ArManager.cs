using _Project._Scripts.UI;
using PanelManager.Scripts;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ArManager : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager _arTrackedImageManager;
    
    public ARTrackedImageManager ARTrackedImageManager => _arTrackedImageManager;

    public Transform TrackedObject => _trackedObject;
    
    private PanelManagerBase _panelManagerBase;

    private Transform _trackedObject;


    public void Init(PanelManagerBase panelManagerBase)
    {
        _panelManagerBase = panelManagerBase;
    }
    
    private void Awake()
    {
        _panelManagerBase.OpenPanel<NotificationArPanel, PopupEnum>(PopupEnum.ArNotification);
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {
        _panelManagerBase.ClosePanel<NotificationArPanel>();
        _trackedObject = obj.added[0].GetComponentInChildren<ArObject>().ModelTransform;
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
}
