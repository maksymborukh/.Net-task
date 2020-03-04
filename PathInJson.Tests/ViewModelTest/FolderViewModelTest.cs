using NUnit.Framework;
using PathInJson.WPF.ViewModels;

namespace PathInJson.Tests.ViewModelTest
{
    public class FolderViewModelTest
    {
        [Test]
        public void Expanded()
        {
            //Arrange
            MainWindowViewModel viewModel1 = new MainWindowViewModel();
            FolderViewModel viewModel = new FolderViewModel(viewModel1.Items[0].Item);

            //Act
            viewModel.IsExpanded = true;

            //Assert
            Assert.IsTrue(viewModel.Children.Count >= 1);
        }
    }
}
