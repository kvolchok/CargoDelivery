using UnityEngine;

public class FinishController : MonoBehaviour
{
    [SerializeField]
    private CoroutineManager _coroutineManager;
    [SerializeField]
    private BoxController _box;

    private void Update()
    {
        var ray = new Ray(transform.position, transform.up);

        if (!Physics.Raycast(ray, out var hitInfo))
        {
            return;
        }

        if (hitInfo.collider.gameObject == _box.gameObject)
        {
            _coroutineManager.StopAllCoroutines();
            _box.DropDown(transform.position);
        }
    }
}