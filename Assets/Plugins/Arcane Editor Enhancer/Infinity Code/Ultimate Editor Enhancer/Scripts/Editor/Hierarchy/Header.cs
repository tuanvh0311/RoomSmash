/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.HierarchyTools
{
    [InitializeOnLoad]
    public static class HierarchyHeader
    {
        static HierarchyHeader()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }

        private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            selectionRect.size = new Vector2(selectionRect.width, selectionRect.height - 1f); 

            GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (go != null && go.name.StartsWith(Prefs.hierarchyHeaderPrefix))
            {
                // Get the color for the text and background
                Color textColor = new Color(0f, 0f, 0f); //black

                Color backTextColor = new Color(1f, 0.1f, 0.1f); //red

                Color backEffectTextColor = new Color(0.95f, 0.8f, 0.8f); //white

                Color backEffectTextColor2 = new Color(0.95f, 0.1f, 0.1f); //red

                Color backgroundColor = new Color(0.35f, 0f, 0f); //blood

                // Save the current GUI color
                Color previousColor = GUI.color;

                // Set the background color
                GUI.color = backgroundColor;
                GUI.DrawTexture(selectionRect, EditorGUIUtility.whiteTexture);

                GUI.color = backEffectTextColor;
                GUIStyle style = new GUIStyle();
                // Set the text color and draw the header text
                style.normal.textColor = backEffectTextColor;
                style.alignment = TextAnchor.MiddleCenter;
                style.fontStyle = FontStyle.Bold;

                int originalFontSize = style.fontSize;

                #region EffectZone
                style.fontSize = 21;
                style.padding = new RectOffset(5, 0, 0, 0);
                EditorGUI.LabelField(selectionRect, go.name, style);

                GUI.color = backEffectTextColor2;
                style.normal.textColor = backEffectTextColor2;
                style.fontSize = 20;
                style.padding = new RectOffset(5, 0, 0, 0);
                EditorGUI.LabelField(selectionRect, go.name, style);

                GUI.color = backEffectTextColor;
                style.normal.textColor = backEffectTextColor;
                style.fontSize = 19;
                style.padding = new RectOffset(5, 0, 0, 0);
                EditorGUI.LabelField(selectionRect, go.name, style);

                GUI.color = backEffectTextColor2;
                style.normal.textColor = backEffectTextColor2;
                style.fontSize = 18;
                style.padding = new RectOffset(5, 0, 0, 0);
                EditorGUI.LabelField(selectionRect, go.name, style);

                GUI.color = backEffectTextColor;
                style.normal.textColor = backEffectTextColor;
                style.fontSize = 17;
                style.padding = new RectOffset(5, 0, 0, 0);
                EditorGUI.LabelField(selectionRect, go.name, style);

                GUI.color = backEffectTextColor2;
                style.normal.textColor = backEffectTextColor2;
                style.fontSize = 16;
                style.padding = new RectOffset(5, 0, 0, 0);
                EditorGUI.LabelField(selectionRect, go.name, style);

                GUI.color = backEffectTextColor;
                style.normal.textColor = backEffectTextColor;
                style.fontSize = 15;
                style.padding = new RectOffset(5, 0, 0, 0);
                EditorGUI.LabelField(selectionRect, go.name, style);

                GUI.color = backEffectTextColor2;
                style.normal.textColor = backEffectTextColor2;
                style.fontSize = 14;
                style.padding = new RectOffset(5, 0, 0, 0);
                EditorGUI.LabelField(selectionRect, go.name, style);

                #endregion

                GUI.color = textColor;
                style.normal.textColor = textColor;
                style.fontSize = 13;
                style.padding = new RectOffset(9, 0, 4, 0);
                EditorGUI.LabelField(selectionRect, go.name, style);

                GUI.color = backTextColor;
                style.normal.textColor = backTextColor;
                style.fontSize = 12;
                style.padding = new RectOffset(8, 0, 3, 0);
                EditorGUI.LabelField(selectionRect, go.name, style);

                GUI.color = textColor;
                style.normal.textColor = textColor;
                style.fontSize = originalFontSize;
                style.padding = new RectOffset(5, 0, 0, 0);
                EditorGUI.LabelField(selectionRect, go.name, style);

                // Restore the previous GUI color
                GUI.color = previousColor;
            }
        }
    }

    [InitializeOnLoad]
    public static class Header
    {
        static Header()
        {
            HierarchyItemDrawer.Register("Header", OnHierarchyItem, HierarchyToolOrder.HEADER);
        }

        [MenuItem("GameObject/Magiao/Create Hierarchy Header UltimateEditorEnhancer", priority = 9)]
        public static GameObject Create()
        {
            GameObject go = new GameObject(Prefs.hierarchyHeaderPrefix + "✮★☠︎༒︎✞︎Header✞︎༒︎☠︎★✮" + Prefs.hierarchyHeaderPrefix);

            //hide the redundant transform component
            go.transform.hideFlags = HideFlags.HideInInspector;

            go.tag = "EditorOnly";
            GameObject active = Selection.activeGameObject;
            if (active != null)
            {
                go.transform.SetParent(active.transform.parent);
                go.transform.SetSiblingIndex(active.transform.GetSiblingIndex());
            }
            Undo.RegisterCreatedObjectUndo(go, go.name);
            Selection.activeGameObject = go;
            return go;
        }

        private static void OnHierarchyItem(HierarchyItem item)
        {
            if (!Prefs.hierarchyHeaders) return;

            GameObject go = item.gameObject;
            if (go == null) return;

            List<HeaderRule> rules = ReferenceManager.headerRules;
            HeaderRule rule = null;

            for (int i = 0; i < rules.Count; i++)
            {
                if (rules[i].Validate(go))
                {
                    rule = rules[i];
                    break;
                }
            }

            if (rule == null) return;

            if (Event.current.type == EventType.Repaint)
            {
                int textPadding = 20;

                bool hasChildren = item.gameObject.transform.childCount > 0;
                if (hasChildren) textPadding = (int)item.rect.x - 30;

                item.rect.size = new Vector2(item.rect.width, item.rect.height - 1f);

                Color ruleColor = new Color(1f, 0f, 0f); // Red

                rule.Draw(item, textPadding);

                if (hasChildren)
                {
                    bool isExpanded = HierarchyHelper.IsExpanded(item.id);
                    Rect r = new Rect(item.rect);
                    r.width = 16;
                    r.x -= 14;
                    EditorStyles.foldout.Draw(r, GUIContent.none, -1, isExpanded);
                }
            }

            HierarchyItemDrawer.StopCurrentRowGUI();
        }
    }
}