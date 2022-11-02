using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.NET.Core.Service
{
	/// <summary>
	/// An entity that provides main form with all functionality concerning actions, stacks of actions and other.
	/// <br />
	/// Сущность, которая предоставляет main form'е функционал, связанный с action'ами, их стеками и другим.
	/// </summary>
	public class ActionHandler
    {



        #region STATE




        ///////////////////////////////////////////////////////////////////////////////////////
        ///  ↓                             ↓   FIELDS   ↓                               ↓   ///
        /////////////////////////////////////////////////////////////////////////////////////// 



        /// <inheritdoc cref="OnPaintPreview"
        private Action<Graphics> _onPaintPreview;


        /// <inheritdoc cref="OnPaint"/>
        private Action<Graphics> _onPaint;


        /// <inheritdoc cref="CurrentAction"/>
        private Action _currentAction;


        /// <inheritdoc cref="PerformedActionsStack"/>
        private Stack<Action<Graphics>> _performedActionsStack;


        /// <inheritdoc cref="CancelledActionsStack"/>
        private Stack<Action<Graphics>> _cancelledActionsStack;




        ///////////////////////////////////////////////////////////////////////////////////////
        ///  ↓                            ↓   PROPERTIES   ↓                            ↓   ///
        /////////////////////////////////////////////////////////////////////////////////////// 



        /// <summary>
        /// To be performed when we press the LMB to take a look at the preview of a chosen effect.
        /// <br />
        /// Выполняется, когда мы нажимаем ЛКМ, чтобы взглянуть на превью выбранного эффекта.
        /// </summary>
        public Action<Graphics> OnPaintPreview
        {
            get { return _onPaintPreview; }
            set
            {
                _onPaintPreview = value;
            }
        }



        /// <summary>
        /// To be performed when we end the preview of the effect and actualy want to keep it on the canvas.
        /// <br />
        /// Выполнится когда мы заверишм превью эффекта и захотим оставить его на холсте.
        /// </summary>
        public Action<Graphics> OnPaint
        {
            get { return _onPaint; }
            set
            {
                _onPaint = value;
            }
        }



        /// <summary>
        /// A reference to the current action chosen.
        /// <br />
        /// Ссылка на выбраное действие.
        /// </summary>
        public Action CurrentAction
        {
            get { return _currentAction; }
            set
            {
                _currentAction = value;
            }
        }



        /// <summary>
        /// The stack of all performed actions.
        /// <br />
        /// Стек всех совершённых действий.
        /// </summary>
        public Stack<Action<Graphics>> PerformedActionsStack
        {
            get { return _performedActionsStack; }
            set
            {
                _performedActionsStack = value;
            }
        }



        /// <summary>
        /// The stack of all cancelled actoins.
        /// <br />
        /// Стек всех отменённых действий.
        /// </summary>
        public Stack<Action<Graphics>> CancelledActionsStack
        {
            get { return _cancelledActionsStack; }
            set
            {
                _cancelledActionsStack = value;
            }
        }




        #endregion STATE





        #region API



        //



        #endregion API





        #region CONSTRUCTION




        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public ActionHandler()
        {
            _cancelledActionsStack = new();
            _performedActionsStack = new();
        }




        #endregion CONSTRUCTION



    }
}
