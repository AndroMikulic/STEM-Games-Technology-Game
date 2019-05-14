using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEntered : MonoBehaviour
{
    public SessionManager sessionManager;

    public InputField caseIDInput;
    public InputField solutionInput;

    public void SendAttack()
    {
        sessionManager.DoAttack(caseIDInput.text, solutionInput.text);
    }
}
