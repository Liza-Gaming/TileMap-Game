using NUnit.Framework;
using UnityEngine;

public class EvadingEnemyTests : MonoBehaviour 
{
    private GameObject enemyObject;
    private EvadingEnemy evadingEnemy;
    private GameObject playerObject;

    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    [SetUp]
    public void Setup()
    {
        enemyObject = new GameObject("EvadingEnemy");
        rb = enemyObject.AddComponent<Rigidbody2D>();
        evadingEnemy = enemyObject.AddComponent<EvadingEnemy>();
        if(evadingEnemy == null)
        {
            Debug.Log("null");
        }
        else
        {
            Debug.Log("true");
        }
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
    public void Test_Flip()
    {
        Assert.IsTrue(evadingEnemy.facingRight);
        evadingEnemy.Flip();
        Assert.IsFalse(evadingEnemy.facingRight);
    }
}