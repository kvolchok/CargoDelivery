using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField]
    private float _minDrawDistance = 0.5f;
    [SerializeField]
    private float _maxDrawDistance = 1f;
    
    private LineRenderer _line;
    private Camera _camera;

    private bool _hasLineDrawn;
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
        MoveMouseCursor();
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
        
        if (distance > _maxDrawDistance)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _hasLineDrawn = true;
        }

        if (distance < _minDrawDistance)
        {
            return;
        }
        
        if (!_hasLineDrawn && Input.GetMouseButton(0))
        {
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