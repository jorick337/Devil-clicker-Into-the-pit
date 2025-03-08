using UnityEngine;

namespace Game.Classes
{
    [CreateAssetMenu(fileName= "StoryPanelCanvas", menuName = "StoryPanelCanvas/Create New Settings", order = 54)]
    public class StorySettings : ScriptableObject
    {
        [Header("Core")]
        [SerializeField] private string[] texts; 

        public string[] Texts => texts;

        [Header("Sounds")]
        [SerializeField] private AudioClip[] audioResources;

        public AudioClip[] AudioResources => audioResources;
    }
}