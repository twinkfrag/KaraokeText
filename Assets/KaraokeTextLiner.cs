using UnityEngine;

[RequireComponent(typeof(TextMesh), typeof(MeshRenderer))]
public class KaraokeTextLiner : MonoBehaviour
{
	private TextMesh textMesh;
	private new MeshRenderer renderer;
	
	[SerializeField]
	private Material karaokeTextMaterial = null;

	[Range(0f, 1f)]
	public float Lerp;

	private void Start()
	{
		textMesh = GetComponent<TextMesh>();
		renderer = GetComponent<MeshRenderer>();
	}

	private void Update()
	{
		SetParams();
	}

	private void SetParams()
	{
		var lerpMargin = textMesh.characterSize / 2;
		var w = renderer.bounds.size.x;
		karaokeTextMaterial.SetFloat("_Lerp", Lerp);

		switch (textMesh.anchor)
		{
			case TextAnchor.UpperLeft:
			case TextAnchor.MiddleLeft:
			case TextAnchor.LowerLeft:
				karaokeTextMaterial.SetFloat("_Start", 0 - lerpMargin);
				karaokeTextMaterial.SetFloat("_End", w + lerpMargin);
				break;
			case TextAnchor.UpperCenter:
			case TextAnchor.MiddleCenter:
			case TextAnchor.LowerCenter:
				karaokeTextMaterial.SetFloat("_Start", -w / 2 - lerpMargin);
				karaokeTextMaterial.SetFloat("_End", w / 2 + lerpMargin);
				break;
			case TextAnchor.UpperRight:
			case TextAnchor.MiddleRight:
			case TextAnchor.LowerRight:
				karaokeTextMaterial.SetFloat("_Start", -w - lerpMargin);
				karaokeTextMaterial.SetFloat("_End", 0 + lerpMargin);
				break;
		}
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	private void OnValidate()
	{
		Start();
		SetParams();
	}
}
