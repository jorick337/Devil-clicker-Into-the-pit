using UnityEngine;

namespace Game.Classes
{
    [CreateAssetMenu(fileName= "StoryPanelCanvas", menuName = "StoryPanelCanvas/Create New Settings", order = 54)]
    public class StorySettings : ScriptableObject
    {
        [Header("Core")]
        [SerializeField] private string[] startStoryTexts; 
        [SerializeField] private string[] endStoryTexts;

        public string[] StartStoryTexts => startStoryTexts;
        public string[] EndStoryTexts => endStoryTexts;

        [Header("Sounds")]
        [SerializeField] private AudioClip[] startAudioResources;
        [SerializeField] private AudioClip[] endAudioResources;

        public AudioClip[] StartAudioResources => startAudioResources;
        public AudioClip[] EndAudioResources => endAudioResources;
    }
}