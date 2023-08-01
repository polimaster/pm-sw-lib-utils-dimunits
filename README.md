# Polimaster.Utils.DimUnits
Radiation units converter and formatter for .NET Standard.

# Setup

> dotnet add package Polimaster.Utils.DimUnits

# Usage

```c#
// setting up variable (SIEVERT)
var value = new UnitValue(100, UnitCode.SIEVERT);

// convert to ROENTGEN
var converted = value.Convert(UnitCode.ROENTGEN, false);

// format value
var formatted = converted.ToString();
```