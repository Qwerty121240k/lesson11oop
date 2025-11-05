using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public Transform platform; // reference for the platform
    public Transform startPoint; // reference for the start point
    public Transform endPoint; // reference for the end point
    public float speed = 1.5f; // speed between points
    public int direction = 1; // direction of travel 1 or -1

    // Function to show the lines between points
    private void OnDrawGizmos()
    {
        // just for
        if (platform != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(platform.transform.position, startPoint.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.position);
        }
    }
    // Additional Code 

    // End of Additional Code
}
