using System;

namespace Polimaster.Utils.DimUnits; 

public class ConvertException : Exception {
    /// <inheritdoc />
    public override string Message => "Unable to convert to unknown dimension units";
}

public class UnknownUnitsCodeException : Exception {
    /// <inheritdoc />
    public override string Message => "Unknown units code in use";
}