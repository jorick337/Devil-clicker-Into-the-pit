using System.Collections;
using DG.Tweening;
using Game.Classes;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Panels
{
    public class EndStoryPanell : MonoBehaviour
    {
        #region CONSTANTS

        private const byte MAX_INDEX = 4;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private StorySettings storySettings;

        private byte _selectedIndex;

        // Animations
        private Sequence devilsGoToPitAnimation;
        private Sequence closePitAnimation;
        private Sequence showMoreDevilsEyesAnimation;

        [Header("UI")]
        [SerializeField] private Text storyText;

        [Header("Devils Go To Pit Animation")]
        [SerializeField] private RectTransform[] devilsRectTransforms; // 0 - left, 1 - centre, 2 - right

        [Header("Close Pit Animation")]
        [SerializeField] private RectTransform pitRectTransform;

        [Header("Show More Devils Eyes Animation")]
        [SerializeField] private RectTransform[] hidenDevilsEyesRectTransforms;

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

        private void OnDisable()
        {
            devilsGoToPitAnimation.Kill();
            closePitAnimation.Kill();
            showMoreDevilsEyesAnimation.Kill();
        }

        #endregion

        #region INITIALIZATION

        private void InitializeAnimations()
        {
            InitializeDevilsGoToPitAnimation();

            closePitAnimation = DOTween.Sequence()
                .Append(pitRectTransform.DOScale(new Vector3(0, 0, 0), 4f).From(new Vector3(1, 1, 1)))
                .Join(pitRectTransform.DORotate(new Vector3(0, 0, 0), 4f).From(new Vector3(0, 0, -90)))
                .Join(pitRectTransform.DORotate(new Vector3(0, 0, -90), 4f).From(new Vector3(0, 0, 0)))
                .SetAutoKill(false).Pause();

            InitializeAppearanceDevilsEyesAnimation();
        }

        private void InitializeDevilsGoToPitAnimation()
        {
            devilsGoToPitAnimation = DOTween.Sequence();

            Vector3 targetPositions = new(0, 200, 0);

            for (int i = 0; i < devilsRectTransforms.Length; i++)
            {
                devilsGoToPitAnimation
                    .Join(devilsRectTransforms[i].DOLocalMove(targetPositions, 3f))
                    .Join(devilsRectTransforms[i].DOScale(new Vector3(0, 0, 0), 3f).From(new Vector3(1, 1, 1)));
            }

            devilsGoToPitAnimation.SetAutoKill(false).Pause();
        }

        private void InitializeAppearanceDevilsEyesAnimation()
        {
            showMoreDevilsEyesAnimation = DOTween.Sequence();

            foreach (var devilEye in hidenDevilsEyesRectTransforms)
            {
                showMoreDevilsEyesAnimation
                    .Join(devilEye.DOScale(new Vector3(1, 1, 1), 2f).From(new Vector3(0, 0, 0)));
            }

            showMoreDevilsEyesAnimation.SetAutoKill(false).Pause();
        }

        #endregion

        #region CORE LOGIC

        IEnumerator StartStory()
        {
            while (_selectedIndex < MAX_INDEX)
            {
                UpdateText();
                UpdateAnimations();
                UpdateAudioSource();

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

            Resources.UnloadUnusedAssets();
        }

        #endregion

        #region UI

        private void UpdateAnimations()
        {
            switch (_selectedIndex)
            {
                case 1:
                    devilsGoToPitAnimation.Restart();
                    break;
                case 2:
                    closePitAnimation.Restart();
                    break;
                case 3:
                    showMoreDevilsEyesAnimation.Restart();
                    break;
                default:
                    break;
            }
        }

        private void UpdateAudioSource()
        {
            audioSource.clip = storySettings.EndAudioResources[_selectedIndex];
            audioSource.Play();
        }

        private void UpdateText() => storyText.text = storySettings.EndStoryTexts[_selectedIndex];

        #endregion
    }
}