namespace Polimaster.Utils.DimUnits{
    public enum UnitCode : byte{
        NONE,
        /// <summary>
        /// R
        /// </summary>
        ROENTGEN,
        /// <summary>
        /// mR (Base value units to store in db for dose)
        /// </summary>
        MROENTGEN,
        /// <summary>
        /// �R
        /// </summary>
        MIROENTGEN,
        /// <summary>
        /// Sv
        /// </summary>
        SIEVERT,
        /// <summary>
        /// mSv
        /// </summary>
        MSIEVERT,
        /// <summary>
        /// �Sv
        /// </summary>
        MISIEVERT,
        /// <summary>
        /// R/h (Base value units to store in db for dose rate)
        /// </summary>
        ROENTGEN_PER_HOUR,
        /// <summary>
        /// mR/h
        /// </summary>
        MROENTGEN_PER_HOUR,
        /// <summary>
        /// �R/h
        /// </summary>
        MIROENTGEN_PER_HOUR,
        /// <summary>
        /// Sv/h
        /// </summary>
        SIEVERT_PER_HOUR,
        /// <summary>
        /// mSv/h
        /// </summary>
        MSIEVERT_PER_HOUR,
        /// <summary>
        /// �Sv/h
        /// </summary>
        MISIEVERT_PER_HOUR,
    }

}