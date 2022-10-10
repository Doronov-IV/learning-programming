using System;

namespace Tools.Formatting
{

    /// <summary>
    /// When you need a clue to understand what does that value mean in the current context;
    /// <br />
    /// Когда нужна подсказка о том, что означает это число в данном контексте;
    /// </summary>
    public static class MagicAssets
    {


        #region GLOBAL_CONSTANTS


        //////////////////////
       //*******ZERO*******//


        /// <summary>
        /// For referencing the very first element of some collection;
        /// <br />
        /// Для обращения к самому первому элементу некой коллекции;
        /// </summary>
        public const byte THE_FIRST_ELEMENT_ordinal = 0;


        /// <summary>
        /// For describing the absence of System.Net byte offset;
        /// <br />
        /// Для сигнализации об отсутствии оффсета из System.Net;
        /// </summary>
        public const byte ZERO_OFFSET = 0;


        //*******ZERO*******//
       //////////////////////


        ///
        ///
        ///


        //////////////////////
       //*******ONE********//


        /// <summary>
        /// Transition from nominal to ordinal counting;
        /// <br />
        /// Перевод из порядковой в количественную и обратно;
        /// </summary>
        public const byte NUMERATION_ISSUE = 1;


        /// <summary>
        /// For adding new element;
        /// <br />
        /// Для добавления элемента в группу;
        /// </summary>
        public const byte NEW_ELEMENT = 1;


        /// <summary>
        /// For referencing a single abstract element of some collection;
        /// <br />
        /// Для описания некого единственного абстрактного элемента некоторой коллекции;
        /// </summary>
        public const byte ONE_ELEMENT_nominal = 1;


        //*******ONE********//
       //////////////////////



        #endregion GLOBAL_CONSTANTS


    }
}
