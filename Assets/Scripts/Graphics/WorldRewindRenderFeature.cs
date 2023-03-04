using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;
using UnityEngine.Rendering.Universal;

//constructed based on Unity docs here: https://docs.unity.cn/Packages/com.unity.render-pipelines.universal@12.1/manual/containers/create-custom-renderer-feature-1.html
public class WorldRewindRenderFeature : ScriptableRendererFeature
{
    class WorldRewindRenderPass : ScriptableRenderPass
    {
        private LayerMask layerMask;
        private Renderer test;
        
        static int tempID = Shader.PropertyToID("_Temp1");
        public WorldRewindRenderPass(LayerMask layerMask, Renderer test)
        {
            this.layerMask = layerMask;
            this.test = test;
        }
        
        public override void Execute(ScriptableRenderContext context,
            ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(name: "WorldRewindPass");

            Camera camera = renderingData.cameraData.camera;

            // create render texture for glow map
            cmd.GetTemporaryRT(tempID, renderingData.cameraData.cameraTargetDescriptor, FilterMode.Bilinear);
            cmd.SetRenderTarget(tempID, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
            
            cmd.ClearRenderTarget(true, true, Color.clear); // clear before drawing to it each frame!!
            
            //https://forum.unity.com/threads/urp-how-to-add-layer-mask-to-custom-render-feature.849283/
            // FilteringSettings filteringSettings = new FilteringSettings(RenderQueueRange.all, layerMask);
            // DrawingSettings drawingSettings = new DrawingSettings(new ShaderTagId("UniversalForward"), new SortingSettings(camera));
            // context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings);
            RendererListDesc desc =
                new RendererListDesc(new ShaderTagId("Universal2D"), renderingData.cullResults, camera);
            desc.layerMask = layerMask;
            desc.excludeObjectMotionVectors = false;
            desc.renderQueueRange = RenderQueueRange.all;
            
            RendererList list = context.CreateRendererList(desc);
            // Debug.Log(list);
            cmd.DrawRendererList(list);
            // cmd.DrawRenderer(test, test.material);

            // set render texture as globally accessable 'glow map' texture
            cmd.SetGlobalTexture("_MaskTexture", tempID);
            cmd.ReleaseTemporaryRT(tempID);
        
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }
    
    private WorldRewindRenderPass _worldRewindRenderPass;
    public LayerMask layerMask;
    private Renderer rend;

    public override void Create()
    {
        // rend = GameManager.Instance.Player.GetComponentInChildren<Renderer>();
        _worldRewindRenderPass = new WorldRewindRenderPass(layerMask, null);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer,
        ref RenderingData renderingData)
    {
        renderer.EnqueuePass(_worldRewindRenderPass);
    }
}


