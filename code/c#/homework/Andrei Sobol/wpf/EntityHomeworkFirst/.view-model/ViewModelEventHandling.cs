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
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong.\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void OnClearButtonClick()
        {
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.Orders.RemoveRange(context.Orders);
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
