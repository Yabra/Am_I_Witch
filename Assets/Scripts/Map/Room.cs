using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] public ConnectionPoint[] connectionPoints;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public IEnumerable<ConnectionPoint> GetConnectionPoints()
    {
        foreach (var connectionPoint in connectionPoints)
        {
            yield return connectionPoint;
        }
    }

    public void ConnectToRoom(Room room)
    {

    }
}
