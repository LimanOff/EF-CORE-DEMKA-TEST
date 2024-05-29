using System.Windows;

namespace EFCore.Model.Helpers
{
    public class UtilityHelper
    {
        public static void ShowErrorMessage(string message)
        {
            MessageBox.Show($"{message}.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowInformationMessage(string message)
        {
            MessageBox.Show($"{message}.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Спрашивает у пользователя вопрос и возвращает результат ДА/НЕТ
        /// </summary>
        /// <param name="question"></param>
        /// <returns>
        ///     <para>
        ///     true, если нажали ДА.
        ///     </para>
        ///      <para>
        ///     false, если нажали НЕТ.
        ///     </para>
        /// </returns>
        public static bool AskQuestion(string question)
        {
            if (MessageBox.Show($"{question}", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                return true;
            return false;
        }
    }
}
