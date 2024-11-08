using Polimaster.Utils.DimUnits.converters;
using Polimaster.Utils.DimUnits.units;

namespace Polimaster.Utils.DimUnits;

/// <summary>
/// Unit Value
/// </summary>
public readonly struct UnitValue {
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

    /// <inheritdoc />
    public override string ToString() {
        if (!Converter.Units.ContainsKey(Code)) throw new UnknownUnitsCodeException();
        return Converter.Units[Code].ValueToString(Value, Accuracy);
    }

    /// <summary>
    /// Convert given value to string with given accuracy.
    /// </summary>
    /// <param name="accuracy"></param>
    /// <returns></returns>
    /// <exception cref="UnknownUnitsCodeException"></exception>
    public string ToString(int accuracy) {
        if (!Converter.Units.ContainsKey(Code)) throw new UnknownUnitsCodeException();
        return Converter.Units[Code].ValueToString(Value, accuracy);
    }

    /// <summary>
    /// Convert current UnitValue to target UnitValue.
    /// </summary>
    /// <param name="code">UnitCode of result.</param>
    /// <param name="tryFindPair"></param>
    /// <returns>New UnitValue</returns>
    public UnitValue Convert(UnitCode code, bool tryFindPair) {
        return code == Code ? this : Converter.Units[Code].Convert(Value, code, Accuracy, tryFindPair);
    }

    /// <summary>
    /// See <see cref="AUnit.Ext"/>
    /// </summary>
    /// <returns></returns>
    public string GetExt() => Converter.Units[Code].Ext;

    public UnitValue(double value, UnitCode code, bool normalize) {
        var nUnitCode = code;
        Value = normalize ? Converter.Units[code].Normalize(value, out nUnitCode) : value;
        Accuracy = AUnit.BASE_ACCURACY;
        Code = nUnitCode;
    }

    public UnitValue(double value, UnitCode code, bool normalize, bool dinamicaccuracy) {
        var nUnitCode = code;
        Value = normalize ? Converter.Units[code].Normalize(value, out nUnitCode) : value;
        Accuracy = AUnit.BASE_ACCURACY;
        if (dinamicaccuracy)
            if (nUnitCode is UnitCode.MICRO_SIEVERT_PER_HOUR or UnitCode.MICRO_SIEVERT)
                Accuracy = 2;
        if (nUnitCode is UnitCode.MILLI_SIEVERT_PER_HOUR or UnitCode.MILLI_SIEVERT) Accuracy = 5;
        if (nUnitCode is UnitCode.SIEVERT_PER_HOUR or UnitCode.SIEVERT) Accuracy = 8;
        Code = nUnitCode;
    }

    /// <summary>
    /// UnitValue Constructor
    /// </summary>
    /// <param name="value"><see cref="Value"/></param>
    /// <param name="code"><see cref="UnitCode"/></param>
    /// <param name="accuracy"><see cref="Accuracy"/></param>
    public UnitValue(double value, UnitCode code, byte accuracy) {
        Value = value;
        Code = code;
        Accuracy = accuracy;
    }

    /// <summary>
    /// UnitValue Constructor
    /// </summary>
    /// <param name="value"><see cref="Value"/></param>
    /// <param name="code"><see cref="UnitCode"/></param>
    public UnitValue(double value, UnitCode code) {
        Value = value;
        Code = code;
        Accuracy = AUnit.BASE_ACCURACY;
    }
}