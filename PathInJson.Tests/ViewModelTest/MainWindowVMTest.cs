using Newtonsoft.Json.Schema;
using NUnit.Framework;
using PathInJson.WPF.ViewModels;
using System;
using System.IO;

namespace PathInJson.Tests.ViewModelTest
{
    public class MainWindowVMTest
    {
        [Test]
        public void ClearCommand()
        {
            //Arrange
            MainWindowViewModel viewModel = new MainWindowViewModel();
            viewModel.JsonText = "Sample";

            //Act
            viewModel.ClearCommand.Execute(null);

            //Assert
            Assert.AreEqual(string.Empty, viewModel.JsonText);
        }

        [Test]
        public void SaveCommand()
        {
            //Arrange
            MainWindowViewModel viewModel = new MainWindowViewModel();
            viewModel.Path = Directory.GetCurrentDirectory();

            //Act
            viewModel.LoadCommand.Execute(null);
            bool exist = File.Exists(viewModel.Path + @"\output.json");
            if (exist)
                File.Delete(viewModel.Path + @"\output.json");

            viewModel.SaveCommand.Execute(null);
            exist = File.Exists(viewModel.Path + @"\output.json");

            //Assert
            Assert.IsTrue(exist);
        }

        [Test]
        public void LoadCommand()
        {
            //Arrange
            MainWindowViewModel viewModel = new MainWindowViewModel();
            viewModel.Path = Directory.GetCurrentDirectory();
            bool isJson;

            //Act
            viewModel.LoadCommand.Execute(null);
            try
            {
                JSchema schema = JSchema.Parse(viewModel.JsonText);
                isJson = true;
            }
            catch (Exception)
            {
                isJson = false;
            }

            //Assert
            Assert.IsTrue(isJson);
        }

        [Test]
        public void SelectedItemChangedCommand()
        {
            //Arrange
            MainWindowViewModel viewModel = new MainWindowViewModel();
            viewModel.Path = string.Empty;
            viewModel.Items[0].IsSelected = true;

            //Act
            viewModel.SelectedItemChangedCommand.Execute(null);

            //Assert
            Assert.IsNotEmpty(viewModel.Path);
        }
    }
}
