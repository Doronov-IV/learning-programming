using EntityHomeworkFirst.Model;

namespace EntityHomeworkFirst.ViewModel.Handling
{
    public class ViewModelEventHandling
    {


        #region HANDLERS


        public void OnFillButtonClick()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                Order order1 = new() { Summ = 1.5, Date = "02.02.2002" };
                Order order2 = new() { Summ = 1.7, Date = "02.02.2003" };

                context.Orders.AddRange(order1, order2);

                var a = context.SaveChanges();
            }
        }


        public void OnClearButtonClick()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                try
                {
                    context.Orders.RemoveRange(context.Orders);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Something went wrong.\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        #endregion HANDLERS



        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ViewModelEventHandling()
        {

        }


        #endregion CONSTRUCTION


    }
}
