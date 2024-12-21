using UnityEngine;
// https://www.youtube.com/watch?v=4mzbDk4Wsmk&ab_channel=MoreBBlakeyyy
public class Patroller : MonoBehaviour
{

    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;
    private bool facingRight = true;

    void Start()
    {
        targetPoint = 0;
    }

    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        Vector3 direction = patrolPoints[targetPoint].position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);

        if (transform.position == patrolPoints[targetPoint].position)
        {
            increasTargetInt();
        }

        if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
        {
            Flip();
        }
    }

    void increasTargetInt()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void SetClosestPatrolPoint()
    {
        float closestDistance = float.MaxValue;

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, patrolPoints[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetPoint = i;
            }
        }
    }
}
