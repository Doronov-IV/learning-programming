using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamlet.Service
{
    /// <summary>
    /// An object that provides scope for basic streamlet objects, such as: ListView(items), FileSystemPointer(custom), TextBox(address);
    /// <br />
    /// Объект, который вмещает в себя базовые streamlet-объекты, такие как: ListView(предметы), FileSystemPointer(кастомный), TextBox(адрес);
    /// </summary>
    public class ExplorerWindow
    {



        #region PROPERTIES


        /// <summary>
        /// @see public ListView? ExplorerListView;
        /// </summary>
        private ListView? _ExplorerListView;

        /// <summary>
        /// Windows Forms ListView (for item list);
        /// <br />
        /// ListView из Windows Forms (для списка предметов);
        /// </summary>
        public ListView? ExplorerListView { get { return _ExplorerListView; } set { _ExplorerListView = value; } }


        /// <summary>
        /// @see public FileSystemPointer? ExplorerFilePointer;
        /// </summary>
        private FileSystemPointer? _ExplorerFilePointer;

        /// <summary>
        /// Custom object for quick filesystem traverse;
        /// <br />
        /// Экземпляр кастомного класса для быстрого передвижения по файловой системе;
        /// </summary>
        public FileSystemPointer? ExplorerFilePointer { get { return _ExplorerFilePointer; } set { _ExplorerFilePointer = value; } }


        /// <summary>
        /// @see public TextBox? ExplorerAddressBox;
        /// </summary>
        private TextBox? _ExplorerAddressBox;

        /// <summary>
        /// Windows Forms TextBox (for address);
        /// <br />
        /// TextBox из Windows Forms (для адреса);
        /// </summary>
        public TextBox? ExplorerAddressBox { get { return _ExplorerAddressBox; } set { _ExplorerAddressBox = value; } }




        /// <summary>
        /// @see public bool Initialized;
        /// </summary>
        private bool _Initialized;

        /// <summary>
        /// True if instance of this class is fully initialized, otherwise false;
        /// <br />
        /// "True" если экземпляр класса полностью инициализован, инача "false";
        /// </summary>
        public bool Initialized { get { return _Initialized; } set { _Initialized = value; } }


        #endregion PROPERTIES





        #region CONSTRUCTION



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ExplorerWindow()
        {
            _ExplorerListView = null;
            _ExplorerFilePointer = null;
            _ExplorerAddressBox = null;
            _Initialized = false;
        }



        /// <summary>
        /// Parametrized constructor;
        /// <br />
        /// Конструктор с параметрами;
        /// </summary>
        /// <param name="listView">
        /// Winforms list view;
        /// <br />
        /// Лист вью из windows forms;
        /// </param>
        /// <param name="filePointer">
        /// Custom class instance;
        /// <br />
        /// Экземпляр кастомного класса;
        /// </param>
        /// <param name="addressBox">
        /// Winforms text box (for address);
        /// <br />
        /// Текстбокс из winforms (для адреса);
        /// </param>
        public ExplorerWindow(ListView? listView, FileSystemPointer? filePointer, TextBox? addressBox)
        {
            _ExplorerListView = listView;
            _ExplorerFilePointer = filePointer;
            _ExplorerAddressBox = addressBox;

            if (_ExplorerListView != null && _ExplorerFilePointer != null && _ExplorerAddressBox != null) Initialized = true;
            else _Initialized = false;
        }



        #endregion CONSTRUCTION



    }
}
