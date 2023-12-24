using System.IO;
using Project.Scripts.Models.Base;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class LoadingAssetsModel : ModelBase
{
    private ARTrackedImageManager _arTrackedImageManager;

    private readonly string _imageURL = "https://user74522.clients-cdnnow.ru/static/uploads/mrk6440mark.png";

    private MutableRuntimeReferenceImageLibrary _imageLibrary;

    private UnityWebRequest _unityWebRequest;


    public LoadingAssetsModel(ArManager arManager)
    {
        _arTrackedImageManager = arManager.ARTrackedImageManager;

        _arTrackedImageManager.enabled = false;
        
        _imageLibrary = _arTrackedImageManager.CreateRuntimeLibrary() as MutableRuntimeReferenceImageLibrary;
        
        _unityWebRequest = UnityWebRequestTexture.GetTexture(_imageURL);
        
        _unityWebRequest.SendWebRequest();
    }

    public bool LoadAssetsBundle()
    {
        while (!_unityWebRequest.isDone)
        {
            return false;
        }

        if (_unityWebRequest.isNetworkError || _unityWebRequest.isHttpError)
        {
            Debug.Log(_unityWebRequest.error);
        }
        else
        {
            var texture = ((DownloadHandlerTexture)_unityWebRequest.downloadHandler).texture;
            var jobHandle = _imageLibrary.ScheduleAddImageJob(texture, Path.GetFileName(_imageURL), 0.2f);
            jobHandle.Complete();
            _arTrackedImageManager.referenceLibrary = _imageLibrary;
            _arTrackedImageManager.enabled = true;
        }

        return true;
    }
}