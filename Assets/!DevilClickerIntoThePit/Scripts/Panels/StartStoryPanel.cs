using System.Collections;
using DG.Tweening;
using Game.Classes;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Panels
{
    public class StartStoryPanel : MonoBehaviour
    {
        #region CONSTANTS

        private const byte MAX_INDEX = 6;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private StorySettings storySettings;

        private byte _selectedIndex;

        // Animations
        private Tween showPitAnimation;
        private Sequence crowledOutDevilsAnimation;
        private Sequence appearanceDevilsEyesAnimation;

        [Header("UI")]
        [SerializeField] private Text storyText;

        [Header("Show Pit Animation")]
        [SerializeField] private RectTransform pitRectTransform;

        [Header("Crowled Out Devils Animation")]
        [SerializeField] private RectTransform[] devilsRectTransforms; // 0 - left, 1 - centre, 2 - right

        [Header("Appearance Devils Eyes")]
        [SerializeField] private RectTransform[] devilsEyesRectTransforms;

        [Header("Sounds")]
        [SerializeField] private AudioSource audioSource;

        #endregion

        #region MONO

        private void Awake()
        {
            _selectedIndex = 0;

            InitializeAnimations();
        }

        private void Start()
        {
            StartCoroutine(StartStory());
        }

        private void Update()
        {
            if (Input.anyKey)
            {
                EndStory();
            }
        }

        #endregion

        #region INITIALIZATION

        private void InitializeAnimations()
        {
            showPitAnimation = DOTween.Sequence()
                .Append(pitRectTransform.DOScale(new Vector3(1, 1, 1), 3f).From(new Vector3(0, 0, 0)))
                .Join(pitRectTransform.DORotate(new Vector3(0, 0, 90), 1f).From(new Vector3(0, 0, 0)))
                .Join(pitRectTransform.DORotate(new Vector3(0, 0, 0), 2f).From(new Vector3(0, 0, 90)))
                .SetAutoKill(false).Pause();

            InitializeCrowledOutDevilsAnimation();
            InitializeAppearanceDevilsEyesAnimation();
        }

        private void InitializeCrowledOutDevilsAnimation()
        {
            crowledOutDevilsAnimation = DOTween.Sequence();
            Vector3[] targetPositions = new Vector3[]
            {
                new(-660, -90, 0),
                new(0, -200, 0),
                new(660, -90, 0)
            };

            for (int i = 0; i < devilsRectTransforms.Length; i++)
            {
                crowledOutDevilsAnimation
                    .Join(devilsRectTransforms[i].DOLocalMove(targetPositions[i], 3f))
                    .Join(devilsRectTransforms[i].DOScale(new Vector3(1, 1, 1), 3f).From(new Vector3(0, 0, 0)));
            }

            crowledOutDevilsAnimation.SetAutoKill(false).Pause();
        }

        private void InitializeAppearanceDevilsEyesAnimation()
        {
            appearanceDevilsEyesAnimation = DOTween.Sequence();

            foreach (var devilEye in devilsEyesRectTransforms)
            {
                appearanceDevilsEyesAnimation
                    .Join(devilEye.DOScale(new Vector3(1, 1, 1), 13f).From(new Vector3(0, 0, 0)));
            }

            appearanceDevilsEyesAnimation.SetAutoKill(false).Pause();
        }

        #endregion

        #region CORE LOGIC

        IEnumerator StartStory()
        {
            while (_selectedIndex < MAX_INDEX)
            {
                UpdateText();
                UpdateAnimations();

                audioSource.clip = storySettings.AudioResources[_selectedIndex];
                audioSource.Play();

                yield return new WaitWhile(() => audioSource.isPlaying);

                _selectedIndex += 1;
            }

            yield return new WaitForSeconds(1.5f);

            EndStory();
        }

        private void EndStory()
        {
            StopAllCoroutines();
            Destroy(gameObject);

            System.GC.Collect();
            Resources.UnloadUnusedAssets();
        }

        #endregion

        #region UI

        private void UpdateAnimations()
        {
            switch (_selectedIndex)
            {
                case 1:
                    showPitAnimation.Restart();
                    break;
                case 2:
                    crowledOutDevilsAnimation.Restart();
                    break;
                case 3:
                    appearanceDevilsEyesAnimation.Restart();
                    break;
                default:
                    break;
            }
        }

        private void UpdateText() => storyText.text = storySettings.Texts[_selectedIndex];

        #endregion
    }
}