# Polimaster.Utils.DimUnits
Dimension units converter and formatter for .NET Standard.

# Setup

> dotnet add package Polimaster.Utils.DimUnits

# Usage

```csharp
// setting up variable (SIEVERT)
var value = new UnitValue(100, UnitCode.SIEVERT);

// convert to ROENTGEN
var converted = value.Convert(UnitCode.ROENTGEN, false);

// format value
var formatted = converted.ToString();
```