using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _frame;
    [SerializeField] private RectTransform _button;
    [SerializeField] private float _buttonRange = 1;
    [SerializeField] private float _minRange = 0;

    private Canvas _canvas;
    private Camera _camera;
    private Vector2 _input = Vector2.zero;
    
    public float Magnitude => _input.magnitude;
    public float Horizontal => _input.x;
    public float Vertical => _input.y;
    public Vector2 Direction => new Vector2(Horizontal, Vertical);

    public float ButtonRange
    {
        get => _buttonRange;
        set => _buttonRange = Mathf.Abs(value);
    }

    public float MinRange
    {
        get => _minRange;
        set => _minRange = Mathf.Abs(value);
    }

    private void Start()
    {
        ButtonRange = _buttonRange;
        MinRange = _minRange;
        _canvas = GetComponentInParent<Canvas>();
        
        if (_canvas == null)
        {
            Debug.LogError("Put the Joystick inside a canvas");
        }

        var center = new Vector2(0.5f, 0.5f);
        _frame.pivot = center;
        _button.anchorMin = center;
        _button.anchorMax = center;
        _button.pivot = center;
        _button.anchoredPosition = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _camera = null;
        if (_canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            _camera = _canvas.worldCamera;
        }

        var position = RectTransformUtility.WorldToScreenPoint(_camera, _frame.position);
        var radius = _frame.sizeDelta / 2;
        
        _input = (eventData.position - position) / (radius * _canvas.scaleFactor);
        HandleInput(_input.magnitude, _input.normalized, radius, _camera);
        _button.anchoredPosition = _input * radius * _buttonRange;
    }

    private void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > _minRange)
        {
            if (magnitude > 1)
            {
                _input = normalised;
            }
        }
        else
        {
            _input = Vector2.zero;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _input = Vector2.zero;
        _button.anchoredPosition = Vector2.zero;
    }
}