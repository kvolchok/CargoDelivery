using UnityEngine;
using UnityEngine.Events;

public class LineDrawer : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _moveCargo;
    
    [SerializeField]
    private float _minDrawDistance = 0.5f;
    [SerializeField]
    private float _maxDrawDistance = 1f;
    
    private LineRenderer _line;
    private Camera _camera;

    private bool _hasLineDrawn;
    private bool _isDrawMode;
    private Vector3 _lastPoint;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _camera = Camera.main;
        SetLastDrawPoint();
    }
    
    public Vector3[] GetRoute()
    {
        var positions = new Vector3[_line.positionCount];
        _line.GetPositions(positions);

        return positions;
    }
    
    private void Update()
    {
        if (!_hasLineDrawn)
        {
            MoveMouseCursor();
        }
    }

    private void MoveMouseCursor()
    {
        var mousePosition = Input.mousePosition;
        var ray = _camera.ScreenPointToRay(mousePosition);

        if (!Physics.Raycast(ray, out var hitInfo))
        {
            return;
        }

        var currentPoint = hitInfo.point;
        var distance = Vector3.Distance(currentPoint, _lastPoint);
        
        if (distance > _maxDrawDistance && !_isDrawMode)
        {
            return;
        }

        if (_isDrawMode && Input.GetMouseButtonUp(0))
        {
            _isDrawMode = false;
            _hasLineDrawn = true;
            _moveCargo.Invoke();
        }

        if (distance < _minDrawDistance)
        {
            return;
        }
        
        if (!_hasLineDrawn && Input.GetMouseButton(0))
        {
            _isDrawMode = true;
            DrawRoute(currentPoint);
        }
    }

    private void DrawRoute(Vector3 currentPoint)
    {
        var index = ++_line.positionCount - 1;
        _line.SetPosition(index, currentPoint);

        SetLastDrawPoint();
    }

    private void SetLastDrawPoint()
    {
        _lastPoint = _line.GetPosition(_line.positionCount - 1);
    }
}