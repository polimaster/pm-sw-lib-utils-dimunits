using System;

namespace Polimaster.Utils.DimUnits.units; 

public class Sievert : AUnit {
    public override UnitCode UnitCode => UnitCode.SIEVERT;
    protected override int Multiplier => 1;
    protected override Type AscType => null;
    protected override Type DescType => typeof(MSievert);
}

public class MSievert : Sievert {
    public override UnitCode UnitCode => UnitCode.MSIEVERT;
    protected override int Multiplier => 1000;
    protected override Type AscType => typeof(Sievert);
    protected override Type DescType => typeof(MiSievert);
}

public class MiSievert : Sievert {
    public override UnitCode UnitCode => UnitCode.MISIEVERT;
    protected override int Multiplier => 1000000;
    protected override Type AscType => typeof(MSievert);
    protected override Type DescType => null;
}

public class SievertPerHour : Sievert {
    public override UnitCode UnitCode => UnitCode.SIEVERT_PER_HOUR;
    protected override Type AscType => null;
    protected override Type DescType => typeof(MSievertPerHour);
}

public class MSievertPerHour : MSievert {
    public override UnitCode UnitCode => UnitCode.MSIEVERT_PER_HOUR;
    protected override Type AscType => typeof(SievertPerHour);
    protected override Type DescType => typeof(MiSievertPerHour);
}

public class MiSievertPerHour : MiSievert {
    public override UnitCode UnitCode => UnitCode.MISIEVERT_PER_HOUR;
    protected override Type AscType => typeof(MSievertPerHour);
    protected override Type DescType => null;
}