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
    protected override Type? DescType => typeof(MilliSievert);
}

/// <summary>
/// MilliSievert unit
/// </summary>
public class MilliSievert : Sievert {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MILLI_SIEVERT;

    /// <inheritdoc />
    protected override int Multiplier => 1000;

    /// <inheritdoc />
    protected override Type AscType => typeof(Sievert);

    /// <inheritdoc />
    protected override Type DescType => typeof(MicroSievert);
}

/// <summary>
/// MicroSievert unit
/// </summary>
public class MicroSievert : Sievert {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MICRO_SIEVERT;

    /// <inheritdoc />
    protected override int Multiplier => 1000000;

    /// <inheritdoc />
    protected override Type AscType => typeof(MilliSievert);

    /// <inheritdoc />
    protected override Type? DescType => typeof(NanoSievert);
}

public class NanoSievert : Sievert {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.NANO_SIEVERT;

    /// <inheritdoc />
    protected override int Multiplier => 1000000000;

    /// <inheritdoc />
    protected override Type AscType => typeof(MicroSievert);

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
    protected override Type DescType => typeof(MilliSievertPerHour);
}

/// <summary>
/// MilliSievertPerHour unit
/// </summary>
public class MilliSievertPerHour : MilliSievert {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MILLI_SIEVERT_PER_HOUR;

    /// <inheritdoc />
    protected override Type AscType => typeof(SievertPerHour);

    /// <inheritdoc />
    protected override Type DescType => typeof(MicroSievertPerHour);
}

/// <summary>
/// MicroSievertPerHour unit
/// </summary>
public class MicroSievertPerHour : MicroSievert {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.MICRO_SIEVERT_PER_HOUR;

    /// <inheritdoc />
    protected override Type AscType => typeof(MilliSievertPerHour);

    /// <inheritdoc />
    protected override Type? DescType => typeof(NanoSievertPerHour);
}

public class NanoSievertPerHour : NanoSievert {
    /// <inheritdoc />
    public override UnitCode UnitCode => UnitCode.NANO_SIEVERT_PER_HOUR;

    /// <inheritdoc />
    protected override Type AscType => typeof(MicroSievertPerHour);

    /// <inheritdoc />
    protected override Type? DescType => null;
}