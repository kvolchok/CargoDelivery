using UnityEngine;

public class FinishController : MonoBehaviour
{
    private const string ROPE_TAG = "Rope";
    
    [SerializeField]
    private MovementController _movementController;
    [SerializeField]
    private BoxController _box;

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag(ROPE_TAG))
        {
            _movementController.Stop();
            _box.DropDown(transform.position);
        }
    }
}