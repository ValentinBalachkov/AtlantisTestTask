using Project.Scripts.Models.Base;
using UniRx;
using UnityEngine.Android;

namespace _Project._Scripts.Models
{
    public class CameraPermissionModel : ModelBase
    {
        public ReactiveCommand OnCameraRequest;

        private bool _viewOpened;
        private bool _permissionAccess;

        public CameraPermissionModel()
        {
            OnCameraRequest = new ReactiveCommand();
        }

        public void UpdateRequestCameraPermission()
        {
            _viewOpened = false;
            Permission.RequestUserPermission(Permission.Camera);
        }

        public bool CheckCameraPermission()
        {
            if (Permission.HasUserAuthorizedPermission(Permission.Camera) == false)
            {
                if (_viewOpened == false)
                {
                    _viewOpened = true;

                    OnCameraRequest.Execute();
                }

                return false;
            }

            return true;
        }
    }
}