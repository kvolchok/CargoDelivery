using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField]
    private float _deep = 12.87f;
    
    private LineRenderer _lineRenderer;
    private Camera _camera;

    private bool _isLineDrawn;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _camera = Camera.main;
    }

    private void Update()
    {
        MoveMouseCursor();
    }

    public Vector3[] GetRoute()
    {
        var positions = new Vector3[_lineRenderer.positionCount];
        _lineRenderer.GetPositions(positions);

        return positions;
    }

    private void MoveMouseCursor()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _isLineDrawn = true;
        }
        
        var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _deep);
        var point = _camera.ScreenToWorldPoint(mousePosition);

        if (Input.GetMouseButton(0) && !_isLineDrawn)
        {
            DrawRoute(point);
        }
    }

    private void DrawRoute(Vector3 point)
    {
        var index = ++_lineRenderer.positionCount - 1;
        _lineRenderer.SetPosition(index, point);
    }
}