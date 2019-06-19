using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour { /* function */ public void Do() {  /* variable */ var tree = 1; } } /* class */ public class TheBehaviour : MonoBehaviour { /* variable */ public Behaviour m_behaviour; /* function */ public void DoBehaviour() { /* function call */ m_behaviour.Do(); } }
