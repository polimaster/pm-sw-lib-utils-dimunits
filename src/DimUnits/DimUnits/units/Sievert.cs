using System;

namespace Polimaster.Utils.DimUnits.units; 

/// <summary>
/// Sievert unit
/// </summary>
public class Sievert : AUnit {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.SIEVERT;

    /// <inheritdoc />
    protected override int Multiplier => 1;

    /// <inheritdoc />
    protected override Type? AscType => null;

    /// <inheritdoc />
    protected override Type? DescType => typeof(MSievert);
}

/// <summary>
/// MSievert unit
/// </summary>
public class MSievert : Sievert {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MSIEVERT;

    /// <inheritdoc />
    protected override int Multiplier => 1000;

    /// <inheritdoc />
    protected override Type AscType => typeof(Sievert);

    /// <inheritdoc />
    protected override Type DescType => typeof(MiSievert);
}

/// <summary>
/// MiSievert unit
/// </summary>
public class MiSievert : Sievert {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MISIEVERT;

    /// <inheritdoc />
    protected override int Multiplier => 1000000;

    /// <inheritdoc />
    protected override Type AscType => typeof(MSievert);

    /// <inheritdoc />
    protected override Type? DescType => null;
}

/// <summary>
/// SievertPerHour unit
/// </summary>
public class SievertPerHour : Sievert {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.SIEVERT_PER_HOUR;

    /// <inheritdoc />
    protected override Type? AscType => null;

    /// <inheritdoc />
    protected override Type DescType => typeof(MSievertPerHour);
}

/// <summary>
/// MSievertPerHour unit
/// </summary>
public class MSievertPerHour : MSievert {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MSIEVERT_PER_HOUR;

    /// <inheritdoc />
    protected override Type AscType => typeof(SievertPerHour);

    /// <inheritdoc />
    protected override Type DescType => typeof(MiSievertPerHour);
}

/// <summary>
/// MiSievertPerHour unit
/// </summary>
public class MiSievertPerHour : MiSievert {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MISIEVERT_PER_HOUR;

    /// <inheritdoc />
    protected override Type AscType => typeof(MSievertPerHour);

    /// <inheritdoc />
    protected override Type? DescType => null;
}