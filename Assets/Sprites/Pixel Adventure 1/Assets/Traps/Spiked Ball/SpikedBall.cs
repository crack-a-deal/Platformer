using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBall : MonoBehaviour
{
    [SerializeField] private int length = 2;
    
    [SerializeField] private GameObject chain;
    [SerializeField] private GameObject ball;
    private List<GameObject> chainList = new List<GameObject>();
    private void Start()
    {
        Init();
        StartCoroutine(Animation());
    }

    [SerializeField] private float angle = 0;
    [SerializeField] private float angleStep = 45;
    [SerializeField] private float radius = 2;
    [SerializeField] private float speed = 1;
    private void RotateObj(GameObject obj,int step)
    {
        var x = Mathf.Cos(angle*speed) * radius;
        var y = Mathf.Sin(angle * speed) * radius;
        Vector2 newPosition = new Vector2(x * step, y * step) + new Vector2(transform.position.x, transform.position.y);
        obj.transform.position = newPosition;
    }
    private void Init()
    {
        GameObject obj;
        for (int i = 0; i < length; i++)
        {
            obj = Instantiate(chain, transform.position, Quaternion.identity);
            obj.transform.SetParent(transform);
            chainList.Add(obj);
        }

        obj = Instantiate(ball, transform.position, Quaternion.identity);
        obj.transform.SetParent(transform);
        chainList.Add(obj);

        Rotate();
    }
    private void Rotate()
    {
        for (int i = 0; i < chainList.Count; i++)
            RotateObj(chainList[i], i + 1);

        angle += angleStep;
        if (angle >= 360)
        {
            angle = 180;
        }
    }
    [SerializeField] private float animTime = 0.1f;
    private IEnumerator Animation()
    {
        angle = 0;
        while (true)
        {
            Rotate();
            yield return new WaitForSeconds(animTime);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
