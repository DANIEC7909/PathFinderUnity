using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroButton : MonoBehaviour
{
    [SerializeField] Image ImageButton;
    [SerializeField] Button button;
    public Hero AssignedHero { get; private set; }
    public void Build(Hero assignedHero)
    {
        AssignedHero = assignedHero;
        ImageButton.sprite = assignedHero.SO.HeroImage;
        button.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        GameController.Instance.SelectHero(AssignedHero);
    }
}
