using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

///
public class Block : MonoBehaviour
{
    protected int Points;
    private PointsAddedEvent PointsAddedEvent;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        PointsAddedEvent = new PointsAddedEvent();
        EventManager.AddPointsAddedInvoker(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPointsAddedEventListener(UnityAction<int> listener)
    {
        PointsAddedEvent.AddListener(listener);
    }

    protected virtual void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.CompareTag("Ball"))
        {
            PointsAddedEvent.Invoke(Points);
            Destroy(gameObject);
        }
    }
}
