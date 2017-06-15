using System.Collections.Generic;
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

	public string Text
	{
		get { return textMesh.text; }
		set
		{
			textMesh.text = value;
			textWidth = 0;
			foreach (var c in value)
			{
				float w;
				if (dictionary.TryGetValue(c, out w))
				{
					textWidth += w;
					continue;
				}

				CharacterInfo ci;
				if (textMesh.font.GetCharacterInfo(c, out ci, textMesh.fontSize, textMesh.fontStyle))
				{
					dictionary.Add(c, ci.advance);
					textWidth += ci.advance;
					continue;
				}

				textWidth += textMesh.characterSize;
			}
			textWidth *= textMesh.characterSize * 0.1f;
		}
	}

	private readonly Dictionary<char, float> dictionary = new Dictionary<char, float>();

	private float textWidth;

	private void Start()
	{
		textMesh = GetComponent<TextMesh>();
		renderer = GetComponent<MeshRenderer>();

		Text = textMesh.text;
	}

	private void Update()
	{
		SetParams();
	}

	private void SetParams()
	{
		var lerpMargin = textMesh.characterSize / 2;
		
		// 常に正面しか向いてないならこれで十分なんだが
		//textWidth = renderer.bounds.size.x;
		
		karaokeTextMaterial.SetFloat("_Lerp", Lerp);

		switch (textMesh.anchor)
		{
			case TextAnchor.UpperLeft:
			case TextAnchor.MiddleLeft:
			case TextAnchor.LowerLeft:
				karaokeTextMaterial.SetFloat("_Start", 0 - lerpMargin);
				karaokeTextMaterial.SetFloat("_End", textWidth + lerpMargin);
				break;
			case TextAnchor.UpperCenter:
			case TextAnchor.MiddleCenter:
			case TextAnchor.LowerCenter:
				karaokeTextMaterial.SetFloat("_Start", -textWidth / 2 - lerpMargin);
				karaokeTextMaterial.SetFloat("_End", textWidth / 2 + lerpMargin);
				break;
			case TextAnchor.UpperRight:
			case TextAnchor.MiddleRight:
			case TextAnchor.LowerRight:
				karaokeTextMaterial.SetFloat("_Start", -textWidth - lerpMargin);
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
