using EntityHomeworkFirst.Model;

namespace EntityHomeworkFirst.ViewModel.Handling
{
    public class ViewModelEventHandling
    {


        #region HANDLERS


        public void OnFillButtonClick()
        {
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    Order orderOne = new Order() { Summ = 5.7 };
                    Order orderTwo = new Order() { Summ = 5.5 };

                    context.Orders.AddRange(orderOne, orderTwo);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong.\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
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
