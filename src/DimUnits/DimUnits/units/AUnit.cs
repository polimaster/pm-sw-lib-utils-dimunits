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
    /// Value for this field MUST be from UnitType enum!
    /// This value will be stored in db.
    /// </summary>
    public abstract UnitCode UnitCode { get; }

    /// <summary>
    /// Divider between digit and Ext.
    /// </summary>
    private const char DIGIT_2_EXT_DIVIDER = ' ';

    /// <summary>
    /// Величина, показывающая отношение базовой еденицы к текущей.
    /// Например, отношение милирентгена к рентгену - 1000, а микрорентгена к рентгену - 1000000.
    /// У базовой еденицы эта величина - всегда 1.
    /// </summary>
    protected abstract int Multiplier { get; }

    /// <summary>
    /// Example: for miliroentgen AscType is roentgen.
    /// </summary>
    protected abstract Type AscType { get; }

    /// <summary>
    /// Example: for miliroentgen DescType is microroentgen.
    /// </summary>
    protected abstract Type DescType { get; }

    /// <summary>
    /// Extension od value. E.q. "R" or "mSv".
    /// </summary>
    public string Ext =>
        Resources.ResourceManager.GetString(Enum.GetName(typeof(UnitCode), UnitCode),
            Thread.CurrentThread.CurrentCulture);

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
        foreach (var list in Converter.CONVERT_PAIRS) {
            if (list.To.Contains(code) && list.From.Contains(UnitCode)) {
                expressionFound = true;
                v = list.From2To(value) * ((double)Converter.UNITS[code].Multiplier / Multiplier);
                break;
            }

            if (list.From.Contains(code) && list.To.Contains(UnitCode)) {
                expressionFound = true;
                v = list.To2From(value) * ((double)Converter.UNITS[code].Multiplier / Multiplier);
                break;
            }
        }

        if (!expressionFound) {
            if (Converter.FAMILIES.Any(families => families.Contains(code) && families.Contains(UnitCode))) {
                expressionFound = true;
                v = value * ((double)Converter.UNITS[code].Multiplier / Multiplier);
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
        if (isInRange == -1 && DescType != null) {
            var unit = (AUnit)Activator.CreateInstance(DescType);
            var nvalue = value * unit.Multiplier / Multiplier;
            return unit.ValueToString(nvalue, accuracy);
        }

        if (isInRange == 1 && AscType != null) {
            var unit = (AUnit)Activator.CreateInstance(AscType);
            var nvalue = value * unit.Multiplier / Multiplier;
            return unit.ValueToString(nvalue, accuracy);
        }

        return value.ToString("F0" + accuracy) + (addExtension ? DIGIT_2_EXT_DIVIDER + Ext : "");
    }

    public double Normalize(double value, out UnitCode code) {
        int isInRange = _VALUE_IS_IN_RANGE(value);
        if (isInRange == -1 && DescType != null) {
            var unit = (AUnit)Activator.CreateInstance(DescType);
            code = unit.UnitCode;
            return Converter.UNITS[code].Normalize(value * unit.Multiplier / Multiplier, out code);
        }

        if (isInRange == 1 && AscType != null) {
            var unit = (AUnit)Activator.CreateInstance(AscType);
            code = unit.UnitCode;
            return Converter.UNITS[code].Normalize(value * unit.Multiplier / Multiplier, out code);
        }

        code = UnitCode;
        return value;
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