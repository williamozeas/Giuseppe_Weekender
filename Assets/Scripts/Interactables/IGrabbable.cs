using UnityEngine;

public interface IGrabbable
{
    void Grabbed(GameObject grabber);
    void Dropped();
}
