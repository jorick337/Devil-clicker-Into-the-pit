using UnityEngine;

namespace Game.Classes
{
    public class Resource : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private string pathToObject;
        [SerializeField] private GameObject window;

        public void LoadAndInstantiateResource()
        {
            GameObject gameObject = Resources.Load<GameObject>(pathToObject);

            Instantiate(gameObject, window.transform);
        }
    }
}