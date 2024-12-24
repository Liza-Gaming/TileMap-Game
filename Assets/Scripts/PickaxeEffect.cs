using UnityEngine;

public class PickaxeEffect : MonoBehaviour
{
    private bool canCreateTrail = false;
    public GameObject groundTilePrefab; // Assign a prefab for the ground trail
    public Transform elevationLayer; // Assign the elevation layer transform
    public void EnableElevationMovement()
    {
        canCreateTrail = true;
        gameObject.layer = LayerMask.NameToLayer("Elevation"); // Set the player to the "Elevation" layer
    }

    void Update()
    {
        if (canCreateTrail && IsOnElevationLayer())
        {
            CreateTrail();
        }
    }

    bool IsOnElevationLayer()
    {
        return gameObject.layer == LayerMask.NameToLayer("Elevation");
    }

    void CreateTrail()
    {
        if (groundTilePrefab != null && elevationLayer != null)
        {
            Vector3 gridPosition = GetSnappedPositionToGrid(transform.position);
            if (!TileExistsAt(gridPosition)) // Check to avoid creating multiple tiles in the same spot
            {
                Instantiate(groundTilePrefab, gridPosition, Quaternion.identity, elevationLayer);
            }
        }
    }

    Vector3 GetSnappedPositionToGrid(Vector3 position)
    {
        float snapX = Mathf.Round(position.x);
        float snapY = Mathf.Round(position.y);
        return new Vector3(snapX, snapY, position.z);
    }

    bool TileExistsAt(Vector3 position)
    {
        // This method checks if a tile already exists at the position to avoid duplicate tiles
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("GroundTile"))
            {
                return true;
            }
        }
        return false;
    }
}