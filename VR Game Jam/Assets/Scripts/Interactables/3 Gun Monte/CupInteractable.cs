using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CupInteractable : Interactable
{
    // object dependancies
    [SerializeField] private Animator m_animator;
    [SerializeField] private GameObject m_hiddenObject;

    // umpire reference
    private UmpireControl m_umpire;
    
    // is this cup the one to shoot?
    private bool m_isDesiredCup = false;
    
    #region getters

    public bool GetIsDesiredCup() { return m_isDesiredCup; }

    #endregion

    #region setters

    public void SetAsDesiredCup() { m_isDesiredCup = true; m_hiddenObject.SetActive(true); }
    public void SetUmpire(UmpireControl umpire) { m_umpire = umpire; }
    public void Reveal() { m_animator.SetTrigger("reveal"); }
    public void Hide() { m_animator.SetTrigger("hide"); }

    #endregion

    /// <summary>
    /// when a cup is shot, apply force to it and evaluate win condition
    /// </summary>
    public override void OnInteract()
    {
        ShootCup();
        EvaluateEndCondition();
    }

    /// <summary>
    /// unparent hidden object, give the cup a rigidbody and apply force to it
    /// </summary>
    private void ShootCup()
    {
        // unparent hidden object
        m_hiddenObject.transform.SetParent(null);

        // give cup a rigidbody
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();

        // apply force at position
        Vector3 force = (Raycaster.GetHitNormal() + Vector3.down) * -1000;
        Vector3 position = Raycaster.GetHitPoint();
        rigidbody.AddForceAtPosition(force, position);
    }

    /// <summary>
    /// 
    /// </summary>
    private void EvaluateEndCondition()
    {
        if (m_isDesiredCup) // right cup
        {
            float reactionTime = UmpireControl.reactionTimer;

            // update highscore
            if (HighscoreManager.GetHighscore("3 Gun Monte") > reactionTime)
                HighscoreManager.SetHighscore("3 Gun Monte", reactionTime);

            // show highscore
            m_umpire.ShowHighscore(HighscoreManager.GetHighscore("3 Gun Monte"));

            // win
            m_umpire.gameSuccess();
        }
        else // wrong cup
        {
            // fail
            m_umpire.gameFailed();
        }
    }
}
