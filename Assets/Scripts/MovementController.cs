using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private LineDrawer _lineDrawer;
    [SerializeField]
    private float _speed = 2f;
    
    [UsedImplicitly]
    public void MoveCargo()
    {
        StartCoroutine(MoveCargoCoroutine());
    }

    [UsedImplicitly]
    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator MoveCargoCoroutine()
    {
        var route = _lineDrawer.GetRoute();
        
        for (var i = 0; i < route.Length - 1; i++)
        {
            var startPosition = route[i];
            var endPosition = route[i + 1];
            
            var distanceToNextPoint = Vector3.Distance(startPosition, endPosition);
            var travelDistance = 0f;

            while (distanceToNextPoint >= travelDistance)
            {
                travelDistance = _speed * Time.deltaTime;
                var currentPosition = Vector3.MoveTowards(transform.position, endPosition, travelDistance);
                transform.position = currentPosition;
                distanceToNextPoint -= travelDistance;

                yield return null;
            }

            transform.position = endPosition;
            yield return null;
        }
    }
}