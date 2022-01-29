
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlitCircleSDFFeature : ScriptableRendererFeature
{
	class BlitPass : ScriptableRenderPass
	{
		private readonly Material _mat;
		private RenderTargetIdentifier _sourceRti;
		private RenderTargetHandle _tempRth;
		private int renderPassIndex = -1;

		public BlitPass( Material mat, int passIndex )
		{
			_mat            = mat;
			renderPassIndex = passIndex;

			_tempRth.Init("_TempSDFTexture");
		}

		public void Init(RenderTargetIdentifier source)
		{
			_sourceRti = source;
		}

		public override void Execute( ScriptableRenderContext ctx, ref RenderingData renderData )
		{
			CommandBuffer           cmd        = CommandBufferPool.Get("BlitCircleSDFFeature");
			RenderTextureDescriptor camTexDesc = renderData.cameraData.cameraTargetDescriptor;

			camTexDesc.depthBufferBits = 0; // disable ztest

			//@TODO: Can we get away with point filtering here?
			cmd.GetTemporaryRT(_tempRth.id, camTexDesc, FilterMode.Bilinear);

			Blit( cmd, _sourceRti, _tempRth.Identifier(), _mat, renderPassIndex );
			Blit( cmd, _tempRth.Identifier(), _sourceRti );

			ctx.ExecuteCommandBuffer(cmd);
			CommandBufferPool.Release(cmd); // good citizens
		}

		public override void FrameCleanup( CommandBuffer cmd )
		{
			cmd.ReleaseTemporaryRT( _tempRth.id );
		}

	}
	private BlitPass _pass;

	[System.Serializable]
	public class Settings
	{
		public Material material;
		public int passIndex = -1;
		public RenderPassEvent passEvent = RenderPassEvent.AfterRenderingPostProcessing;
	}

	[SerializeField] private Settings settings = new Settings();

	public override void Create()
	{
		var material = settings.material;

		//@TODO: I'm assuming we want to run separate postprocessing passes for both cameras?
		_pass                 = new BlitPass( settings.material, settings.passIndex );
		_pass.renderPassEvent = settings.passEvent;
	}

	public override void AddRenderPasses( ScriptableRenderer renderer, ref RenderingData renderData )
	{
		if( !renderData.cameraData.camera.CompareTag( "MainCamera" ) ) return;

		_pass.Init( renderer.cameraColorTarget );
		renderer.EnqueuePass(_pass);
	}
}
