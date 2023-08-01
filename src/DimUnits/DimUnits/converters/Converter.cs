using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Polimaster.Utils.DimUnits.units;

namespace Polimaster.Utils.DimUnits.converters; 

/// <summary>
/// Values converter
/// </summary>
public static class Converter {
    private static Dictionary<UnitCode, AUnit>? _UNITS;

    /// <summary>
    /// Available units list.
    /// </summary>
    public static Dictionary<UnitCode, AUnit> Units {
        get {
            if (_UNITS != null) return _UNITS;
            _UNITS = new Dictionary<UnitCode, AUnit>();
            var assembly = Assembly.GetExecutingAssembly();
            var utypes = assembly.GetTypes();

            var ifsName = typeof(IUnit).ToString();
            foreach (var type in utypes) {
                var iface = type.GetInterface(ifsName);
                if (iface == null || type.ToString() == typeof(AUnit).ToString()) continue;
                var t = (AUnit)Activator.CreateInstance(type);
                _UNITS.Add(t.UnitCode, t);
            }

            return _UNITS;
        }
    }

    /// <summary>
    /// Converts given unit value to target unit value.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="targetUnit"></param>
    /// <param name="tryFindPair"></param>
    /// <returns></returns>
    public static UnitValue Convert(UnitValue source, UnitCode targetUnit, bool tryFindPair) {
        return source.Convert(targetUnit, tryFindPair);
    }

    /// <summary>
    /// Converts given unit value to target unit value.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="targetUnit"></param>
    /// <returns></returns>
    public static UnitValue Convert(UnitValue source, UnitCode targetUnit) {
        return source.Convert(targetUnit, false);
    }

    private static List<ConvertiblePair>? _CONVERT_PAIRS;

    /// <summary>
    /// Pairs of possible conversions
    /// </summary>
    public static List<ConvertiblePair> ConvertPairs {
        get {
            if (_CONVERT_PAIRS != null) return _CONVERT_PAIRS;
            _CONVERT_PAIRS = new List<ConvertiblePair>();

            double FromExpression(double value) => value / 100;
            double ToExpression(double value) => value * 100;
            var edPair = new ConvertiblePair {
                From2To = FromExpression,
                To2From = ToExpression,
                To = new List<UnitCode> { UnitCode.SIEVERT, UnitCode.MSIEVERT, UnitCode.MISIEVERT },
                From = new List<UnitCode> { UnitCode.ROENTGEN, UnitCode.MROENTGEN, UnitCode.MIROENTGEN }
            };
            var medPair = new ConvertiblePair {
                From2To = FromExpression,
                To2From = ToExpression,
                To = new List<UnitCode>
                    { UnitCode.SIEVERT_PER_HOUR, UnitCode.MSIEVERT_PER_HOUR, UnitCode.MISIEVERT_PER_HOUR },
                From = new List<UnitCode>
                    { UnitCode.ROENTGEN_PER_HOUR, UnitCode.MROENTGEN_PER_HOUR, UnitCode.MIROENTGEN_PER_HOUR }
            };
            _CONVERT_PAIRS.Add(edPair);
            _CONVERT_PAIRS.Add(medPair);

            return _CONVERT_PAIRS;
        }
    }

    private static List<List<UnitCode>?>? _FAMILIES;

    /// <summary>
    /// List of units families
    /// </summary>
    public static IEnumerable<List<UnitCode>?> Families =>
        _FAMILIES ??= new List<List<UnitCode>?> {
            new() { UnitCode.ROENTGEN, UnitCode.MROENTGEN, UnitCode.MIROENTGEN },
            new() { UnitCode.SIEVERT, UnitCode.MSIEVERT, UnitCode.MISIEVERT },
            new() { UnitCode.ROENTGEN_PER_HOUR, UnitCode.MROENTGEN_PER_HOUR, UnitCode.MIROENTGEN_PER_HOUR },
            new() { UnitCode.SIEVERT_PER_HOUR, UnitCode.MSIEVERT_PER_HOUR, UnitCode.MISIEVERT_PER_HOUR },
        };

    /// <summary>
    /// Returns family for given <see cref="UnitCode"/>
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static List<UnitCode>? GET_FAMILY(UnitCode code) => Families.FirstOrDefault(t => t != null && t.Any(code1 => code1 == code));

    private static readonly List<UnitCode[]> UNIT_PAIRS = new() {
        new[] { UnitCode.ROENTGEN, UnitCode.ROENTGEN_PER_HOUR },
        new[] { UnitCode.MROENTGEN, UnitCode.MROENTGEN_PER_HOUR },
        new[] { UnitCode.MIROENTGEN, UnitCode.MIROENTGEN_PER_HOUR },
        new[] { UnitCode.SIEVERT, UnitCode.SIEVERT_PER_HOUR },
        new[] { UnitCode.MSIEVERT, UnitCode.MSIEVERT_PER_HOUR },
        new[] { UnitCode.MISIEVERT, UnitCode.MISIEVERT_PER_HOUR },
    };

    /// <summary>
    /// Returns pair for given unit code.
    /// Example: pair for UnitCode.ROENTGEN is UnitCode.ROENTGEN_PER_HOUR.
    /// </summary>
    /// <param name="code">Unit code</param>
    /// <returns></returns>
    public static UnitCode GET_UNIT_PAIR(UnitCode code) {
        var res = (UnitCode)0;
        foreach (var c in UNIT_PAIRS) {
            if (c[0] == code) {
                res = c[1];
                break;
            }

            if (c[1] != code) continue;
            res = c[0];
            break;
        }

        return res;
    }
}

/// <summary>
/// Delegate which will be executed for conversion.
/// </summary>
/// <param name="value">Value to convert</param>
/// <returns></returns>
public delegate double ConvertValue(double value);