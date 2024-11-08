using System;

namespace Polimaster.Utils.DimUnits.units; 

/// <summary>
/// Roentgen units class (R).
/// </summary>
public class Roentgen : AUnit {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.ROENTGEN;

    /// <inheritdoc />
    protected override int Multiplier => 1;

    /// <inheritdoc />
    protected override Type? AscType => null;

    /// <inheritdoc />
    protected override Type? DescType => typeof(MilliRoentgen);
}

/// <summary>
/// Miliroentgen clas (mR).
/// </summary>
public class MilliRoentgen : Roentgen {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MILLI_ROENTGEN;

    /// <inheritdoc />
    protected override int Multiplier => 1000;

    /// <inheritdoc />
    protected override Type? AscType => typeof(Roentgen);

    /// <inheritdoc />
    protected override Type? DescType => typeof(MicroRoentgen);
}

/// <summary>
/// Microroentgen class (μR)
/// </summary>
public class MicroRoentgen : Roentgen {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MICRO_ROENTGEN;

    /// <inheritdoc />
    protected override int Multiplier => 1000000;

    /// <inheritdoc />
    protected override Type? AscType => typeof(MilliRoentgen);

    /// <inheritdoc />
    protected override Type? DescType => null;
}

public class RoentgenPerHour : Roentgen {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.ROENTGEN_PER_HOUR;

    /// <inheritdoc />
    protected override Type? AscType => null;

    /// <inheritdoc />
    protected override Type DescType => typeof(MilliRoentgenPerHour);
}

public class MilliRoentgenPerHour : MilliRoentgen {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MILLI_ROENTGEN_PER_HOUR;

    /// <inheritdoc />
    protected override Type AscType => typeof(RoentgenPerHour);

    /// <inheritdoc />
    protected override Type DescType => typeof(MicroRoentgenPerHour);
}

public class MicroRoentgenPerHour : MicroRoentgen {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MICRO_ROENTGEN_PER_HOUR;

    /// <inheritdoc />
    protected override Type AscType => typeof(MilliRoentgenPerHour);

    /// <inheritdoc />
    protected override Type? DescType => null;
}