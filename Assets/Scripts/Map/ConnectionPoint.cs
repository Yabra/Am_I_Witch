using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPoint : MonoBehaviour
{
    [SerializeField] private GameObject openedPart;
    [SerializeField] private GameObject blockedPart;

    public enum Destinaion
    {
        Up,
        Down,
        Left,
        Right
    }

    public Destinaion destination { get; private set; }
    public bool isClosed { get; private set; }
    public bool isBlocked { get; private set; }

    public void ConnectRoomsByPoints(Room room, ConnectionPoint other)
    {
        room.transform.Translate(other.transform.position - this.transform.position);
    }

    public void ClosePoint()
    {
        isClosed = true;
    }

    public void BlockPoint()
    {
        isClosed = true;
        isBlocked = true;
        openedPart.SetActive(false);
        blockedPart.SetActive(true);
    }

    public bool IsOpposed(ConnectionPoint other)
    {
        switch (other.destination)
        {
            case Destinaion.Up:
                return destination == Destinaion.Down;
            case Destinaion.Down:
                return destination == Destinaion.Up;
            case Destinaion.Left:
                return destination == Destinaion.Right;
            case Destinaion.Right:
                return destination == Destinaion.Left;
            default:
                return false;
        }
    }
}
