using System;

namespace Polimaster.Utils.DimUnits.units; 

/// <summary>
/// Roentgen units class (R).
/// </summary>
public class Roentgen : AUnit {
    public override UnitCode UnitCode => UnitCode.ROENTGEN;
    protected override int Multiplier => 1;
    protected override Type AscType => null;
    protected override Type DescType => typeof(MRoentgen);
}

/// <summary>
/// Miliroentgen clas (mR).
/// </summary>
public class MRoentgen : Roentgen {
    public override UnitCode UnitCode => UnitCode.MROENTGEN;
    protected override int Multiplier => 1000;
    protected override Type AscType => typeof(Roentgen);
    protected override Type DescType => typeof(MiRoentgen);
}

/// <summary>
/// Microroentgen class (μR)
/// </summary>
public class MiRoentgen : Roentgen {
    public override UnitCode UnitCode => UnitCode.MIROENTGEN;
    protected override int Multiplier => 1000000;
    protected override Type AscType => typeof(MRoentgen);
    protected override Type DescType => null;
}

public class RoentgenPerHour : Roentgen {
    public override UnitCode UnitCode => UnitCode.ROENTGEN_PER_HOUR;
    protected override Type AscType => null;
    protected override Type DescType => typeof(MRoentgenPerHour);
}

public class MRoentgenPerHour : MRoentgen {
    public override UnitCode UnitCode => UnitCode.MROENTGEN_PER_HOUR;
    protected override Type AscType => typeof(RoentgenPerHour);
    protected override Type DescType => typeof(MiRoentgenPerHour);
}

public class MiRoentgenPerHour : MiRoentgen {
    public override UnitCode UnitCode => UnitCode.MIROENTGEN_PER_HOUR;
    protected override Type AscType => typeof(MRoentgenPerHour);
    protected override Type DescType => null;
}