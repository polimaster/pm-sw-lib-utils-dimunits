using Polimaster.Utils.DimUnits;

namespace Tests;

public class Tests {
    [SetUp]
    public void Setup() {
    }

    [Test]
    public void Test1() {

        var value = new UnitValue(100, UnitCode.SIEVERT);
        var converted = value.Convert(UnitCode.MICRO_SIEVERT, false);

        var formatted = converted.ToString();

        Assert.Pass();
    }
}