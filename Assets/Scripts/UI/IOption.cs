using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for main menu options
/// </summary>
public interface IOption {

    /// <summary>
    /// Initialize this option by loading and applying saved values
    /// </summary>
    void InitializeOption();

}