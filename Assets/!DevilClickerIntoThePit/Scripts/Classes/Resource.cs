using System.Threading.Tasks;
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

        public T GetInstantiateGameObject<T>() where T : MonoBehaviour
        {
            T gameObject = Resources.Load<T>(pathToObject);
            gameObject = Instantiate(gameObject, window.transform);

            return gameObject;
        }
    }
}