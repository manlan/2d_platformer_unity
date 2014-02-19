using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(UnityJellySprite))]
class UnityJellySpriteEditor : JellySpriteEditor
{
	public SerializedProperty m_Sprite;
	public Object m_InitialSprite;
	
	protected override void OnEnable() 
	{
		base.OnEnable();
		m_Sprite = serializedObject.FindProperty("m_Sprite");
	}
	
	protected override void DisplayInspectorGUI()
	{
		EditorGUILayout.PropertyField(m_Sprite, new GUIContent("Sprite"));
		base.DisplayInspectorGUI();
	}

	protected override void StoreInitialValues()
	{
		m_InitialSprite = m_Sprite.objectReferenceValue;
		base.StoreInitialValues();
	}

	protected override void CheckForObjectChanges()
	{
		base.CheckForObjectChanges();
		JellySprite targetObject = this.target as JellySprite;
		
		if(m_InitialSprite != m_Sprite.objectReferenceValue)
		{
			targetObject.RefreshMesh();
		}
	}

	void OnSceneGUI ()
	{
		UpdateHandles();
	}
}