using System.Collections;
using _Project._Scripts.Models;
using _Project._Scripts.Presenters;
using _Project._Scripts.UI;
using UnityEngine;

public class ApplicationEntryPoint : MonoBehaviour
{
    [SerializeField] private ArManager _arManager;

    [SerializeField] private MainPanelManager _mainPanelManager;

    [SerializeField] private PopupTextConfig _popupTextConfig;
    

    private LoadingAssetsModel _loadingAssetsModel;
    private CameraPermissionModel _cameraPermissionModel;
    
    private void Awake()
    {
        Application.targetFrameRate = 60;

        UIInit();

        InitCameraPermissionModule();
        
        InitLoadingAssetsModule();

        InitInteractionObjectModule();
    }

    private IEnumerator Start()
    {
        var notificationView = _mainPanelManager.SudoGetPanel<NotificationArPanel>();
        
        while (_cameraPermissionModel.CheckCameraPermission() == false)
        {
            yield return null;
        }
        
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            notificationView.AcceptArg(PopupEnum.InternetConnectionNotification);
            _mainPanelManager.OpenPanel<NotificationArPanel, PopupEnum>(PopupEnum.InternetConnectionNotification);
            
            while (Application.internetReachability == NetworkReachability.NotReachable)
            {
                yield return null;
            }
            
            _mainPanelManager.ClosePanel<NotificationArPanel>();
        }
        
        
        _mainPanelManager.OpenPanel<LoadingAssetsView>();
        
        while (!_loadingAssetsModel.LoadAssetsBundle())
        {
            yield return null;
        }
        
        _mainPanelManager.OpenPanel<InteractionObjectView>();
    }
    
    private void InitLoadingAssetsModule()
    {
        _loadingAssetsModel = new LoadingAssetsModel(_arManager);
            
        var view = _mainPanelManager.SudoGetPanel<LoadingAssetsView>();

        var loadingAssetsPresenter = new LoadingAssetsPresenter(_loadingAssetsModel, view);
    }
    
    private void InitCameraPermissionModule()
    {
        _cameraPermissionModel = new CameraPermissionModel();
            
        var view = _mainPanelManager.SudoGetPanel<CameraPermissionView>();

        var cameraPermissionPresenter = new CameraPermissionPresenter(_cameraPermissionModel, view);
            
    }
    
    private void InitInteractionObjectModule()
    {
        var interactionObjectModel  = new InteractionObjectModel();
            
        var view = _mainPanelManager.SudoGetPanel<InteractionObjectView>();
        
        var notificationView = _mainPanelManager.SudoGetPanel<NotificationArPanel>();

        var interactionObjectPresenter = new InteractionObjectPresenter(interactionObjectModel, view, notificationView, _arManager);
    }

    private void UIInit()
    {
        _mainPanelManager.Init();
        _popupTextConfig.Init();
        _arManager.Init(_mainPanelManager);
    }
    
}
