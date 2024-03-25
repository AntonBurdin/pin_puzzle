using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayableItem : MonoBehaviour
{
    [SerializeField]
    private List<Transform> sockets;

    public bool TryGetClosestDistanceToSocket(Vector2 position, out float distance)
    {
        float closestDistanceSqr = float.MaxValue;
        Transform closestPin = null;
        for(int i = 0; i < this.sockets.Count; i++)
        {
            var pin = this.sockets[i];
            var sqrDistance = Vector2.SqrMagnitude(position - (Vector2)pin.position);
            if (sqrDistance < closestDistanceSqr)
            {
                closestDistanceSqr = sqrDistance;
                closestPin = pin;
            }
        }

        bool result = closestPin != null;
        distance = result ? Mathf.Sqrt(closestDistanceSqr) : default(float);
        return result;
    }

    private void Awake()
    {
        var layerName = LayerMask.LayerToName(this.gameObject.layer);
        if (int.TryParse(layerName.Substring(layerName.Length - 1), out var index))
        {
            this.GetComponent<SpriteRenderer>().sortingOrder = index;
        }
    }
}
