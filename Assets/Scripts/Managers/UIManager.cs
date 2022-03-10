using System;
using System.Collections.Generic;
using Core;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Managers
{
    public class UIManager : MonoBehaviourSingleton<UIManager>
    {
        [SerializeField] private Image fadeImage = null;
        [SerializeField] private List<Panel> panels = new List<Panel>();

        [Space, Header("Level Texts")]
		[SerializeField] private TextMeshProUGUI winLevelText = null;
		[SerializeField] private TextMeshProUGUI loseLevelText = null;
		[SerializeField] private TextMeshProUGUI levelText = null;
		
		

		private void OnEnable()
		{
			EventBus.OnLevelWin += OnLevelWin;
			EventBus.OnLevelLose += OnLevelLose;
			EventBus.OnLevelReset += OnLevelReset;
		}

		private void OnDisable()
		{
			EventBus.OnLevelWin -= OnLevelWin;
			EventBus.OnLevelLose -= OnLevelLose;
			EventBus.OnLevelReset -= OnLevelReset;
		}

		private void OnTapToPlay()
		{
		}

		private void OnLevelReset()
		{
			UpdateLevelTexts();
			ShowPanel(PanelType.Gameplay);
		}

		private void OnLevelWin()
		{
			UpdateLevelTexts();
			ShowPanel(PanelType.Win);
		}

		private void OnLevelLose()
		{
			UpdateLevelTexts();
			ShowPanel(PanelType.Lose);
		} 


		public YieldInstruction Fade(FadeType fadeType, float duration = 0.5f, Ease ease = Ease.InOutSine)
		{
			fadeImage.gameObject.SetActive(true);
			fadeImage.raycastTarget = true;
			return fadeImage.DOFade(fadeType == FadeType.In ? 1.0f : 0.0f, duration)
				.SetEase(ease).OnComplete(() => fadeImage.raycastTarget = false).WaitForCompletion();
		}

		public void ShowPanel(PanelType panelType, bool hideOthers = true)
		{
			panels.ForEach(p =>
			{
				if (p.panelType == panelType || panelType == PanelType.All)
					p.Show();
				else if (hideOthers)
					p.Hide();
			});
		}
		

		public void HidePanel(PanelType panelType)
		{
			panels.ForEach(p =>
			{
				if (p.panelType == panelType || panelType == PanelType.All)
					p.Hide();
			});
		}
		
		private void UpdateLevelTexts()
		{
			int level = PlayerPrefs.GetInt(LevelManager.PrefsLevelKey) + 1;
		
			if (winLevelText)
			{
				winLevelText.text = $"Level {level} Completed";
			}

			if (loseLevelText)
			{
				loseLevelText.text = $"Level {level} Failed";
			}
			if (levelText)
			{
				levelText.text = $"Level {level}";
			}
		}
    }

	public enum FadeType
	{
		In,
		Out
	}

	[Serializable]
	public class Panel
	{
		public GameObject panelObject;
		public PanelType panelType = PanelType.None;

		public void Show() => panelObject.SetActive(true);
		public void Hide() => panelObject.SetActive(false);
	}
	
	public enum PanelType
	{
		None,
		All,
		TapToPlay,
		Gameplay,
		Win,
		Lose,
		Tutorial,
	}
}