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

    private static List<ConvertablePair>? _CONVERT_PAIRS;

    /// <summary>
    /// Pairs of possible conversions
    /// </summary>
    public static List<ConvertablePair> ConvertPairs {
        get {
            if (_CONVERT_PAIRS != null) return _CONVERT_PAIRS;
            _CONVERT_PAIRS = [];

            var edPair = new ConvertablePair {
                From2To = FromExpression,
                To2From = ToExpression,
                To = [UnitCode.SIEVERT, UnitCode.MILLI_SIEVERT, UnitCode.MICRO_SIEVERT],
                From = [UnitCode.ROENTGEN, UnitCode.MILLI_ROENTGEN, UnitCode.MICRO_ROENTGEN]
            };
            var medPair = new ConvertablePair {
                From2To = FromExpression,
                To2From = ToExpression,
                To = [UnitCode.SIEVERT_PER_HOUR, UnitCode.MILLI_SIEVERT_PER_HOUR, UnitCode.MICRO_SIEVERT_PER_HOUR],
                From = [UnitCode.ROENTGEN_PER_HOUR, UnitCode.MILLI_ROENTGEN_PER_HOUR, UnitCode.MICRO_ROENTGEN_PER_HOUR]
            };
            _CONVERT_PAIRS.Add(edPair);
            _CONVERT_PAIRS.Add(medPair);

            return _CONVERT_PAIRS;

            double ToExpression(double value) => value * 100;
            double FromExpression(double value) => value / 100;
        }
    }

    private static List<List<UnitCode>?>? _FAMILIES;

    /// <summary>
    /// List of units families
    /// </summary>
    public static IEnumerable<List<UnitCode>?> Families =>
        _FAMILIES ??= [
            new List<UnitCode> { UnitCode.ROENTGEN, UnitCode.MILLI_ROENTGEN, UnitCode.MICRO_ROENTGEN },
            new List<UnitCode> { UnitCode.SIEVERT, UnitCode.MILLI_SIEVERT, UnitCode.MICRO_SIEVERT },
            new List<UnitCode> { UnitCode.ROENTGEN_PER_HOUR, UnitCode.MILLI_ROENTGEN_PER_HOUR, UnitCode.MICRO_ROENTGEN_PER_HOUR },
            new List<UnitCode> { UnitCode.SIEVERT_PER_HOUR, UnitCode.MILLI_SIEVERT_PER_HOUR, UnitCode.MICRO_SIEVERT_PER_HOUR }
        ];

    /// <summary>
    /// Returns family for given <see cref="UnitCode"/>
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static List<UnitCode>? GET_FAMILY(UnitCode code) => Families.FirstOrDefault(t => t != null && t.Any(code1 => code1 == code));

    private static readonly List<UnitCode[]> UNIT_PAIRS = [
        new[] { UnitCode.ROENTGEN, UnitCode.ROENTGEN_PER_HOUR },
        new[] { UnitCode.MILLI_ROENTGEN, UnitCode.MILLI_ROENTGEN_PER_HOUR },
        new[] { UnitCode.MICRO_ROENTGEN, UnitCode.MICRO_ROENTGEN_PER_HOUR },
        new[] { UnitCode.SIEVERT, UnitCode.SIEVERT_PER_HOUR },
        new[] { UnitCode.MILLI_SIEVERT, UnitCode.MILLI_SIEVERT_PER_HOUR },
        new[] { UnitCode.MICRO_SIEVERT, UnitCode.MICRO_SIEVERT_PER_HOUR }
    ];

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