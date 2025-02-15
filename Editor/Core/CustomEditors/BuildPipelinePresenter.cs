using UnityEditor;
using UnityEngine;

namespace UniTools.Build
{
    public sealed class BuildPipelinePresenter
    {
        private readonly BuildPipeline m_buildPipeline = default;
        private bool m_foldout = false;

        public BuildPipelinePresenter(BuildPipeline buildPipeline)
        {
            m_buildPipeline = buildPipeline;
        }

        public void Draw()
        {
            bool run = false;
            bool modify = false;
            if (m_foldout)
            {
                EditorGUILayout.BeginVertical("box");
                {
                    m_foldout = EditorGUILayout.Foldout(m_foldout, m_buildPipeline.name);
                    DrawButtons(out run, out modify);
                }
                EditorGUILayout.EndVertical();
            }
            else
            {
                EditorGUILayout.BeginHorizontal("box");
                {
                    m_foldout = EditorGUILayout.Foldout(m_foldout, m_buildPipeline.name);
                    DrawButtons(out run, out modify);
                }
                EditorGUILayout.EndHorizontal();
            }

            if (run)
            {
                m_buildPipeline.Run().GetAwaiter().OnCompleted(() => { Debug.Log($"Pipeline {m_buildPipeline} is completed!"); });
            }

            if (modify)
            {
                EditorGUIUtility.PingObject(m_buildPipeline);
            }
        }

        private void DrawButtons(out bool run, out bool modify)
        {
            EditorGUILayout.BeginHorizontal();
            {
                modify = GUILayout.Button("Modify", GUILayout.Width(100));
                run = GUILayout.Button("Run", GUILayout.Width(100));
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}