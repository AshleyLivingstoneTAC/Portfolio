using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    Planet planet;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawSettingsEditor(planet.shape, planet.OnShapeSettingsUpdated, ref planet.ssFoldout);
        DrawSettingsEditor(planet.color, planet.OnColorSettingsUpdated, ref planet.csFoldout);
    }
    void DrawSettingsEditor(Object Settings, System.Action onSettingsUpdated, ref bool foldout)
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, Settings);

            if (foldout)
            {
                Editor editor = CreateEditor(Settings);
                editor.OnInspectorGUI();
                if (check.changed)
                {
                    if (onSettingsUpdated != null)
                    {
                        onSettingsUpdated();
                    }
                }
            }
        }
    }
    private void OnEnable()
    {
        planet = (Planet)target;
    }
}
