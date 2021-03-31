using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ObjectTweener))]
public class ObjectTweenerInspector : PropertyDrawer
{
    private float fieldSize = 16;

    private int fieldAmount = 4;

    float lineHeigth = EditorGUIUtility.singleLineHeight + 2;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        //set the height of the drawer by the field size and padding
        //return (fieldSize * fieldAmount) + (padding * fieldAmount);
        return lineHeigth * (fieldAmount + 1);
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int counter = 0;
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 1;

        counter++;
        AddPropertyField(position, property.FindPropertyRelative("target"), counter, true);
        counter++;
        AddPropertyField(position, property.FindPropertyRelative("currentPoint"), counter, true);

        counter++;
        SerializedProperty pointArray = property.FindPropertyRelative("points");

        AddPropertyField(position, pointArray, counter, true);
        for (int i = 0; i < pointArray.arraySize; ++i)
        {
            counter++;
            //AddPropertyField(position, pointArray.GetArrayElementAtIndex(i), counter, false);
        }
        counter++;

        EditorGUI.indentLevel = indent;
        fieldAmount = counter;

        //EditorGUI.
        EditorGUI.EndProperty();
    }

    void AddPropertyField(Rect p, SerializedProperty sp, int index, bool includeChild)
    {
        Rect r = new Rect(p.x, p.y + lineHeigth * index, p.width, lineHeigth);
        EditorGUI.PropertyField(r, sp, includeChild);
    }
}
