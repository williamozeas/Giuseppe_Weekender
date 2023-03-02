using System;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

//based on https://lindenreidblog.com/2018/09/13/using-command-buffers-in-unity-selective-bloom/

public class CustomGlowSystem
{
    static CustomGlowSystem m_Instance; // singleton
    static public CustomGlowSystem instance {
        get {
            if (m_Instance == null)
                m_Instance = new CustomGlowSystem();
            return m_Instance;
        }
    }

    internal HashSet<CustomGlowObj> m_GlowObjs = new HashSet<CustomGlowObj>();

    public void Add(CustomGlowObj o)
    {
        Remove(o);
        m_GlowObjs.Add(o);
        // Debug.Log("added effect " + o.gameObject.name);
    }

    public void Remove(CustomGlowObj o)
    {
        m_GlowObjs.Remove(o);
        // Debug.Log("removed effect " + o.gameObject.name);
    }
}

[ExecuteInEditMode]
public class CustomGlowRenderer : MonoBehaviour
{
    private CommandBuffer m_GlowBuffer;
    // private static Dictionary<Camera, CommandBuffer> m_Cameras = new Dictionary<Camera, CommandBuffer>();
    // private Camera cam;

    private void Cleanup()
    {
        // foreach(var cam in m_Cameras)
        // {
        //     if(cam.Key)
        //         cam.Key.RemoveCommandBuffer(CameraEvent.BeforeLighting, cam.Value);
        // }
        // m_Cameras.Clear();
        // cam = GetComponent<Camera>();
    }

    public void OnDisable()
    {
        Cleanup();
        RenderPipelineManager.beginContextRendering -= OnBeginContextRendering;
    }

    public void OnEnable()
    {
        Cleanup();
        RenderPipelineManager.beginContextRendering += OnBeginContextRendering;
    }

    void Start()
    {
        RenderPipelineManager.beginContextRendering += OnBeginContextRendering;
    }

    private void OnDestroy()
    {
        RenderPipelineManager.beginContextRendering -= OnBeginContextRendering;
    }

    public void OnBeginContextRendering(ScriptableRenderContext context, List<Camera> cameras)
    {
        var render = gameObject.activeInHierarchy && enabled;
        if(!render)
        {
            Cleanup();
            return;
        }

        // if(!cam)
        //     return;
        //
        // if(m_Cameras.ContainsKey(cam))
        //     return;
            
        // create new command buffer
        m_GlowBuffer = new CommandBuffer();
        m_GlowBuffer.name = "Glow map buffer";
        // m_Cameras[cam] = m_GlowBuffer;

        var glowSystem = CustomGlowSystem.instance;

        // create render texture for glow map
        int tempID = Shader.PropertyToID("_Temp1");
        m_GlowBuffer.GetTemporaryRT(tempID, -1, -1, 24, FilterMode.Bilinear);
        m_GlowBuffer.SetRenderTarget(tempID);
        m_GlowBuffer.ClearRenderTarget(true, true, Color.black); // clear before drawing to it each frame!!

        // draw all glow objects to it
        foreach(CustomGlowObj o in glowSystem.m_GlowObjs)
        {
            Renderer r = o.GetComponent<Renderer>();
            Material glowMat = o.glowMaterial;
            if(r && glowMat)
                m_GlowBuffer.DrawRenderer(r, glowMat);
        }

        // set render texture as globally accessable 'glow map' texture
        m_GlowBuffer.SetGlobalTexture("_GlowMap", tempID);

        // add this command buffer to the pipeline
        foreach(Camera cam in cameras)
        {
            cam.AddCommandBuffer(CameraEvent.BeforeLighting, m_GlowBuffer);
        }
        
    }
    
//     private void AddMesh()
//     {
// // As a trick to make OnWillRenderObject() to be called every frame we add a renderer and a big bounding box around the world. This bounding box will trigger the event for us.
// // (We want to use OnWillRenderObject as this has one advantage: It is called once for every camera = Camera.current and so we can make things work for every camera automatically.)
// // Two vertex points are sufficient to make Unity create a bounding box.
//
//         float extents = WorldSize / 2f;
//
//         Mesh mesh = new Mesh();
//         Vector3[] verts = new Vector3[2];
//         verts[0] = new Vector3(-extents, 0.0f, -extents);
//         verts[1] = new Vector3(extents, 1.0f, extents);
//         mesh.vertices = verts;
//         mesh.RecalculateBounds();
//
//         gameObject.AddComponent();
//         MeshFilter mf = gameObject.AddComponent();
//
// #if UNITY_EDITOR
// // Prevent uncritical editor warning: “SendMessage cannot be called during Awake, CheckConsistency, or OnValidate – UnityEngine.MeshFilter:set_sharedMesh(Mesh)”
//         UnityEditor.EditorApplication.delayCall += () =>
// #endif
//         {
//             mf.sharedMesh = mesh;
//         };
}