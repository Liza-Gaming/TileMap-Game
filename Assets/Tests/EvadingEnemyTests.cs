using NUnit.Framework;
using UnityEngine;

public class EvadingEnemyTests
{
    private GameObject enemyObject;
    private EvadingEnemy evadingEnemy;
    private GameObject playerObject;

    private Rigidbody2D rb;

    [SetUp]
    public void Setup()
    {
        enemyObject = new GameObject("EvadingEnemy");
        rb = enemyObject.AddComponent<Rigidbody2D>();
        evadingEnemy = enemyObject.AddComponent<EvadingEnemy>();

        // Initialize the evading enemy's properties
        evadingEnemy.speed = 5f;
        evadingEnemy.evadeDistance = 5f;

        // Create and assign a mock player to the evading enemy
        playerObject = new GameObject("Player");
        evadingEnemy.player = playerObject.transform;

        // Set the obstacle layer to a valid layer (make sure default layer is there)
        evadingEnemy.obstacleLayer = LayerMask.GetMask("Default");
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(enemyObject);
    }

    [Test]
    public void Test_GetEscapeDirections()
    {
        // Define direction away from the player
        Vector2 directionAway = new Vector2(1, 0);
        var escapeDirections = evadingEnemy.GetEscapeDirections(directionAway);

        Assert.Contains(directionAway, escapeDirections);
        Assert.Contains(Vector2.Perpendicular(directionAway), escapeDirections);
        Assert.Contains(-Vector2.Perpendicular(directionAway), escapeDirections);
    }

    [Test]
    public void Test_FindBestEvadeDirection_NoObstacles()
    {
        // Create a layer mask that doesn't hit any obstacles
        LayerMask mask = LayerMask.GetMask("Default");
        evadingEnemy.obstacleLayer = mask; // Set to a layer where no obstacles are present

        // Set the player position
        evadingEnemy.player.position = new Vector3(0, 0, 0);
        enemyObject.transform.position = new Vector3(2, 2, 0); // Enemy starts at (2,2)

        // Get possible escape directions
        var directions = evadingEnemy.GetEscapeDirections((enemyObject.transform.position - evadingEnemy.player.position).normalized);
        var bestDirection = evadingEnemy.FindBestEvadeDirection(directions);

        // Verify that the best direction is one of the calculated escape directions
        Assert.Contains(bestDirection, directions);
    }

    [Test]
    public void Test_CornerEvade()
    {
        // Setup obstacles in a corner
        CreateObstacleAt(new Vector2(1, 1)); // Bottom left corner
        CreateObstacleAt(new Vector2(1, 2)); // Middle left
        CreateObstacleAt(new Vector2(2, 1)); // Middle bottom

        // Position the player in a way that forces the enemy to evade towards a corner
        evadingEnemy.player.position = new Vector3(0, 0, 0); // Player in the middle
        enemyObject.transform.position = new Vector3(1.5f, 1.5f, 0); // Enemy starts in a corner

        // Simulate several frames to allow the enemy to attempt to evade
        for (int i = 0; i < 10; i++)
        {
            evadingEnemy.Update(); // Call Update manually
        }

        // Assert that the enemy has moved away from its initial position
        Assert.IsFalse(enemyObject.transform.position.Equals(new Vector3(1.5f, 1.5f, 0)), "Enemy got stuck in a corner!");
    }

    private void CreateObstacleAt(Vector2 position)
    {
        GameObject obstacle = new GameObject("Default");
        obstacle.transform.position = position;
        BoxCollider2D collider = obstacle.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1); // Set size based on your needs
        // Make sure the obstacle is part of the layer you've designated for obstacles
        obstacle.layer = LayerMask.NameToLayer("Default");
    }


    [Test]
    public void Test_Flip()
    {
        Assert.IsTrue(evadingEnemy.facingRight);

        // Call Flip method and check direction
        evadingEnemy.Flip();
        Assert.IsFalse(evadingEnemy.facingRight);
    }
}