using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityHomeworkFirst.Model
{
    /// <summary>
    /// Order. A member unit of the 'Orders' table;
    /// <br />
    /// Заказ. Базовая единица из таблицы "Orders";
    /// </summary>
    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Date { get; set; } = null!;
    }
}
