using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Polimaster.Utils.DimUnits{
    public static class Converter {
        private static Dictionary<UnitCode, AUnit> _UNITS;

        /// <summary>
        /// Avaliable units list.
        /// </summary>
        public static Dictionary<UnitCode,AUnit> UNITS{
            get{
                if(_UNITS == null){
                    _UNITS = new Dictionary<UnitCode, AUnit>();
                    var assembly = Assembly.GetExecutingAssembly();
                    var utypes = assembly.GetTypes();

                    var ifsName = typeof(IUnit).ToString();
                    foreach(var type in utypes) {
                        var iface = type.GetInterface(ifsName);
                        if(iface == null || type.ToString() == typeof(AUnit).ToString()) continue;
                        var t = (AUnit)Activator.CreateInstance(type);
                        _UNITS.Add(t.UnitCode, t);
                    }
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
        public static UnitValue CONVERT(UnitValue source, UnitCode targetUnit, bool tryFindPair) {
            return source.Convert(targetUnit, tryFindPair);
        }

        public static UnitValue CONVERT(UnitValue source, UnitCode targetUnit) {
            return source.Convert(targetUnit, false);
        }

        private static List<CPair> _CONVERT_PAIRS;
        /// <summary>
        /// Pairs of possible conversions
        /// </summary>
        public static List<CPair> CONVERT_PAIRS {
            get{
                if(_CONVERT_PAIRS == null){
                    _CONVERT_PAIRS = new List<CPair>();

                    ConvertValue fromExpression = value => value/100;
                    ConvertValue toExpression = value => value*100;
                    var edPair = new CPair{ 
                        From2To = fromExpression,
                        To2From = toExpression,
                        To = new List<UnitCode>{ UnitCode.SIEVERT, UnitCode.MSIEVERT, UnitCode.MISIEVERT },
                        From = new List<UnitCode>{ UnitCode.ROENTGEN, UnitCode.MROENTGEN,UnitCode.MIROENTGEN }
                    };
                    var medPair = new CPair{
                        From2To = fromExpression,
                        To2From = toExpression,
                        To = new List<UnitCode>{ UnitCode.SIEVERT_PER_HOUR, UnitCode.MSIEVERT_PER_HOUR, UnitCode.MISIEVERT_PER_HOUR },
                        From = new List<UnitCode>{ UnitCode.ROENTGEN_PER_HOUR, UnitCode.MROENTGEN_PER_HOUR, UnitCode.MIROENTGEN_PER_HOUR }
                    };
                    _CONVERT_PAIRS.Add(edPair);
                    _CONVERT_PAIRS.Add(medPair);
                }
                return _CONVERT_PAIRS;
            }
        }

        private static List<List<UnitCode>> _FAMILIES;
        /// <summary>
        /// List of units families
        /// </summary>
        public static List<List<UnitCode>> FAMILIES {
            get{
                if(_FAMILIES == null){
                    _FAMILIES = new List<List<UnitCode>> {
                        new List<UnitCode>{UnitCode.ROENTGEN, UnitCode.MROENTGEN, UnitCode.MIROENTGEN},
                        new List<UnitCode>{UnitCode.SIEVERT, UnitCode.MSIEVERT, UnitCode.MISIEVERT},
                        new List<UnitCode>{UnitCode.ROENTGEN_PER_HOUR, UnitCode.MROENTGEN_PER_HOUR, UnitCode.MIROENTGEN_PER_HOUR},
                        new List<UnitCode>{UnitCode.SIEVERT_PER_HOUR, UnitCode.MSIEVERT_PER_HOUR, UnitCode.MISIEVERT_PER_HOUR},
                    };

                }
                return _FAMILIES;
            }
        }

        public static List<UnitCode> GET_FAMILY(UnitCode code){
            return FAMILIES.FirstOrDefault(t => t.Any(code1 => code1 == code));
        }

        private static readonly List<UnitCode[]> _UNIT_PAIRS = new List<UnitCode[]>{
                                       new []{UnitCode.ROENTGEN, UnitCode.ROENTGEN_PER_HOUR},
                                       new []{UnitCode.MROENTGEN, UnitCode.MROENTGEN_PER_HOUR},
                                       new []{UnitCode.MIROENTGEN, UnitCode.MIROENTGEN_PER_HOUR},
                                       new []{UnitCode.SIEVERT, UnitCode.SIEVERT_PER_HOUR},
                                       new []{UnitCode.MSIEVERT, UnitCode.MSIEVERT_PER_HOUR},
                                       new []{UnitCode.MISIEVERT, UnitCode.MISIEVERT_PER_HOUR},
                                        };

        /// <summary>
        /// Returns pair for given unit code.
        /// Example: pair for UnitCode.ROENTGEN is UnitCode.ROENTGEN_PER_HOUR.
        /// </summary>
        /// <param name="code">Unit code</param>
        /// <returns></returns>
        public static UnitCode GET_UNIT_PAIR(UnitCode code){
            var res = (UnitCode) 0;
            foreach(var c in _UNIT_PAIRS) {
                if(c[0] == code){
                    res = c[1];
                    break;
                }
                if(c[1] == code){
                    res = c[0];
                    break;
                }
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
    /// <summary>
    /// Convertible pair
    /// </summary>
    public struct CPair {
        /// <summary>
        /// Expression to be executed while converting "From" to "To"
        /// </summary>
        public ConvertValue From2To;
        /// <summary>
        /// Expression to be executed while converting "To" to "From"
        /// </summary>
        public ConvertValue To2From;
        /// <summary>
        /// List of codes to conversion.
        /// </summary>
        public List<UnitCode> To;
        /// <summary>
        /// List of codes for conversion from.
        /// </summary>
        public List<UnitCode> From;
    }
}