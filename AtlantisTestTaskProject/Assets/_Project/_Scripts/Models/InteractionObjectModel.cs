using Project.Scripts.Models.Base;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project._Scripts.Models
{
    public class InteractionObjectModel : ModelBase
    {
        private Transform _model;

        private float _startZoom;
        private float _zoom;

        private Touch _firstTouch;
        private Touch _secondTouch;

        private float _startDistance;
        
        private const float ROTATE_SPEED = 15;
        private const float SCALE_FACTOR = 0.0001f;

        public void Init(Transform model)
        {
            _model = model;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_model == null)
            {
                return;
            }


            if (eventData.delta.x > 0)
            {
                _model.Rotate(Vector3.back, ROTATE_SPEED);
            }

            if (eventData.delta.x < 0)
            {
                _model.Rotate(Vector3.forward, ROTATE_SPEED);
            }
        }

        public void ZoomObject(PointerEventData eventData)
        {
            if (Input.touchCount > 1)
            {
                if (Input.touches[0].phase == TouchPhase.Began || Input.touches[1].phase == TouchPhase.Began)
                {
                    _firstTouch = Input.GetTouch(0);
                    _secondTouch = Input.GetTouch(1);
                    _startDistance = Vector2.Distance(_firstTouch.position, _secondTouch.position);
                    _startZoom = _zoom;
                }

                if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved)
                {
                    var currentDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                    var delta = _startDistance - currentDistance;
                    _zoom = _startZoom + delta * SCALE_FACTOR;
                    _model.localScale = new Vector3(_zoom, _zoom, _zoom);

                    if (_zoom < 0f)
                    {
                        _zoom = 0f;
                    }

                    else if (_zoom > 0.1f)

                    {
                        _zoom = 0.1f;
                    }
                }
            }
        }

        public void ResetModel()
        {
            if (_model == null)
            {
                return;
            }

            _model.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            _model.localEulerAngles = Vector3.zero;
        }
    }
}