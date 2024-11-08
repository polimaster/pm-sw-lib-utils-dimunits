using System.Collections.Generic;

namespace Polimaster.Utils.DimUnits.converters;

/// <summary>
/// Convertible pair
/// </summary>
public struct ConvertablePair {
    /// <summary>
    /// Expression to be executed while converting "From" to "To"
    /// </summary>
    public ConvertValue From2To;

    /// <summary>
    /// Expression to be executed while converting "To" to "From"
    /// </summary>
    public ConvertValue To2From;

    /// <summary>
    /// List of codes to conversion.
    /// </summary>
    public List<UnitCode> To;

    /// <summary>
    /// List of codes for conversion from.
    /// </summary>
    public List<UnitCode> From;
}