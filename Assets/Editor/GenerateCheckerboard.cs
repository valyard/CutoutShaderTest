using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class GenerateCheckerboard : EditorWindow {

	[MenuItem("Custom/Generate Checkerboard")]
	private static void Open()
	{
		EditorWindow.GetWindow<GenerateCheckerboard>();
	}

	private int width = 750;
	private int height = 1334;
	private int size = 1;

	private void OnGUI()
	{
		width = EditorGUILayout.IntField("Width:", width); 
		height = EditorGUILayout.IntField("Height:", height);
		size = EditorGUILayout.IntField("Block size:", size);
		if (GUILayout.Button("Generate"))
		{
			var texture = generate(width, height, size);
			var bytes = texture.EncodeToPNG();
			File.WriteAllBytes("Assets/Textures/texture.png",bytes);
			AssetDatabase.Refresh();
		}
	}

	private Texture2D generate(int width, int height, int size)
	{
		var tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
		var arr = new Color32[width * height];
		for (var y = 0; y < height; y++)
		{
			var ycoeff = Mathf.FloorToInt(y/size);
			for (var x = 0; x < width; x++)
			{
				var xcoeff = Mathf.FloorToInt(x/size);
				arr[y*width + x] = (xcoeff + ycoeff) % 2 == 0 ? Color.clear : Color.white;
			}
		}
		tex.SetPixels32(arr);
		tex.Apply();

		return tex;
	}

}
