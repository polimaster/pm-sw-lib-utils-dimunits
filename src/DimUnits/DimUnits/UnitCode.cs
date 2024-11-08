namespace Polimaster.Utils.DimUnits;

/// <summary>
/// Units code
/// </summary>
public enum UnitCode : byte {
    /// <summary>
    /// None
    /// </summary>
    NONE,

    /// <summary>
    /// R
    /// </summary>
    ROENTGEN,

    /// <summary>
    /// mR (Base value units to store in db for dose)
    /// </summary>
    MILLI_ROENTGEN,

    /// <summary>
    /// μR
    /// </summary>
    MICRO_ROENTGEN,

    /// <summary>
    /// nR
    /// </summary>
    NANO_ROENTGEN,

    /// <summary>
    /// Sv
    /// </summary>
    SIEVERT,

    /// <summary>
    /// mSv
    /// </summary>
    MILLI_SIEVERT,

    /// <summary>
    /// μSv
    /// </summary>
    MICRO_SIEVERT,

    /// <summary>
    /// nSv
    /// </summary>
    NANO_SIEVERT,

    /// <summary>
    /// R/h (Base value units to store in db for dose rate)
    /// </summary>
    ROENTGEN_PER_HOUR,

    /// <summary>
    /// mR/h
    /// </summary>
    MILLI_ROENTGEN_PER_HOUR,

    /// <summary>
    /// μR/h
    /// </summary>
    MICRO_ROENTGEN_PER_HOUR,

    /// <summary>
    /// nR/h
    /// </summary>
    NANO_ROENTGEN_PER_HOUR,

    /// <summary>
    /// Sv/h
    /// </summary>
    SIEVERT_PER_HOUR,

    /// <summary>
    /// mSv/h
    /// </summary>
    MILLI_SIEVERT_PER_HOUR,

    /// <summary>
    /// μSv/h
    /// </summary>
    MICRO_SIEVERT_PER_HOUR,

    /// <summary>
    /// nSv/h
    /// </summary>
    NANO_SIEVERT_PER_HOUR,
}