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
    protected override Type? DescType => typeof(MRoentgen);
}

/// <summary>
/// Miliroentgen clas (mR).
/// </summary>
public class MRoentgen : Roentgen {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MROENTGEN;

    /// <inheritdoc />
    protected override int Multiplier => 1000;

    /// <inheritdoc />
    protected override Type? AscType => typeof(Roentgen);

    /// <inheritdoc />
    protected override Type? DescType => typeof(MiRoentgen);
}

/// <summary>
/// Microroentgen class (μR)
/// </summary>
public class MiRoentgen : Roentgen {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MIROENTGEN;

    /// <inheritdoc />
    protected override int Multiplier => 1000000;

    /// <inheritdoc />
    protected override Type? AscType => typeof(MRoentgen);

    /// <inheritdoc />
    protected override Type? DescType => null;
}

public class RoentgenPerHour : Roentgen {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.ROENTGEN_PER_HOUR;

    /// <inheritdoc />
    protected override Type? AscType => null;

    /// <inheritdoc />
    protected override Type DescType => typeof(MRoentgenPerHour);
}

public class MRoentgenPerHour : MRoentgen {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MROENTGEN_PER_HOUR;

    /// <inheritdoc />
    protected override Type AscType => typeof(RoentgenPerHour);

    /// <inheritdoc />
    protected override Type DescType => typeof(MiRoentgenPerHour);
}

public class MiRoentgenPerHour : MiRoentgen {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MIROENTGEN_PER_HOUR;

    /// <inheritdoc />
    protected override Type AscType => typeof(MRoentgenPerHour);

    /// <inheritdoc />
    protected override Type? DescType => null;
}