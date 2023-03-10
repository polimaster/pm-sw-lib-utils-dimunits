# Polimaster.Utils.DimUnits
Dimension units converter and formatter for .NET Standard.

# Usage

```csharp
var value = new UnitValue(100, UnitCode.SIEVERT);
var converted = value.Convert(UnitCode.MISIEVERT, false);
```