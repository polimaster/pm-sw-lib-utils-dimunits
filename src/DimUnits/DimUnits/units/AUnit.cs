using System;
using System.Linq;
using System.Threading;
using Polimaster.Utils.DimUnits.converters;
using Polimaster.Utils.DimUnits.res;

namespace Polimaster.Utils.DimUnits.units; 

public abstract class AUnit : IUnit {
    /// <summary>
    /// Base accuracy for to string conversions.
    /// </summary>
    public const int BASE_ACCURACY = 4;
    
    /// <summary>
    /// See <see cref="UnitCode"/>
    /// </summary>
    public abstract UnitCode UnitCode { get; }

    /// <summary>
    /// Divider between digit and Ext.
    /// </summary>
    private const char DIGIT_2_EXT_DIVIDER = ' ';

    /// <summary>
    /// Value showing the ratio of the base unit to the current one.
    /// For example, the ratio of miro-roentgen to roentgen is 1000, and micro-roentgen to roentgen is 1000000.
    /// For a base unit, this value is always 1.
    /// </summary>
    protected abstract int Multiplier { get; }

    /// <summary>
    /// Example: for micro-roentgen AscType is roentgen.
    /// </summary>
    protected abstract Type? AscType { get; }

    /// <summary>
    /// Example: for miro-roentgen DescType is micro-roentgen.
    /// </summary>
    protected abstract Type? DescType { get; }

    /// <summary>
    /// Extension of value. E.q. "R" or "mSv".
    /// </summary>
    public string Ext =>
        Resources.ResourceManager.GetString(Enum.GetName(typeof(UnitCode), UnitCode) ?? string.Empty,
            Thread.CurrentThread.CurrentCulture) ?? string.Empty;

    /// <summary>
    /// Converts given unit value to target.
    /// </summary>
    /// <param name="value">Value to convert</param>
    /// <param name="code">UnitCode to convert from.</param>
    /// <param name="accuracy">Accuracy field for converted value.</param>
    /// <returns></returns>
    /// <exception cref="Exception">Throws when impossible to convert to target units.</exception>
    public UnitValue Convert(double value, UnitCode code, byte accuracy) {
        if (UnitCode == code) return new UnitValue(value, code, accuracy);

        var v = 0d;
        var expressionFound = false;
        foreach (var list in Converter.ConvertPairs) {
            if (list.To.Contains(code) && list.From.Contains(UnitCode)) {
                expressionFound = true;
                v = list.From2To(value) * ((double)Converter.Units[code].Multiplier / Multiplier);
                break;
            }

            if (!list.From.Contains(code) || !list.To.Contains(UnitCode)) continue;
            expressionFound = true;
            v = list.To2From(value) * ((double)Converter.Units[code].Multiplier / Multiplier);
            break;
        }

        if (!expressionFound) {
            if (Converter.Families.Any(families => families != null && families.Contains(code) && families.Contains(UnitCode))) {
                expressionFound = true;
                v = value * ((double)Converter.Units[code].Multiplier / Multiplier);
            }
        }

        if (expressionFound) return new UnitValue(v, code, accuracy);

        throw new Exception("Impossible to convert to target units");
    }

    public UnitValue Convert(double value, UnitCode code, byte accuracy, bool tryFindPair) {
        if (!tryFindPair) return Convert(value, code, accuracy);
        UnitValue res;
        try { res = Convert(value, code, accuracy); } catch (Exception) {
            var unitCode = Converter.GET_UNIT_PAIR(code);
            res = Convert(value, unitCode, accuracy);
        }

        return res;
    }

    /// <summary>
    /// Convert given value to string with given accuracy.
    /// </summary>
    /// <param name="value">Value</param>
    /// <param name="accuracy">Accuracy while ToString conversion.</param>
    /// <param name="addExtension">Add Units?</param>
    /// <returns></returns>
    public string ValueToString(double value, int accuracy, bool addExtension = true) {
        var isInRange = _VALUE_IS_IN_RANGE(value);
        switch (isInRange) {
            case -1 when DescType != null: {
                var unit = (AUnit)Activator.CreateInstance(DescType);
                var nvalue = value * unit.Multiplier / Multiplier;
                return unit.ValueToString(nvalue, accuracy);
            }
            case 1 when AscType != null: {
                var unit = (AUnit)Activator.CreateInstance(AscType);
                var nvalue = value * unit.Multiplier / Multiplier;
                return unit.ValueToString(nvalue, accuracy);
            }
            default:
                return value.ToString("F0" + accuracy) + (addExtension ? DIGIT_2_EXT_DIVIDER + Ext : "");
        }
    }

    public double Normalize(double value, out UnitCode code) {
        int isInRange = _VALUE_IS_IN_RANGE(value);
        switch (isInRange) {
            case -1 when DescType != null: {
                var unit = (AUnit)Activator.CreateInstance(DescType);
                code = unit.UnitCode;
                return Converter.Units[code].Normalize(value * unit.Multiplier / Multiplier, out code);
            }
            case 1 when AscType != null: {
                var unit = (AUnit)Activator.CreateInstance(AscType);
                code = unit.UnitCode;
                return Converter.Units[code].Normalize(value * unit.Multiplier / Multiplier, out code);
            }
            default:
                code = UnitCode;
                return value;
        }
    }

    /// <summary>
    /// Returns 0 if Value * 1000 between 1 and 1000.
    /// Returns -1 if Value * 1000 &lt; 1000.
    /// Returns 1 if Value &rt; 1000 range.
    /// </summary>
    private static sbyte _VALUE_IS_IN_RANGE(double val) {
        return val switch {
            >= 1 and < 1000 => 0,
            < 1 => -1,
            _ => 1
        };
    }
}