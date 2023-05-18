using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private CoroutineManager _coroutineManager;
    [SerializeField]
    private LineDrawer _lineDrawer;
    [SerializeField]
    private float _speed = 2f;
    
    [UsedImplicitly]
    public void MoveCargo()
    {
        _coroutineManager.StartCoroutine(MoveCargoCoroutine());
    }

    private IEnumerator MoveCargoCoroutine()
    {
        var route = _lineDrawer.GetRoute();
        
        for (var i = 0; i < route.Length - 1; i++)
        {
            var startPosition = route[i];
            var endPosition = route[i + 1];
            
            var travelDistance = Vector3.Distance(startPosition, endPosition);
            var travelTime = travelDistance / _speed;
            
            var currentTime = 0f;

            while (currentTime < travelTime)
            {
                var progress = currentTime / travelTime;
                var currentPosition = Vector3.Lerp(startPosition, endPosition, progress);
                transform.position = currentPosition;
                currentTime += Time.deltaTime;

                yield return null;
            }
        }
    }
}