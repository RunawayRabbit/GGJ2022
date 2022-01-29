using UnityEngine;

public class RenderToTexture : MonoBehaviour {

	[SerializeField] private Material material;
	[SerializeField] private string textureName = "_NiceTex";
	[SerializeField] private float textureResolution = 1.0f;


	private void Awake() {
		var tex = new RenderTexture((int)(textureResolution * Screen.width),
								  (int)(textureResolution * Screen.height), 8);
		Debug.Assert(tex.Create(), "Couldn't make a RT");

		Camera cam = GetComponent<Camera>();
		cam.targetTexture = tex;
		material.SetTexture(textureName, tex);
	}
}
