using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EnnemySubwave))]
public class EnnemyWaveDrawer : PropertyDrawer {

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label){
		int numberOfLines = 1;

		return (EditorGUIUtility.singleLineHeight + 1) * numberOfLines;
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
		//base.OnGUI(position, property, label);

		int currentIndentation = EditorGUI.indentLevel;

		EditorGUI.indentLevel = 0;

		float indentationPixels = currentIndentation * 16;

		float usableSpace = position.width - indentationPixels;
		// à partir d'ici on utilise plus position.width


		float space = usableSpace * 0.01f;
		float currentX = position.x + indentationPixels; // le point de départ

		//Construction de nos rectangles
		Rect leftRect = new Rect(currentX, position.y, usableSpace * 0.2f, position.height);

		currentX += leftRect.width;
		currentX += space;

		Rect middleRect = new Rect(currentX, position.y, usableSpace * 0.68f, position.height);

		currentX += middleRect.width;
		currentX += space;

		Rect rightRect = new Rect(currentX, position.y, usableSpace * 0.1f, position.height);

		//Débugger de couleur
		/* XEditorGUI.DrawRect(leftRect, new Color(1,0,0,0.5f));
		EditorGUI.DrawRect(middleRect, new Color(0,1,0,0.5f));
		EditorGUI.DrawRect(rightRect, new Color(0,0,1,0.5f)); */


		//Récupération des propriété et affichage
		EditorGUI.LabelField(leftRect, "Enemy Wave", EditorStyles.boldLabel);
		SerializedProperty enemyAmount = property.FindPropertyRelative("enemyAmount");
		enemyAmount.intValue = EditorGUI.IntField(rightRect, enemyAmount.intValue);

		SerializedProperty ennemy = property.FindPropertyRelative("ennemy");
		ennemy.objectReferenceValue = EditorGUI.ObjectField(middleRect, ennemy.objectReferenceValue, typeof(GameObject), false);


		EditorGUI.indentLevel = currentIndentation;
	}
}
