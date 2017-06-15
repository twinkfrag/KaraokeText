using UnityEngine;

[RequireComponent(typeof(TextMesh), typeof(MeshRenderer))]
public class KaraokeTextLiner : MonoBehaviour
{
	private TextMesh textMesh;
	private new Renderer renderer;

#pragma warning disable 649 // unassigned field

	[SerializeField]
	private Material karaokeTextMaterial;

#pragma warning restore 649

	[Range(0f, 1f)]
	public float Lerp;

	private void Start()
	{
		textMesh = GetComponent<TextMesh>();
		renderer = GetComponent<MeshRenderer>();
		karaokeTextMaterial.SetFloat("_LerpMargin", textMesh.characterSize / 2);
	}

	private void Update()
	{
		SetParams();
	}

	private void SetParams()
	{
		var w = renderer.bounds.max.x;
		karaokeTextMaterial.SetFloat("_LineWidth", w);
		karaokeTextMaterial.SetFloat("_Lerp", Lerp);
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	private void OnValidate()
	{
		Start();
		SetParams();
	}
}
