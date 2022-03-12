using UnityEngine;

namespace Environment
{
    public class Ground : MonoBehaviour
    {
        private Renderer _groundRenderer;
        private static readonly int GroundColor = Shader.PropertyToID("_Color");

        private void Awake()
        {
            _groundRenderer = GetComponent<Renderer>();
        }

        public void SetRandomColor(Color color)
        {
            _groundRenderer.material.SetColor(GroundColor,color);
        }
    }
}
