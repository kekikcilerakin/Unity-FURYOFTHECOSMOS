using UnityEngine;

public class MouseMarker : MonoBehaviour
{
    public GameObject marker;  // Assign your marker object in the Inspector
    public LayerMask groundLayer;  // LayerMask to specify which layer is the ground
    public GameObject player;  // Assign your player object in the Inspector
    public float maxRange = 4f;  // Maximum allowed range from the player to the marker

    void Update()
    {
        PlaceMarkerAtMousePosition();
    }

    void PlaceMarkerAtMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            float distance = Vector3.Distance(player.transform.position, hit.point);
            if (distance <= maxRange)
            {
                marker.transform.position = hit.point;
            }
            else
            {
                // Optional: Move marker to the edge of the allowed range
                Vector3 direction = (hit.point - player.transform.position).normalized;
                marker.transform.position = player.transform.position + direction * maxRange;
            }
        }
    }
}
