using UnityEngine;

//from https://lindenreidblog.com/2018/09/13/using-command-buffers-in-unity-selective-bloom/

[ExecuteInEditMode]
public class CustomGlowObj : MonoBehaviour
{
    public Material glowMaterial;

    public void OnEnable()
    {
        CustomGlowSystem.instance.Add(this);
    }

    public void Start()
    {
        CustomGlowSystem.instance.Add(this);
    }

    public void OnDisable()
    {
        CustomGlowSystem.instance.Remove(this);
    }

}