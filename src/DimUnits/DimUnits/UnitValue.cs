using Polimaster.Utils.DimUnits.converters;
using Polimaster.Utils.DimUnits.units;

namespace Polimaster.Utils.DimUnits;

public struct UnitValue {
    /// <summary>
    /// Value
    /// </summary>
    public readonly double Value;

    /// <summary>
    /// Unit code.
    /// </summary>
    /// <see cref="UnitCode"/>
    public readonly UnitCode Code;

    /// <summary>
    /// Accuracy for conversions (number of digists after comma)
    /// </summary>
    public readonly byte Accuracy;

    public override string ToString() {
        if (!Converter.UNITS.ContainsKey(Code)) throw new UnknownUnitsCodeException();
        //Console.WriteLine(Code.ToString());
        //Console.ReadLine();
        return Converter.UNITS[Code].ValueToString(Value, Accuracy);
    }

    public string ToString(int accuracy) {
        if (!Converter.UNITS.ContainsKey(Code)) throw new UnknownUnitsCodeException();
        return Converter.UNITS[Code].ValueToString(Value, accuracy);
    }

    /// <summary>
    /// Convert current UnitValue to target UnitValue.
    /// </summary>
    /// <param name="code">UnitCode of result.</param>
    /// <param name="tryFindPair"></param>
    /// <returns>New UnitValue</returns>
    public UnitValue Convert(UnitCode code, bool tryFindPair) {
        return code == Code ? this : Converter.UNITS[Code].Convert(Value, code, Accuracy, tryFindPair);
    }

    public string GetExt() {
        return Converter.UNITS[Code].Ext;
    }

    public UnitValue(double value, UnitCode code, bool normalize) {
        UnitCode nUnitCode = code;
        Value = normalize ? Converter.UNITS[code].Normalize(value, out nUnitCode) : value;
        Accuracy = AUnit.BASE_ACCURACY;
        Code = nUnitCode;
    }

    public UnitValue(double value, UnitCode code, bool normalize, bool dinamicaccuracy) {
        UnitCode nUnitCode = code;
        Value = normalize ? Converter.UNITS[code].Normalize(value, out nUnitCode) : value;
        Accuracy = AUnit.BASE_ACCURACY;
        if (dinamicaccuracy)
            if (nUnitCode == UnitCode.MISIEVERT_PER_HOUR || nUnitCode == UnitCode.MISIEVERT)
                Accuracy = 2;
        if (nUnitCode == UnitCode.MSIEVERT_PER_HOUR || nUnitCode == UnitCode.MSIEVERT) Accuracy = 5;
        if (nUnitCode == UnitCode.SIEVERT_PER_HOUR || nUnitCode == UnitCode.SIEVERT) Accuracy = 8;
        Code = nUnitCode;
    }

    /// <summary>
    /// UnitValue Costructor.
    /// </summary>
    /// <param name="value">Value</param>
    /// <param name="code">UnitCode</param>
    /// <param name="accuracy">Accuracy</param>
    public UnitValue(double value, UnitCode code, byte accuracy) {
        Value = value;
        Code = code;
        Accuracy = accuracy;
    }

    public UnitValue(double value, UnitCode code) {
        Value = value;
        Code = code;
        Accuracy = AUnit.BASE_ACCURACY;
    }
}