using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MovingPlatform : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    private float moveSpeed = 2f;

    private Vector3 nextPosition;
    private string bootstrapSceneName = "Bootstrap";

    void Start()
    {
        nextPosition = point2.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

        if(transform.position == nextPosition)
        {
            nextPosition = (nextPosition == point1.position) ? point2.position : point1.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;

            Scene bootstrapScene = SceneManager.GetSceneByName(bootstrapSceneName);
            if (bootstrapScene.IsValid())
            {
                SceneManager.MoveGameObjectToScene(collision.gameObject, bootstrapScene);
            }
        }
    }
}
